using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using model.entities;
using persistence.interfaces;
using persistence.utils;

namespace persistence.implementations
{
    public class FestivalDbRepository : IFestivalRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger("FestivalDbRepository");

        private readonly IDictionary<String, string> _props;

        public FestivalDbRepository(IDictionary<String, string> props)
        {
            Logger.Info("Creating FestivalDbRepository");
            _props = props;
        }

        public void Add(Festival festival)
        {
            Logger.InfoFormat("Saving festival {0}", festival);

            IDbConnection con = DbUtils.GetConnection(_props);

            try
            {
                using (IDbCommand comm = con.CreateCommand())
                {
                    comm.CommandText =
                        "insert into Festivals(artist_name, date, place, available_spots, sold_spots) values(@artist_name, @date, @place, @available_spots, @sold_spots)";
                    IDbDataParameter paramArtistName = comm.CreateParameter();
                    paramArtistName.ParameterName = "@artist_name";
                    paramArtistName.Value = festival.ArtistName;
                    comm.Parameters.Add(paramArtistName);

                    IDbDataParameter paramDate = comm.CreateParameter();
                    paramDate.ParameterName = "@date";
                    paramDate.Value = festival.Date;
                    comm.Parameters.Add(paramDate);

                    IDbDataParameter paramPlace = comm.CreateParameter();
                    paramPlace.ParameterName = "@place";
                    paramPlace.Value = festival.Place;
                    comm.Parameters.Add(paramPlace);

                    IDbDataParameter paramAvailable = comm.CreateParameter();
                    paramAvailable.ParameterName = "@available_spots";
                    paramAvailable.Value = festival.AvailableSpots;
                    comm.Parameters.Add(paramAvailable);

                    IDbDataParameter paramSold = comm.CreateParameter();
                    paramSold.ParameterName = "@sold_spots";
                    paramSold.Value = festival.SoldSpots;
                    comm.Parameters.Add(paramSold);

                    int result = comm.ExecuteNonQuery();
                    Logger.InfoFormat("Added {0} festivals", result);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            Logger.InfoFormat("Exiting Add");
        }

        public void Delete(Festival festival)
        {
            Logger.InfoFormat("Deleting festival {0}", festival);

            IDbConnection con = DbUtils.GetConnection(_props);

            try
            {
                using (IDbCommand comm = con.CreateCommand())
                {
                    comm.CommandText =
                        "delete from Festivals where id=@id";
                    IDbDataParameter paramId = comm.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = festival.Id;
                    comm.Parameters.Add(paramId);

                    int result = comm.ExecuteNonQuery();
                    Logger.InfoFormat("Deleted {0} festivals", result);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            Logger.InfoFormat("Exiting Delete");
        }

        public void Update(Festival festival, int id)
        {
            Logger.InfoFormat("Updating festival with id: {0} with info: {1}", id, festival);

            IDbConnection con = DbUtils.GetConnection(_props);

            try
            {
                using (IDbCommand comm = con.CreateCommand())
                {
                    comm.CommandText =
                        "update Festivals" +
                        " set id=(@id_new),artist_name=(@artist_name),date=(@date),place=(@place),available_spots=(@available_spots),sold_spots=(@sold_spots)" +
                        " where id=(@id)";

                    IDbDataParameter paramIdOld = comm.CreateParameter();
                    paramIdOld.ParameterName = "@id_new";
                    paramIdOld.Value = festival.Id;
                    comm.Parameters.Add(paramIdOld);

                    IDbDataParameter paramArtistName = comm.CreateParameter();
                    paramArtistName.ParameterName = "@artist_name";
                    paramArtistName.Value = festival.ArtistName;
                    comm.Parameters.Add(paramArtistName);

                    IDbDataParameter paramDate = comm.CreateParameter();
                    paramDate.ParameterName = "@date";
                    paramDate.Value = festival.Date;
                    comm.Parameters.Add(paramDate);

                    IDbDataParameter paramPlace = comm.CreateParameter();
                    paramPlace.ParameterName = "@place";
                    paramPlace.Value = festival.Place;
                    comm.Parameters.Add(paramPlace);

                    IDbDataParameter paramAvailable = comm.CreateParameter();
                    paramAvailable.ParameterName = "@available_spots";
                    paramAvailable.Value = festival.AvailableSpots;
                    comm.Parameters.Add(paramAvailable);

                    IDbDataParameter paramSold = comm.CreateParameter();
                    paramSold.ParameterName = "@sold_spots";
                    paramSold.Value = festival.SoldSpots;
                    comm.Parameters.Add(paramSold);

                    IDbDataParameter paramId = comm.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = id;
                    comm.Parameters.Add(paramId);

                    int result = comm.ExecuteNonQuery();
                    Logger.InfoFormat("Updated {0} festivals", result);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            Logger.InfoFormat("Exiting Update");
        }
        
        private Festival ExtractFestival(IDataReader dataReader)
        {
            Logger.InfoFormat("Extracting festival");
            int id = dataReader.GetInt32(0);
            string artistName = dataReader.GetString(1);
            DateTime date = dataReader.GetDateTime(2);
            string place = dataReader.GetString(3);
            int availableSpots = dataReader.GetInt32(4);
            int soldSpots = dataReader.GetInt32(5);
            Festival toReturn = new Festival(id, artistName, date, place,  availableSpots,soldSpots);
            Logger.InfoFormat("Extracted festival {0}", toReturn);
            return toReturn;
        }

        public Festival FindById(int id)
        {
            Logger.InfoFormat("Searching festival with id: {0}", id);
            Festival toReturn = null;
            IDbConnection con = DbUtils.GetConnection(_props);

            try
            {
                using (IDbCommand comm = con.CreateCommand())
                {
                    comm.CommandText =
                        "select id,artist_name,date,place,available_spots,sold_spots from Festivals where id=@id";
                    IDbDataParameter paramId = comm.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = id;
                    comm.Parameters.Add(paramId);

                    using (IDataReader dataR = comm.ExecuteReader())
                    {
                        if (dataR.Read())
                        {
                            toReturn = ExtractFestival(dataR);
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

        public IEnumerable<Festival> FindAll()
        {
            Logger.InfoFormat("Finding all festivals");
            IEnumerable<Festival> toReturn = GetAll();
            Logger.InfoFormat("Exiting FindAll with value {0}", toReturn);
            return toReturn;
        }

        public ICollection<Festival> GetAll()
        {
            Logger.InfoFormat("Getting all festivals");
            IList<Festival> toReturn = new List<Festival>();
            IDbConnection con = DbUtils.GetConnection(_props);
            try
            {
                using (IDbCommand comm = con.CreateCommand())
                {
                    comm.CommandText =
                        "select id,artist_name,date,place,available_spots,sold_spots from Festivals";

                    using (IDataReader dataR = comm.ExecuteReader())
                    {
                        while (dataR.Read())
                        {
                            Festival festival = ExtractFestival(dataR);
                            toReturn.Add(festival);
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

        public ICollection<Festival> GetAllOnDate(DateTime date)
        {
            Logger.InfoFormat("Getting all festivals on date {0}", date);
            IList<Festival> toReturn = new List<Festival>();
            IDbConnection con = DbUtils.GetConnection(_props);
            try
            {
                using (IDbCommand comm = con.CreateCommand())
                {
                    comm.CommandText =
                        "select id,artist_name,date,place,available_spots,sold_spots from Festivals where DATE(date)=Date(@date)";

                    IDbDataParameter paramId = comm.CreateParameter();
                    paramId.ParameterName = "@date";
                    paramId.Value = date;
                    comm.Parameters.Add(paramId);

                    using (IDataReader dataR = comm.ExecuteReader())
                    {
                        while (dataR.Read())
                        {
                            Festival festival = ExtractFestival(dataR);
                            toReturn.Add(festival);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            Logger.InfoFormat("Exiting GetAllOnDate with value {0}", toReturn);
            return toReturn;
        }
    }
}