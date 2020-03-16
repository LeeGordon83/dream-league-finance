using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LG.DLFinance.Models
{
    [Table("Transaction", Schema = "DL")]
    public class Transaction
    {

        public Guid TransactionId { get; set; }

        [Display(Name = "Value")]
        public decimal Value { get; set; }

        [Display(Name = "Transaction Type")]
        public string TransactionType { get; set; }

        [Display(Name = "Transaction Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TransactionDate {get; set;}
        public string Notes { get; set; }

        public Guid ManagerId { get; set; }
        [ForeignKey("Week")]
        public int WeekId { get; set; }

        public virtual Week Week { get; set; }

        public virtual Manager Manager { get; set; }

        public Transaction()
        {
            TransactionId = Guid.NewGuid();
        }

    }
}