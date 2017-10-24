using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{

    [Table(Name = "TPoints")]
    public class Points
    {
        /// <summary>
        /// texte 20
        /// </summary>
        [Column(Name = "ClePoints")]
        public string ClePoints { get; set; }

        /// <summary>
        /// Texte 50
        /// </summary>
        [Column(Name = "DescriptionP")]
        public string DescriptionP { get; set; }
    }
}
