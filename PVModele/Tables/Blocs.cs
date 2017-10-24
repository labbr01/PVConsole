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
    [Table(Name = "TBlocs")]
    public class Blocs
    {
        /// <summary>
        /// Identifiant du bloc (entier long incrémentation automatique)
        /// </summary>
        [PrimaryKey, Identity]
        public int IDBlocs { get; set; }

        /// <summary>
        /// Numéro de compétition (numérique)
        /// </summary>
        [Column(Name = "NoCompetition")]
        public int NoCompetition { get; set; }

        /// <summary>
        /// Numéro de bloc numérique
        /// </summary>
        [Column(Name = "No_Bloc")]
        public int No_Bloc { get; set; }

        /// <summary>
        /// Titre du bloc caractère 50
        /// </summary>
        [Column(Name = "Titre")]
        public string Titre { get; set; }
    }
}
