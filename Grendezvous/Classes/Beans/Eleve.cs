using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grendezvous.Classes.Beans
{
    class Eleve
    {
        private int id;
        private string nom, postnom, prenom,sexe, classe, dateInscri;
        private Byte[] profil, empreinteImage;
        private Byte[] empreinte = new byte[400];

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Postnom { get => postnom; set => postnom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Sexe { get => sexe; set => sexe = value; }
        public Byte[] Profil { get => profil; set => profil = value; }
        public string Classe { get => classe; set => classe = value; }
        public string DateInscri { get => dateInscri; set => dateInscri = value; }
        public byte[] Empreinte { get => empreinte; set => empreinte = value; }
        public byte[] EmpreinteImage { get => empreinteImage; set => empreinteImage = value; }
    }
}
