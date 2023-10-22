import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import network.protobuffprotocol.ProtoProxy;
import ticketseller.client.controllers.LoginController;
import ticketseller.services.interfaces.ISuperService;

import java.io.IOException;
import java.net.URL;
import java.util.Properties;


public class StartClient extends Application {
    private static int defaultChatPort = 55555;
    private static String defaultServer = "localhost";

    public static void main(String[] args) {
        launch(args);
    }

    public void start(Stage primaryStage) throws Exception {
        System.out.println("In start");
        Properties clientProps = new Properties();
        try {
            clientProps.load(StartClient.class.getResourceAsStream("/client.properties"));
            System.out.println("Client properties set. ");
            clientProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find client.properties " + e);
            return;
        }
        String serverIP = clientProps.getProperty("chat.server.host", defaultServer);
        int serverPort = defaultChatPort;

        try {
            serverPort = Integer.parseInt(clientProps.getProperty("chat.server.port"));
        } catch (NumberFormatException ex) {
            System.err.println("Wrong port number " + ex.getMessage());
            System.out.println("Using default port: " + defaultChatPort);
        }
        System.out.println("Using server IP " + serverIP);
        System.out.println("Using server port " + serverPort);

        ISuperService server = new ProtoProxy(serverIP, serverPort);


        FXMLLoader loader = new FXMLLoader(LoginController.class.getResource("login.fxml"));
        Parent root=loader.load();


        LoginController ctrl = loader.getController();
        ctrl.setSuperService(server);
        ctrl.setStage(primaryStage);

        primaryStage.setTitle("TicketSeller");
        primaryStage.setScene(new Scene(root));
        primaryStage.show();
    }


}


