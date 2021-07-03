using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSIiZ_WPF.Data;

namespace WSIiZ_WPF.Services
{
    /// <summary>
    /// Contains methods used only by derived services
    /// </summary>
    public abstract class BaseService
    {
        protected readonly DataContext _dataContext;

        public BaseService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected void SaveChanges()
        {
            _dataContext.SaveChanges();
        }
    }
}
