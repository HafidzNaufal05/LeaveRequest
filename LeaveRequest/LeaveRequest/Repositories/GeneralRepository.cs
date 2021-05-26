using LeaveRequest.Context;
using LeaveRequest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Repositories
{
    public class GeneralRepository<Entity, Context, Id> : IGenericRepository<Entity, Id>
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext;
        private DbSet<Entity> entities;

        //constructor
        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }

        //create data
        public int Create(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                entities.Add(entity);
                var result = myContext.SaveChanges();
                return result;
            }
        }

        //delete data
        public int Delete(Id id)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                Entity entity = entities.Find(id);
                entities.Remove(entity);
                var result = myContext.SaveChanges();
                return result;
            }
        }

        //get all data
        public IEnumerable<Entity> GetAll()
        {
            return entities.AsEnumerable();
        }

        //get data by id
        public Entity GetById(Id id)
        {
            return entities.Find(id);
        }

        //update data
        public int Update(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                try
                {
                    myContext.Entry(entity).State = EntityState.Modified;
                    var result = myContext.SaveChanges();
                    return result;
                }

                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
