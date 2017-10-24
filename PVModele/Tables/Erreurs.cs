namespace PVModele.Tables
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using LinqToDB.Mapping;
    using LinqToDB;
    using System.Data.OleDb;

    /// <summary>
    /// Mapping Table des erreurs
    /// </summary>
    [Table(Name = "Table des erreurs")]
    public class Erreurs
    {
        [PrimaryKey, Identity]
        public int NoErreur { get; set; }

        [Column(Name = "Champ0")]
        public string Champ0 { get; set; }


    }
}
