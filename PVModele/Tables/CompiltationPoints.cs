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
    [Table(Name = "TCompilationPoints")]
    public class CompilationPoints
    {
        /// <summary>
        /// Id Entiel long auto incrément
        /// </summary>
        [PrimaryKey, Identity]
        public int IDCompilPoints { get; set; }

        /// <summary>
        /// Rang du patineur (entier)
        /// </summary>
        [Column(Name = "Rang")]
        public int Rang { get; set; }

        /// <summary>
        /// Numéro de patineur entier long
        /// </summary>
        [Column(Name = "NoPatineur")]
        public int NoPatineur { get; set; }

        /// <summary>
        /// Ce champs ne sert que si on mixe plusieurs catégories ensemble, pour donner la catégorie de chaque patineur texte 255
        /// </summary>
        [Column(Name = "Categorie")]
        public string Categorie { get; set; }

        /// <summary>
        /// Numero de la competition (texte 50)
        /// </summary>
        [Column(Name = "Compe1")]
        public string Compe1 { get; set; }

        /// <summary>
        /// Points accumulés par le patineur a la compe1 (entier long)
        /// </summary>
        [Column(Name = "Points1")]
        public int Points1 { get; set; }

        /// <summary>
        /// Pondération à appliquer aux résultats de cette compétition Réel simple
        /// </summary>
        [Column(Name = "ponderation1")]
        public int ponderation1 { get; set; }

        [Column(Name = "Compe2")]
        public string Compe2 { get; set; }

        [Column(Name = "Points2")]
        public int Points2 { get; set; }

        [Column(Name = "ponderation2")]
        public int ponderation2 { get; set; }

        [Column(Name = "Compe3")]
        public string Compe3 { get; set; }

        [Column(Name = "Points3")]
        public int Points3 { get; set; }

        [Column(Name = "ponderation3")]
        public int ponderation3 { get; set; }

        [Column(Name = "Compe4")]
        public string Compe4 { get; set; }

        [Column(Name = "Points4")]
        public int Points4 { get; set; }

        [Column(Name = "ponderation4")]
        public int ponderation4 { get; set; }

        [Column(Name = "Compe5")]
        public string Compe5 { get; set; }

        [Column(Name = "Points5")]
        public int Points5 { get; set; }

        [Column(Name = "ponderation5")]
        public int ponderation5 { get; set; }

        [Column(Name = "Compe6")]
        public string Compe6 { get; set; }

        [Column(Name = "Points6")]
        public int Points6 { get; set; }

        [Column(Name = "ponderation6")]
        public int ponderation6 { get; set; }

        [Column(Name = "Compe7")]
        public string Compe7 { get; set; }

        [Column(Name = "Points7")]
        public int Points7 { get; set; }

        [Column(Name = "ponderation7")]
        public int ponderation7 { get; set; }

        [Column(Name = "Compe8")]
        public string Compe8 { get; set; }

        [Column(Name = "Points8")]
        public int Points8 { get; set; }

        [Column(Name = "ponderation8")]
        public int ponderation8 { get; set; }

        [Column(Name = "Compe9")]
        public string Compe9 { get; set; }

        [Column(Name = "Points9")]
        public int Points9 { get; set; }

        [Column(Name = "ponderation9")]
        public int ponderation9 { get; set; }

        /// <summary>
        /// Retrait des points pour les X pires competitions (réel simple)
        /// </summary>
        [Column(Name = "Retrait")]
        public int Retrait { get; set; }

        /// <summary>
        /// Total des points a considerer pour la compilation (réel simple)
        /// </summary>
        [Column(Name = "Total")]
        public int Total { get; set; }

        /// <summary>
        /// Valeur de regroupement des rapports. Elle peut valoir "Sexe" si on veut du non-mixte, ou "" si on veut du mixte. (texte 255)
        /// </summary>
        [Column(Name = "RegroupementSexe")]
        public string RegroupementSexe { get; set; }

        /// <summary>
        /// Valeur de regroupement des rapports. Elle peut valoir "NoCategorie" si on veut les catégories sur des pages différentes, ou "" sinon (texte 255)
        /// </summary>
        [Column(Name = "RegroupementCateg")]
        public string RegroupementCateg { get; set; }

    }
}
