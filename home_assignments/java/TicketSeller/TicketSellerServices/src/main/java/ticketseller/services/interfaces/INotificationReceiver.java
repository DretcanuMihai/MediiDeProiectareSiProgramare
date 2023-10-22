package ticketseller.services.interfaces;

public interface INotificationReceiver {
    void start(INotificationSubscriber subscriber);
    void stop();
}
