using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LG.DLFinance.Models
{
    [Table("Manager", Schema = "DL")]

    public class Manager
    {

    public Guid ManagerId { get; set; }
    [Display(Name = "Manager Name")]
    public String ManagerName { get; set; }

    public decimal Balance { get; set; }

    public bool Active { get; set; }
    public virtual List<Emails> Email { get; set; }

    public Manager()
        {
            ManagerId = Guid.NewGuid();
            Balance = 0;
            Email = new List<Emails>();
            Active = true;
        }

    

    }
}