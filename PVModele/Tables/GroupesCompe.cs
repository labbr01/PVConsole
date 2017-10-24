using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;
using LinqToDB;

namespace PVModele.Tables
{
    [Table(Name = "TGroupes_Compe")]
    public class GroupesCompe
    {
        /// <summary>
        /// Entier long numérotation automatique
        /// </summary>
        [PrimaryKey, Identity]
        public int IDGroupesCompe { get; set; }

        /// <summary>
        /// Numéro de la compétition (entier long)
        /// </summary>
        [Column(Name = "NoCompetition")]
        public int NoCompetition { get; set; }

        /// <summary>
        /// groupe dans lequel le patineur sera placé lorsque les patineurs sont regroupés par classement dans une compé (Texte 50)
        /// </summary>
        [Column(Name = "Groupe")]
        public string Groupe { get; set; }

        /// <summary>
        /// Description du groupe (texte 100)
        /// </summary>
        [Column(Name = "Description du groupe")]
        public string DescriptionGroupe { get; set; }

        /// <summary>
        /// Séquence qui détermine si les points d'un groupe en finale sont à la suite d'un autre groupe (entier)
        /// </summary>
        [Column(Name = "SequencePointage")]
        public int SequencePointage { get; set; }

        /// <summary>
        /// Regroupement de groupes. Le premier est utilisé pour la première distance. Les vagues sont générés pour les valeurs > 0  même si NbPat = 0. (Entier long)
        /// </summary>
        [Column(Name = "Regroupement")]
        public int Regroupement { get; set; }
    }  
}



