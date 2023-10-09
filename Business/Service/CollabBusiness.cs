using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Repository.Entity;
using Repository.Interface;
using Business.Interface;
using Repository.Service;
using Common.Model;

namespace Business.Service
{
    public class CollabBusiness : ICollabBusiness
    {
        public readonly ICollabRepo _collabRepo;
        public CollabBusiness(ICollabRepo collabRepo)
        {
            this._collabRepo = collabRepo;
        }
        public CollabEntity Create(CollabModel collabModel,long userId)
        {
            try
            {
                return _collabRepo.Create(collabModel, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(long collabId, long userId)
        {
            try
            {
                return _collabRepo.Delete(collabId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CollabEntity GetById(long collabId, long userId)       {
            try
            {
                return _collabRepo.GetById(collabId,userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<CollabEntity> GetAll(long userId)
        {
            try
            {
                return _collabRepo.GetAll(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
