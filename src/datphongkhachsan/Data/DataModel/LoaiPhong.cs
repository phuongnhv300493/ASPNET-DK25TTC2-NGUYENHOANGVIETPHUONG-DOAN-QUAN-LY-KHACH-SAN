using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace datphongkhachsan.Data.DataModel
{
    public interface ILoaiPhong
    {
        IQueryable<LoaiPhong> GetAll();
        LoaiPhong GetOneById(int? id);
        void New(LoaiPhong LoaiPhong);
        void Edit(LoaiPhong LoaiPhong);
        void Delete(int id);
    }
    public  class LoaiPhong
    {
        public LoaiPhong()
        {
            Phongs = new HashSet<Phong>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Mô tả ngắn")]
        public string ShortDescription { get; set; }
        public string HinhUrl { get; set; }


        public virtual ICollection<Phong> Phongs { get; set; }
    }
}
