using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using services.interfaces;

namespace networking
{
    
    public abstract class AbstractServer
    {
        private TcpListener _server;
        private String host;
        private int port;
        public AbstractServer(String host, int port)
        {
            this.host = host;
            this.port = port;
        }
        public void Start()
        {
            IPAddress adr = IPAddress.Parse(host);
            IPEndPoint ep=new IPEndPoint(adr,port);
            _server=new TcpListener(ep);
            _server.Start();
            while (true)
            {
                Console.WriteLine("Waiting for clients ...");
                TcpClient client = _server.AcceptTcpClient();
                Console.WriteLine("Client connected ...");
                ProcessRequest(client);
            }
        }

        public abstract void ProcessRequest(TcpClient client);
        
    }

    
        public abstract class ConcurrentServer:AbstractServer
        {
            
            public ConcurrentServer(string host, int port) : base(host, port)
            {}

            public override void ProcessRequest(TcpClient client)
            {
                
                Thread t = CreateWorker(client);
                t.Start();
                
            }

            protected abstract  Thread CreateWorker(TcpClient client);
            
        }
        
        public class SerialServer: ConcurrentServer 
        {
            private ISuperService server;
            private TicketSellerProtoWorker _worker;
            public SerialServer(string host, int port, ISuperService server) : base(host, port)
            {
                this.server = server;
                Console.WriteLine("SerialChatServer...");
            }
            protected override Thread CreateWorker(TcpClient client)
            {
                _worker = new TicketSellerProtoWorker(server, client);
                return new Thread(_worker.Run);
            }
        }
   
}
