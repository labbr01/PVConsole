using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{
    [Table(Name = "TVague1")]
    public class Vague1
    {

        /// <summary>
        /// Entier long auto
        /// </summary>
        [PrimaryKey, Identity]
        public int IDVague1 { get; set; }

        /// <summary>
        /// Texte 50
        /// </summary>
        [Column(Name = "NoVague")]
        public string NoVague { get; set; }

        /// <summary>
        /// Texte 50
        /// </summary>
        [Column(Name = "NomPat")]
        public string NomPat { get; set; }

        /// <summary>
        /// Entier
        /// </summary>
        [Column(Name = "NoCasque")]
        public int NoCasque { get; set; }

        /// <summary>
        /// Texte 50
        /// </summary>
        [Column(Name = "NomduClub")]
        public string NomduClub { get; set; }

        /// <summary>
        /// Texte 255
        /// </summary>
        [Column(Name = "Distances")]
        public string Distances { get; set; }

        /// <summary>
        /// texte 6
        /// </summary>
        [Column(Name = "Qual_ou_Fin")]
        public string Qual_ou_Fin { get; set; }

        /// <summary>
        /// Catégorie (texte 50)
        /// </summary>
        [Column(Name = "Nom")]
        public string Nom { get; set; }

        /// <summary>
        /// Texte 50
        /// </summary>
        [Column(Name = "Sexe")]
        public string Sexe { get; set; }

        /// <summary>
        /// Texte 20
        /// </summary>
        [Column(Name = "Division")]
        public string Division { get; set; }

        /// <summary>
        /// Critère pour le choix des patineurs en finale (ex. 1 + 2) (texte 20)
        /// </summary>
        [Column(Name = "CritVagueA")]
        public string CritVagueA { get; set; }

        /// <summary>
        /// Voir RProg_Courses pour valeur (texte50)
        /// </summary>
        [Column(Name = "Categorie_Modif")]
        public string Categorie_Modif { get; set; }

        /// <summary>
        /// Voir RProg_Courses pour valeur texte 50
        /// </summary>
        [Column(Name = "Division_Modif")]
        public string Division_Modif { get; set; }

        /// <summary>
        /// Entier
        /// </summary>
        [Column(Name = "Groupe")]
        public int Groupe { get; set; }

        /// <summary>
        /// codef donné à un patineur par l utilisateur (obligatoire) (texte 50)
        /// </summary>
        [Column(Name = "CodePat")]
        public string CodePat { get; set; }

        /// <summary>
        /// Format de la Compétition (1-->Compétition provinciale, 4-->Sélections nationales) Entier long
        /// </summary>
        [Column(Name = "Format")]
        public int Format { get; set; }
    }
}
