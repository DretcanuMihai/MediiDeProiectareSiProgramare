import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import network.protobuffprotocol.ProtoProxy;
import network.rpcprotocol.ams.TicketSellerAMSRpcProxy;
import org.springframework.context.support.ClassPathXmlApplicationContext;
import ticketseller.client.ams.AMSLoginController;
import ticketseller.client.controllers.LoginController;
import ticketseller.services.interfaces.IAMSSuperService;
import ticketseller.services.interfaces.ISuperService;

import java.io.IOException;
import java.util.Properties;


public class StartAMSClient extends Application {
    private static int defaultChatPort = 55555;
    private static String defaultServer = "localhost";

    public static void main(String[] args) {
        launch(args);
    }

    public void start(Stage primaryStage) throws Exception {
        System.out.println("In start");
        ClassPathXmlApplicationContext context = new ClassPathXmlApplicationContext("spring-client.xml");
        IAMSSuperService server=context.getBean("chatServices", TicketSellerAMSRpcProxy.class);



        FXMLLoader loader = new FXMLLoader(AMSLoginController.class.getResource("login.fxml"));
        Parent root=loader.load();


        AMSLoginController ctrl = loader.getController();
        ctrl.setSuperService(server);
        ctrl.setStage(primaryStage);

        primaryStage.setTitle("TicketSeller");
        primaryStage.setScene(new Scene(root));
        primaryStage.show();
    }


}


