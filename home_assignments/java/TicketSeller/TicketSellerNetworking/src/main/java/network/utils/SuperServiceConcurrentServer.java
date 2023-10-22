package network.utils;

import network.protobuffprotocol.ProtoWorker;
import ticketseller.services.interfaces.ISuperService;

import java.net.Socket;


public class SuperServiceConcurrentServer extends AbsConcurrentServer {
    private ISuperService superService;
    public SuperServiceConcurrentServer(int port, ISuperService superService) {
        super(port);
        this.superService = superService;
        System.out.println("SuperService - ChatRpcConcurrentServer");
    }

    @Override
    protected Thread createWorker(Socket client) {
        ProtoWorker worker=new ProtoWorker(superService, client);

        Thread tw=new Thread(worker);
        return tw;
    }

    @Override
    public void stop(){
        System.out.println("Stopping services ...");
    }
}
