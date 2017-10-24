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
    [Table(Name = "TDivision")]
    public class Division
    {
        /// <summary>
        /// Nom de la division (texte 20)
        /// </summary>
        [Column(Name = "Division")]
        public string Divisionp { get; set; }

        /// <summary>
        /// Champ temporaire OUI ou NON pour sélectionner les divisions d'une compétition (Oui/Non)
        /// </summary>
        [Column(Name = "Selectionne")]
        public bool Selectionne { get; set; }

        /// <summary>
        /// Champ OUI ou NON pour déterminer si la division est sélectionnée dans la compilation des résultats
        /// </summary>
        [Column(Name = "SelectionneCompil")]
        public bool SelectionneCompil { get; set; }
    }
}
