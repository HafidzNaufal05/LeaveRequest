using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Repositories.Interfaces
{
    public interface IGenericRepository<Entity, Id> where Entity : class
    {
        IEnumerable<Entity> GetAll();
        Entity GetById(Id id);
        int Create(Entity entity);
        int Update(Entity entity);
        int Delete(Id id);
    }
}
