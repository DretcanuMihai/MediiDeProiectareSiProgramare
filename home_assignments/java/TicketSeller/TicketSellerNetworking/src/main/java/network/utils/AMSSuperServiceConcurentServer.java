package network.utils;

import network.rpcprotocol.ams.TicketSellerAMSRpcReflectionWorker;
import ticketseller.services.interfaces.IAMSSuperService;

import java.net.Socket;


public class AMSSuperServiceConcurentServer extends AbsConcurrentServer {
    private IAMSSuperService superService;
    public AMSSuperServiceConcurentServer(int port, IAMSSuperService superService) {
        super(port);
        this.superService = superService;
        System.out.println("SuperService - ChatRpcConcurrentServer");
    }

    @Override
    protected Thread createWorker(Socket client) {
        TicketSellerAMSRpcReflectionWorker worker=new TicketSellerAMSRpcReflectionWorker(superService, client);

        Thread tw=new Thread(worker);
        return tw;
    }

    @Override
    public void stop(){
        System.out.println("Stopping services ...");
    }
}
