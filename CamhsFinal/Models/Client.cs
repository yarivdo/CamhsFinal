using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamhsFinal.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string NHI { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public Nullable<int> Age { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Episode> Episodes { get; set; }
    }
}
