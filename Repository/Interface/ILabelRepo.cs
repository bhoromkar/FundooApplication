using System.Collections.Generic;
using Common.Model;
using Repository.Entity;

namespace Repository.Interface
{
    public interface ILabelRepo
    {
        public LabelEntity Create(LabelModel  model, long userId);
        public LabelEntity Update(LabelModel model, long userId);
        public bool Delete(long userId, long labelId);
        public IEnumerable<LabelEntity> GetAll(long userId);
    }
}