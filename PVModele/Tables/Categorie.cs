using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{
    [Table(Name = "TCategorie")]
    public class Categorie
    {
        /// <summary>
        /// Numéro catégorie entier long
        /// </summary>
        [PrimaryKey, Identity]
        public int NoCategorie { get; set; }

        /// <summary>
        /// Nom de catégorie texte 50
        /// </summary>
        [Column(Name = "Nom")]
        public string Nom { get; set; }

        /// <summary>
        /// Age minimum entier long
        /// </summary>
        [Column(Name = "AgeMin")]
        public int AgeMin { get; set; }

        /// <summary>
        /// Age maximum entier long
        /// </summary>
        [Column(Name = "AgeMax")]
        public int AgeMax { get; set; }

        /// <summary>
        /// Age minimum fille entier long
        /// </summary>
        [Column(Name = "AgeMinF")]
        public int AgeMinF { get; set; }

        /// <summary>
        /// Age maximum fille entier long
        /// </summary>
        [Column(Name = "AgeMaxF")]
        public int AgeMaxF { get; set; }
    }
}
