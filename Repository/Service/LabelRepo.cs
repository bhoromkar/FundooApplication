using System;
using Common.Model;
using Microsoft.Extensions.Configuration;
using Repository.Context;
using Repository.Entity;
using Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Service
{
    public class LabelRepo : ILabelRepo
    {

        public readonly FundoDBContext _userDBContext;
        public readonly IConfiguration Iconfiguration;



        public LabelRepo(FundoDBContext userDBContext, IConfiguration configuration)
        {
            this.Iconfiguration = configuration;
            this._userDBContext = userDBContext;

        }

        public LabelEntity Create(LabelModel model, long userId)
        {
            try
            {
                var result = _userDBContext.Users.FirstOrDefault(x => x.userId == userId);
                if (result != null)
                {
                    LabelEntity entity = new LabelEntity();

                    entity.UserId = userId;
                    entity .LabelName= model.LabelName;
                    entity.NoteId= model.NoteId;
                    _userDBContext.Label.Add(entity);
                    _userDBContext.SaveChanges();
                    return entity;

                }

                return null;
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
                var result = _userDBContext.Users.First(x => x.userId == userId);
                if (result != null)
                {
                    var label = _userDBContext.Label.FirstOrDefault(x => x.LabelId == labelId);
                    if (label != null)
                    {
                        _userDBContext.Label.Remove(label);
                        _userDBContext.SaveChanges();
                        return true;

                    }
                }
                return false;
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
                var result = _userDBContext.Users.First(x => x.userId == userId);
                if (result != null)
                {
                    return _userDBContext.Label.Where(x => x.UserId == userId).ToList();
                }

                return null;
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
                var result = _userDBContext.Label.FirstOrDefault(x => x.UserId == userId);
                if (result != null)
                {
                    var label = _userDBContext.Label.FirstOrDefault(x => x.LabelId == model.LabelId);
                    if (label != null)
                    {
                        result.UserId = userId;
                        result.LabelName = model.LabelName;
                        result.NoteId = model.NoteId;
                        _userDBContext.Label.Update(result);
                        _userDBContext.SaveChanges();
                        return result;

                    }
                }

                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}