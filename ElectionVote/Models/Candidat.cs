using System;
using System.Collections.Generic;

#nullable disable

namespace ElectionVote
{
    public partial class Candidat
    {
        public Candidat()
        {
            Electeurs = new HashSet<Electeur>();
        }

        public int CandidatId { get; set; }
        public string NomCandidat { get; set; }
        public string PrenomCandidat { get; set; }
        public string CinCandidat { get; set; }
        public string ImageCandidat { get; set; }
        public int? AdministrateurId { get; set; }

        public virtual Administrateur Administrateur { get; set; }
        public virtual ICollection<Electeur> Electeurs { get; set; }
    }
}
