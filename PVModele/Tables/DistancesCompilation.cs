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
    [Table(Name = "TDistancesCompilation")]
    public class DistancesCompilations
    {     
        /// <summary>
        /// Distance a considerer pour la compilation (Entier long)
        /// </summary>
        [Column(Name = "Distance")]
        public int Distance { get; set; }

        /// <summary>
        /// Pour la distance selectionnee, x meilleurs temps a considerer (entier)
        /// </summary>
        [Column(Name = "XMeilleursTemps")]
        public int XMeilleursTemps { get; set; }
    }
}
