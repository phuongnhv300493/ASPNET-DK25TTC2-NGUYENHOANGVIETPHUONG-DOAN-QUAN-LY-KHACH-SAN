using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using datphongkhachsan.Data.DataModel;

namespace datphongkhachsan.Areas.Admin.Models
{
    public class DatPhongCartVM
    {
        public DatPhong newDatPhong { get; set; }
        public List<Phong> LsPhongDatPhongs { get; set; }
        public List<DichVu> LsDichVuDatPhongs { get; set; }

        public List<ChiTietDatPhong> NewLsChiTietDatPhong { get; set; }
        public List<ChiTietDichVuDatPhong> NewLsChiTietDichVuDatPhongs { get; set; }

        // dùng cho control GetListDatPhong 
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Thời Gian Nhận")]
        public DateTime NgayNhanPhongDuKien { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Thời Gian Trả")]
        public DateTime NgayTraPhongDuKien { get; set; }
    }
}
