using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSIiZ_WPF.Data;

namespace WSIiZ_WPF.Services
{
    public class DataService : BaseService
    {
        public DataService(DataContext dataContext) : base(dataContext) { }

        /// <summary>
        /// Creates a .db file if doesn't exist
        /// </summary>
        public void EnsureDbCreated()
        {
            _dataContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Clean up database connections
        /// </summary>
        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
