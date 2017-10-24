using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;
using LinqToDB;

namespace PVModele.Tables
{
    [Table(Name = "TClubs")]
    public class Club
    {
        /// <summary>
        /// Numéro du club entier long incrément
        /// </summary>
        [PrimaryKey, Identity]
        public int NoClub { get; set; }

        /// <summary>
        /// Nom du club texte 50
        /// </summary>
        [Column(Name = "Nom du Club")]
        public string NomClub { get; set; }

        /// <summary>
        /// Adresse du club texte 50
        /// </summary>
        [Column(Name = "Adresse")]
        public string Adresse { get; set; }

        /// <summary>
        /// Nom du responsable texte 50
        /// </summary>
        [Column(Name = "Responsable")]
        public string Responsable { get; set; }

        /// <summary>
        /// Numéro de téléphone (texte 255)
        /// </summary>
        [Column(Name = "Tel")]
        public string Tel { get; set; }

        /// <summary>
        /// Commentaire (texte 100)
        /// </summary>
        [Column(Name = "Commentaire")]
        public string Commentaire { get; set; }

        /// <summary>
        /// Numéro de région (entier long)
        /// </summary>
        [Column(Name = "NoRegion")]
        public int NoRegion { get; set; }

        /// <summary>
        /// Abrévéviation pour le club (texte 50)
        /// </summary>
        [Column(Name = "Abreviation")]
        public string Abreviation { get; set; }

        /// <summary>
        /// Sélection booléen
        /// </summary>
        [Column(Name = "Selection")]
        public bool Selection { get; set; }
    }

    //public class DBPatinVitesse : LinqToDB.Data.DataConnection
    //{
    //    public DBPatinVitesse() : base("PatinVitesse") { }

    //    public ITable<Patineur> Patineur { get { return GetTable<Patineur>(); } }
    //    public ITable<Club> Club { get { return GetTable<Club>(); } }
    //    public ITable<Categorie> Categorie { get { return GetTable<Categorie>(); } }
    //    public ITable<PatineurCompe> PatineurCompe { get { return GetTable<PatineurCompe>(); } }

    //    // ... other tables ...
    //}
}



