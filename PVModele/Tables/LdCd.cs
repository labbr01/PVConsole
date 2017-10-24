using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{
   
    [Table(Name = "TLD_CD")]
    public class LdCd
    {       
        /// <summary>
        /// LD ou CD (texte 2)
        /// </summary>
        [Column(Name = "NoLD_CD")]
        public string NoLD_CD { get; set; }

        /// <summary>
        /// Description du numéro (texte 50)
        /// </summary>
        [Column(Name = "NomLD_CD")]
        public string NomLD_CD { get; set; }
    }
}
