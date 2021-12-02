using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ElectionVote.Models
{
    public class Vote
    {
        public int VoteId { get; set; }

        public int CandidatId { get; set; }
        public virtual Candidat Candidat { get; set; }
        public int ElecteurId { get; set; }
        public virtual Electeur Electeur { get; set; }
    }

}
