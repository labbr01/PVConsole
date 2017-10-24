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
    [Table(Name = "TCompilationTemps")]
    public class CompilationTemps
    {
        /// <summary>
        /// Entien long
        /// </summary>
        [PrimaryKey, Identity]
        public int CleCompilTemps { get; set; }

        /// <summary>
        /// Rang du patineur Entier
        /// </summary>
        [Column(Name = "Rang")]
        public int Rang { get; set; }

        /// <summary>
        /// Numéro de patineur entier long
        /// </summary>
        [Column(Name = "NoPatineur")]
        public int NoPatineur { get; set; }

        /// <summary>
        /// Première distance sélectionnée par l'utilisateur (entier)
        /// </summary>
        [Column(Name = "Distance1")]
        public int Distance1 { get; set; }

        /// <summary>
        /// Meilleur temps pour la distance Distance1 réel simple
        /// </summary>
        [Column(Name = "Temps1")]
        public int Temps1 { get; set; }

        /// <summary>
        /// Endroit de la competition pendant laquelle le meilleur temps à été enregistré texte
        /// </summary>
        [Column(Name = "Compe1")]
        public string Compe1 { get; set; }

        [Column(Name = "Distance2")]
        public int Distance2 { get; set; }

        [Column(Name = "Temps2")]
        public int Temps2 { get; set; }

        [Column(Name = "Compe2")]
        public string Compe2 { get; set; }

        [Column(Name = "Distance3")]
        public int Distance3 { get; set; }

        [Column(Name = "Temps3")]
        public int Temps3 { get; set; }

        [Column(Name = "Compe3")]
        public string Compe3 { get; set; }

        [Column(Name = "Distance4")]
        public int Distance4 { get; set; }

        [Column(Name = "Temps4")]
        public int Temps4 { get; set; }

        [Column(Name = "Compe4")]
        public string Compe4 { get; set; }

        [Column(Name = "Distance5")]
        public int Distance5 { get; set; }

        [Column(Name = "Temps5")]
        public int Temps5 { get; set; }

        [Column(Name = "Compe5")]
        public string Compe5 { get; set; }

        [Column(Name = "Distance6")]
        public int Distance6 { get; set; }

        [Column(Name = "Temps6")]
        public int Temps6 { get; set; }

        [Column(Name = "Compe6")]
        public string Compe6 { get; set; }

        [Column(Name = "Distance7")]
        public int Distance7 { get; set; }

        [Column(Name = "Temps7")]
        public int Temps7 { get; set; }

        [Column(Name = "Compe7")]
        public string Compe7 { get; set; }

        [Column(Name = "Distance8")]
        public int Distance8 { get; set; }

        [Column(Name = "Temps8")]
        public int Temps8 { get; set; }

        [Column(Name = "Compe8")]
        public string Compe8 { get; set; }

        /// <summary>
        /// Total des temps a considerer pour la compilation (réel simple)
        /// </summary>
        [Column(Name = "Total")]
        public int Total { get; set; }

        /// <summary>
        /// 1 = première année de la catégorie, 2 = deuxième année de la catégorie texte 10
        /// </summary>
        [Column(Name = "Annee")]
        public string Annee { get; set; }

        /// <summary>
        /// Valeur de regroupement des rapports. Elle peut valoir "Sexe" si on veut du non-mixte, ou "" si on veut du mixte. texte 255
        /// </summary>
        [Column(Name = "RegroupementSexe")]
        public string RegroupementSexe { get; set; }

        /// <summary>
        /// Valeur de regroupement des rapports. Elle peut valoir "NoCategorie" si on veut les catégories sur des pages différentes, ou "" sinon Texte 255
        /// </summary>
        [Column(Name = "RegroupementCateg")]
        public string RegroupementCateg { get; set; }

    }
}
