using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{
    [Table(Name = "TRegions")]
    public class Regions
    {

        /// <summary>
        /// Entier long numérotation auto
        /// </summary>
        [PrimaryKey, Identity]
        public int NoRegion { get; set; }

        /// <summary>
        /// Texte 10
        /// </summary>
        [Column(Name = "Region")]
        public string Region { get; set; }

        /// <summary>
        /// Texte 50
        /// </summary>
        [Column(Name = "Secteur")]
        public string Secteur { get; set; }
    }
}
