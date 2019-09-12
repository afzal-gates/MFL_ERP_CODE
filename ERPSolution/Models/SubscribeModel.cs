using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPSolution.Models
{
    public class SubscribeModel
    {
        //model specific fields 
        [Required]
        [Display(Name = "How much is the sum")]
        public string Captcha { get; set; } 
    }
}