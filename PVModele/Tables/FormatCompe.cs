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
    [Table(Name = "TFormat_Compe")]
    public class FormatCompe
    {
        /// <summary>
        /// Numéro du format de compétition (Entier long)
        /// </summary>
        [Column(Name = "NoFormat")]
        public int NoFormat { get; set; }

        /// <summary>
        /// Description du format de la compétition (1--> Compétition provinciale, 4 --> Essais Canadiens) (texte 75)
        /// </summary>
        [Column(Name = "DescrFormat")]
        public string DescrFormat { get; set; }

        /// <summary>
        /// Description du format de la compétition en anglais (texte 75)
        /// </summary>
        [Column(Name = "DescrFormatA")]
        public string DescrFormatA { get; set; }
    }
}
