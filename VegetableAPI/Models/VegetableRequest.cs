using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VegetableAPI.Models
{
    [NotMapped]
    public class VegetableRequest : Vegetable
    {
         public byte[] ImageArray { get; set; }
    }
}