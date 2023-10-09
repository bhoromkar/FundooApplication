using Common.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Repository.Context;
using Repository.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Repository.Service
{
    public class  CollabRepo : ICollabRepo
    {
        

         public readonly FundoDBContext _userDBContext;
        public readonly IConfiguration Iconfiguration;
       


        public CollabRepo(FundoDBContext userDBContext, IConfiguration configuration)
        {
            this.Iconfiguration = configuration;
            this._userDBContext = userDBContext;
            
        }
        public CollabEntity Create(CollabModel collabModel, long userId)
        {
            try
            {
                var result = _userDBContext.Users.First(x => x.userId == userId);
                if (result != null)
                {
                    var note = _userDBContext.Note.FirstOrDefault(x => x.NoteId == collabModel.NoteID);
                    if (note != null)
                    {
                        var email = _userDBContext.Users.FirstOrDefault(x => x.Email == collabModel.Email);
                        if (email != null)
                        {
                            CollabEntity collabEntity = new CollabEntity();
                            collabEntity.UserId = userId;
                            collabEntity.Email = collabModel.Email;
                            collabEntity.NoteId = collabModel.NoteID;
                            _userDBContext.Add(collabEntity);
                            _userDBContext.SaveChanges();
                            return collabEntity;

                        }
                    }
                }
                return null;
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
                var result = _userDBContext.Collab.Where(X => X.UserId == userId);
                if (result != null)
                {
                    var collab = _userDBContext.Collab.FirstOrDefault(X =>  X.CollabId == collabId);
                    if (collab != null)
                    {

                        _userDBContext.Collab.Remove(collab);
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

        public IEnumerable<CollabEntity> GetAll(long userid)
        {
            try
            {
                var result = _userDBContext.Collab.Where(X => X.UserId == userid).ToList();
                if (result != null)
                {
                    return result;

                }

                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }



        public CollabEntity GetById(long collabId, long userId)
        {
            var result = _userDBContext.Collab.First(X => X.UserId == userId);
            if (result != null)
            {
                var collab = _userDBContext.Collab.FirstOrDefault(x => x.CollabId == collabId);
                if (collab != null)
                {
                    return collab;
                }
            }
            return null;

        }
    }

    }

