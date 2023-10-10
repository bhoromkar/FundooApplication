using System;
using Business.Interface;
using Common.Model;
using Repository.Entity;
using Repository.Interface;
using System.Collections.Generic;

namespace Business.Service
{
    public class LabelBusiness : ILabelBusiness
    {
        public readonly ILabelRepo _labelRepo;
        public LabelBusiness(ILabelRepo labelRepo)
        {
            this._labelRepo = labelRepo;
        }

        public LabelEntity Create(LabelModel model, long userId)
        {
            try
            {
                var result = _labelRepo.Create(model, userId);
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool Delete(long userId, long labelId)
        {
            try
            {
                return _labelRepo.Delete(userId,labelId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<LabelEntity> GetAll(long userId)
        {
            try
            {
                var result = _labelRepo.GetAll(userId);
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public LabelEntity Update(LabelModel model, long userId)
        {
            try
            {
                var result = _labelRepo.Update(model, userId);
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}