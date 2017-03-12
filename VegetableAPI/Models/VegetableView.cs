using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VegetableAPI.Models
{
    public class VegetableView
    {
        public int VegetableId { get; set; } 
        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(50, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 1)]

        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "You must enter a {0}")]
        public decimal Price { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Last purchase")]
        public DateTime? LastPurchase { get; set; }
        public string Image { get; set; }

        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }
        [Display(Name = "Is active")]
        public bool IsActive { get; set; }
        [DataType(DataType.MultilineText)]
        public string Observation { get; set; }


    }
}