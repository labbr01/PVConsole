using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{
    [Table(Name = "TPatineurs")]
    public class Patineur
    {
        /// <summary>
        /// Numéro du patineur généré par le logiciel (entier long auto)
        /// </summary>
        [PrimaryKey, Identity]
        public int NoPatineur { get; set; }

        /// <summary>
        /// Prénom du patineur (texte 50)
        /// </summary>
        [Column(Name = "Prenom")]
        public string Prenom { get; set; }

        /// <summary>
        /// Nom du patineur (texte 50)
        /// </summary>
        [Column(Name = "Nom")]
        public string Nom { get; set; }

        /// <summary>
        /// Date de naissance du patineur (date)
        /// </summary>
        [Column(Name = "Date de naissance")]
        public DateTime DateNaissance { get; set; }

        /// <summary>
        /// F = Féminin ou M =  Masculin (texte 1)
        /// </summary>
        [Column(Name = "Sexe")]
        public string Sexe { get; set; }

        /// <summary>
        /// Division du patineur (1,2 ou 3) texte 20
        /// </summary>
        [Column(Name = "Division")]
        public string Division { get; set; }

        /// <summary>
        /// Noméro de catégorie (ex: cadet) (voir tableTCategorie) Entier long
        /// </summary>
        [Column(Name = "NoCategorie")]
        public int NoCategorie { get; set; }

        /// <summary>
        /// Numéro du club à qui appartient le patineur (voir table TClubs) entier long
        /// </summary>
        [Column(Name = "NoClub")]
        public int NoClub { get; set; }

        /// <summary>
        /// Classement du patineur pour fin d'élaboration des vagues (classement au 500 pour les essais canadiens) (réel double)
        /// </summary>
        [Column(Name = "Classement")]
        public double Classement { get; set; }

        /// <summary>
        /// OUI si la catégorie est calculée automatiquement en fonction de l'age (boolean)
        /// </summary>
        [Column(Name = "CategCalc")]
        public bool CategCalc { get; set; }

        /// <summary>
        /// code donné à un patineur par l' utilisateur (obligatoire) texte 50
        /// </summary>
        [Column(Name = "CodePat")]
        public string CodePat { get; set; }

        /// <summary>
        /// Classement au 1000 pour les essais canadiens Réel double
        /// </summary>
        [Column(Name = "Classement1000")]
        public double Classement1000 { get; set; }

        /// <summary>
        /// Classement au 1500 pour les essais canadiens Réel double
        /// </summary>
        [Column(Name = "Classement1500")]
        public double Classement1500 { get; set; }

        /// <summary>
        /// Classement général du patineur pour fin d'élaboration des vagues (réel double)
        /// </summary>
        [Column(Name = "ClassementGeneral")]
        public double ClassementGeneral { get; set; }

        /// <summary>
        /// Numéro du casque pour le patineur, (information optionnelle) (réel simple)
        /// </summary>
        [Column(Name = "NoCasque")]
        public short NoCasque { get; set; }

        /// <summary>
        /// Classement de surplus no1 réel double
        /// </summary>
        [Column(Name = "Classement2000")]
        public double Classement2000 { get; set; }

        /// <summary>
        /// Classement de surplus no2 réel double
        /// </summary>
        [Column(Name = "Classement2500")]
        public double Classement2500 { get; set; }

    }
}
