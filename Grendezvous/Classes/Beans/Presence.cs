using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grendezvous.Classes.Beans
{
    class Presence
    {
        private int id, presences, eleve;
        private DateTime date_;
        private String nom, postnom, prenom, classe, presence, sexe;
        public int Id { get => id; set => id = value; }
        public int Presences { get => presences; set => presences = value; }
        public int Eleve { get => eleve; set => eleve = value; }
        public DateTime Date_ { get => date_; set => date_ = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Postnom { get => postnom; set => postnom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Classe { get => classe; set => classe = value; }
        public string PresenceEl { get => presence; set => presence = value; }
        public string Sexe { get => sexe; set => sexe = value; }
    }
}
