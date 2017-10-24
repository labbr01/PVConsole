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
    [Table(Name = "TCompetition")]
    public class Competition
    {
        /// <summary>
        /// Numéro compétition (entier long auto incrément)
        /// </summary>
        [PrimaryKey, Identity]
        public int NoCompetition { get; set; }

        /// <summary>
        /// Lieu texte 50
        /// </summary>
        [Column(Name = "Lieu")]
        public string Lieu { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        [Column(Name = "Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Indicateur Longuepiste, courte piste texte 2
        /// </summary>
        [Column(Name = "NoLP_CP")]
        public string NoLP_CP { get; set; }

        /// <summary>
        /// Nombre de bloc pour la compétition entier
        /// </summary>
        [Column(Name = "Nb_Blocs")]
        public int Nb_Blocs { get; set; }

        /// <summary>
        /// Commentaire pour la compétition (champ mémo)
        /// </summary>
        [Column(Name = "Commentaire")]
        public string Commentaire { get; set; }

        /// <summary>
        /// Booléen sSi la compétition est intégrée dans le rapport cumulatif
        /// </summary>
        [Column(Name = "Selection")]
        public bool Selection { get; set; }

        /// <summary>
        ///Facteur de pondération dans le cumul des points (rééel simple)
        /// </summary>
        [Column(Name = "Ponderation")]
        public int Ponderation { get; set; }

        /// <summary>
        /// Numéro du club hote (entier long)
        /// </summary>
        [Column(Name = "NoClub")]
        public int NoClub { get; set; }

        /// <summary>
        /// Numéro de la sélection (première compétition sélectionnée a le numéro 1)
        /// </summary>
        [Column(Name = "NoSelection")]
        public int NoSelection { get; set; }

        /// <summary>
        /// Format de la compétition entier long 1 provincial 4 sélection nationales
        /// </summary>
        [Column(Name = "Format")]
        public int Format { get; set; }

        /// <summary>
        /// 1->Par ordre de temps à l' étape précédente. 2->Par ordre de rang d' abord et ensuite par ordre de temps, à l' étape précédente. 3->Selon le critère ayant servi à la formation des qualifications 4->Mode FPVQ décrit au guide de procédures. 5->Mode national (entier)
        /// </summary>
        [Column(Name = "TriQualifies")]
        public int TriQualifies { get; set; }

        /// <summary>
        /// 1->Sa place dans la vague qu' il aurait occupée sera laissée vacante. 2->Sa place dans la vague qu' il aurait occupée sera prise par un patineur de la vague suivante. (entier automatique)
        /// </summary>
        [Column(Name = "PlaceAvance")]
        public int PlaceAvance { get; set; }

        /// <summary>
        /// 1-> ABC CBA ABC CBA ABC CBA     2-> ABC CBA CBA CBA CBA (entier)
        /// </summary>
        [Column(Name = "ABC-CBA")]
        public int ABC_CBA { get; set; }

        /// <summary>
        /// Pour compléter les vagues de demi ou finale A, on sélectionne les meilleurs temps parmi : 0 --> les rangs X+1, 1--> tous les patineurs entier auto
        /// </summary>
        [Column(Name = "AutresPatineurs")]
        public int AutresPatineurs { get; set; }

        /// <summary>
        /// générer les casques dans les vagues autres que les premières d'une distance (sauf pour sous-groupes mode nat). 1->De façon aléatoire (défaut)2->À partir des temps à l'étape précédente. (entier long)
        /// </summary>
        [Column(Name = "genererCasques")]
        public int genererCasques { get; set; }

        /// <summary>
        /// générer les casques dans les vagues autres que les premières d'une distance (sauf pour sous-groupes mode nat). 1->De façon aléatoire (défaut)2->À partir des temps à l'étape précédente. (entier long)
        /// </summary>
        [Column(Name = "NbJoursFormatOuvert")]
        public int NbJoursFormatOuvert { get; set; }

        /// <summary>
        /// Mode ModeElite
        /// </summary>
        [Column(Name = "ModeElite")]
        public bool ModeElite { get; set; }

    }

}
