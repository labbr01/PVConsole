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
    [Table(Name = "TDistances_Standards")]
    public class DistanceStandards
    {
        /// <summary>
        /// Entier long numérotation automatique
        /// </summary>
        [PrimaryKey, Identity]
        public int NoDistance { get; set; }

        /// <summary>
        /// Nom de l'épreuve, unique pour chaque catégorie/division. Anciennement : Distances attitrées pour une catégorie de patineur(texte 255)
        /// </summary>
        [Column(Name = "Distance")]
        public string Distance { get; set; }

        /// <summary>
        /// Lien pour la catégorie de patineur (voir table TCatégorie) Entier long
        /// </summary>
        [Column(Name = "NoCategorie")]
        public int NoCategorie { get; set; }

        /// <summary>
        /// LP pour Longue piste ou CP pour Courte piste (texte 2)
        /// </summary>
        [Column(Name = "NoLP_CP")]
        public string NoLP_CP { get; set; }

        /// <summary>
        /// LD pour Longue distance, CD pour courte distance (texte 2)
        /// </summary>
        [Column(Name = "NoLD_CD")]
        public string NoLD_CD { get; set; }

        /// <summary>
        /// Record canadien masculin de la distance (réel double)
        /// </summary>
        [Column(Name = "RecordMasc")]
        public double RecordMasc { get; set; }

        /// <summary>
        /// Nombre maximum de patineurs dans une vague lors d'une qualification, quart, demi, ou finale entier
        /// </summary>
        [Column(Name = "Nomb_max_Qual")]
        public int Nomb_max_Qual { get; set; }

        /// <summary>
        /// Nombre minimum de patineurs dans une  vague lors d'une qualification (Entier)
        /// </summary>
        [Column(Name = "Nomb_min_Qual")]
        public int Nomb_min_Qual { get; set; }

        /// <summary>
        /// Nombre maximum de patineurs dans une vague lors d'une finale  (annulé) (Entier)
        /// </summary>
        [Column(Name = "Nomb_max_Fin")]
        public int Nomb_max_Fin { get; set; }

        /// <summary>
        /// Nombre de patineurs par vague passant à la vague A (1+ ou bien 2+) (entier)
        /// </summary>
        [Column(Name = "Nomb_min_Fin")]
        public int Nomb_min_Fin { get; set; }

        /// <summary>
        /// Division pour laquelle la distance s'applique (texte 20)
        /// </summary>
        [Column(Name = "Division")]
        public string Division { get; set; }

        /// <summary>
        /// Champ temporaire pour la sélection des dist. à une compétition Masculin (boolean)
        /// </summary>
        [Column(Name = "SelectionneM")]
        public bool SelectionneM { get; set; }

        /// <summary>
        /// Champ temporaire pour la sélection des dist. à une compétition Féminin (boolean)
        /// </summary>
        [Column(Name = "SelectionneF")]
        public bool SelectionneF { get; set; }

        /// <summary>
        /// Détermine si les demi Finales sont permises (boolean)
        /// </summary>
        [Column(Name = "DemiFinale")]
        public bool DemiFinale { get; set; }

        /// <summary>
        /// Détermine si les quarts de Finales sont permises (boolean)
        /// </summary>
        [Column(Name = "QuartFinale")]
        public bool QuartFinale { get; set; }

        /// <summary>
        /// si pas patineurs avancés, nb add de patineurs permis pour éviter la demi finale (entier)
        /// </summary>
        [Column(Name = "PatAdd1")]
        public int PatAdd1 { get; set; }

        /// <summary>
        /// si patineurs avancés, nb add de patineurs permis pour éviter la demi finale (entier)
        /// </summary>
        [Column(Name = "PatAdd2")]
        public int PatAdd2 { get; set; }

        /// <summary>
        /// si pas patineurs avancés, nb add de patineurs permis pour éviter le quart de finale (Entier)
        /// </summary>
        [Column(Name = "PatAdd3")]
        public int PatAdd3 { get; set; }

        /// <summary>
        /// si patineurs avancés, nb add de patineurs permis pour éviter le quart de finale (entier)
        /// </summary>
        [Column(Name = "PatAdd4")]
        public int PatAdd4 { get; set; }

        /// <summary>
        /// Record canadien féminin de la distance (double)
        /// </summary>
        [Column(Name = "RecordFem")]
        public double RecordFem { get; set; }

        /// <summary>
        /// Numéro du format de la compétition 1--> Compétition provinciale, 4 --> Essais Canadiens (entier long)
        /// </summary>
        [Column(Name = "NoFormat")]
        public int NoFormat { get; set; }

        /// <summary>
        /// Record provincial féminin de la distance double
        /// </summary>
        [Column(Name = "RecordProvFem")]
        public double RecordProvFem { get; set; }

        /// <summary>
        /// Record provincial masculin de la distance double
        /// </summary>
        [Column(Name = "RecordProvMasc")]
        public double RecordProvMasc { get; set; }

        /// <summary>
        /// suite à une course, le classement pour l' étape suivante se fera :  1--> selon les rangs,     2 --> selon les temps (entier)
        /// </summary>
        [Column(Name = "Classement")]
        public int Classement { get; set; }

        /// <summary>
        /// 1 --> les coureurs de sexe différent sont regroupés dans les courses, 2 --> les coureurs de sexe différent sont séparés (entier long)
        /// </summary>
        [Column(Name = "MixteOuNon")]
        public int MixteOuNon { get; set; }

        /// <summary>
        /// 1 --> Les patineurs sont regroupés par catégories lors d'une compétition;  2 --> les patineurs sont regroupés selon leur classement lors d' une compétition (entier)
        /// </summary>
        [Column(Name = "Groupe")]
        public int Groupe { get; set; }

        /// <summary>
        /// Nombre maximum de patineurs dans une vague lors d'une Demi-Finale  (annulé) (entier)
        /// </summary>
        [Column(Name = "Nomb_max_Demi")]
        public int Nomb_max_Demi { get; set; }

        /// <summary>
        /// Les X premiers de la qualif accèdent à la demi ou à la vague A de la finale (entier
        /// </summary>
        [Column(Name = "XPremiersQualif")]
        public int XPremiersQualif { get; set; }

        /// <summary>
        /// Les X premiers de la qualif accèdent aux quarts de finale. (entier)
        /// </summary>
        [Column(Name = "XPremiersQualif_Quart")]
        public int XPremiersQualif_Quart { get; set; }

        /// <summary>
        /// réduire de XPremiersQualif_Reduit, le nombre XPremiersQualif, s'il y a trop de patineurs dans la vague A ou bien, afin d' éviter la demi-finale
        /// </summary>
        [Column(Name = "XPremiersQualif_Reduit")]
        public int XPremiersQualif_Reduit { get; set; }

        /// <summary>
        /// Les X premiers de la Demi-finale accèdent à la vague A de la finale
        /// </summary>
        [Column(Name = "XPremiersDemi")]
        public int XPremiersDemi { get; set; }

        /// <summary>
        /// Les X premiers des quarts de finale accèdent à la vague A de la demi-finale
        /// </summary>
        [Column(Name = "XPremiersQuart")]
        public int XPremiersQuart { get; set; }

        /// <summary>
        /// réduire de XPremiersDemi_Reduit, le nombre XPremiersDemi, s'il y a trop de patineurs dans la vague A
        /// </summary>
        [Column(Name = "XPremiersDemi_Reduit")]
        public int XPremiersDemi_Reduit { get; set; }

        /// <summary>
        /// réduire de XPremiersQuart_Reduit, le nombre XPremiersQuart, s'il y a trop de patineurs dans la vague A de la Demi-finale
        /// </summary>
        [Column(Name = "XPremiersQuart_Reduit")]
        public int XPremiersQuart_Reduit { get; set; }

        /// <summary>
        /// réduire de XPremiersQualif_ReduitDemi, le nombre XPremiersQualif, afin de limiter le nombre de demi-finales à 2 à partir des qualif ou quart
        /// </summary>
        [Column(Name = "XPremiersQualif_ReduitDemi")]
        public int XPremiersQualif_ReduitDemi { get; set; }

        /// <summary>
        /// réduire de XPremiersQualif_ReduitQuart, le nombre XPremiersQualif, afin de limiter le nombre de quarts de finales à 4 (entier)
        /// </summary>
        [Column(Name = "XPremiersQualif_ReduitQuart")]
        public int XPremiersQualif_ReduitQuart { get; set; }

        /// <summary>
        /// Clé pour faire le lien avec la table de points à utiliser lors d' une compétition (texte 20)
        /// </summary>
        [Column(Name = "ClePoints")]
        public string ClePoints { get; set; }

        /// <summary>
        /// Pour compléter les vagues de demi ou finale A, on sélectionne les meilleurs temps parmi : 0 --> les rangs X+1, 1--> tous les patineurs (entier)
        /// </summary>
        [Column(Name = "AutresPatineurs")]
        public string AutresPatineurs { get; set; }

        /// <summary>
        /// Lorsqu'un patineur est avancé, 0-->Sa place est laissée vacante, 1 --> sa place est prise par un patineur de la vague suivante (entier)
        /// </summary>
        [Column(Name = "PlaceAvance")]
        public int PlaceAvance { get; set; }

        /// <summary>
        /// POur les essais canadiens, détermine si la distance est un relais ou non Oui/Non
        /// </summary>
        [Column(Name = "Relais")]
        public bool Relais { get; set; }

        /// <summary>
        /// Nombre maximum de demi-finale à partir duquel, on génère des quarts de finale (entier)
        /// </summary>
        [Column(Name = "NbMaxDemi")]
        public int NbMaxDemi { get; set; }

        /// <summary>
        /// Détermine si Oui ou NON tous les patineurs doivent faire la demi-finale. (Oui/Non)
        /// </summary>
        [Column(Name = "TousDemi")]
        public bool TousDemi { get; set; }

        /// <summary>
        /// Ce champs enregistre la longueur, en m, de l'épreuve (entier long)
        /// </summary>
        [Column(Name = "LongueurEpreuve")]
        public int LongueurEpreuve { get; set; }
    }
}
