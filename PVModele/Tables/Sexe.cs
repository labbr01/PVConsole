using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{
    [Table(Name = "TSexe")]
    public class Sexe
    {
        /// <summary>
        /// Texte 1
        /// </summary>
        [Column(Name = "SEXE")]
        public string SEXE { get; set; }
    }
}
