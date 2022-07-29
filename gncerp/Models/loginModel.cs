using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Models
{
    public class loginModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Bu Alan Boş Geçilemez!")]
        public string email { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Bu Alan Boş Geçilemez!")]
        [DataType(DataType.Password)]
        public string password { get; set; }


        [Display(Name = "Beni Hatırla")]
        public bool rememberMe { get; set; }
    }
}
