using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CamhsFinal.Models
{
    public class Episode
    {
        public int EpisodeID { get; set; }
        public float Grade { get; set; }
        public string Outcome { get; set; }
        public string Comments { get; set; }
        public string Keyworker { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime MdtDate { get; set; }

        [DefaultValue(true)]
        public bool isOpen { get; set; }


        public int ClientID { get; set; } // Foreign key
        public int ReferralID { get; set; } // Foreign key  
        public Client Client { get; set; }
        public Referral Referral { get; set; }
    }
}
