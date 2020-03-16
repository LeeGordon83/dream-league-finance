using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LG.DLFinance.Models
{
    [Table("Prizes", Schema = "DL")]
    public class Prizes
    {
        [Key]
        public Guid PrizesId { get; set; }
        [Display(Name = "Type")]
        public string PrizeType { get; set; }
        [Display(Name = "Amount")]
        public decimal PrizeAmount { get; set; }
        [Display(Name = "League Prize")]
        public bool LeaguePrize { get; set; }

        public bool CupPrize { get; set; }

        public Prizes()
        {
            PrizesId = Guid.NewGuid();
        }
    }
}