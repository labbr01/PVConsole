using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;



namespace PVModele.Tables
{
    [Table(Name = "TProgCoursesFormatOuvert")]
    public class ProgCoursesFormatOuvert
    {
        /// <summary>
        /// Clé pour le programme de course (entier long)
        /// </summary>
        [PrimaryKey, Identity]
        [Column(Name = "NoProgCourses")]
        public int NoProgCourses { get; set; }

        /// <summary>
        /// Numéro de la compétition (voir table TCompetition) (entier long)
        /// </summary>
        [Column(Name = "NoSeqCourses")]
        public int NoSeqCourses { get; set; }

        /// <summary>
        /// Nombre de vagues en qualification (entier)
        /// </summary>
        [Column(Name = "Bloc")]
        public string Bloc { get; set; }

        /// <summary>
        /// Numéro de la compétition (voir table TCompetition) (entier long)
        /// </summary>
        [Column(Name = "SeqRd1")]
        public int SeqRd1 { get; set; }

        /// <summary>
        /// Numéro de la compétition (voir table TCompetition) (entier long)
        /// </summary>
        [Column(Name = "CritRd1")]
        public int CritRd1 { get; set; }

        /// <summary>
        /// Nombre de vagues en qualification (entier)
        /// </summary>
        [Column(Name = "VaguesRd1")]
        public string VaguesRd1 { get; set; }

        /// <summary>
        /// Numéro de la compétition (voir table TCompetition) (entier long)
        /// </summary>
        [Column(Name = "SeqRd2")]
        public int SeqRd2 { get; set; }

        /// <summary>
        /// Numéro de la compétition (voir table TCompetition) (entier long)
        /// </summary>
        [Column(Name = "CritRd2")]
        public int CritRd2 { get; set; }

        /// <summary>
        /// Nombre de vagues en qualification (entier)
        /// </summary>
        [Column(Name = "VaguesRd2")]
        public string VaguesRd2 { get; set; }

        /// <summary>
        /// Numéro de la compétition (voir table TCompetition) (entier long)
        /// </summary>
        [Column(Name = "SeqRd3")]
        public int SeqRd3 { get; set; }

        /// <summary>
        /// Numéro de la compétition (voir table TCompetition) (entier long)
        /// </summary>
        [Column(Name = "CritRd3")]
        public int CritRd3 { get; set; }

        /// <summary>
        /// Nombre de vagues en qualification (entier)
        /// </summary>
        [Column(Name = "VaguesRd3")]
        public string VaguesRd3 { get; set; }

                /// <summary>
        /// Sexe des patineurs F = Féminin ou M = Masculin) (texte 5)
        /// </summary>
        [Column(Name = "Sexe")]
        public string Sexe { get; set; }

        /// <summary>
        /// Nombre de patineurs inscrits dans la catégorie (entier)
        /// </summary>
        [Column(Name = "NbPatineurs")]
        public int NbPatineurs { get; set; }

        /// <summary>
        /// Sexe des patineurs F = Féminin ou M = Masculin) (texte 5)
        /// </summary>
        [Column(Name = "Groupe")]
        public string Groupe { get; set; }

        /// <summary>
        /// Numéro de la compétition (voir table TCompetition) (entier long)
        /// </summary>
        [Column(Name = "OrdreSequence")]
        public int OrdreSequence { get; set; }
    }
}
