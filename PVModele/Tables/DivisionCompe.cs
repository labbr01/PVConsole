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
    [Table(Name = "TDivisionCompe")]
    public class DivisionCompe
    {
        /// <summary>
        /// Entier long numérotation automatique
        /// </summary>
        [PrimaryKey, Identity]
        public int IDDivCompe { get; set; }

        /// <summary>
        /// No de la compétition Entier
        /// </summary>
        [Column(Name = "NoCompetition")]
        public int NoCompetition { get; set; }

        /// <summary>
        /// Divisions pouvant s'inscrire à la compétition (texte 20)
        /// </summary>
        [Column(Name = "Division")]
        public string Division { get; set; }

    }
}
