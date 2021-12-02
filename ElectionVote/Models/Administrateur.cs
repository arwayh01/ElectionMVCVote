using System;
using System.Collections.Generic;

#nullable disable

namespace ElectionVote
{
    public partial class Administrateur
    {
        public Administrateur()
        {
            Candidats = new HashSet<Candidat>();
        }

        public int AdministrateurId { get; set; }
        public string NomAdmin { get; set; }
        public string PrenomAdmin { get; set; }
        public string CinAdmin { get; set; }
        public string MotDePasse { get; set; }

        public virtual CentreElection CentreElection { get; set; }
        public virtual ICollection<Candidat> Candidats { get; set; }
    }
}
