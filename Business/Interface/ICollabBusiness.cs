using Common.Model;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interface
{
    public  interface ICollabBusiness
    {
        public CollabEntity Create(CollabModel collabModel, long userId);
        public IEnumerable<CollabEntity> GetAll(long userid);
   public CollabEntity GetById(long collabId,long userId);   
   public  bool Delete(long collabId, long userId);
    }
}
