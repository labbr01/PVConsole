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
    [Table(Name = "TAction")]
    public class Action
    {
        /// <summary>
        /// Code caractère (10)
        /// </summary>
        [PrimaryKey, Identity]
        public string Code { get; set; }

        /// <summary>
        /// Action caractère (55)
        /// </summary>
        [Column(Name = "Action")]
        public string Action_F { get; set; }  

        /// <summary>
        /// Action en anglais caractère (55)
        /// </summary>
        [Column(Name = "Action_A")]
        public string Action_A { get; set; }
    }
}
