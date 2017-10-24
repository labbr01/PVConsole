using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{  
    [Table(Name = "TProg_Courses1")]
    public class ProgCourses1
    {
        /// <summary>
        ///  Numérotation auto
        /// </summary>
        [PrimaryKey, Identity]
        public int IDTProg_Courses1 { get; set; }

        /// <summary>
        /// Table utilisée pour monter le rapport de l'officiel (entier long)
        /// </summary>
        [Column(Name = "NoCompetition")]
        public int NoCompetition { get; set; }

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
        /// Séquence des courses (entier)
        /// </summary>
        [Column(Name = "Seq")]
        public int Seq { get; set; }

        /// <summary>
        /// Numéro du Bloc auquel appartient la série (entier)
        /// </summary>
        [Column(Name = "NoBloc")]
        public int NoBloc { get; set; }

        /// <summary>
        /// Nom du bloc (texte 50)
        /// </summary>
        [Column(Name = "NomBloc")]
        public string NomBloc { get; set; }

        /// <summary>
        /// Entier long
        /// </summary>
        [Column(Name = "NoDistance")]
        public int NoDistance { get; set; }

        /// <summary>
        /// A, B, C (texte 1)
        /// </summary>
        [Column(Name = "Lettre")]
        public string Lettre { get; set; }

        /// <summary>
        /// Qualif, Finale, Demi (texte 50)
        /// </summary>
        [Column(Name = "Qual_ou_Fin")]
        public string Qual_ou_Fin { get; set; }

        /// <summary>
        /// 0 pour prelim, 1 pour qualif, 2 pour demi, 3 pour finale (entier)
        /// </summary>
        [Column(Name = "OrdreQualFin")]
        public int OrdreQualFin { get; set; }

        /// <summary>
        /// Lors d'une compétition selon le classement, nom du groupe dans lequel le patineur est placé (voir table TGroupes_Compe)) (texte 50)
        /// </summary>
        [Column(Name = "Groupe")]
        public string Groupe { get; set; }

        /// <summary>
        /// Format dans lequel la distance va se courir (1--> Coupe du Québec, 2--> Ca-Nam, 4--> Essais canadiens) (entier long)
        /// </summary>
        [Column(Name = "Format")]
        public int Format { get; set; }

        /// <summary>
        /// Nom de la catégorie ou du Groupe selon le cas (texte 50)
        /// </summary>
        [Column(Name = "Categorie_Modif")]
        public string Categorie_Modif { get; set; }

        /// <summary>
        /// Critère pour le choix des patineurs à l' étape suivante (ex. 1 + 2) (texte 20)
        /// </summary>
        [Column(Name = "CritVagueA")]
        public string CritVagueA { get; set; }
    }
}
