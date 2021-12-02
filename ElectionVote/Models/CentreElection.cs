using System;
using System.Collections.Generic;

#nullable disable

namespace ElectionVote
{
    public partial class CentreElection
    {
        public CentreElection()
        {
            Electeurs = new HashSet<Electeur>();
        }

        public int CentreElectionId { get; set; }
        public string LibelleCentre { get; set; }
        public string AdresseCentre { get; set; }
        public int AdministrateurId { get; set; }

        public virtual Administrateur Administrateur { get; set; }
        public virtual ICollection<Electeur> Electeurs { get; set; }
    }
}
