package ticketseller.client.ams;

import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.stage.Stage;
import org.springframework.context.support.ClassPathXmlApplicationContext;
import ticketseller.client.controllers.MainController;
import ticketseller.services.interfaces.IAMSSuperService;
import ticketseller.services.interfaces.ISuperService;

import java.io.IOException;

public class AMSLoginController {

    //javaFX
    public TextField usernameTextField;
    public PasswordField passwordTextField;

    //mine
    private IAMSSuperService superService;
    private Stage stage;

    public IAMSSuperService getSuperService() {
        return superService;
    }

    public void setSuperService(IAMSSuperService superService) {
        this.superService = superService;
    }

    public Stage getStage() {
        return stage;
    }

    public void setStage(Stage stage) {
        this.stage = stage;
    }

    @FXML
    public void onLoginButtonClick() {
        try {
            FXMLLoader loader = new FXMLLoader(AMSMainController.class.getResource("main-window.fxml"));
            Parent parent=loader.load();
            AMSMainController mainController=loader.getController();
            String username=usernameTextField.getText();
            superService.login(username, passwordTextField.getText());

            ClassPathXmlApplicationContext context = new ClassPathXmlApplicationContext("spring-client.xml");
            NotificationReceiver notificationReceiver=context.getBean("notificationReceiver", NotificationReceiver.class);
            mainController.setReceiver(notificationReceiver);

            mainController.setSuperService(getSuperService());
            mainController.setStage(getStage());
            mainController.setUsername(username);
            mainController.initializeForShow();
            stage.setOnCloseRequest(event -> {
                try {
                    mainController.onLogoutButtonClick();
                } catch (IOException e) {
                    e.printStackTrace();
                }
                System.exit(0);
            });
            stage.setScene(new Scene(parent));
            stage.show();
        } catch (Exception e) {
            Alert alert = new Alert(Alert.AlertType.WARNING, e.getMessage());
            alert.showAndWait();
        }
    }
}