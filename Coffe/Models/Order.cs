using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coffe.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int Userid { get; set; }
        public int Itemid { get; set; }

        [Range(0,16)]
        public int? Chair { get; set; }
        public int? Price { get; set; }
        public bool? IsDone { get; set; }
    }
}