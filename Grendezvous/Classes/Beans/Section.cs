using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grendezvous.Classes.Beans
{
    class Section
    {
        private int id;
        private String designation;
        private String short_design;
        public string Designation { get => designation; set => designation = value; }
        public int Id { get => id; set => id = value; }
        public string Short_design { get => short_design; set => short_design = value; }
    }
}
