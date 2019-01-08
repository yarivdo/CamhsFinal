using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CamhsFinal.Models
{
    public class Referral
    {
        public int ReferralID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        public string Content { get; set; }


        public ICollection<Episode> Episodes { get; set; }
    }
}
