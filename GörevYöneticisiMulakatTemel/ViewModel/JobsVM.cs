using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GörevYöneticisiMulakatTemel.ViewModel
{
    public class JobsVM
    {
        public int JobId { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [StringLength(30, ErrorMessage = "{0} alanı en az 2 karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [Display(Name = "İş Tanımı")]
        public string JobComment { get; set; }

        [Display(Name = "İş Tipi")]
        public string JobType { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [Display(Name = "İş Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime JobDate { get; set; }

        public List<SelectListItem> getTypeList { get; set; }
        public List<SelectListItem> getAllList()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="Günlük",Text="Günlük"},
                 new SelectListItem{ Value="Haftalık",Text="Haftalık"},
                 new SelectListItem{ Value="Aylık",Text="Aylık"},
            };
            myList = data.ToList();
            return myList;
        }
    }
}