using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Coffe.Models
{
    public class User
    {
        [Key]
        public int Uid { get; set; }

        [Required]
        public string Username { get; set; }


        [DataType(DataType.Password)]
        [Required]
        [StringLength(15,MinimumLength =8, ErrorMessage = "Password must be between 8 - 15 charackters.")]
        public string Password { get; set; }



        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
            ErrorMessage = "Please provide a valid Email.")]
        public string Email { get; set; }


        public bool isBarista { get; set; }
        public bool isAdmin { get; set; }
    }
}