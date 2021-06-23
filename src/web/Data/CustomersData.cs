using System;
using System.Collections.Generic;
using System.Data;
using Gameday.DotNet.Web.Models;
using Npgsql;

namespace Gameday.DotNet.Web.Data
{
    public class CustomersData
    {
        private readonly string connectionString;

        public CustomersData(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<Customer> GetCustomers(int limit = 100)
        {
            var customers = new List<Customer>();

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = $"SELECT id, " +
                                         $"first_name, " +
                                         $"last_name, " +
                                         $"email, " +
                                         $"phone, " +
                                         $"address, " +
                                         $"city, " +
                                         $"state, " +
                                         $"zip, " +
                                         $"preferred_name, " +
                                         $"created_at, " +
                                         $"updated_at " + 
                                  $"FROM public.customers " +
                                  $"ORDER BY updated_at DESC " +
                                  $"LIMIT {limit};";
            
            command.CommandType = CommandType.Text;
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                customers.Add(reader.ToCustomer());
            }

            return customers;
        }

        public bool UpdateCustomer(long id, string firstName, string lastName, string phone)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName));
            
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException(nameof(lastName));
            
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentNullException(nameof(phone));
                    
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            using var command = connection.CreateCommand();
            
            command.CommandText =
                $"UPDATE customers " +
                $"SET first_name = '{firstName}', " +
                    $"last_name = '{lastName}', " +
                    $"phone = '{phone}', " +
                    $"updated_at = '{DateTime.UtcNow}' " +
                $"WHERE id = {id};";
            
            command.CommandType = CommandType.Text;
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected == 1;
        }
    }
}