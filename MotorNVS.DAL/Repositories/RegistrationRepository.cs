using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MotorNVS.DAL.Database;
using MotorNVS.DAL.Database.Entities;
using System.Data;

namespace MotorNVS.DAL.Repositories
{
    public interface IRegistrationRepository
    {
        Task<List<Registration>> SelectAllRegistrations();
        Task<List<Registration>> SelectAllRegistrationsByVehicleId(int vehicleId);
        Task<Registration> SelectRegistrationById(int registrationId);
        Task<Registration> InsertNewRegistration(Registration registration);
        Task<Registration> DeleteRegistrationById(int registrationId);
        Task<Registration> UpdateRegistrationById(int registrationId, Registration registration);
    }

    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly MotorDBContext _dBContext;
        private readonly DBConnect _dBConnect;
        public RegistrationRepository(MotorDBContext dBContext, DBConnect dBConnect)
        {
            _dBContext = dBContext;
            _dBConnect = dBConnect;
        }

        public async Task<Registration> DeleteRegistrationById(int registrationId)
        {
            Registration registrationToDelete = await _dBContext
                .Registration
                .FirstOrDefaultAsync(x => x.Id == registrationId);

            if (registrationToDelete != null)
            {
                _dBContext.Remove(registrationToDelete);

                await _dBContext.SaveChangesAsync();
            }

            return registrationToDelete;
        }

        public async Task<Registration> InsertNewRegistration(Registration registration)
        {
            await _dBContext.AddAsync(registration);
            await _dBContext.SaveChangesAsync();

            return registration;
        }

        public async Task<List<Registration>> SelectAllRegistrations()
        {
            return await _dBContext
                .Registration
                .Include(x => x.Vehicle)
                .Include(x => x.Vehicle.Fuel)
                .Include(x => x.Vehicle.Category)
                .Include(x => x.Customer)
                .Include(x => x.Customer.Address)
                .Include(x => x.Customer.Address.Zipcode)
                .ToListAsync();
        }

        public async Task<List<Registration>> SelectAllRegistrationsByVehicleId(int vehicleId)
        {
            List<Registration> regList = new List<Registration>();
            string procedure = "GetAllRegistrationsWithSpecificVehicleId";

            using (_dBConnect.conn)
            {
                SqlCommand cmd = new SqlCommand(procedure, _dBConnect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                await _dBConnect.conn.OpenAsync();
                SqlDataReader reader = cmd.ExecuteReader();

                int readCount = reader.FieldCount;

                while (reader.Read())
                {
                    regList.Add(new Registration()
                    {
                        Id = (int)reader["rid"],
                        RegistrationDate = (DateTime)reader["RegistrationDate"],
                        CustomerId = (int)reader["cusid"],
                        VehicleId = (int)reader["vid"],
                        Customer = new Customer()
                        {
                            Id = (int)reader["cusid"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            CreateDate = (DateTime)reader["CreateDate"],
                            IsActive = reader["IsActive"].ToString(),
                            AddressId = (int)reader["aid"],
                            Address = new Address()
                            {
                                Id = (int)reader["aid"],
                                StreetAndNo = reader["StreetAndNo"].ToString(),
                                CreateDate = (DateTime)reader["CreateDate"],
                                ZipCodeId = (int)reader["zid"],
                                Zipcode = new Zipcode()
                                {
                                    Id = (int)reader["aid"],
                                    ZipcodeNo = reader["ZipcodeNo"].ToString(),
                                    City = reader["City"].ToString()
                                }
                            }
                        },
                        Vehicle = new Vehicle()
                        {
                            Id = (int)reader["vid"],
                            Make = reader["Make"].ToString(),
                            Model = reader["Model"].ToString(),
                            CreateDate = (DateTime)reader["CreateDate"],
                            IsActive = reader["IsActive"].ToString(),
                            CategoryId = (int)reader["catid"],
                            FuelId = (int)reader["fid"],
                            Category = new Category()
                            {
                                Id = (int)reader["catid"],
                                CategoryName = reader["CategoryName"].ToString()
                            },
                            Fuel = new Fuel()
                            {
                                Id = (int)reader["fid"],
                                FuelName = reader["FuelName"].ToString()
                            }
                        }
                    });
                }

                await _dBConnect.conn.CloseAsync();

                return regList;
                //public Customer retrieveProc(Customer obj)
                //{
                //    string query = "usp_readCustomerById";
                //    //usp_readCustomerById
                //    Customer customer = new Customer();
                //    using (SqlConnection connection = new SqlConnection(connectionString))
                //    {
                //        //SqlCommand cmd = new SqlCommand("vores storedprocedures navn", connection);
                //        SqlCommand cmd = new SqlCommand(query, connection);
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        //cmd.Parameters.AddWithValue("sqlscript", c# parameter );
                //        cmd.Parameters.AddWithValue("@customerId", obj.customerId);

                //        // talk to our server
                //        connection.Open();
                //        SqlDataReader reader = cmd.ExecuteReader();
                //        reader.Read();
                //        customer.firstname = reader["firstname"].ToString();
                //        customer.lastname = reader["lastname"].ToString();
                //    }
                //    return customer;
                //}
            }
        }

        public async Task<Registration> SelectRegistrationById(int registrationId)
        {
            return await _dBContext
                .Registration
                .Include(x => x.Vehicle)
                .Include(x => x.Vehicle.Fuel)
                .Include(x => x.Vehicle.Category)
                .Include(x => x.Customer)
                .Include(x => x.Customer.Address)
                .Include(x => x.Customer.Address.Zipcode)
                .FirstOrDefaultAsync(x => x.Id == registrationId);
        }

        public async Task<Registration> UpdateRegistrationById(int registrationId, Registration registration)
        {
            Registration registrationToUpdate = await _dBContext
                .Registration
                .FirstOrDefaultAsync(x => x.Id == registrationId);

            if (registrationToUpdate != null)
            {
                registrationToUpdate.CustomerId = registration.CustomerId;
                registrationToUpdate.VehicleId = registration.VehicleId;

                await _dBContext.SaveChangesAsync();
            }

            return registrationToUpdate;
        }
    }
}
