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
    [Table(Name = "TCategCompil")]
    public class CateCompil
    {
        /// <summary>
        /// Numéro de catégorie entier long
        /// </summary>
        [Column(Name = "NoCateg")]
        public int NoCateg { get; set; }

        /// <summary>
        /// Nom de catégorie texte 255
        /// </summary>
        [Column(Name = "NomCateg")]
        public string NomCateg { get; set; }

        /// <summary>
        /// Age minimum pour la cadégorie entier long
        /// </summary>
        [Column(Name = "AgeMin")]
        public int AgeMin { get; set; }
   }
}
