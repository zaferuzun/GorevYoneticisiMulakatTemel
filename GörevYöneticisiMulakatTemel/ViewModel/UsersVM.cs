using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GörevYöneticisiMulakatTemel.ViewModel
{
    public class UsersVM
    {
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [StringLength(30, ErrorMessage = "{0} alanı en az 2 karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [StringLength(30, ErrorMessage = "{0} alanı en az 2 karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [Display(Name = "Parola")]
        public string UserPassword { get; set; }
        public int UserId { get; set; }

    }
}