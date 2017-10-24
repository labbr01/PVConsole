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
    [Table(Name = "TCommentaire")]
    public class Commentaire
    {
        /// <summary>
        /// Code entier long incrément
        /// </summary>
        [PrimaryKey, Identity]
        public int Code { get; set; }

        /// <summary>
        /// Description texte 150
        /// </summary>
        [Column(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Abréviation texte 50
        /// </summary>
        [Column(Name = "Abreviation")]
        public string Abreviation { get; set; }

        /// <summary>
        /// Code action texte 10
        /// </summary>
        [Column(Name = "CodeAction")]
        public string CodeAction { get; set; }
    }
}
