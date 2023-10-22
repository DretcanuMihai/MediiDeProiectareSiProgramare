using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Threading;
using Google.Protobuf;
using model.entities;
using services;
using services.interfaces;
using TicketProtobufs=networking.proto;


namespace networking
{
    public class SuperServiceProxy : ISuperService
    {
        private string host;
        private int port;

        private ITicketObserver client;

        private NetworkStream stream;

        private IFormatter formatter;
        private TcpClient connection;

        private Queue<TicketProtobufs.Response> responses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;

        public SuperServiceProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            responses = new Queue<TicketProtobufs.Response>();
        }

        public void Login(string username, string password, ITicketObserver observer)
        {
            InitializeConnection();
            User user = new User(username, password);
            SendRequest(ProtoUtils.createLoginRequest(user));
            TicketProtobufs.Response response = ReadResponse();
            if (response.Type == TicketProtobufs.Response.Types.Type.Ok)
            {
                this.client = observer;
            }
            else if (response.Type == TicketProtobufs.Response.Types.Type.Error)
            {
                String err = ProtoUtils.getError(response);
                CloseConnection();
                throw new ServiceException(err);
            }
        }

        public void Logout(string username)
        {
            SendRequest(ProtoUtils.createLogoutRequest(new User(username,null)));
            TicketProtobufs.Response response=ReadResponse();
            CloseConnection();
            if (response.Type == TicketProtobufs.Response.Types.Type.Error)
            {
                String err = ProtoUtils.getError(response);
                throw new ServiceException(err);
            }
        }

        public ICollection<Festival> GetAllFestivals()
        {
            SendRequest(ProtoUtils.createGetFestivalsRequest());
            TicketProtobufs.Response response=ReadResponse();
            if (response.Type== TicketProtobufs.Response.Types.Type.Error)
            {
                String err = ProtoUtils.getError(response);
                throw new ServiceException(err);
            }
            Festival[] data=ProtoUtils.getFestivals(response);
            ICollection<Festival> toReturn = data.ToList();
            return toReturn;
        }

        public ICollection<Festival> GetAllFestivalsOnDate(DateTime date)
        {

            SendRequest(ProtoUtils.createGetFestivalsOnDateRequest(date));
            TicketProtobufs.Response response=ReadResponse();
            if (response.Type== TicketProtobufs.Response.Types.Type.Error)
            {
                String err = ProtoUtils.getError(response);
                throw new ServiceException(err);
            }
            Festival[] data=ProtoUtils.getFestivals(response);
            ICollection<Festival> toReturn = data.ToList();
            return toReturn;
        }

        public void SellTicket(int festivalId, string buyerName, int spots)
        {
            Festival festival=new Festival(festivalId,null,DateTime.Now, null,0,0);
            Ticket ticket=new Ticket(0,buyerName,festival,spots);
            SendRequest(ProtoUtils.createBuyTicketRequest(ticket));
            TicketProtobufs.Response response=ReadResponse();
            if (response.Type== TicketProtobufs.Response.Types.Type.Error)
            {
                String err = ProtoUtils.getError(response);
                throw new ServiceException(err);
            }
        }

        private void CloseConnection()
        {
            finished = true;
            try
            {
                stream.Close();

                connection.Close();
                _waitHandle.Close();
                client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void SendRequest(TicketProtobufs.Request request)
        {
            try
            {
                request.WriteDelimitedTo(stream);
                stream.Flush();
            }
            catch (Exception e)
            {
                throw new ServerException("Error sending object " + e);
            }
        }

        private TicketProtobufs.Response ReadResponse()
        {
            TicketProtobufs.Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (responses)
                {
                    //Monitor.Wait(responses); 
                    response = responses.Dequeue();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return response;
        }

        private void InitializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                StartReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void StartReader()
        {
            Thread tw = new Thread(Run);
            tw.Start();
        }


        private void HandleUpdate(TicketProtobufs.Response response)
        {
            if (response.Type == TicketProtobufs.Response.Types.Type.TicketBought)
            {
                Ticket ticket = ProtoUtils.getTicket(response);
                Console.WriteLine("Tickets bought" + ticket);
                client.UpdateTicketSold(ticket);
            }
        }

        private bool IsUpdate(TicketProtobufs.Response response)
        {
            return response.Type == TicketProtobufs.Response.Types.Type.TicketBought;
        }

        public virtual void Run()
        {
            while (!finished)
            {
                try
                {
                    
                    TicketProtobufs.Response response = TicketProtobufs.Response.Parser.ParseDelimitedFrom(stream);
                    Console.WriteLine("response received " + response);
                    if (IsUpdate(response))
                    {
                        HandleUpdate(response);
                    }
                    else
                    {
                        lock (responses)
                        {
                            responses.Enqueue(response);
                        }

                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error " + e);
                }
            }
        }
    }
}