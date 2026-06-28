using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using datphongkhachsan.Data.DataModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace datphongkhachsan.Areas.Admin.Models
{
    public class DatPhongIndexVM
    {
        public List<DatPhong> LisDatPhongs { get; set; }
        public TrangThai trangThai { get; set; }
        public SelectList ListNameTrangThai { get; set; }
        public string TrangThai { get; set; }
        public string CMND { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime ThoiGianNhan { get; set; } /*= DateTime.Today.Date;*/
    }
}
