using System;
using System.Collections.Generic;

#nullable disable

namespace ElectionVote
{
    public partial class Electeur
    {
        public int ElecteurId { get; set; }
        public string NomElecteur { get; set; }
        public string PrenomElecteur { get; set; }
        public string CinElecteur { get; set; }
        public string GenreElecteur { get; set; }
        public int? CentreElectionId { get; set; }
        public int? CondidatcandidatId { get; set; }

        public virtual CentreElection CentreElection { get; set; }
        public virtual Candidat Condidatcandidat { get; set; }
    }
}
