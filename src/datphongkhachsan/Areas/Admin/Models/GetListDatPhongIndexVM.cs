using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using datphongkhachsan.Data.DataModel;

namespace datphongkhachsan.Areas.Admin.Models
{
    public class GetListDatPhongIndexVM
    {
        // dùng cho index, lọc phòng theo thời gian nhận thời gian trả.\

        public List<Phong> LsPhongDatPhongs { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Thời Gian Nhận")]
        [DataType(DataType.Date)]
        public DateTime NgayNhanPhongDuKien { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Thời Gian Trả")]
        [DataType(DataType.Date)]
        public DateTime NgayTraPhongDuKien { get; set; }

        public bool isFillter { get; set; }
    }
}
