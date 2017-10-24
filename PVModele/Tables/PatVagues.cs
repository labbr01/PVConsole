using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{

    [Table(Name = "TPatVagues")]
    public class PatVagues
    {
       
        /// <summary>
        /// Entier long auto
        /// </summary>
        [PrimaryKey, Identity]
        public int CleTPatVagues { get; set; }

        /// <summary>
        /// Commentaire de l'officiel (ex: disqualification ou autre) (voir table TCode) entier long
        /// </summary>
        [Column(Name = "Juge")]
        public int Juge { get; set; }

        /// <summary>
        /// Code pour relation avec la table TVagues Entier long auto
        /// </summary>
        [Column(Name = "CleTVagues")]
        public int CleTVagues { get; set; }

        /// <summary>
        /// Numéro donné au patineur enregistré à la compé. Entier long
        /// </summary>
        [Column(Name = "NoPatCompe")]
        public int NoPatCompe { get; set; }

        /// <summary>
        /// Temps du patineur en seconde (réel double)
        /// </summary>
        [Column(Name = "Temps")]
        public double Temps 
        { 
            get 
            { return tx; } 
            set 
            { tx = value; } 
        }

        private double tx;
        /// <summary>
        /// Rang du patineur lors d'une vague (entier long)
        /// </summary>
        [Column(Name = "Rang")]
        public int Rang { get; set; }

        /// <summary>
        /// Point recueilli par un patineur lors d'une vague finale (réel simple)
        /// </summary>
        [Column(Name = "Point")]
        public int Point { get; set; }

        /// <summary>
        /// Points ISU pour les essais canadiens, pour la répartition des patineurs dans les vagues (entier long)
        /// </summary>
        [Column(Name = "PointISU")]
        public int PointISU { get; set; }

        /// <summary>
        /// No de casque dans la vague (entier)
        /// </summary>
        [Column(Name = "NoCasque")]
        public int NoCasque { get; set; }

        /// <summary>
        /// Somme des points pour la compétition en cours (Doit être mis à jour par programmation) (réel double)
        /// </summary>
        [Column(Name = "SommeDePoint")]
        public int SommeDePoint { get; set; }

        /// <summary>
        /// 1 --> Avancé mais aurait quand même fait la demi ou la finale. 2 --> Avancé et n' aurait pas fait la demi ou la finale A (entier long)
        /// </summary>
        [Column(Name = "AvanceSansAvantage")]
        public int AvanceSansAvantage { get; set; }

        /// <summary>
        /// Cette valeur est utilisée en Mode national pour donner le rang préliminaire des patineurs. Il doit être mis dans le code du logiciel avant de générer le rapport classementDistance (Entier long)
        /// </summary>
        [Column(Name = "RangGlobal")]
        public int RangGlobal { get; set; }
    }
}
