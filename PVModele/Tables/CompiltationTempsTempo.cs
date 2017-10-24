namespace PVModele.Tables
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using LinqToDB.Mapping;
    using LinqToDB;

    /// <summary>
    /// Mapping Table des actions
    /// </summary>
    [Table(Name = "TCompilationTempsTempo")]
    public class CompilationTempsTempo
    {
        /// <summary>
        /// Entier long numérotation auto
        /// </summary>
        [PrimaryKey, Identity]
        public int IDTempo { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        [Column(Name = "Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Ce champs ne sert que si on mixe plusieurs catégories ensemble, pour donner la catégorie de chaque patineur (Texte 255)
        /// </summary>
        [Column(Name = "Categorie")]
        public string Categorie { get; set; }

        /// <summary>
        /// Distance Entier Long
        /// </summary>
        [Column(Name = "Distance")]
        public int Distance { get; set; }

        /// <summary>
        /// Temps réel double
        /// </summary>
        [Column(Name = "Temps")]
        public int Temps { get; set; }

        /// <summary>
        /// Division texte 50
        /// </summary>
        [Column(Name = "Division")]
        public string Division { get; set; }

        /// <summary>
        /// No patineur entier long
        /// </summary>
        [Column(Name = "NoPatineur")]
        public int NoPatineur { get; set; }

        /// <summary>
        /// Lieu texte année
        /// </summary>
        [Column(Name = "Lieu")]
        public string Lieu { get; set; }

        /// <summary>
        /// Année texte 10
        /// </summary>
        [Column(Name = "Annee")]
        public string Annee { get; set; }      

        /// <summary>
        /// Valeur de regroupement des rapports. Elle peut valoir "Sexe" si on veut du non-mixte, ou "" si on veut du mixte. Texte 255
        /// </summary>
        [Column(Name = "RegroupementSexe")]
        public string RegroupementSexe { get; set; }

        /// <summary>
        /// Valeur de regroupement des rapports. Elle peut valoir "NoCategorie" si on veut les catégories sur des pages différentes, ou "" sinon (Texte 255)
        /// </summary>
        [Column(Name = "RegroupementCateg")]
        public string RegroupementCateg { get; set; }

    }
}
