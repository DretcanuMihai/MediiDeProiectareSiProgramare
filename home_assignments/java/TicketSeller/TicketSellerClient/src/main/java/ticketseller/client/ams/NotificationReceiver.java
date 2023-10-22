package ticketseller.client.ams;

import org.springframework.jms.core.JmsOperations;
import ticketseller.model.entities.Festival;
import ticketseller.model.entities.Ticket;
import ticketseller.model.entities.TicketNotif;
import ticketseller.services.interfaces.INotificationReceiver;
import ticketseller.services.interfaces.INotificationSubscriber;

import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.TimeUnit;


public class NotificationReceiver implements INotificationReceiver {
    private JmsOperations jmsOperations;
    private boolean running;

    public NotificationReceiver(JmsOperations operations) {
        jmsOperations = operations;
    }

    ExecutorService service;
    INotificationSubscriber subscriber;

    @Override
    public void start(INotificationSubscriber subscriber) {
        System.out.println("Starting notification receiver ...");
        running = true;
        this.subscriber = subscriber;
        service = Executors.newSingleThreadExecutor();
        service.submit(() -> {
            run();
        });
    }

    private void run() {
        while (running) {
            TicketNotif notif = (TicketNotif) jmsOperations.receiveAndConvert();
            System.out.println("Received Notification... " + notif);
            Festival festival=new Festival();
            try {
                festival.setId(notif.getFestivalId());
                Ticket ticket=new Ticket(notif.getId(),notif.getBuyerName(),festival,notif.getNumberOfSpots());
                subscriber.ticketBought(ticket);
            }catch (Exception e){
                e.printStackTrace();
            }
        }
    }


    @Override
    public void stop() {
        running = false;
        try {
            service.awaitTermination(100, TimeUnit.MILLISECONDS);
            service.shutdown();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        System.out.println("Stopped notification receiver");
    }
}
