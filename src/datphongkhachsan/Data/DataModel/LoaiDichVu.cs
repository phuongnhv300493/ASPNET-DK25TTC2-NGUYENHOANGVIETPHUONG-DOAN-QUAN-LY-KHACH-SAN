using System;
using System.Collections.Generic;
using System.Linq;

namespace datphongkhachsan.Data.DataModel
{
    public interface ILoaiDichVu
    {
        IQueryable<LoaiDichVu> GetAll();
        LoaiDichVu GetOneById(int? id);
        void New(LoaiDichVu LoaiDv);
        void Edit(LoaiDichVu LoaiDv);
        void Delete(int id);
    }
    public partial class LoaiDichVu
    {
        public LoaiDichVu()
        {
            DichVus = new HashSet<DichVu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<DichVu> DichVus { get; set; }
    }
}
