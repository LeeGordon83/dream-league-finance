using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LG.DLFinance.Models
{
    [Table("Jackpot", Schema = "DL")]

    public class Jackpot
    {
        public int JackpotId {get; set;}
        [Display(Name = "Jackpot Start Week")]
        public int JackpotStartWk { get; set; }
        [Display(Name = "Jackpot End Week")]
        public int JackpotEndWk { get; set; }
        [Display(Name = "Jackpot Amount")]
        public int JackpotValue { get; set; }
        [Display(Name = "Jackpot Active?")]
        public bool Active { get; set; }

        public Jackpot()
        {

            JackpotValue = 0;
            Active = true;
        }
    }
}