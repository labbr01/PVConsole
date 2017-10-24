using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{
    [Table(Name = "TVaguesFormatOuvert")]
    public class VaguesFormatOuvert
    {
        /// <summary>
        /// Clé primaire pour les vagues entier long auto)
        /// </summary>
        [PrimaryKey, Identity]
        [Column(Name = "NoTVagues")]
        public int NoTVagues { get; set; }

        /// <summary>
        /// Clé pour le lien avec le programme de course (entier long)
        /// </summary>
        [Column(Name = "NoProgCourses")]
        public int NoProgCourses { get; set; }

        /// <summary>
        /// Numéro des vagues lors d'une compétition (non visible pour l'utilisateur) (texte 50)
        /// </summary>
        [Column(Name = "NomVague")]
        public string NomVague { get; set; }

        /// <summary>
        /// 'Qual' si la distance est une qualif, 'Fin' pour Finale, Demi pour demiFinale, FinA+ ou FinB+ lorsque le patineur fait demi mais pas finale (texte 5)
        /// </summary>
        [Column(Name = "Ronde")]
        public int Ronde { get; set; }      
    }
}
