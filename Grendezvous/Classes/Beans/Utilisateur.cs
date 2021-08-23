using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grendezvous.Classes.Beans
{
    class Utilisateur
    {
        private int id;
        private string nom, username, password;
        private Byte[] profil;

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public Byte[] Profil { get => profil; set => profil = value; }
    }
    }
