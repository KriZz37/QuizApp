using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSIiZ_WPF.Data;
using WSIiZ_WPF.Entities;
using WSIiZ_WPF.Interfaces;

namespace WSIiZ_WPF.Services
{
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
