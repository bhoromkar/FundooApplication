using Common.Model;
using System.Collections.Generic;
using Repository.Entity;


namespace Business.Interface
{
    public interface ILabelBusiness
    {
        public LabelEntity Create(LabelModel model, long userId);
        public LabelEntity Update(LabelModel model, long userId);
        public bool Delete(long userId, long labelId);
        public IEnumerable<LabelEntity> GetAll(long userId);
    }
}