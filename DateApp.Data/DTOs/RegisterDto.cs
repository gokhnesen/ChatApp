using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateApp.Data.DTOs
{
   public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(9,MinimumLength =4)]
        public string Password { get; set; }
    }
}
