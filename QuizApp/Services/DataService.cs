using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApp.Data;

namespace QuizApp.Services
{
    public class DataService : BaseService
    {
        /// <summary>
        /// Database service.
        /// </summary>
        public DataService(DataContext dataContext) : base(dataContext) { }

        /// <summary>
        /// Creates a .db file if doesn't exist.
        /// </summary>
        public void EnsureDbCreated()
        {
            _dataContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Clean up database connections.
        /// </summary>
        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
