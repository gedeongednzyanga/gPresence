using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grendezvous.Classes.Beans
{
    class Classe
    {
        private int id;
        private String designation, section, option;

        public int Id { get => id; set => id = value; }
        public String Section { get => section; set => section = value; }
        public String Option { get => option; set => option = value; }
        public string Designation { get => designation; set => designation = value; }
    }
}
