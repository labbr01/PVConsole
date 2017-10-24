using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{
     [Table(Name = "TSecteurs")]
    public class Secteurs
    {    
         /// <summary>
         /// Texte 50
         /// </summary>
        [Column(Name = "Secteur")]
        public string Secteur { get; set; }
    }
}
