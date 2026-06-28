using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace datphongkhachsan.Data.DataModel
{
    public interface IHinhPhong
    {

    }
    public partial class HinhPhong
    {
        [Key]
        public int HinhId { get; set; }
        public int PhongId { get; set; }
        public string HinhUrl { get; set; }

        public virtual Phong Phong { get; set; }
    }
}
