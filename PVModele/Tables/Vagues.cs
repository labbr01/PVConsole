using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{
    [Table(Name = "TVagues")]
    public class Vagues
    {
        /// <summary>
        /// Clé primaire pour les vagues entier long auto)
        /// </summary>
        [PrimaryKey, Identity]
        public int CleTVagues { get; set; }

/// <summary>
        /// Clé pour le lien avec le programme de course (entier long)
/// </summary>
        [Column(Name = "CleDistancesCompe")]
        public int CleDistancesCompe { get; set; }

        /// <summary>
        /// Numéro des vagues lors d'une compétition (non visible pour l'utilisateur) (texte 50)
        /// </summary>
        [Column(Name = "NoVague")]
        public string NoVague { get; set; }

        /// <summary>
        /// 'Qual' si la distance est une qualif, 'Fin' pour Finale, Demi pour demiFinale, FinA+ ou FinB+ lorsque le patineur fait demi mais pas finale (texte 5)
        /// </summary>
        [Column(Name = "Qual_ou_Fin")]
        public string Qual_ou_Fin { get; set; }

        /// <summary>
        /// Lettre donné à une vague pour chaque catégorie (texte 3) A par défaut
        /// </summary>
        [Column(Name = "Lettre")]
        public string Lettre { get; set; }

        /// <summary>
        /// numéro de la série dans laquelle la vague est située (entier)
        /// </summary>
        [Column(Name = "Seq")]
        public int Seq { get; set; } 
    }
}
