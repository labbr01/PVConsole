using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;
using LinqToDB;

namespace PVModele.Tables
{
    [Table(Name = "TPatineur_compe")]
    public class PatineurCompe
    {
         [LinqToDB.Mapping.NotColumn]
        public static DBPatinVitesse DB { get; set; }

        /// <summary>
         /// Clé pour le patineur (Entier long)
        /// </summary>
        [PrimaryKey, Identity]
        public int NoPatCompe { get; set; }

        /// <summary>
        /// Numéro de la competition (voir table TCompetition) (entier long)
        /// </summary>
        [Column(Name = "NoCompetition")]
        public int NoCompetition { get; set; }

        /// <summary>
        /// Numéro du patineur (voir table TPatineurs) (entier long)
        /// </summary>
        [Column(Name = "NoPatineur")]
        public int NoPatineur { get; set; }

        /// <summary>
        /// Division du patineur au moment de la compétition  (texte 20)
        /// </summary>
        [Column(Name = "Division")]
        public string Division { get; set; }

        /// <summary>
        /// Catégorie du patineur au moment de la compétition (entier long)
        /// </summary>
        [Column(Name = "NoCategorie")]
        public int NoCategorie { get; set; }

        /// <summary>
        /// Club du patineur au moment de la compétition (entier long)
        /// </summary>
        [Column(Name = "NoClub")]
        public int NoClub { get; set; }

        /// <summary>
        /// Entier
        /// </summary>
        [Column(Name = "Rang")]
        public int Rang { get; set; }

        /// <summary>
        /// Si l'on doit retirer le patineur pour la balance de la compétition (oui/non)
        /// </summary>
        [Column(Name = "Retirer")]
        public bool Retirer { get; set; }

        /// <summary>
        /// lorsque les patineurs sont regroupés dans les courses en fonction de leur classement,  groupe dans lequel le patineurest inscrit (texte 50)
        /// </summary>
        [Column(Name = "Groupe")]
        public string Groupe { get; set; }

        /// <summary>
        /// détermine si le patineur sera regroupé selon : 1 --> sa catégorie ou bien 2 --> son classement (entier
        /// </summary>
        [Column(Name = "Si_Regroup_Classement")]
        public int Si_Regroup_Classement { get; set; }

        /// <summary>
        /// Numéro du casque (Optionnel) (réel simple)
        /// </summary>
        [Column(Name = "NoCasque")]
        public short NoCasque { get; set; }

        [LinqToDB.Mapping.NotColumn]
        public Patineur Patineur 
        { 
            get 
            {
                return DB.Patineur.SingleOrDefault(z => z.NoPatineur == this.NoPatineur);
            } 
        }

        [LinqToDB.Mapping.NotColumn]
        public Club Club 
        { 
            get 
            {
                return DB.Club.SingleOrDefault(z => z.NoClub == this.NoClub);
            } 
        }
        [LinqToDB.Mapping.NotColumn]
        public Categorie Categorie
        {
            get
            {
                return DB.Categorie.SingleOrDefault(z => z.NoCategorie == this.NoCategorie);
            }
        }
    }
}
