using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{
    [Table(Name = "TParametres")]
    public class Parametres
    {
        /// <summary>
        /// No de la compétition en cours lorsque l'on a quitté l'écran Compétition (entier long)
        /// </summary>
        [Column(Name = "NoCompetition")]
        public int? NoCompetition { get; set; }

        /// <summary>
        /// Version de la base de données (réel simple)
        /// </summary>
        [Column(Name = "Version")]
        public int? Version { get; set; }

        //Chemin pour récupérer les fichiers du Photo Finish texte 250
        [Column(Name = "CheminPhotoFinish")]
        public string CheminPhotoFinish { get; set; }

        /// <summary>
        /// Lors de la compilation des resultats pour plusieurs competitions, determine si le classement des patineurs doit etre fait (oui/non)
        /// </summary>
        [Column(Name = "Classement")]
        public bool Classement { get; set; }

        /// <summary>
        /// 1 = Compilation en fonction des points accumulés    2 = Compilation en fonction des temps (entier)
        /// </summary>
        [Column(Name = "ChoixCompilation")]
        public int? ChoixCompilation { get; set; }

        /// <summary>
        /// Les "Nombre Compe" pires competitions a considerer pour chaque patineur, lors d'une compilation en fonction des points (numérique)
        /// </summary>
        [Column(Name = "NombreCompe")]
        public int? NombreCompe { get; set; }
        
        /// <summary>
        /// 1 = Toutes les catégories    2 = Une catégorie en particulier, pour classement par points (entier)
        /// </summary>
        [Column(Name = "ChoixCategPoints")]
        public int? ChoixCategPoints { get; set; }

        /// <summary>
        /// Numero de la catégorie selectionnée pour la compilation par les points (entier long)
        /// </summary>
        [Column(Name = "CategClassementPoints")]
        public int? CategClassementPoints { get; set; }

        /// <summary>
        /// Numero de la catégorie selectionnée pour la compilation par les points (entier)
        /// </summary>
        [Column(Name = "ChoixDivPoints")]
        public int? ChoixDivPoints { get; set; }

        /// <summary>
        /// Division selectionnee pour la compilation par les points (texte 20)
        /// </summary>
        [Column(Name = "DivClassementPoints")]
        public string DivClassementPoints { get; set; }

        /// <summary>
        /// 1 = Toutes les divisions     2 = Une division en particulier, pour un classement par temps (entier)
        /// </summary>
        [Column(Name = "ChoixDivTemps")]
        public int? ChoixDivTemps { get; set; }

        /// <summary>
        /// Division selectionne pour la compilation par temps (texte 10)
        /// </summary>
        [Column(Name = "DivClassementTemps")]
        public string DivClassementTemps { get; set; }

        /// <summary>
        /// 1 = Une categorie en particulier   2 = Limite d'age pour la compilation en fonction du temps (entier)
        /// </summary>
        [Column(Name = "ChoixCategTemps")]
        public int? ChoixCategTemps { get; set; }

        /// <summary>
        /// Age inferieure a considerer pour la compilation en fonction du temps (date)
        /// </summary>
        [Column(Name = "AgeInfCompilTemps")]
        public DateTime? AgeInfCompilTemps { get; set; }

        /// <summary>
        /// Age superieure a considerer pour la compilation en fonction du temps (date)
        /// </summary>
        [Column(Name = "AgeSupCompilTemps")]
        public DateTime? AgeSupCompilTemps { get; set; }

        /// <summary>
        /// Titre ou nom des categories a donner pour la compilation en fonction des ages min et max, en fonction du temps (texte 255)
        /// </summary>
        [Column(Name = "TitreCompilAgeTemps")]
        public string TitreCompilAgeTemps { get; set; }

        /// <summary>
        /// No de la catégorie Pour les patineurs de deuxième année (entier long)
        /// </summary>
        [Column(Name = "CatDeuxTemps")]
        public int? CatDeuxTemps { get; set; }

        /// <summary>
        /// No de la catégorie Pour les patineurs de première année (entier long)
        /// </summary>
        [Column(Name = "CatUnTemps")]
        public int? CatUnTemps { get; set; }

        /// <summary>
        /// Titre à donner au rapport de compilation en fonction des temps (texte 100)
        /// </summary>
        [Column(Name = "TitreCompilTemps")]
        public string TitreCompilTemps { get; set; }

        /// <summary>
        /// Titre du rapport de compilation en fonction des points texte 100
        /// </summary>
        [Column(Name = "TitreCompilPoints")]
        public string TitreCompilPoints { get; set; }

        /// <summary>
        /// Nom de la ou des catégories en compilation (texte 50)
        /// </summary>
        [Column(Name = "NomCatCompil")]
        public string NomCatCompil { get; set; }

        /// <summary>
        /// Nom de la ou des divisions en compilation (texte 50)
        /// </summary>
        [Column(Name = "NomDivCompil")]
        public string NomDivCompil { get; set; }

        /// <summary>
        /// Compilation par points. Exclure un patineur ne faisant plus partie de la division sélectionnée. (oui/non)
        /// </summary>
        [Column(Name = "Exclure")]
        public bool Exclure { get; set; }

        /// <summary>
        /// Titre du rapport Points_Sommaire.rpt (texte 100)
        /// </summary>
        [Column(Name = "TitrePoints_Sommaire")]
        public string TitrePoints_Sommaire { get; set; }

        /// <summary>
        /// Titre du rapport Points.rpt (texte 100)
        /// </summary>
        [Column(Name = "TitrePoints")]
        public string TitrePoints { get; set; }

        /// <summary>
        /// Langue du Logiciel (F pour Français, A pour Anglais) (texte 50)
        /// </summary>
        [Column(Name = "Langue")]
        public string Langue { get; set; }

        /// <summary>
        /// Description du champs Classement de la table TPatineurs (Table utilisée pour le classement au 500 aux essais canadiens) (texte 50)
        /// </summary>
        [Column(Name = "NomClassement")]
        public string NomClassement { get; set; }

        /// <summary>
        /// Description du champs Classement 1000 de la table TPatineurs (texte 50)
        /// </summary>
        [Column(Name = "NomClassement1000")]
        public string NomClassement1000 { get; set; }

        /// <summary>
        /// Description du champs Classement 1500 de la table TPatineurs (texte 50)
        /// </summary>
        [Column(Name = "NomClassement1500")]
        public string NomClassement1500 { get; set; }

        /// <summary>
        /// Description du champs ClassementGeneral de la table TPatineurs (texte 50)
        /// </summary>
        [Column(Name = "NomClassementGeneral")]
        public string NomClassementGeneral { get; set; }

        /// <summary>
        /// Mot de passe pour l' écran des paramètres provinciaux (texte 50)
        /// </summary>
        [Column(Name = "PasseParamProv")]
        public string PasseParamProv { get; set; }

        /// <summary>
        /// 1 --> Format "Rang",      2 --> Format "Temps" (entier)
        /// </summary>
        [Column(Name = "FormatClassement")]
        public int? FormatClassement { get; set; }

        /// <summary>
        /// 1 --> Format "Rang",      2 --> Format "Temps" )entier)
        /// </summary>
        [Column(Name = "FormatClassement1000")]
        public int? FormatClassement1000 { get; set; }

        /// <summary>
        /// 1 --> Format "Rang",      2 --> Format "Temps" (entier)
        /// </summary>
        [Column(Name = "FormatClassement1500")]
        public int? FormatClassement1500 { get; set; }

        /// <summary>
        /// 1 --> Format "Rang",      2 --> Format "Temps" (entier)
        /// </summary>
        [Column(Name = "FormatClassementGeneral")]
        public int? FormatClassementGeneral { get; set; }
  
        /// <summary>
        /// Mot de passe pour l' écran des parametres d' une compétition (texte 50)
        /// </summary>
        [Column(Name = "PasseParamCompe")]
        public string PasseParamCompe { get; set; }

        /// <summary>
        /// Date de référence pour le calcul des catégories. (TPatineurs) (date)
        /// </summary>
        [Column(Name = "DateRefCateg")]
        public DateTime DateRefCateg { get; set; }

        /// <summary>
        /// Description du champs Classement 2000 de la table TPatineurs (texte 50)
        /// </summary>
        [Column(Name = "NomClassement2000")]
        public string NomClassement2000 { get; set; }

        /// <summary>
        /// Description du champs Classement 2500 de la table TPatineurs texte 50
        /// </summary>
        [Column(Name = "NomClassement2500")]
        public string NomClassement2500 { get; set; }

        /// <summary>
        /// 1 --> Format "Rang",      2 --> Format "Temps" entier
        /// </summary>
        [Column(Name = "FormatClassement2000")]
        public int? FormatClassement2000 { get; set; }

        /// <summary>
        /// 1 --> Format "Rang",      2 --> Format "Temps" (entier)
        /// </summary>
        [Column(Name = "FormatClassement2500")]
        public int? FormatClassement2500 { get; set; }

        /// <summary>
        /// Oui si la case "rapport mixte" est cochée (boolean)
        /// </summary>
        [Column(Name = "ChoixRapportMixteTemps")]
        public bool ChoixRapportMixteTemps { get; set; }

        /// <summary>
        /// Oui si la case "Catégories séparés" est cochée (boolean)
        /// </summary>
        [Column(Name = "ChoixRapportCategSepares")]
        public bool ChoixRapportCategSepares { get; set; }

        /// <summary>
        /// Retiens si on a choisi de réinitialiser les rankings avant une compilation (boolean)
        /// </summary>
        [Column(Name = "ReinitialiserClassement")]
        public bool ReinitialiserClassement { get; set; }      
    }
}
