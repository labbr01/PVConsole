using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;



namespace PVModele.Tables
{
    [Table(Name = "TSeqCoursesFormatOuvert")]
    public class SeqCoursesFormatOuvert
    {
        /// <summary>
        /// Clé pour le programme de course (entier long)
        /// </summary>
        [PrimaryKey, Identity]
        [Column(Name = "NoSeqCoursesFormatOuvert")]
        public int NoSeqCoursesFormatOuvert { get; set; }

        /// <summary>
        /// Numéro de la compétition (voir table TCompetition) (entier long)
        /// </summary>
        [Column(Name = "Jour")]
        public int Jour { get; set; }


        /// <summary>
        /// Numéro de la compétition (voir table TCompetition) (entier long)
        /// </summary>
        [Column(Name = "NoEpreuve")]
        public int NoEpreuve { get; set; }

        /// <summary>
        /// Mode ModeElite
        /// </summary>
        [Column(Name = "IsDistanceCourue")]
        public bool IsDistanceCourue { get; set; }
        
        /// <summary>
        /// Numéro de la compétition (voir table TCompetition) (entier long)
        /// </summary>
        [Column(Name = "DernierBlocACourir")]
        public int DernierBlocACourir { get; set; }
    }
}
