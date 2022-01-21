using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Coffe.Models
{
    public class Items
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        public string ItemDescription { get; set; }


        //[Required]
        public int? price { get; set; }


        //[Required]
        public String images { get; set; }


        //[Required]
        public bool? availability { get; set; }


        public int? sales { get; set; }



        public int? age { get; set; }


        public bool? isonSale { get; set; }

        
        public int? newPrice { get; set; }
    }

    
}
