import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

public class CarsDBRepository implements CarRepository{

    private final JdbcUtils dbUtils;



    private static final Logger logger= LogManager.getLogger();

    public CarsDBRepository(Properties props) {
        logger.info("Initializing CarsDBRepository with properties: {} ",props);
        dbUtils=new JdbcUtils(props);
    }

    @Override
    public List<Car> findByManufacturer(String manufacturerN) {
        logger.traceEntry("finding with manufacturer {}",manufacturerN);
        Connection connection=dbUtils.getConnection();
        List<Car> cars=new ArrayList<>();
        try(PreparedStatement preparedStatement= connection.prepareStatement("select * from Cars where manufacturer=(?)")){
            preparedStatement.setString(1,manufacturerN);
            try(ResultSet resultSet=preparedStatement.executeQuery()){
                while(resultSet.next()){
                    int id=resultSet.getInt("id");
                    String manufacturer=resultSet.getString("manufacturer");
                    String model=resultSet.getString("model");
                    int year=resultSet.getInt("year");
                    Car car=new Car(manufacturer,model,year);
                    car.setId(id);
                    cars.add(car);
                }
            }
        }catch (SQLException e){
            logger.error(e);
            System.err.println("Error DB "+e);
        }
        logger.traceExit(cars);
        return cars;
    }

    @Override
    public List<Car> findBetweenYears(int min, int max) {
        logger.traceEntry("finding between {} and {}",min,max);
        Connection connection=dbUtils.getConnection();
        List<Car> cars=new ArrayList<>();
        try(PreparedStatement preparedStatement= connection.prepareStatement("select * from Cars where year between (?) and (?)")){
            preparedStatement.setInt(1,min);
            preparedStatement.setInt(2,max);
            try(ResultSet resultSet=preparedStatement.executeQuery()){
                while(resultSet.next()){
                    int id=resultSet.getInt("id");
                    String manufacturer=resultSet.getString("manufacturer");
                    String model=resultSet.getString("model");
                    int year=resultSet.getInt("year");
                    Car car=new Car(manufacturer,model,year);
                    car.setId(id);
                    cars.add(car);
                }
            }
        }catch (SQLException e){
            logger.error(e);
            System.err.println("Error DB "+e);
        }
        logger.traceExit(cars);
        return cars;
    }

    @Override
    public void add(Car elem) {
        logger.traceEntry("saving car {}",elem);
        Connection connection=dbUtils.getConnection();
        try(PreparedStatement preparedStatement= connection.prepareStatement("insert into Cars(manufacturer,model,year) values(?,?,?)")){
            preparedStatement.setString(1,elem.getManufacturer());
            preparedStatement.setString(2,elem.getModel());
            preparedStatement.setInt(3,elem.getYear());
            int result=preparedStatement.executeUpdate();
            logger.traceEntry("Saved {} instances", result);
        }catch (SQLException e){
            logger.error(e);
            System.err.println("Error DB "+e);
        }
        logger.traceExit();
    }

    @Override
    public void update(Integer integer, Car elem) {
        logger.traceEntry("updating car id:{} with: {}",integer,elem);
        Connection connection=dbUtils.getConnection();
        try(PreparedStatement preparedStatement= connection.prepareStatement("update Cars set manufacturer=(?),model=(?),year=(?) where id=(?)")){
            preparedStatement.setString(1,elem.getManufacturer());
            preparedStatement.setString(2,elem.getModel());
            preparedStatement.setInt(3,elem.getYear());
            preparedStatement.setInt(4,integer);
            int result=preparedStatement.executeUpdate();
            logger.traceEntry("Updated {} instances", result);
        }catch (SQLException e){
            logger.error(e);
            System.err.println("Error DB "+e);
        }
        logger.traceExit();
    }

    @Override
    public Iterable<Car> findAll() {
        logger.traceEntry();
        Connection connection=dbUtils.getConnection();
        List<Car> cars=new ArrayList<>();
        try(PreparedStatement preparedStatement= connection.prepareStatement("select * from Cars")){
            try(ResultSet resultSet=preparedStatement.executeQuery()){
                while(resultSet.next()){
                    int id=resultSet.getInt("id");
                    String manufacturer=resultSet.getString("manufacturer");
                    String model=resultSet.getString("model");
                    int year=resultSet.getInt("year");
                    Car car=new Car(manufacturer,model,year);
                    car.setId(id);
                    cars.add(car);
                }
            }
        }catch (SQLException e){
            logger.error(e);
            System.err.println("Error DB "+e);
        }
        logger.traceExit(cars);
        return cars;
    }
}
