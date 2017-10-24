using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{    
    [Table(Name = "TPointageEssaisCISU")]
    public class PointageEssaisCISU
    {
        /// <summary>
        /// Entier lont numéro auto
        /// </summary>
        [PrimaryKey, Identity]
        public int NoPoints { get; set; }

        /// <summary>
        /// Rang du patineur en finale (entier)
        /// </summary>
        [Column(Name = "Rang")]
        public int Rang { get; set; }

        /// <summary>
        /// Nombre de points accordés pour le rang (réel simple)
        /// </summary>
        [Column(Name = "Points")]
        public short Points { get; set; }

        /// <summary>
        /// LP pour Longue piste ouCP pour Courte piste (texte 2)
        /// </summary>
        [Column(Name = "LP_ou_CP")]
        public string LP_ou_CP { get; set; }
    }
}
