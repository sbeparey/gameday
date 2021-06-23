using System;
using System.Data;
using Gameday.DotNet.Web.Models;

namespace Gameday.DotNet.Web.Data
{
    public static class Extensions
    {
        internal static Customer ToCustomer(this IDataReader reader)
        {
            return new Customer
            {
                Id = reader.GetFieldValue<long>("id"),
                FirstName = reader.GetFieldValue<string>("first_name"),
                LastName = reader.GetFieldValue<string>("last_name"),
                Email = reader.GetFieldValue<string>("email"),
                Phone = reader.GetFieldValue<string>("phone"),
                Address = reader.GetFieldValue<string>("address"),
                City = reader.GetFieldValue<string>("city"),
                State = reader.GetFieldValue<string>("state"),
                Zip = reader.GetFieldValue<string>("zip"),
                PreferredName = reader.GetFieldValue<string>("preferred_name"),
                CreatedAt = reader.GetFieldValue<DateTime>("created_at"),
                UpdatedAt = reader.GetFieldValue<DateTime>("updated_at"),
            };
        }

        public static T GetFieldValue<T>(this IDataReader reader, string column, T defaultValue = default)
        {
            if (reader is null)
                throw new ArgumentNullException(nameof(reader));

            if (string.IsNullOrWhiteSpace(column))
                throw new ArgumentNullException(nameof(column));

            int index = reader.GetOrdinal(column);
            return !reader.IsDBNull(index) ? (T)reader[index] : defaultValue;
        }
    }
}