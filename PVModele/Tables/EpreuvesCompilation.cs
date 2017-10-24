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
    [Table(Name = "TEpreuvesCompilation")]
    public class EpreuvesCompilation
    {
        /// <summary>
        /// Nom de l'épreuve à considérer pour la compilation (texte 255)
        /// </summary>
        [Column(Name = "Epreuve")]
        public string Epreuve { get; set; }
    }
}
