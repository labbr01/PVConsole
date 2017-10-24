using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{
    [Table(Name = "TSommeDePoints")]
    public class SommeDePoints
    {
        /// <summary>
        ///  entier long auto
        /// </summary>
        [PrimaryKey, Identity]
        public int IDTSommeDePoints { get; set; }

        /// <summary>
        /// Texte 50
        /// </summary>
        [Column(Name = "Division_Modif")]
        public string Division_Modif { get; set; }

        /// <summary>
        /// Texte 50
        /// </summary>
        [Column(Name = "Categorie_Modif")]
        public string Categorie_Modif { get; set; }

        /// <summary>
        /// Texte 5
        /// </summary>
        [Column(Name = "Sexe")]
        public string Sexe { get; set; }

        /// <summary>
        /// Entier long
        /// </summary>
        [Column(Name = "NoPatineur")]
        public int NoPatineur { get; set; }

        /// <summary>
        /// Réel simple
        /// </summary>
        [Column(Name = "SommeDePoint")]
        public int SommeDePoint { get; set; }

        /// <summary>
        /// Entier
        /// </summary>
        [Column(Name = "GlobalRang")]
        public int GlobalRang { get; set; }

        /// <summary>
        /// Texte 50
        /// </summary>
        [Column(Name = "Prenom")]
        public string Prenom { get; set; }

        /// <summary>
        /// Texte 50
        /// </summary>
        [Column(Name = "Nom")]
        public string Nom { get; set; }

        /// <summary>
        /// code donné à un patineur par l' utilisateur (obligatoire) (texte 50)
        /// </summary>
        [Column(Name = "CodePat")]
        public string CodePat { get; set; }

        /// <summary>
        /// Texte 50
        /// </summary>
        [Column(Name = "NomDuClub")]
        public string NomDuClub { get; set; }

        /// <summary>
        /// Texte 100
        /// </summary>
        [Column(Name = "TitreDuRapport")]
        public string TitreDuRapport { get; set; }

        /// <summary>
        /// Texte 50
        /// </summary>
        [Column(Name = "Lieu")]
        public string Lieu { get; set; }

        /// <summary>
        /// Texte 50
        /// </summary>
        [Column(Name = "NoLP_CP")]
        public string NoLP_CP { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        [Column(Name = "Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// NoVague texte 50)
        /// </summary>
        [Column(Name = "NoVague")]
        public string NoVague { get; set; }

        /// <summary>
        /// Texte 255
        /// </summary>
        [Column(Name = "Distance")]
        public string Distance { get; set; }

        /// <summary>
        /// texte 5
        /// </summary>
        [Column(Name = "Qual_ou_Fin")]
        public string Qual_ou_Fin { get; set; }

        /// <summary>
        /// Réel double
        /// </summary>
        [Column(Name = "Temps")]
        public double Temps { get; set; }

        /// <summary>
        /// Réel simple
        /// </summary>
        [Column(Name = "Point")]
        public short Point { get; set; }

        /// <summary>
        /// Entier
        /// </summary>
        [Column(Name = "Rang")]
        public int Rang { get; set; }

        /// <summary>
        /// TCommentaire.Abreviation (Texte 50)
        /// </summary>
        [Column(Name = "Abreviation")]
        public string Abreviation { get; set; }

        /// <summary>
        /// Concaténation des deux variables pour séparer les patineurs ayant le même nombre de points (réel double)
        /// </summary>
        [Column(Name = "SommeDePointNoPatineur")]
        public double SommeDePointNoPatineur { get; set; }

        /// <summary>
        /// numéro de la série dans laquelle la vague est située (entier)
        /// </summary>
        [Column(Name = "Seq")]
        public int Seq { get; set; }

        /// <summary>
        /// Catégorie et sexe du patineur (texte 50)
        /// </summary>
        [Column(Name = "CatSexe")]
        public string CatSexe { get; set; }

        /// <summary>
        /// 1 --> Les patineurs sont regroupés par catégories lors d'une compétition;  2 --> les patineurs sont regroupés selon leur classement lors d' une compétition (entier)
        /// </summary>
        [Column(Name = "Regroupe")]
        public int Regroupe { get; set; }

        /// <summary>
        /// Sert à contrôler l'affichage des rapports. Ce champs doit être égal à toutes les rangées et non présent dans toutes les autres colonnes. (texte 255)
        /// </summary>
        [Column(Name = "ControleRapport")]
        public int ControleRapport { get; set; }
    }
}
