using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;



namespace PVModele.Tables
{
    [Table(Name = "TProg_Courses")]
    public class ProgCourses
    {
        /// <summary>
        /// Clé pour le programme de course (entier long)
        /// </summary>
        [PrimaryKey, Identity]
        public int CleDistancesCompe { get; set; }

        /// <summary>
        /// Numéro de la compétition (voir table TCompetition) (entier long)
        /// </summary>
        [Column(Name = "NoCompetition")]
        public int NoCompetition { get; set; }

        /// <summary>
        /// Sexe des patineurs F = Féminin ou M = Masculin) (texte 5)
        /// </summary>
        [Column(Name = "Sexe")]
        public string Sexe { get; set; }

        /// <summary>
        /// Nombre de patineurs inscrits dans la catégorie (entier)
        /// </summary>
        [Column(Name = "NbPatineurs")]
        public int NbPatineurs { get; set; }

        /// <summary>
        /// Si OUI, distance directement en finale, sans qualification (texte 3)
        /// </summary>
        [Column(Name = "DirectFinale")]
        public string DirectFinale { get; set; }

        /// <summary>
        /// Nombre de vagues en qualification (entier)
        /// </summary>
        [Column(Name = "NbQualif")]
        public int NbQualif { get; set; }

        /// <summary>
        /// Nombre de Huitième de finale pour la série (entier long)
        /// </summary>
        [Column(Name = "NbHuit")]
        public int NbHuit { get; set; }

        /// <summary>
        /// Nombre de Quart de finale pour la série (entier long)
        /// </summary>
        [Column(Name = "NbQuart")]
        public int NbQuart { get; set; }

        /// <summary>
        /// Nombre de demi finale pour la série, dans le cas où seulement les patineurs obligés font la demi (entier long)
        /// </summary>
        [Column(Name = "NbDemi")]
        public int NbDemi { get; set; }

        /// <summary>
        /// Nombre de demi finale pour la série, (correspond au nombre de qualif si tous les pat de la série font la demi. correspond à NbDemi si seulement les patineurs obligés font la demi) ( entier long)
        /// </summary>
        [Column(Name = "NbDemiTotal")]
        public int NbDemiTotal { get; set; }

        /// <summary>
        /// Nombre de vagues en finale (entier)
        /// </summary>
        [Column(Name = "NbFinale")]
        public int NbFinale { get; set; }

        /// <summary>
        /// Séquence des courses en qualification (entier)
        /// </summary>
        [Column(Name = "SeqQualif")]
        public int SeqQualif { get; set; }

        /// <summary>
        /// Séquence des courses en Huitième de Finale (entier)
        /// </summary>
        [Column(Name = "SeqHuit")]
        public int SeqHuit { get; set; }

        /// <summary>
        /// Séquence des courses en Quart de Finale Entier long
        /// </summary>
        [Column(Name = "SeqQuart")]
        public int SeqQuart { get; set; }

        /// <summary>
        /// Séquence des courses en Demi-Finale Entier long
        /// </summary>
        [Column(Name = "SeqDemi")]
        public int SeqDemi { get; set; }

        /// <summary>
        /// Séquence des courses en finale (entier long)
        /// </summary>
        [Column(Name = "SeqFinale")]
        public int SeqFinale { get; set; }

        /// <summary>
        /// Critère pour le choix des patineurs enhuitième de finale, essais canadiens (ex. 1 + 2) (texte 20)
        /// </summary>
        [Column(Name = "CritVagueAHuit")]
        public string CritVagueAHuit { get; set; }

        /// <summary>
        /// Critère pour le choix des patineurs en quart de finale, essais canadiens (ex. 1 + 2) (texte 20)
        /// </summary>
        [Column(Name = "CritVagueAQuart")]
        public string CritVagueAQuart { get; set; }

        /// <summary>
        /// Critère pour le choix des patineurs en demi-finale, essais canadiens (ex. 1 + 2) (texte 20
        /// </summary>
        [Column(Name = "CritVagueADemi")]
        public string CritVagueADemi { get; set; }

        /// <summary>
        /// Critère pour le choix des patineurs en finale (ex. 1 + 2) (texte 20)
        /// </summary>
        [Column(Name = "CritVagueA")]
        public string CritVagueA { get; set; }

        /// <summary>
        /// Critère pour le choix des patineurs en finale (ex. 1 + 2) s' il y a une demi-Finale Spéciale (texte 20)
        /// </summary>
        [Column(Name = "CritVagueASiDemiS")]
        public string CritVagueASiDemiS { get; set; }

        /// <summary>
        /// Critère pour le choix des patineurs en finale (ex. 1 + 2) pour le second bloc des patineurs provenant des demi-finales lorsque tous les patineurs ont fait les demi-finales (texte 50)
        /// </summary>
        [Column(Name = "CritVagueABloc2TousDemi")]
        public string CritVagueABloc2TousDemi { get; set; }

        /// <summary>
        /// Nombre maximum de patineurs dans une vague lors d'une qualification, quart, demi, ou finale (entier)
        /// </summary>
        [Column(Name = "Nomb_max_Qual")]
        public int Nomb_max_Qual { get; set; }

        /// <summary>
        /// Nombre max de patineurs dans une vague en huitième de finale  (annulé) (entier)
        /// </summary>
        [Column(Name = "Nomb_max_Huit")]
        public int Nomb_max_Huit { get; set; }

        /// <summary>
        /// Nombre max de patineurs dans une vague en Quart de finale  (annulé) (entier)
        /// </summary>
        [Column(Name = "Nomb_max_Quart")]
        public int Nomb_max_Quart { get; set; }

        /// <summary>
        /// Nombre maximum de patineurs dans une vague en demi-finale (annulé) (entier)
        /// </summary>
        [Column(Name = "Nomb_max_Demi")]
        public int Nomb_max_Demi { get; set; }

        /// <summary>
        /// Nombre maximum de patineurs dans une vague en finale  (annulé) (entier)
        /// </summary>
        [Column(Name = "Nomb_max_Fin")]
        public int Nomb_max_Fin { get; set; }

        /// <summary>
        /// Nombre de vagues en finale, requis par les patineurs des demi-finales (entier)
        /// </summary>
        [Column(Name = "NbFinaleDeDemi")]
        public int NbFinaleDeDemi { get; set; }

        /// <summary>
        /// Nombre de vagues en finale, requis par les patineurs de qualif n' ayant pas participés aux demi-finales (entier)
        /// </summary>
        [Column(Name = "NbFinaleDeQualif")]
        public int NbFinaleDeQualif { get; set; }

        /// <summary>
        /// Les X premiers de la qualif accèdent à la vague A de la série suivante (entier)
        /// </summary>
        [Column(Name = "XPremiersQualif")]
        public int XPremiersQualif { get; set; }

        /// <summary>
        /// Les X premiers de la qualif accèdent aux quarts de finale. (entier)
        /// </summary>
        [Column(Name = "XPremiersQualif_Quart")]
        public int XPremiersQualif_Quart { get; set; }

        /// <summary>
        /// Les X premiers des huitième de finale accèdent à la vague A de la série suivante Entier)
        /// </summary>
        [Column(Name = "XPremiersHuit")]
        public int XPremiersHuit { get; set; }

        /// <summary>
        /// Les X premiers des quart de finale accèdent à la vague A de la série suivante (entier)
        /// </summary>
        [Column(Name = "XPremiersQuart")]
        public int XPremiersQuart { get; set; }

        /// <summary>
        /// Les X premiers de la Demi-finale accèdent à la vague A de la finale (entier)
        /// </summary>
        [Column(Name = "XPremiersDemi")]
        public int XPremiersDemi { get; set; }

        /// <summary>
        /// réduire de XPremiersQualif_Reduit, le nombre XPremiersQualif, s'il y a trop de patineurs dans la vague A ou bien, afin d' éviter la demi-finale (entier)
        /// </summary>
        [Column(Name = "XPremiersQualif_Reduit")]
        public int XPremiersQualif_Reduit { get; set; }

        /// <summary>
        /// réduire de XPremiersDemi_Reduit, le nombre XPremiersDemi, s'il y a trop de patineurs dans la vague A (entier)
        /// </summary>
        [Column(Name = "XPremiersDemi_Reduit")]
        public int XPremiersDemi_Reduit { get; set; }

        /// <summary>
        /// réduire de XPremiersQualif_ReduitDemi, le nombre XPremiersQualif, afin de limiter le nombre de demi-finales à 2 à partir des qualif ou quart (entier)
        /// </summary>
        [Column(Name = "XPremiersQualif_ReduitDemi")]
        public int XPremiersQualif_ReduitDemi { get; set; }

        /// <summary>
        /// réduire de XPremiersQualif_ReduitQuart, le nombre XPremiersQualif, afin de limiter le nombre de quarts de finales à 4 (entier)
        /// </summary>
        [Column(Name = "XPremiersQualif_ReduitQuart")]
        public int XPremiersQualif_ReduitQuart { get; set; }

        /// <summary>
        /// Numéro du Bloc auquel appartient la série (entier)
        /// </summary>
        [Column(Name = "NoBloc")]
        public int NoBloc { get; set; }

        /// <summary>
        /// Clé pour se relier à la table des distances standards Entier long
        /// </summary>
        [Column(Name = "NoDistance")]
        public int NoDistance { get; set; }

        /// <summary>
        /// Détermine si les demi finales sont requises (bool)
        /// </summary>
        [Column(Name = "DemiFinale")]
        public bool DemiFinale { get; set; }

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
        /// Nombre de patineurs retirés durant la compétition, dans la catégorie (entier)
        /// </summary>
        [Column(Name = "NbRetires")]
        public int NbRetires { get; set; }

        /// <summary>
        /// Lors d'une compétition selon le classement, nom du groupe dans lequel le patineur est placé (voir table TGroupes_Compe)) (texte 50)
        /// </summary>
        [Column(Name = "Groupe")]
        public string Groupe { get; set; }

        /// <summary>
        /// Format dans lequel la distance va se courir (1--> Compétition provinciale, 4--> Essais canadiens) (entier long)
        /// </summary>
        [Column(Name = "Format")]
        public int Format { get; set; }

        /// <summary>
        /// Nom de l'épreuve, unique pour chaque catégorie/division. Anciennement : Distance à parcourir dans la course (texte 255)
        /// </summary>
        [Column(Name = "Distance")]
        public string Distance { get; set; }

        /// <summary>
        /// 1 --> Les patineurs sont regroupés par catégories lors d'une compétition;  2 --> les patineurs sont regroupés selon leur classement lors d' une compétition (entier)
        /// </summary>
        [Column(Name = "Regroupe")]
        public int Regroupe { get; set; }

        /// <summary>
        /// suite à une course, le classement pour l' étape suivante se fera :  1--> selon les rangs,     2 --> selon les temps (entier long)
        /// </summary>
        [Column(Name = "Classement")]
        public int Classement { get; set; }

        /// <summary>
        /// Lorsqu'un patineur est avancé, 0-->Sa place est laissée vacante, 1 --> sa place est prise par un patineur de la vague suivante (entier)
        /// </summary>
        [Column(Name = "PlaceAvance")]
        public int PlaceAvance { get; set; }

        /// <summary>
        /// Clé reliée à la table TPoints pour déterminer la table de pointage à utiliser pour la série concernée (texte 20)
        /// </summary>
        [Column(Name = "ClePoints")]
        public string ClePoints { get; set; }

        /// <summary>
        /// Pour compléter les vagues de demi ou finale A, on sélectionne les meilleurs temps parmi : 0 --> les rangs X+1, 1--> tous les patineurs (entier)
        /// </summary>
        [Column(Name = "AutresPatineurs")]
        public int AutresPatineurs { get; set; }

        /// <summary>
        /// NON, cette distance ne doit pas apparaitre dans le programme de course (boolean)
        /// </summary>
        [Column(Name = "SiDistCourue")]
        public bool SiDistCourue { get; set; }

        /// <summary>
        /// Critère utilisé pour la création des vagues. 1--> Aleatoire, 2-->Aleatoire, répartir patineurs même club, 3 --> Classement general, 4 --> Classement, 5 --> Classement 1000, 6-->Classement 1500, 7 --> Points cumulés, 8-->Classement 2000, 9-->Classement2500 (entier long)
        /// </summary>
        [Column(Name = "TriSerie")]
        public int TriSerie { get; set; }

        /// <summary>
        /// POur les essais canadiens, détermine si la distance est un relais ou non (boolean)
        /// </summary>
        [Column(Name = "Relais")]
        public bool Relais { get; set; }

        /// <summary>
        /// réduire de XPremiersQuart_Reduit, le nombre XPremiersQuart, s'il y a trop de patineurs dans la vague A de la Demi-finale (entier)
        /// </summary>
        [Column(Name = "XPremiersQuart_Reduit")]
        public int XPremiersQuart_Reduit { get; set; }

        /// <summary>
        /// Détermine si les quarts de Finales sont permises (boolean)
        /// </summary>
        [Column(Name = "QuartFinale")]
        public bool QuartFinale { get; set; }

        /// <summary>
        /// si pas patineurs avancés, nb add de patineurs permis pour éviter le quart de finale (entier)
        /// </summary>
        [Column(Name = "PatAdd3")]
        public int PatAdd3 { get; set; }

        /// <summary>
        /// si patineurs avancés, nb add de patineurs permis pour éviter le quart de finale (entier)
        /// </summary>
        [Column(Name = "PatAdd4")]
        public int PatAdd4 { get; set; }

        /// <summary>
        /// Nombre maximum de demi-finale à partir duquel, on génère des quarts de finale (entier)
        /// </summary>
        [Column(Name = "NbMaxDemi")]
        public int NbMaxDemi { get; set; }

        /// <summary>
        /// Détermine si Oui ou NON tous les patineurs doivent faire la demi-finale. (boolean)
        /// </summary>
        [Column(Name = "TousDemi")]
        public bool TousDemi { get; set; }

        /// <summary>
        /// Détermine si OUI ou NON la distance en est une préliminaire, dans le cas affirmatif, seulement la qualif est courue. (texte 255 NON)
        /// </summary>
        [Column(Name = "Prelim")]
        public string Prelim { get; set; }

        /// <summary>
        /// Dans le cas où on a des sous-groupes et que le nombre de patineurs = 0, on peut simuler le nombre de patineurs pour avoir un programme de courses complet au début de la compétition (entier long)
        /// </summary>
        [Column(Name = "NbPatSimul")]
        public int NbPatSimul { get; set; }

        /// <summary>
        /// Ce champs n'est pas visible dans le logiciel, il sert à définir l'ordre d'apparence des courses. La partie entière sert à définir le bloc alors que la partie décimale sert à définir la séquence. On utilise les millièmes pour faire l'ordre. (réel simple)
        /// </summary>
        [Column(Name = "OrdreSequence")]
        public short OrdreSequence { get; set; }

        /// <summary>
        /// Ce champs enregistre la longueur, en m, de l'épreuve (entier long)
        /// </summary>
        [Column(Name = "LongueurEpreuve")]
        public int LongueurEpreuve { get; set; }
    }
}
