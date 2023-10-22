using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using model.entities;
using persistence.interfaces;
using persistence.utils;

namespace persistence.implementations
{
    public class UserDbRepository : IUserRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger("UserDbRepository");

        private readonly IDictionary<String, string> _props;

        public UserDbRepository(IDictionary<String, string> props)
        {
            Logger.Info("UserDbRepository");
            this._props = props;
        }


        public void Add(User user)
        {
            Logger.InfoFormat("Saving user {0}", user);

            IDbConnection con = DbUtils.GetConnection(_props);

            try
            {
                using (IDbCommand comm = con.CreateCommand())
                {
                    comm.CommandText =
                        "insert into Users(username,password) values(@username,@password)";
                    IDbDataParameter paramUsername = comm.CreateParameter();
                    paramUsername.ParameterName = "@username";
                    paramUsername.Value = user.Username;
                    comm.Parameters.Add(paramUsername);

                    IDbDataParameter paramPassword = comm.CreateParameter();
                    paramPassword.ParameterName = "@password";
                    paramPassword.Value = user.Password;
                    comm.Parameters.Add(paramPassword);

                    int result = comm.ExecuteNonQuery();
                    Logger.InfoFormat("Added {0} users", result);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            Logger.InfoFormat("Exiting Add");
        }

        public void Delete(User user)
        {
            Logger.InfoFormat("Deleting user {0}", user);

            IDbConnection con = DbUtils.GetConnection(_props);

            try
            {
                using (IDbCommand comm = con.CreateCommand())
                {
                    comm.CommandText =
                        "delete from Users where username=@username";

                    IDbDataParameter paramUsername = comm.CreateParameter();
                    paramUsername.ParameterName = "@username";
                    paramUsername.Value = user.Username;
                    comm.Parameters.Add(paramUsername);

                    int result = comm.ExecuteNonQuery();
                    Logger.InfoFormat("Deleted {0} users", result);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            Logger.InfoFormat("Exiting Delete");
        }

        public void Update(User user, string username)
        {
            Logger.InfoFormat("Updating user with username: {0} with info: {1}", username, user);

            IDbConnection con = DbUtils.GetConnection(_props);

            try
            {
                using (IDbCommand comm = con.CreateCommand())
                {
                    comm.CommandText =
                        "update Users set username=@username,password=@password where username=@old_username";

                    IDbDataParameter paramUsername = comm.CreateParameter();
                    paramUsername.ParameterName = "@username";
                    paramUsername.Value = user.Username;
                    comm.Parameters.Add(paramUsername);

                    IDbDataParameter paramPassword = comm.CreateParameter();
                    paramPassword.ParameterName = "@password";
                    paramPassword.Value = user.Password;
                    comm.Parameters.Add(paramPassword);

                    IDbDataParameter paramOldUsername = comm.CreateParameter();
                    paramOldUsername.ParameterName = "@old_username";
                    paramOldUsername.Value = username;
                    comm.Parameters.Add(paramOldUsername);

                    int result = comm.ExecuteNonQuery();
                    Logger.InfoFormat("Updated {0} users", result);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            Logger.InfoFormat("Exiting Update");
        }
        
        private User ExtractUser(IDataReader dataReader)
        {
            Logger.InfoFormat("Extracting user");
            string username = dataReader.GetString(0);
            string password = dataReader.GetString(1);
            User toReturn = new User(username, password);
            Logger.InfoFormat("Extracted user:{0}", toReturn);
            return toReturn;
        }

        public User FindById(string username)
        {
            Logger.InfoFormat("Searching user with id: {0}", username);
            User toReturn = null;
            IDbConnection con = DbUtils.GetConnection(_props);

            try
            {
                using (IDbCommand comm = con.CreateCommand())
                {
                    comm.CommandText =
                        "select username,password from Users where username=@username";
                    IDbDataParameter paramUsername = comm.CreateParameter();
                    paramUsername.ParameterName = "@username";
                    paramUsername.Value = username;
                    comm.Parameters.Add(paramUsername);

                    using (IDataReader dataR = comm.ExecuteReader())
                    {
                        if (dataR.Read())
                        {
                            toReturn = ExtractUser(dataR);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            Logger.InfoFormat("Exiting findOne with value {0}", toReturn);
            return toReturn;
        }

        public IEnumerable<User> FindAll()
        {
            Logger.InfoFormat("Finding all festivals");
            IEnumerable<User> toReturn = GetAll();
            Logger.InfoFormat("Exiting FindAll with value {0}", toReturn);
            return toReturn;
        }

        public ICollection<User> GetAll()
        {
            Logger.InfoFormat("Getting users");
            ICollection<User> toReturn = new List<User>();
            IDbConnection con = DbUtils.GetConnection(_props);

            try
            {
                using (IDbCommand comm = con.CreateCommand())
                {
                    comm.CommandText =
                        "select username,password from Users";
                    using (IDataReader dataR = comm.ExecuteReader())
                    {
                        while (dataR.Read())
                        {
                            User user = ExtractUser(dataR);
                            toReturn.Add(user);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            Logger.InfoFormat("Exiting GetAll with value {0}", toReturn);
            return toReturn;
        }
    }
}