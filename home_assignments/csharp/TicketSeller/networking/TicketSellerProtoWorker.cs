using System;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using model.entities;
using services;
using services.interfaces;
using TicketProtobufs=networking.proto;

namespace networking
{
    public class TicketSellerProtoWorker : ITicketObserver
    {
        private ISuperService server;
        private TcpClient connection;

        private NetworkStream stream;
        private volatile bool _connected;

        public TicketSellerProtoWorker(ISuperService server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {
                stream = connection.GetStream();
                _connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public virtual void Run()
        {
            while (_connected)
            {
                try
                {
                    TicketProtobufs.Request request = TicketProtobufs.Request.Parser.ParseDelimitedFrom(stream);
                    TicketProtobufs.Response response = HandleRequest(request);
                    if (response != null)
                    {
                        SendResponse(response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }

            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }

        public void UpdateTicketSold(Ticket ticket)
        {
            TicketProtobufs.Response resp = ProtoUtils.createTicketBoughtResponse(ticket);
            Console.WriteLine("Ticket bought");
            try
            {
                SendResponse(resp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static TicketProtobufs.Response okResponse = ProtoUtils.createOkResponse();


        private TicketProtobufs.Response HandleRequest(TicketProtobufs.Request request)
        {
            TicketProtobufs.Response response = null;
            String handlerName = "HandleRequest" + request.Type.ToString();
            Console.WriteLine("HandlerName " + handlerName);
            try
            {
                MethodInfo method = GetType().GetMethod(handlerName);
                Object[] parameters = {request};
                response = (TicketProtobufs.Response) method.Invoke(this, parameters);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return response;
        }

        public TicketProtobufs.Response HandleRequestLogin(TicketProtobufs.Request request)
        {
            Console.WriteLine("Login request ...");
            try
            {
                User user = ProtoUtils.getUser(request);
                lock (server)
                {
                    server.Login(user.Username, user.Password, this);
                }

                return okResponse;
            }
            catch (ServiceException e)
            {
                _connected = false;
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }

        public TicketProtobufs.Response HandleRequestLogout(TicketProtobufs.Request request)
        {
            Console.WriteLine("Logout request ...");
            try
            {
                User user = ProtoUtils.getUser(request);
                lock (server)
                {
                    server.Logout(user.Username);
                }

                _connected = false;
                return okResponse;
            }
            catch (ServiceException e)
            {
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }

        public TicketProtobufs.Response HandleRequestGetFestivals(TicketProtobufs.Request request)
        {
            Console.WriteLine("GetFestivals Request ...");
            try
            {
                Festival[] festivals;
                lock (server)
                {
                    festivals = server.GetAllFestivals().ToArray();
                }

                return ProtoUtils.createGetFestivalsResponse(festivals);
            }
            catch (ServiceException e)
            {
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }
        
        public TicketProtobufs.Response HandleRequestGetFestivalsOnDate(TicketProtobufs.Request request)
        {
            Console.WriteLine("GetFestivals Request ...");
            try
            {
                Festival data = ProtoUtils.getFestival(request);
                Festival[] festivals;
                lock (server)
                {
                    festivals = server.GetAllFestivalsOnDate(data.Date).ToArray();
                }

                return ProtoUtils.createGetFestivalsOnDateResponse(festivals);
            }
            catch (ServiceException e)
            {
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }
        
        public TicketProtobufs.Response HandleRequestBuyTicket(TicketProtobufs.Request request)
        {
            Console.WriteLine("GetFestivals Request ...");
            try
            {
                Ticket data = ProtoUtils.getTicket(request);
                lock (server)
                {
                    server.SellTicket(data.Festival.Id,data.BuyerName,data.NumberOfSpots);
                }
                return okResponse;
            }
            catch (ServiceException e)
            {
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }

        public void SendResponse(TicketProtobufs.Response response)
        {
            Console.WriteLine("sending response " + response);
            lock (stream)
            {
                response.WriteDelimitedTo(stream);
                stream.Flush();
            }
        }
    }
}