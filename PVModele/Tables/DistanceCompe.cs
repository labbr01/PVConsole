namespace PVModele.Tables
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using LinqToDB.Mapping;
    using LinqToDB;

    /// <summary>
    /// Mapping Table des actions
    /// </summary>
    [Table(Name = "TDistances_Compe")]
    public class DistanceCompe
    {
        /// <summary>
        /// Entier long auto incrément
        /// </summary>
        [PrimaryKey, Identity]
        public int IDDistancesCompe { get; set; }

        /// <summary>
        /// Entier long
        /// </summary>
        [Column(Name = "CleDistancesCompe")]
        public int CleDistancesCompe { get; set; }

        /// <summary>
        /// Entier long
        /// </summary>
        [Column(Name = "NoDistance")]
        public int NoDistance { get; set; }       
    }
}
