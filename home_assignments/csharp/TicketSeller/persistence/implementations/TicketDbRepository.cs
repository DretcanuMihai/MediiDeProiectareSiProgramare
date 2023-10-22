using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using model.entities;
using persistence.interfaces;
using persistence.utils;

namespace persistence.implementations
{
    public class TicketDbRepository : ITicketRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger("TicketDbRepository");

        private readonly IDictionary<String, string> _props;

        public TicketDbRepository(IDictionary<String, string> props)
        {
            Logger.Info("TicketDbRepository");
            _props = props;
        }

        public void Add(Ticket ticket)
        {
            Logger.InfoFormat("Saving ticket {0}", ticket);

            IDbConnection con = DbUtils.GetConnection(_props);

            try
            {
                using (IDbCommand comm = con.CreateCommand())
                {
                    comm.CommandText =
                        "insert into Tickets(buyer_name, festival_id, number_of_spots) values (@buyer_name,@festival_id,@number_of_spots)";

                    IDbDataParameter paramBuyerName = comm.CreateParameter();
                    paramBuyerName.ParameterName = "@buyer_name";
                    paramBuyerName.Value = ticket.BuyerName;
                    comm.Parameters.Add(paramBuyerName);

                    IDbDataParameter paramFestivalId = comm.CreateParameter();
                    paramFestivalId.ParameterName = "@festival_id";
                    paramFestivalId.Value = ticket.Festival.Id;
                    comm.Parameters.Add(paramFestivalId);

                    IDbDataParameter paramSpots = comm.CreateParameter();
                    paramSpots.ParameterName = "@number_of_spots";
                    paramSpots.Value = ticket.NumberOfSpots;
                    comm.Parameters.Add(paramSpots);

                    int result = comm.ExecuteNonQuery();
                    Logger.InfoFormat("Added {0} tickets", result);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            Logger.InfoFormat("Exiting Add");
        }

        public void Delete(Ticket ticket)
        {
            Logger.InfoFormat("Deleting ticket {0}", ticket);

            IDbConnection con = DbUtils.GetConnection(_props);

            try
            {
                using (IDbCommand comm = con.CreateCommand())
                {
                    comm.CommandText =
                        "delete from Tickets where id=@id";
                    IDbDataParameter paramId = comm.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = ticket.Id;
                    comm.Parameters.Add(paramId);

                    int result = comm.ExecuteNonQuery();
                    Logger.InfoFormat("Deleted {0} ticket", result);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            Logger.InfoFormat("Exiting Delete");
        }

        public void Update(Ticket ticket, int id)
        {
            Logger.InfoFormat("Updating ticket with id: {0} with info: {1}", id, ticket);

            IDbConnection con = DbUtils.GetConnection(_props);

            try
            {
                using (IDbCommand comm = con.CreateCommand())
                {
                    comm.CommandText =
                        "update Tickets" +
                        " set id=@id_new,buyer_name=@buyer_name,festival_id=@festival_id,number_of_spots=@number_of_spots" +
                        " where id=@id";

                    IDbDataParameter paramIdOld = comm.CreateParameter();
                    paramIdOld.ParameterName = "@id_new";
                    paramIdOld.Value = ticket.Id;
                    comm.Parameters.Add(paramIdOld);

                    IDbDataParameter paramBuyerName = comm.CreateParameter();
                    paramBuyerName.ParameterName = "@buyer_name";
                    paramBuyerName.Value = ticket.BuyerName;
                    comm.Parameters.Add(paramBuyerName);

                    IDbDataParameter paramFestivalId = comm.CreateParameter();
                    paramFestivalId.ParameterName = "@festival_id";
                    paramFestivalId.Value = ticket.Festival.Id;
                    comm.Parameters.Add(paramFestivalId);

                    IDbDataParameter paramNumberSpots = comm.CreateParameter();
                    paramNumberSpots.ParameterName = "@number_of_spots";
                    paramNumberSpots.Value = ticket.NumberOfSpots;
                    comm.Parameters.Add(paramNumberSpots);

                    IDbDataParameter paramId = comm.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = id;
                    comm.Parameters.Add(paramId);

                    int result = comm.ExecuteNonQuery();
                    Logger.InfoFormat("Updated {0} tickets", result);
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
            int id = dataReader.GetInt32(2);
            string artistName = dataReader.GetString(4);
            DateTime date = dataReader.GetDateTime(5);
            string place = dataReader.GetString(6);
            int availableSpots = dataReader.GetInt32(7);
            int soldSpots = dataReader.GetInt32(8);
            Festival toReturn = new Festival(id, artistName, date, place, soldSpots, availableSpots);
            Logger.InfoFormat("Extracted festival {0}", toReturn);
            return toReturn;
        }

        
        private Ticket ExtractTicket(IDataReader dataReader)
        {
            Logger.InfoFormat("Extracting ticket");
            int id = dataReader.GetInt32(0);
            string buyerName = dataReader.GetString(1);
            int numberOfSpots = dataReader.GetInt32(3);
            Festival festival = ExtractFestival(dataReader);
            Ticket toReturn = new Ticket(id, buyerName, festival, numberOfSpots);
            Logger.InfoFormat("Extracted ticket {0}", toReturn);
            return toReturn;
        }

        public Ticket FindById(int id)
        {
            Logger.InfoFormat("Searching Ticket with id: {0}", id);
            Ticket toReturn = null;
            IDbConnection con = DbUtils.GetConnection(_props);

            try
            {
                using (IDbCommand comm = con.CreateCommand())
                {
                    comm.CommandText =
                        "select T.id, T.buyer_name, T.festival_id, T.number_of_spots, F.artist_name, F.date, F.place," +
                        "F.available_spots, F.sold_spots from Tickets T inner join Festivals F on F.id = T.festival_id where" +
                        " T.id=@id";
                    IDbDataParameter paramId = comm.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = id;
                    comm.Parameters.Add(paramId);

                    using (IDataReader dataR = comm.ExecuteReader())
                    {
                        if (dataR.Read())
                        {
                            toReturn = ExtractTicket(dataR);
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

        public IEnumerable<Ticket> FindAll()
        {
            Logger.InfoFormat("Finding all Tickets");
            IEnumerable<Ticket> toReturn = GetAll();
            Logger.InfoFormat("Exiting FindAll with value {0}", toReturn);
            return toReturn;
        }

        public ICollection<Ticket> GetAll()
        {
            Logger.InfoFormat("Getting all tickets");
            List<Ticket> toReturn = new List<Ticket>();
            IDbConnection con = DbUtils.GetConnection(_props);

            try
            {
                using (IDbCommand comm = con.CreateCommand())
                {
                    comm.CommandText =
                        "select T.id, T.buyer_name, T.festival_id, T.number_of_spots, artist_name, date, place," +
                        "available_spots, sold_spots from Tickets T inner join Festivals F on F.id = T.festival_id";

                    using (IDataReader dataR = comm.ExecuteReader())
                    {
                        while (dataR.Read())
                        {
                            Ticket ticket = ExtractTicket(dataR);
                            toReturn.Add(ticket);
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