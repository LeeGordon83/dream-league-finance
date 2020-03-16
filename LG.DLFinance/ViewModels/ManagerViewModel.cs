using LG.DLFinance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LG.DLFinance.ViewModels
{
    public class ManagerViewModel
    {

        public Guid ManagerId { get; set; }

        [Display(Name = "Manager Name")]
        [Required]
        public string ManagerName { get; set; }

        [Display(Name = "Primary Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string PrimaryEmail { get; set; }

        [Display(Name = "Secondary Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string SecondaryEmail { get; set; }

    }
}