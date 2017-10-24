using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{    
    [Table(Name = "TListePoints")]
    public class ListePoints
    {
        /// <summary>
        /// Entier numérotation automatique
        /// </summary>
        [PrimaryKey, Identity]
        public int IDListePoints { get; set; }

        /// <summary>
        /// Réel simple
        /// </summary>
        [Column(Name = "Points")]
        public Single Points { get; set; }

        /// <summary>
        /// Entier
        /// </summary>
        [Column(Name = "Rang")]
        public int Rang { get; set; }

        /// <summary>
        /// Texte 20
        /// </summary>
        [Column(Name = "ClePoints")]
        public string ClePoints { get; set; }
    }
}
