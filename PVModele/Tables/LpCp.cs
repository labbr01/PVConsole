using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{
    
    [Table(Name = "TLP_CP")]
    public class LpCp
    {      
        /// <summary>
        /// LP ou CP (texte 2)
        /// </summary>
        [Column(Name = "NoLP_CP")]
        public string NoLP_CP { get; set; }

        /// <summary>
        /// Description du code (texte 50)
        /// </summary>
        [Column(Name = "NomLP_CP")]
        public string NomLP_CP { get; set; }

        /// <summary>
        /// Description du code en anglais (texte 50)
        /// </summary>
        [Column(Name = "NomLP_CPA")]
        public string NomLP_CPA { get; set; }
    }
}
