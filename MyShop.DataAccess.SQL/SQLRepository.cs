using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal DataContext context;
        internal DbSet<T> dbSet;

        public SQLRepository (DataContext context)               // Constructor
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            var t = Find(Id);       // using find method defined below
            if (context.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);
            dbSet.Remove(t);
        }

        public T Find(string Id)
        {
            return dbSet.Find(Id);          // dbSet has its own find method
        }

        public void Insert(T t)
        {
            dbSet.Add(t);                    // dbSet has its own insert method
        }

        public void Update(T t)
        {
            dbSet.Attach(t);                // passsing in the object and attach to entity framework table
            context.Entry(t).State = EntityState.Modified;  // when call save changes method, to look for objevt t and save it to the database

        }
    }
}
