using Core.Intrtfaces;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly DataContext _context;
        private IGenericRepository<T> _entity;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public IGenericRepository<T> Entity{
            get
            {
                return _entity ?? (_entity = new GemericRepository<T>(_context));
            }
        
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
