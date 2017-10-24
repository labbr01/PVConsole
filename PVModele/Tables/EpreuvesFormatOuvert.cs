using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{
    [Table(Name = "TEpreuvesFormatOuvert")]
    public class EpreuvesFormatOuvert
    {
        /// <summary>
        /// LD ou CD (texte 2)
        /// </summary>
        [PrimaryKey, Identity]
        [Column(Name = "NoEpreuve")]
        public int NoEpreuve { get; set; }

        /// <summary>
        /// LD ou CD (texte 2)
        /// </summary>
        [Column(Name = "NoCompetition")]
        public int NoCompetition { get; set; }

        /// <summary>
        /// Description du numéro (texte 50)
        /// </summary>
        [Column(Name = "NomEpreuve")]
        public string NomEpreuve { get; set; }

        /// <summary>
        /// LD ou CD (texte 2)
        /// </summary>
        [Column(Name = "NombreDeBlocs")]
        public int NombreDeBlocs { get; set; }
    }
}
