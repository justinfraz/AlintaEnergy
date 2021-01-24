using System;
using System.IO;
//using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using AlintaEnergyWebAPI.Models;

namespace AlintaEnergyWebAPITest
{
    public class ConnectionFactory : IDisposable
    {
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        public CustomerContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<CustomerContext>().UseInMemoryDatabase(databaseName: "Test_Database").Options;

            var context = new CustomerContext(option);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
