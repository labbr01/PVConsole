using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;
using LinqToDB.Linq;
using LinqToDB;
using LinqToDB.Extensions;
using PVModele;
using PVModele.Tables;
using System.Globalization;
using System.Net;
using Newtonsoft.Json.Linq;

namespace PVConsole
{
    class Program
    {
        private static List<PatFedeTemps> listePatFede = null;
        private static List<PatFedeTemps> ListePatFede
        {
            get
            {
                if (listePatFede == null)
                {
                    listePatFede = DataFederation.ObtenirTempsCSV();
                }
                return listePatFede;
            }
        }

        private static double inscrireTemps(Double valeur)
        {
            if (valeur < 10)
            {
                return valeur;
            }
            return 999;
        }

        static void Main(string[] args)
        {
            using (var db = new DBPatinVitesse())
            {
                PatineurCompe.DB = db;

                bool continuerBoucle = true;
                while (continuerBoucle)
                {
                    Console.WriteLine("Action : C (Compiler Résultats), CI (Compiler Résultats Inter), E (Extraire),T (Mise à jour temps), M (Mise à jour), R (Update rang), Q (Quitter), I (Inscription), S(Supprimer InterRegional), W (Inscription Web), R (Recharger Temps), RP (Regrouper Patineur), N (Nettoyer groupe compé)");
                    string rep = Console.ReadLine();
                    switch (rep.ToUpper())
                    {
                        case "ABC":
                            InfoCompeVagues(db);
                            break;
                        case "B":
                            BilanResultat(db);
                            break;
                        case "C":
                            CompilerLesResultats(db);
                            break;
                        case "CI":
                            CompilerLesResultatsInter(db);
                            break;
                        case "I":
                            InscrirePatineurs(db);
                            break;
                        case "R":
                            CalculerRang(db);
                            break;
                        case "E":
                            ExtrairePatineur(db);
                            break;
                        case "M":
                            MiseAJourPatineur(db);
                            break;
                        case "N":
                            NettoyerGroupecompe(db);
                            break;
                        case "T":
                            MiseAJourTempsPatineur(db);
                            break;
                        case "RT":
                            RechargerTemps(db);
                            break;
                        case "RP":
                            AssignerPatineurGroupe(db);
                            //RegrouperPatineur(db);
                            break;
                        case "S":
                            SupprimerInter(db);
                            break;
                        case "WJQ":
                            InscriptionWebJQ(db);
                            break;
                        case "W":
                            InscriptionWeb(db);
                            break;
                        case "Q":
                            continuerBoucle = false;
                            break;
                        default:
                            continuerBoucle = false;
                            break;
                    }
                }

                return;
            }
        }

        public static void MiseAJourPatineur(DBPatinVitesse db)
        {
            Console.WriteLine("Entrer le nom de fichier d'extraction");
            string nomFichier = Console.ReadLine();

            using (StreamReader sr = new StreamReader(nomFichier))
            {
                string[] ligneLue = sr.ReadLine().Split(';');
                ligneLue = sr.ReadLine().Split(';');
               
                while (sr.Peek() > 0)
                {
                    ligneLue = sr.ReadLine().Split(';');
                    IEnumerable<Patineur> EnumPat = db.Patineur.Where(z => z.Nom == ligneLue[1] && z.Prenom == ligneLue[0]);

                    if (EnumPat.Count() > 0)
                    {
                        //if (EnumPat.Count() > 1)
                        //{
                        //    DateTime date;
                        //    DateTime.TryParse(ligneLue[4], out date);
                        //    EnumPat.Where(z => z.DateNaissance == date);
                        //}
                        if (EnumPat.Count() == 1)
                        {
                            Patineur p = EnumPat.First();
                            double d;
                            if (Double.TryParse(ligneLue[2], out d))
                            {
                                if (d < p.Classement1500)
                                {
                                    Console.WriteLine("Mise à jour temps 300 patineur {0}, {1} : {2}, {3}", p.Nom, p.Prenom, p.Classement1500, d);
                                    p.Classement1500 = d;
                                }
                            }
                            if (Double.TryParse(ligneLue[3], out d))
                            {
                                if (d < p.Classement2000)
                                {
                                    Console.WriteLine("Mise à jour temps 400 patineur {0}, {1} : {2}, {3}", p.Nom, p.Prenom, p.Classement2000, d);
                                    p.Classement2000 = d;
                                }
                            }
                            if (Double.TryParse(ligneLue[4], out d))
                            {
                                if (d < p.Classement2500)
                                {
                                    Console.WriteLine("Mise à jour temps 800 patineur {0}, {1} : {2}, {3}", p.Nom, p.Prenom, p.Classement2500, d);
                                    p.Classement2500 = d;
                                }
                            }
                            db.Update(p);
                        }
                        else
                        {
                            Console.WriteLine("Patineur non trouvé : " + string.Join(";",ligneLue));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Patineur non trouvé : " + string.Join(";", ligneLue));
                    }
                }
            }
        }

        /// <summary>
        /// Inscription web régulière
        /// </summary>
        /// <param name="db"></param>
        public static void InscriptionWeb(DBPatinVitesse db)
        {
            bool donneeslues = false;
            string getpage = string.Empty;
            Console.WriteLine("Entrer le nom du fichier");
            string nomFich = Console.ReadLine();
            if (!string.IsNullOrEmpty(nomFich))
            {
                if (System.IO.File.Exists(nomFich))
                {
                    donneeslues = true;
                    using (StreamReader sr = new StreamReader(System.IO.File.OpenRead(nomFich)))
                    {
                        getpage = sr.ReadToEnd();
                    }
                }
            }
            if (!donneeslues)
            {
                Console.WriteLine("Entrer le numéro d'organisation (10476)");
                int noOrg = 10476;
                string organitation = Console.ReadLine();
                if (!string.IsNullOrEmpty(organitation))
                {
                    int.TryParse(organitation, out noOrg);
                }

                Console.WriteLine("Entrer le numéro de formulaire (136)");
                string formulaire = Console.ReadLine();
                int noForm = 136;
                if (!string.IsNullOrEmpty(formulaire))
                {
                    int.TryParse(formulaire, out noForm);
                }
                string url = string.Format("https://www.publicationsports.com/vProd/fr/service/apps/json/SportsDynastyOnlineForm/readConfirmedRegistrationByOnlineForm/?idOrganisation={0}&idOnlineForm={1}", noOrg, noForm);
                Console.WriteLine(url);
                using (WebClient wc = new WebClient())
                {
                    IWebProxy defaultProxy = WebRequest.DefaultWebProxy;
                    if (defaultProxy != null)
                    {
                        defaultProxy.Credentials = CredentialCache.DefaultCredentials;
                        wc.Proxy = defaultProxy;
                    }


                    getpage = wc.DownloadString(url);
                }
            }
                JObject jo = JObject.Parse(getpage);

                foreach (JProperty jp in jo.Properties())
                {
                    if (jp.Name == "success")
                    {
                        JToken infoSucces = jo[jp.Name];

                        if (infoSucces.Type != JTokenType.Boolean || !infoSucces.Value<bool>())
                        {
                            Console.WriteLine("La valeur retournée n'est pas un succès");
                            return;
                        }

                    }
                    if (jp.Name == "members")
                    {
                        JToken membres = jo[jp.Name];
                        if (membres.Type == JTokenType.Object)
                        {
                            JObject membreso = membres as JObject;
                            Dictionary<string, PubSportInscrit> dictInscrit = new Dictionary<string, PubSportInscrit>();
                            foreach (JProperty jp2 in membreso.Properties())
                            {
                                if (jp2.Type == JTokenType.Property)
                                {
                                    string noInscription = jp2.Name;
                                    JObject jo3 = jp2.Value as JObject;
                                    dictInscrit.Add(noInscription, Newtonsoft.Json.JsonConvert.DeserializeObject<PubSportInscrit>(jo3.ToString()));
                                }
                            }

                            PoursuivreInscription(db, dictInscrit);
                        }
                    }
                }
            

        }

        /// <summary>
        /// Inscription web spécialisée pour les jeux du québec
        /// </summary>
        /// <param name="db"></param>
        public static void InscriptionWebJQ(DBPatinVitesse db)
        {
            Console.WriteLine("Entrer le numéro d'organisation (10476)");
            int noOrg = 10476;
            string organitation = Console.ReadLine();
            if (!string.IsNullOrEmpty(organitation))
            {
                int.TryParse(organitation, out noOrg);
            }

            Console.WriteLine("Entrer le numéro de formulaire (136)");
            string formulaire = Console.ReadLine();
            int noForm = 136;
            if (!string.IsNullOrEmpty(formulaire))
            {
                int.TryParse(formulaire, out noForm);
            }
            string url = string.Format("https://www.publicationsports.com/vProd/fr/service/apps/json/SportsDynastyOnlineForm/readConfirmedRegistrationByOnlineForm/?idOrganisation={0}&idOnlineForm={1}", noOrg, noForm);
            Console.WriteLine(url);
            using (WebClient wc = new WebClient())
            {
                //IWebProxy defaultProxy = WebRequest.DefaultWebProxy;
                //if (defaultProxy != null)
                //{
                //    defaultProxy.Credentials = CredentialCache.DefaultCredentials;
                //    wc.Proxy = defaultProxy;
                //}

                //string getpage = wc.DownloadString(url);

                StreamReader sr = new StreamReader(@"C:\Users\Bruno\Documents\Arpvq 2016-2017\QualifJQ\QualifJQ.json");
                string getpage = sr.ReadToEnd();
                JObject jo = JObject.Parse(getpage);

                foreach (JProperty jp in jo.Properties())
                {
                    if (jp.Name == "success")
                    {
                        JToken infoSucces = jo[jp.Name];

                        if (infoSucces.Type != JTokenType.Boolean || !infoSucces.Value<bool>())
                        {
                            Console.WriteLine("La valeur retournée n'est pas un succès");
                            return;
                        }

                    }
                    if (jp.Name == "members")
                    {
                        JToken membres = jo[jp.Name];
                        if (membres.Type == JTokenType.Object)
                        {
                            JObject membreso = membres as JObject;
                            Dictionary<string, PubSportInscrit> dictInscrit = new Dictionary<string, PubSportInscrit>();
                            foreach (JProperty jp2 in membreso.Properties())
                            {
                                if (jp2.Type == JTokenType.Property)
                                {
                                    string noInscription = jp2.Name;
                                    JObject jo3 = jp2.Value as JObject;
                                    dictInscrit.Add(noInscription, Newtonsoft.Json.JsonConvert.DeserializeObject<PubSportInscrit>(jo3.ToString()));
                                }
                            }

                            PoursuivreInscriptionJQ(db, dictInscrit);
                        }
                    }
                }
            }

        }



        private class PatineurCompeExtra
        {
            public PatineurCompe PatineurCompe { get; set; }

            public Patineur Patineur { get; set; }

            public int NoPatCompe { get; set; }
            public int NoPat { get; set; }

            public double Classement2000 { get; set; }
            public double Classement1500 { get; set; }
            public double Classement2500 { get; set; }

        }

        public static void NettoyerGroupecompe(DBPatinVitesse db)
        {
            Console.WriteLine("Entrer le numéro de compétition");
            string noStrCompe = Console.ReadLine();
            int noCompetition;
            if (int.TryParse(noStrCompe, out noCompetition))
            {
                foreach (GroupesCompe gp in db.GroupesCompe.Where(z => z.NoCompetition == noCompetition).ToList())
                    {
                    if (string.IsNullOrEmpty(gp.DescriptionGroupe))
                    {
                        db.Delete(gp);
                    }
                }
            }

        }

        /// <summary>
        /// Assigne les patineurs à un groupe de compétition
        /// </summary>
        /// <param name="db">base de donnée</param>
        public static void AssignerPatineurGroupe(DBPatinVitesse db)
        {
            
            db.Competition.ToList().ForEach(z => Console.WriteLine(string.Format("No : {0}, Nom : {1}", z.NoCompetition, z.Lieu)));
            Console.WriteLine("Entrer le numéro de compétition");
            string noStrCompe = Console.ReadLine();
            int noCompetition;
            if (int.TryParse(noStrCompe, out noCompetition))
            {
                // Mettre le classement général à 999 pour tous
                db.Patineur.ToList().ForEach(z => z.ClassementGeneral = 999);
                // Obtenir les patineurs inscrits à la compétition
                IList<PatineurCompe> lpc = db.PatineurCompe.Where(z => z.NoCompetition == noCompetition).ToList();
                //IList<PatineurCompe> ppx = lpc.Where(z => z.Patineur.Nom.ToUpper() == "CARON").ToList();
                //Console.WriteLine("");
                //return;
                //foreach (PatineurCompe p in lpc)
                //{
                //    Console.WriteLine(string.Format("Patineur {0},{1} rang {2}", p.Patineur.Nom, p.Patineur.Prenom, p.Rang));
                //}
                //return;
                IList<int> cadetJuveniles = db.Categorie.Where(z => z.AgeMin >= 10 && z.AgeMin <= 13).Select(z => z.NoCategorie).ToList();
                List<Categorie> lcx =db.Categorie.ToList();

                // Obtenir les cadets et juvéniles
                IList<PatineurCompe> cadetJuvenilesCompe = lpc.Where(z => cadetJuveniles.Contains(z.NoCategorie)).ToList();

                var cadetJuvenilesTemps =
                     from patineurCompe in cadetJuvenilesCompe
                     join patineur in db.Patineur on patineurCompe.NoPatineur equals patineur.NoPatineur
                     select new PatineurCompeExtra()
                     {
                         Classement1500 = patineur.Classement1500,
                         Classement2000 = patineur.Classement2000,
                         Classement2500 = patineur.Classement2500,
                         Patineur = patineur,
                         PatineurCompe = patineurCompe,
                         NoPat = patineur.NoPatineur,
                         NoPatCompe = patineurCompe.NoPatCompe
                     };

                int no = 0;
                int rang = 1;

                if (cadetJuvenilesCompe.Count() > 0)
                {
                    GroupesCompe gc = null; // CreerGroupeCompe(db, noCompetition, "Cadets Juvénile #" + no.ToString(), "10-13 ans #" + no.ToString());

                    // Assigner les cadets et juvéniles par groupes de 10, triés sur le meilleur temps 400 et ensuite par 300.
                    int nb = 0;
                    bool creer = true;
                    foreach (var pt in cadetJuvenilesTemps.OrderBy(z => z.Classement2000).ThenBy(z => z.Classement1500).ThenBy(z => z.Patineur.DateNaissance).ToList())
                    {                        
                        if (creer)
                        {
                            creer = false;
                            nb = 0;
                            no += 1;
                            gc = CreerGroupeCompe(db, noCompetition, string.Format("10-13 ans #{0:00}", no), string.Format("Cadets Juvénile #{0:00}", no));
                        }
                        Console.WriteLine(string.Format("Rang: {4}, Nom : {1}, Prénom : {2}, Temps 400= {0}, No groupe CJ : {3}", pt.Classement2000, pt.Patineur.Nom, pt.Patineur.Prenom, no, rang));

                        PatineurCompe pcc = pt.PatineurCompe;
                        pcc.Rang = rang;
                        pcc.Patineur.ClassementGeneral = rang;
                        Patineur px = pcc.Patineur;
                        px.ClassementGeneral = rang;
                        db.Update(px);
                        rang += 1;
                        pcc.Groupe = gc.Groupe;
                        db.Update(pcc);
                        nb += 1;
                        if (nb == 10)
                        {
                            creer = true;
                        }
                    }
                }

                // Obtenir les patineurs minime et benjamin inscrits à la compétition
                IList<int> minimesBenjamin = db.Categorie.Where(z => z.AgeMin < 10 && z.AgeMin > 5).Select(z => z.NoCategorie).ToList();
                IList<PatineurCompe> minimesBenjaminCompe = lpc.Where(z => minimesBenjamin.Contains(z.NoCategorie)).ToList();

                var minimesBenjaminTemps =
                     from patineurCompe in minimesBenjaminCompe
                     join patineur in db.Patineur on patineurCompe.NoPatineur equals patineur.NoPatineur
                     select new PatineurCompeExtra()
                     {
                         Classement1500 = patineur.Classement1500,
                         Classement2000 = patineur.Classement2000,
                         Classement2500 = patineur.Classement2500,
                         Patineur = patineur,
                         PatineurCompe = patineurCompe,
                         NoPat = patineur.NoPatineur,
                         NoPatCompe = patineurCompe.NoPatCompe
                     };//{ a = patineurCompe.NoPatCompe, b = patineurCompe.Rang, c = patineur.Classement2000};

                no = 0;


                if (minimesBenjaminTemps.Count() > 0)
                {
                    no = 0;
                    GroupesCompe gc = null; // = CreerGroupeCompe(db, noCompetition, "Minime Benjamin #" + no.ToString(), "6-9 ans #" + no.ToString());
                    int nb = 0;
                    bool creer = true;

                    // Ne plus trier sur le temps 400 les minimes et benjamin, seulement le temps 300
                    //////// Assigner les patineurs Minimes et Benjamin par groupe de 10, en commençant par ceux qui ont un temps 300 meilleur
                    //////// que 55 secondes, et trié sur leur temps 400.
                    //////var minimeTemps400et300MonsQue55 = minimesBenjaminTemps.Where(z => z.Classement2000 < 99 && z.Classement1500 < 0.55);
                    //////IList<int> noPatineurTraites = minimeTemps400et300MonsQue55.Select(z => z.NoPat).ToList();

                    //////foreach (var pt in minimeTemps400et300MonsQue55.OrderBy(z => z.Classement2000).ThenBy(z=> z.Classement1500).ThenBy(z => z.Patineur.DateNaissance).ToList())
                    //////{
                    //////    if (nb == 10)
                    //////    {
                    //////        nb = 0;
                    //////        no += 1;
                    //////        gc = CreerGroupeCompe(db, noCompetition, "Minime Benjamin #" + no.ToString(), "6-9 ans #" + no.ToString());
                    //////    }

                    //////    PatineurCompe pcc = pt.PatineurCompe;
                    //////    pcc.Rang = rang;
                    //////    pcc.Patineur.ClassementGeneral = rang;
                    //////    Patineur px = pcc.Patineur;
                    //////    px.ClassementGeneral = rang;
                    //////    db.Update(px);
                    //////    rang += 1;
                    //////    pcc.Groupe = gc.Groupe;
                    //////    db.Update(pcc);
                    //////    nb += 1;
                    //////}

                    // traiter le reste qui ont un temps.
                    // obtenir les patineurs qui ont un temps 300 mètres et qui n'ont pas été encore assigné par leur temps 400
                    // (ils n'avaient probablement pas de temps 400)
                    // le tri est sur le temps 300, et ces patineurs s'ajoutent à la suite des groupes existants
                    //////var reste = minimesBenjaminTemps.Where(z => z.Classement1500 < 99 && noPatineurTraites.Contains(z.NoPat) == false).ToList();
                    //////reste.Select(z => z.NoPat).ToList().ForEach(z => noPatineurTraites.Add(z));
                    ////////noPatineurTraites.addR(z => z.NoPat).ToList();
                    //////foreach (var pt in reste.OrderBy(z => z.Patineur.Classement1500).ThenBy(z => z.Patineur.DateNaissance).ToList())
                    ////// Rempacement
                    var minimeTemps300 = minimesBenjaminTemps.Where(z => z.Classement1500 < 99);
                    IList<int> noPatineurTraites = minimeTemps300.Select(z => z.NoPat).ToList();
                    foreach (var pt in minimeTemps300.OrderBy(z => z.Classement1500).ThenBy(z => z.Patineur.DateNaissance).ToList())
                    {                        
                        if (creer)
                        {
                            creer = false;
                            nb = 0;
                            no += 1;
                            gc = CreerGroupeCompe(db, noCompetition, string.Format("6-9 ans #{0:00}",no), string.Format("Minime Benjamin #{0:00}",no));
                        }
                        Console.WriteLine(string.Format("Rang : {4}, Nom : {1}, Prénom : {2}, Temps 300= {0}, No groupe MB : {3}", pt.Classement1500, pt.Patineur.Nom, pt.Patineur.Prenom, no, rang));

                        PatineurCompe pcc = pt.PatineurCompe;
                        pcc.Rang = rang;
                        pcc.Patineur.ClassementGeneral = rang;
                        Patineur px = pcc.Patineur;
                        px.ClassementGeneral = rang;
                        db.Update(px);
                        rang += 1;
                        pcc.Groupe = gc.Groupe;
                        db.Update(pcc);
                        nb += 1;
                        if (nb == 10)
                        {
                            creer = true;
                        }
                    }

                    // traiter les autres minime et benjamin qui n'ont pas de temps et qui ne sont pas encore
                    // assigné à un groupe.
                    var autres = minimesBenjaminTemps.Where(z => z.Classement1500 >= 99 && noPatineurTraites.Contains(z.NoPat) == false).ToList();
                    // ces patineurs sont alors triés par leur date de naissance, du plus vieux au plus petit.
                    foreach (var pt in autres.OrderBy(z => z.Patineur.DateNaissance).ToList())
                    {                        
                        if (creer)
                        {
                            creer = false;
                            // Si le groupe a 10 patineurs, créer un nouveau groupe
                            nb = 0;
                            no += 1;
                            gc = CreerGroupeCompe(db, noCompetition, string.Format("6-9 ans #{0:00}", no), string.Format("Minime Benjamin #{0:00}", no));
                        }

                        Console.WriteLine(string.Format("Rang : {4}, Nom : {1}, Prénom : {2}, Temps 300= {0}, No groupe MB : {3}", pt.Classement1500, pt.Patineur.Nom, pt.Patineur.Prenom, no, rang));

                        PatineurCompe pcc = pt.PatineurCompe;
                        pcc.Rang = rang;
                        pcc.Patineur.ClassementGeneral = rang;
                        Patineur px = pcc.Patineur;
                        px.ClassementGeneral = rang;
                        db.Update(px);
                        rang += 1;
                        pcc.Groupe = gc.Groupe;
                        db.Update(pcc);
                        nb += 1;
                        if (nb == 10)
                        {
                            creer = true;
                        }
                    }
                }

                // Obtenir les patineurs minime et benjamin inscrits à la compétition
                IList<int> novice = db.Categorie.Where(z => z.AgeMin == 5 && z.AgeMax ==  5).Select(z => z.NoCategorie).ToList();
                IList<PatineurCompe> noviceCompe = lpc.Where(z => novice.Contains(z.NoCategorie)).ToList();

                var novicesTemps =
                     from patineurCompe in noviceCompe
                     join patineur in db.Patineur on patineurCompe.NoPatineur equals patineur.NoPatineur
                     select new PatineurCompeExtra()
                     {
                         Classement1500 = patineur.Classement1500,
                         Classement2000 = patineur.Classement2000,
                         Classement2500 = patineur.Classement2500,
                         Patineur = patineur,
                         PatineurCompe = patineurCompe,
                         NoPat = patineur.NoPatineur,
                         NoPatCompe = patineurCompe.NoPatCompe
                     };//{ a = patineurCompe.NoPatCompe, b = patineurCompe.Rang, c = patineur.Classement2000};

                no = 0;


                if (novicesTemps.Count() > 0)
                {
                    no = 0;
                    GroupesCompe gc = null; // = CreerGroupeCompe(db, noCompetition, "Minime Benjamin #" + no.ToString(), "6-9 ans #" + no.ToString());
                    int nb = 0;
                    bool creer = true;

                    
                    var novicesTemps300 = novicesTemps.Where(z => z.Classement1500 < 99);
                    IList<int> noPatineurTraites = novicesTemps300.Select(z => z.NoPat).ToList();
                    foreach (var pt in novicesTemps300.OrderBy(z => z.Classement1500).ThenBy(z => z.Patineur.DateNaissance).ToList())
                    {
                        if (creer)
                        {
                            creer = false;
                            nb = 0;
                            no += 1;
                            gc = CreerGroupeCompe(db, noCompetition, string.Format("5 ans #{0:00}", no), string.Format("Novices #{0:00}", no));
                        }
                        Console.WriteLine(string.Format("Rang : {4}, Nom : {1}, Prénom : {2}, Temps 300= {0}, No groupe Novices : {3}", pt.Classement1500, pt.Patineur.Nom, pt.Patineur.Prenom, no, rang));

                        PatineurCompe pcc = pt.PatineurCompe;
                        pcc.Rang = rang;
                        pcc.Patineur.ClassementGeneral = rang;
                        Patineur px = pcc.Patineur;
                        px.ClassementGeneral = rang;
                        db.Update(px);
                        rang += 1;
                        pcc.Groupe = gc.Groupe;
                        db.Update(pcc);
                        nb += 1;
                        if (nb == 10)
                        {
                            creer = true;
                        }
                    }

                    // traiter les autres minime et benjamin qui n'ont pas de temps et qui ne sont pas encore
                    // assigné à un groupe.
                    var autres = novicesTemps.Where(z => z.Classement1500 >= 99 && noPatineurTraites.Contains(z.NoPat) == false).ToList();
                    // ces patineurs sont alors triés par leur date de naissance, du plus vieux au plus petit.
                    foreach (var pt in autres.OrderBy(z => z.Patineur.DateNaissance).ToList())
                    {
                        if (creer)
                        {
                            creer = false;
                            // Si le groupe a 10 patineurs, créer un nouveau groupe
                            nb = 0;
                            no += 1;
                            gc = CreerGroupeCompe(db, noCompetition, string.Format("5 ans #{0:00}", no), string.Format("Novices #{0:00}", no));
                        }

                        Console.WriteLine(string.Format("Rang : {4}, Nom : {1}, Prénom : {2}, Temps 300= {0}, No groupe Novices : {3}", pt.Classement1500, pt.Patineur.Nom, pt.Patineur.Prenom, no, rang));

                        PatineurCompe pcc = pt.PatineurCompe;
                        pcc.Rang = rang;
                        pcc.Patineur.ClassementGeneral = rang;
                        Patineur px = pcc.Patineur;
                        px.ClassementGeneral = rang;
                        db.Update(px);
                        rang += 1;
                        pcc.Groupe = gc.Groupe;
                        db.Update(pcc);
                        nb += 1;
                        if (nb == 10)
                        {
                            creer = true;
                        }
                    }
                }
            }
        }

        private static GroupesCompe CreerGroupeCompe(DBPatinVitesse db, int noCompetition, string nom, string desc)
        {
            GroupesCompe gc = new GroupesCompe();
            gc.NoCompetition = noCompetition;
            gc.Groupe = nom;
            gc.DescriptionGroupe = desc;
            gc.SequencePointage = 1;
            gc.Regroupement = 0;
            db.Insert(gc);
            return gc;
        }


        private static void RegrouperPatineur(DBPatinVitesse db)
        {
              db.Competition.ToList().ForEach(z => Console.WriteLine(string.Format("No : {0}, Nom : {1}", z.NoCompetition, z.Lieu)));
            Console.WriteLine("Entrer le numéro de compétition");
            string noStrCompe = Console.ReadLine();
            int noCompeti;
            if (int.TryParse(noStrCompe, out noCompeti))
            {
                foreach (PatineurCompe pc in db.PatineurCompe)
                {
                    Console.WriteLine(pc.NoPatineur);
                }

                foreach( ProgCourses pcx in db.ProgCourses)
                {
                    Console.WriteLine(pcx.DemiFinale);
                }

                foreach (ProgCourses1 pcx in db.ProgCourses1)
                {
                    Console.WriteLine(pcx.NoCompetition);
                }

                foreach (Vague1 o in db.Vague1)
                {
                    Console.WriteLine(o.Distances);
                }

                foreach (Vagues o in db.Vagues)
                    {
                    Console.WriteLine(o.Seq);
                }

                foreach (GroupesCompe o in db.GroupesCompe)
                {
                    Console.WriteLine(o.IDGroupesCompe);
                }

                

            }

        }

        private static void PoursuivreInscription(DBPatinVitesse db, Dictionary<string, PubSportInscrit> dictInscrit)
        {
            Console.WriteLine(string.Format("Patineurs dans la liste {0}", dictInscrit.Count()));
            foreach (PubSportInscrit inscrit in dictInscrit.Values)
            {
                Console.WriteLine(string.Format("{0};{1};{2};{3};{4}", inscrit.LastName, inscrit.FirstName, inscrit.Birthday, inscrit.Gender, inscrit.OrganisationName));
            }
                db.Competition.ToList().ForEach(z => Console.WriteLine(string.Format("No : {0}, Nom : {1}", z.NoCompetition, z.Lieu)));
            Console.WriteLine("Entrer le numéro de compétition");
            string noStrCompe = Console.ReadLine();
            int noCompeti;
            if (int.TryParse(noStrCompe, out noCompeti))
            {
                foreach (PubSportInscrit inscrit in dictInscrit.Values)
                {
                    int differenceNom = 0;
                    inscrit.FirstName = inscrit.FirstName.TrimEnd().TrimStart();
                    inscrit.LastName = inscrit.LastName.TrimStart().TrimEnd();
                    DateTime dateNaisse;
                    DateTime.TryParse(inscrit.Birthday, out dateNaisse);

                    IEnumerable<Patineur> ep = db.Patineur.ToList().Where(z => z.Nom.RemoveDiacritics().ToUpper() == inscrit.LastName.RemoveDiacritics().ToUpper()).ToList();
                    //IEnumerable<Patineur> ep = db.Patineur.Where(z => z.Nom.RemoveDiacritics() == elements[0].RemoveDiacritics());
                    IEnumerable<Patineur> ep1 = ep.Where(z => z.Prenom.RemoveDiacritics().ToUpper() == inscrit.FirstName.RemoveDiacritics().ToUpper()).ToList();
                    IEnumerable<Patineur> ep2 = ep1.Where(z => z.DateNaissance.Day == dateNaisse.Day && z.DateNaissance.Month == dateNaisse.Month && z.DateNaissance.Year == dateNaisse.Year);
                    if (ep1.Count() == 0)
                    {
                        IEnumerable<Patineur> ep2x = db.Patineur.ToList().Where(z => z.Nom.RemoveDiacritics().Replace(" ",string.Empty).Replace("-",string.Empty).ToUpper() == inscrit.LastName.RemoveDiacritics().Replace(" ",string.Empty).Replace("-",string.Empty).ToUpper()).ToList();
                        IEnumerable<Patineur> ep21 = ep.Where(z => z.Prenom.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper() == inscrit.FirstName.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper()).ToList();
                        if (ep21.Count() == 1)
                        {
                            Patineur ptr1 = ep21.First();
                            Console.WriteLine(string.Format("Les noms ne correspondent pas tout à fait <{0}> <{1}> comparé à <{2}> <{3}>",ptr1.Nom, ptr1.Prenom, inscrit.LastName, inscrit.FirstName));
                            Console.WriteLine(string.Format("Voulez-vous utiliser le patineur {0} {1}", ptr1.Nom, ptr1.Prenom));
                            string rep = Console.ReadLine();
                            differenceNom = 1;
                            if (!string.IsNullOrEmpty(rep) && rep.ToUpper().Substring(0, 1) == "O")
                            {
                                differenceNom = 2;
                                ep1 = ep2;
                                Console.WriteLine(string.Format("Voulez-vous mettre à jour le nom du patineur <{0}> <{1}>", ptr1.Nom, ptr1.Prenom));
                                rep = Console.ReadLine();
                                if (!string.IsNullOrEmpty(rep) && rep.ToUpper().Substring(0, 1) == "O")
                                {
                                    differenceNom = 3;
                                    ptr1.Nom = inscrit.LastName;
                                    ptr1.Prenom = inscrit.FirstName;
                                    db.Update(ptr1);
                                }

                                ep = db.Patineur.ToList().Where(z => z.Nom.RemoveDiacritics().ToUpper() == inscrit.LastName.RemoveDiacritics().ToUpper()).ToList();
                                ep1 = ep.Where(z => z.Prenom.RemoveDiacritics().ToUpper() == inscrit.FirstName.RemoveDiacritics().ToUpper()).ToList();
                                ep2 = ep1.Where(z => z.DateNaissance.Day == dateNaisse.Day && z.DateNaissance.Month == dateNaisse.Month && z.DateNaissance.Year == dateNaisse.Year);
                            }
         
                        }
                    }

                    if (ep1.Count() == 0)
                    {
                        Dictionary<Patineur, int> dictCmp = new Dictionary<Patineur, int>();
                        foreach (Patineur p in db.Patineur)
                        {
                            int d1 = LevenshteinDistance.Compute(p.Nom.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper(), inscrit.LastName.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper());
                            int d2 = LevenshteinDistance.Compute(p.Prenom.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper(), inscrit.FirstName.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper());
                            dictCmp.Add(p, d1 + d2);
                        }

                        int diff = 999;
                        Patineur patPotentiel = null;
                        if (dictCmp.Count() > 0)
                        { 
                            diff = dictCmp.OrderBy(z => z.Value).First().Value;
                            patPotentiel = dictCmp.OrderBy(z => z.Value).First().Key;
                        }
                         
                        if (diff < 5)
                        {
                            differenceNom = 5;
                            Console.WriteLine(string.Format("Les noms ne correspondent pas tout à fait  BD:<{0}> <{1}> comparé à Inscription:<{2}> <{3}>", patPotentiel.Nom, patPotentiel.Prenom, inscrit.LastName, inscrit.FirstName));
                            Console.WriteLine(string.Format("Voulez-vous utiliser le patineur {0} {1}", patPotentiel.Nom, patPotentiel.Prenom));
                            string rep = Console.ReadLine();
                            if (!string.IsNullOrEmpty(rep) && rep.ToUpper().Substring(0, 1) == "O")
                            {
                                differenceNom = 6;
                                ep1 = ep2;
                                Console.WriteLine(string.Format("Voulez-. mettre à jour le nom du patineur <{0}> <{1}>", patPotentiel.Nom, patPotentiel.Prenom));
                                rep = Console.ReadLine();
                                if (!string.IsNullOrEmpty(rep) && rep.ToUpper().Substring(0, 1) == "O")
                                {
                                    differenceNom = 7;
                                    patPotentiel.Nom = inscrit.LastName;
                                    patPotentiel.Prenom = inscrit.FirstName;
                                    db.Update(patPotentiel);
                                }
                                else
                                {
                                    inscrit.LastName = patPotentiel.Nom;
                                    inscrit.FirstName = patPotentiel.Prenom;
                                }

                                ep = db.Patineur.ToList().Where(z => z.Nom.RemoveDiacritics().ToUpper() == inscrit.LastName.RemoveDiacritics().ToUpper()).ToList();
                                ep1 = ep.Where(z => z.Prenom.RemoveDiacritics().ToUpper() == inscrit.FirstName.RemoveDiacritics().ToUpper()).ToList();
                                ep2 = ep1.Where(z => z.DateNaissance.Day == dateNaisse.Day && z.DateNaissance.Month == dateNaisse.Month && z.DateNaissance.Year == dateNaisse.Year);
                            }

                        }
                    }
                    if (ep2.Count() == 0 && ep1.Count() == 1)
                    {
                        differenceNom = 8;
                        Patineur ptr = ep1.First();
                        Console.WriteLine(string.Format("Un patineur a été retrouvé mais sa date de fête ne correspond pas {0} vs {1}", ptr.DateNaissance, inscrit.Birthday));
                        Console.WriteLine(string.Format("Voulez-vous utiliser le patineur {0} {1}", ptr.Nom, ptr.Prenom));
                        string rep = Console.ReadLine();
                        if (!string.IsNullOrEmpty(rep) && rep.ToUpper().Substring(0, 1) == "O")
                        {
                            differenceNom = 9;
                            ep2 = ep1;
                            Console.WriteLine(string.Format("Voulez-vous mettre à jour la date de fête du patineur {0} {1}", ptr.Nom, ptr.Prenom));
                            rep = Console.ReadLine();
                            if (!string.IsNullOrEmpty(rep) && rep.ToUpper().Substring(0, 1) == "O")
                            {
                                differenceNom = 10;
                                ptr.DateNaissance = dateNaisse;
                                db.Update(ptr);
                            }
                        }
                    }
                    bool inscrire = false;
                    if (differenceNom == 0 || differenceNom == 3 || differenceNom == 7 || differenceNom == 10)
                    {
                        inscrire = true;
                    }
                    else
                    {
                        Console.WriteLine("Il existe des différences entre");
                        if (ep2.Count() == 0)
                        {
                            Console.WriteLine("Attention! Un nouveau patineur sera créé");
                        }
                        else {
                            Patineur px = ep2.First();
                            Console.WriteLine(string.Format("Trouvé :{0}, {1},{2}", px.Nom, px.Prenom, px.DateNaissance));
                        }
                            Console.WriteLine(string.Format("Trouvé :{0}, {1},{2}", inscrit.LastName, inscrit.FirstName, inscrit.Birthday));
                        Console.WriteLine("Voulez vous procéder à l'inscription automatique? (0/N)");
                        string input = Console.ReadLine();
                        if (!string.IsNullOrEmpty(input) && input.ToUpper().StartsWith("O"))
                        {
                            inscrire = true;
                        }
                    }

                    if (ep2.Count() == 0 && inscrire)
                    {
                        
                        Patineur p = new Patineur();
                        p.Nom = inscrit.LastName.ToUpper();
                        p.Prenom = inscrit.FirstName;
                        p.Classement = 999;
                        p.Classement1000 = 999;
                        p.Classement1500 = 999;
                        p.Classement2000 = 999;
                        p.Classement2000 = 999;
                        p.Classement2500 = 999;
                        p.ClassementGeneral = 999;
                        p.CodePat = p.Nom.Substring(0, 3) + p.Prenom.Substring(0, 2);
                        p.CodePat = p.CodePat.ToUpper();
                        if(db.Patineur.SingleOrDefault(z=> z.CodePat == p.CodePat)!= null)
                        {
                            int nb = db.Patineur.Count(z => z.CodePat.StartsWith(p.CodePat));
                            p.CodePat = p.CodePat + nb.ToString();
                        }

                        p.DateNaissance = dateNaisse;
                        p.Division = "Régional";
                        Club c = db.Club.SingleOrDefault(z => z.NomClub == inscrit.OrganisationName);
                        if (c != null)
                        {
                            p.NoClub = c.NoClub;
                        }
                        else
                        {
                            bool fait = false;
                            foreach (Club cc in db.Club)
                            {

                                if (inscrit.OrganisationName.Contains(cc.NomClub))
                                {
                                    p.NoClub = cc.NoClub;
                                    fait = true;
                                    break;
                                }
                                else
                                {
                                    if (inscrit.OrganisationName.Contains("Sainte"))
                                    {
                                        if (inscrit.OrganisationName.Contains(cc.NomClub.Replace("Ste", "Sainte")))
                                        {
                                            fait = true;
                                            p.NoClub = cc.NoClub;
                                            break;
                                        }
                                    }
                                    else if (inscrit.OrganisationName.Contains("Saint"))
                                    {
                                        if (inscrit.OrganisationName.Contains(cc.NomClub.Replace("St", "Saint")))
                                        {
                                            fait = true;
                                            p.NoClub = cc.NoClub;
                                            break;
                                        }
                                    }
                                    else if (inscrit.OrganisationName.Contains("Labaie"))
                                    {
                                        if (inscrit.OrganisationName.Contains(cc.NomClub.Replace("La Baie", "Labaie")))
                                        {
                                            fait = true;
                                            p.NoClub = cc.NoClub;
                                            break;
                                        }
                                    }
                                }   
                            }
                            if (!fait)
                            {
                                System.Diagnostics.Debug.Write("test");
                            }
                        }

                        p.Sexe = inscrit.Gender.ToUpper();
                        TimeSpan ts = new DateTime(2016, 06, 30) - dateNaisse;
                        double jours = ts.TotalDays;
                        double ans = jours / 365.2425;
                        Decimal dd = System.Convert.ToDecimal(ans);
                        decimal an = Math.Truncate(dd);
                        Categorie cat = db.Categorie.SingleOrDefault(z => z.AgeMax == an && z.AgeMin == an);
                        if (cat == null)
                        {
                            IEnumerable<Categorie> lcat = db.Categorie.Where(z => z.AgeMax >= an && z.AgeMin <= an);
                            if (lcat.Count() > 0)
                            {
                                cat = lcat.First();
                            }
                            else
                            {
                                Console.WriteLine(string.Format("Vérifier l'age du patineur {0}, {1}, {2}", p.Nom, p.Prenom, p.DateNaissance));
                            }

                        }
                        p.NoCategorie = cat.NoCategorie;
                        p.CategCalc = true;
                        db.Insert(p);
                        ep2 = db.Patineur.Where(z => z.CodePat == p.CodePat);
                        Console.WriteLine(String.Format("Le patineur {0}, {1}, {2} a été créé dans la BD", p.Nom, p.Prenom, p.CodePat));
                    }
                    else if (ep2.Count() > 0 && inscrire)
                    {
                        Patineur p = ep2.First();
                        if (p != null)
                        {
                            // On inscrit le patineur                                    
                            int max = db.PatineurCompe.Count();
                            if (max > 0)
                            {
                                max = db.PatineurCompe.Max(z => z.NoPatCompe);
                            }
                            if (db.PatineurCompe.SingleOrDefault(z => z.NoPatineur == p.NoPatineur && z.NoCompetition == noCompeti) != null)
                            {
                                Console.WriteLine(string.Format("Le Patineur {0} {1} du club {2} Était déjè inscrit", p.Nom, p.Prenom, db.Club.SingleOrDefault(z => z.NoClub == p.NoClub).NomClub));
                            }
                            else {
                                PatineurCompe pc = new PatineurCompe();
                                pc.NoPatineur = p.NoPatineur;
                                pc.NoPatCompe = max + 1;
                                pc.Rang = System.Convert.ToInt32(p.Classement);
                                pc.Retirer = false;
                                pc.Si_Regroup_Classement = 2;
                                pc.Division = "Régional";
                                pc.NoCompetition = noCompeti;
                                pc.NoCategorie = p.NoCategorie;
                                //pc.Categorie = db.Categorie.SingleOrDefault(z => z.NoCategorie == p.NoCategorie).Nom;
                                pc.NoClub = p.NoClub;
                                db.Insert(pc);
                                Console.WriteLine(string.Format("Patineur {0} {1} du club {2} Inscrit", p.Nom, p.Prenom, db.Club.SingleOrDefault(z => z.NoClub == p.NoClub).NomClub));
                            }
                            }
                        else
                        {
                            Console.WriteLine(string.Format("Patineur {0}, {1} non trouvé, procéder manuellement à l'inscription", inscrit.FirstName, inscrit.LastName));
                        }
                    }
                    else
                    {

                        Console.WriteLine(string.Format("Patineur {0}, {1} non trouvé, procéder manuellement à l'inscription", inscrit.FirstName, inscrit.LastName));
                    }

                }
            }
        }

        /// <summary>
        /// Poursuite de l'inscription web pour les jeux du québec
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dictInscrit"></param>
        private static void PoursuivreInscriptionJQ(DBPatinVitesse db, Dictionary<string, PubSportInscrit> dictInscrit)
        {
            db.Competition.ToList().ForEach(z => Console.WriteLine(string.Format("No : {0}, Nom : {1}", z.NoCompetition, z.Lieu)));
            Console.WriteLine("Entrer le numéro de compétition");
            string noStrCompe = Console.ReadLine();
            int noCompeti;
            if (int.TryParse(noStrCompe, out noCompeti))
            {
                foreach (PubSportInscrit inscrit in dictInscrit.Values)
                {
                    inscrit.FirstName = inscrit.FirstName.TrimEnd().TrimStart();
                    inscrit.LastName = inscrit.LastName.TrimStart().TrimEnd();
                    DateTime dateNaisse;
                    DateTime.TryParse(inscrit.Birthday, out dateNaisse);

                    IEnumerable<Patineur> ep = db.Patineur.ToList().Where(z => z.Nom.RemoveDiacritics().ToUpper() == inscrit.LastName.RemoveDiacritics().ToUpper()).ToList();
                    //IEnumerable<Patineur> ep = db.Patineur.Where(z => z.Nom.RemoveDiacritics() == elements[0].RemoveDiacritics());
                    IEnumerable<Patineur> ep1 = ep.Where(z => z.Prenom.RemoveDiacritics().ToUpper() == inscrit.FirstName.RemoveDiacritics().ToUpper()).ToList();
                    IEnumerable<Patineur> ep2 = ep1.Where(z => z.DateNaissance.Day == dateNaisse.Day && z.DateNaissance.Month == dateNaisse.Month && z.DateNaissance.Year == dateNaisse.Year);
                    if (ep1.Count() == 0)
                    {
                        IEnumerable<Patineur> ep2x = db.Patineur.ToList().Where(z => z.Nom.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper() == inscrit.LastName.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper()).ToList();
                        IEnumerable<Patineur> ep21 = ep.Where(z => z.Prenom.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper() == inscrit.FirstName.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper()).ToList();
                        if (ep21.Count() == 1)
                        {
                            Patineur ptr1 = ep21.First();
                            Console.WriteLine(string.Format("Les noms ne correspondent pas tout à fait <{0}> <{1}> comparé à <{2}> <{3}>", ptr1.Nom, ptr1.Prenom, inscrit.LastName, inscrit.FirstName));
                            Console.WriteLine(string.Format("Voulez-vous utiliser le patineur {0} {1}", ptr1.Nom, ptr1.Prenom));
                            string rep = Console.ReadLine();
                            if (!string.IsNullOrEmpty(rep) && rep.ToUpper().Substring(0, 1) == "O")
                            {
                                ep1 = ep2;
                                Console.WriteLine(string.Format("Voulez-vous mettre à jour le nom du patineur <{0}> <{1}>", ptr1.Nom, ptr1.Prenom));
                                rep = Console.ReadLine();
                                if (!string.IsNullOrEmpty(rep) && rep.ToUpper().Substring(0, 1) == "O")
                                {
                                    ptr1.Nom = inscrit.LastName;
                                    ptr1.Prenom = inscrit.FirstName;
                                    db.Update(ptr1);
                                }

                                ep = db.Patineur.ToList().Where(z => z.Nom.RemoveDiacritics().ToUpper() == inscrit.LastName.RemoveDiacritics().ToUpper()).ToList();
                                ep1 = ep.Where(z => z.Prenom.RemoveDiacritics().ToUpper() == inscrit.FirstName.RemoveDiacritics().ToUpper()).ToList();
                                ep2 = ep1.Where(z => z.DateNaissance.Day == dateNaisse.Day && z.DateNaissance.Month == dateNaisse.Month && z.DateNaissance.Year == dateNaisse.Year);
                            }

                        }
                    }

                    if (ep1.Count() == 0)
                    {
                        Dictionary<Patineur, int> dictCmp = new Dictionary<Patineur, int>();
                        foreach (Patineur p in db.Patineur)
                        {
                            int d1 = LevenshteinDistance.Compute(p.Nom.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper(), inscrit.LastName.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper());
                            int d2 = LevenshteinDistance.Compute(p.Prenom.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper(), inscrit.FirstName.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper());
                            dictCmp.Add(p, d1 + d2);
                        }
                        int diff = dictCmp.OrderBy(z => z.Value).First().Value;
                        Patineur patPotentiel = dictCmp.OrderBy(z => z.Value).First().Key;
                        if (diff < 5)
                        {
                            Console.WriteLine(string.Format("Les noms ne correspondent pas tout à fait  BD:<{0}> <{1}> comparé à Inscription:<{2}> <{3}>", patPotentiel.Nom, patPotentiel.Prenom, inscrit.LastName, inscrit.FirstName));
                            Console.WriteLine(string.Format("Voulez-vous utiliser le patineur {0} {1}", patPotentiel.Nom, patPotentiel.Prenom));
                            string rep = Console.ReadLine();
                            if (!string.IsNullOrEmpty(rep) && rep.ToUpper().Substring(0, 1) == "O")
                            {
                                ep1 = ep2;
                                Console.WriteLine(string.Format("Voulez-. mettre à jour le nom du patineur <{0}> <{1}>", patPotentiel.Nom, patPotentiel.Prenom));
                                rep = Console.ReadLine();
                                if (!string.IsNullOrEmpty(rep) && rep.ToUpper().Substring(0, 1) == "O")
                                {
                                    patPotentiel.Nom = inscrit.LastName;
                                    patPotentiel.Prenom = inscrit.FirstName;
                                    db.Update(patPotentiel);
                                }

                                ep = db.Patineur.ToList().Where(z => z.Nom.RemoveDiacritics().ToUpper() == inscrit.LastName.RemoveDiacritics().ToUpper()).ToList();
                                ep1 = ep.Where(z => z.Prenom.RemoveDiacritics().ToUpper() == inscrit.FirstName.RemoveDiacritics().ToUpper()).ToList();
                                ep2 = ep1.Where(z => z.DateNaissance.Day == dateNaisse.Day && z.DateNaissance.Month == dateNaisse.Month && z.DateNaissance.Year == dateNaisse.Year);
                            }

                        }
                    }
                    if (ep2.Count() == 0 && ep1.Count() == 1)
                    {
                        Patineur ptr = ep1.First();
                        Console.WriteLine(string.Format("Un patineur a été retrouvé mais sa date de fête ne correspond pas {0} vs {1}", ptr.DateNaissance, inscrit.Birthday));
                        Console.WriteLine(string.Format("Voulez-vous utiliser le patineur {0} {1}", ptr.Nom, ptr.Prenom));
                        string rep = Console.ReadLine();
                        if (!string.IsNullOrEmpty(rep) && rep.ToUpper().Substring(0, 1) == "O")
                        {
                            ep2 = ep1;
                            Console.WriteLine(string.Format("Voulez-vous mettre à jour la date de fête du patineur {0} {1}", ptr.Nom, ptr.Prenom));
                            rep = Console.ReadLine();
                            if (!string.IsNullOrEmpty(rep) && rep.ToUpper().Substring(0, 1) == "O")
                            {
                                ptr.DateNaissance = dateNaisse;
                                db.Update(ptr);
                            }
                        }
                    }

                    if (ep2.Count() == 0)
                    {
                        try
                        {

                       
                        // Le patineur n'a pas été retrouvé, cependant on va le chercher
                        // dans le fichier CSV de la fédération
                        Dictionary<PatFedeTemps, int> dictCmpFed = new Dictionary<PatFedeTemps, int>();
                        foreach (PatFedeTemps pfed in ListePatFede)
                        {
                            int d1 = LevenshteinDistance.Compute(pfed.Nom.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper(), inscrit.LastName.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper());
                            int d2 = LevenshteinDistance.Compute(pfed.Prenom.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper(), inscrit.FirstName.RemoveDiacritics().Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper());
                            dictCmpFed.Add(pfed, d1 + d2);
                        }

                        int diff = dictCmpFed.OrderBy(z => z.Value).First().Value;
                        PatFedeTemps patPotentiel = dictCmpFed.OrderBy(z => z.Value).First().Key;
                        if (diff < 5)
                        {
                            Patineur p = new Patineur();
                            p.Nom = patPotentiel.Nom.ToUpper().TrimEnd().TrimStart();
                            p.Prenom = patPotentiel.Prenom.TrimEnd().TrimStart();
                            p.Classement = 999;
                            p.Classement1000 = patPotentiel.Temps1000;
                            p.Classement1500 = 999;
                            if (patPotentiel.Temps400 == 0)
                            {
                                p.Classement2000 = 999;
                            }
                            else
                            {
                                p.Classement2000 = patPotentiel.Temps400;
                            }
                            
                            p.Classement2000 = 999;
                            p.Classement2500 = 999;
                            p.ClassementGeneral = 999;
                            p.CodePat = p.Nom.Substring(0, 3) + p.Prenom.Substring(0, 2);
                            p.CodePat = p.CodePat.ToUpper();
                            if (db.Patineur.SingleOrDefault(z => z.CodePat == p.CodePat) != null)
                            {
                                int nb = db.Patineur.Count(z => z.CodePat.StartsWith(p.CodePat));
                                p.CodePat = p.CodePat + nb.ToString();
                            }

                            if (patPotentiel.Age == 11)
                            {
                                p.NoCategorie = 15;
                            }
                            else if (patPotentiel.Age == 12)
                            {
                                p.NoCategorie = 5;
                            }
                            else if (patPotentiel.Age == 13)
                            {
                                p.NoCategorie = 16;
                            }
                            else if (patPotentiel.Age == 14)
                            {
                                p.NoCategorie = 1;
                            }

                            p.DateNaissance = dateNaisse;
                            p.Division = "Interrégional";
                            Club c = db.Club.SingleOrDefault(z => z.NomClub == inscrit.OrganisationName);
                            if (c != null)
                            {
                                p.NoClub = c.NoClub;
                            }
                            else
                            {
                                foreach (Club cc in db.Club)
                                {
                                    if (inscrit.OrganisationName.Contains(cc.NomClub))
                                    {
                                        p.NoClub = cc.NoClub;
                                        break;
                                    }
                                }
                            }

                            p.Sexe = inscrit.Gender.ToUpper();
                            TimeSpan ts = new DateTime(2016, 06, 30) - dateNaisse;
                            double jours = ts.TotalDays;
                            double ans = jours / 365.2425;
                            Decimal dd = System.Convert.ToDecimal(ans);
                            decimal an = Math.Truncate(dd);
                                // l'age est fixe à 1 année par groupe maintenant
                            Categorie cat = db.Categorie.SingleOrDefault(z => z.AgeMax == an && z.AgeMin == an);
                            p.NoCategorie = cat.NoCategorie;
                            p.CategCalc = true;
                            db.Insert(p);
                            ep2 = db.Patineur.Where(z => z.CodePat == p.CodePat);
                            Console.WriteLine(String.Format("Le patineur {0}, {1}, {2} a été créé dans la BD avec le temps 1000m = {3}", p.Nom, p.Prenom, p.CodePat, p.Classement1000));
                        }

                        else {

                            Patineur p = new Patineur();
                            p.Nom = inscrit.LastName.ToUpper().TrimEnd().TrimStart();
                            p.Prenom = inscrit.FirstName.TrimEnd().TrimStart();
                            p.Classement = 999;
                            p.Classement1000 = 999;
                            p.Classement1500 = 999;
                            p.Classement2000 = 999;
                            p.Classement2000 = 999;
                            p.Classement2500 = 999;
                            p.ClassementGeneral = 999;
                            p.CodePat = p.Nom.Substring(0, 3) + p.Prenom.Substring(0, 2);
                            p.CodePat = p.CodePat.ToUpper();
                            if (db.Patineur.SingleOrDefault(z => z.CodePat == p.CodePat) != null)
                            {
                                int nb = db.Patineur.Count(z => z.CodePat.StartsWith(p.CodePat));
                                p.CodePat = p.CodePat + nb.ToString();
                            }

                            p.DateNaissance = dateNaisse;
                            p.Division = "Interrégional";
                                string club = inscrit.OrganisationName;
                                if (inscrit.OrganisationName == "CEPVRQ")
                                {
                                    club = "Québec";
                                }
                            Club c = db.Club.SingleOrDefault(z => z.NomClub == club);
                            if (c != null)
                            {
                                p.NoClub = c.NoClub;
                            }
                            else
                            {
                                foreach (Club cc in db.Club)
                                {
                                    if (club.Contains(cc.NomClub))
                                    {
                                        p.NoClub = cc.NoClub;
                                        break;
                                    }
                                }
                            }

                            p.Sexe = inscrit.Gender.ToUpper();
                            TimeSpan ts = new DateTime(2016, 06, 30) - dateNaisse;
                            double jours = ts.TotalDays;
                            double ans = jours / 365.2425;
                            Decimal dd = System.Convert.ToDecimal(ans);
                            decimal an = Math.Truncate(dd);
                            Categorie cat = db.Categorie.SingleOrDefault(z => z.AgeMax == an && z.AgeMin == an);
                            p.NoCategorie = cat.NoCategorie;
                            p.CategCalc = true;
                            db.Insert(p);
                            ep2 = db.Patineur.Where(z => z.CodePat == p.CodePat);
                            Console.WriteLine(String.Format("Le patineur {0}, {1}, {2} a été créé dans la BD", p.Nom, p.Prenom, p.CodePat));
                        }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            Console.ReadLine();
                        }
                    }
                    if (ep2.Count() > 0)
                    {
                        Patineur p = ep2.First();
                        if (p != null)
                        {
                            // On inscrit le patineur                                    
                            int max = db.PatineurCompe.Count();
                            if (max > 0)
                            {
                                max = db.PatineurCompe.Max(z => z.NoPatCompe);
                            }
                            if (db.PatineurCompe.SingleOrDefault(z => z.NoPatineur == p.NoPatineur && z.NoCompetition == noCompeti) != null)
                            {
                                Console.WriteLine(string.Format("Le Patineur {0} {1} du club {2} Était déjè inscrit", p.Nom, p.Prenom, db.Club.SingleOrDefault(z => z.NoClub == p.NoClub).NomClub));
                            }
                            else {
                                PatineurCompe pc = new PatineurCompe();
                                pc.NoPatineur = p.NoPatineur;
                                pc.NoPatCompe = max + 1;
                                pc.Rang = System.Convert.ToInt32(p.Classement);
                                pc.Retirer = false;
                                pc.Si_Regroup_Classement = 2;
                                pc.Division = "Interrégional";
                                pc.NoCompetition = noCompeti;
                                pc.NoCategorie = p.NoCategorie;
                                //pc.Categorie = db.Categorie.SingleOrDefault(z => z.NoCategorie == p.NoCategorie).Nom;
                                pc.NoClub = p.NoClub;
                                db.Insert(pc);
                                Console.WriteLine(string.Format("Patineur {0} {1} du club {2} Inscrit", p.Nom, p.Prenom, db.Club.SingleOrDefault(z => z.NoClub == p.NoClub).NomClub));
                            }
                        }
                        else
                        {
                            Console.WriteLine(string.Format("Patineur {0}, {1} non trouvé, procéder manuellement à l'inscription", inscrit.FirstName, inscrit.LastName));
                        }
                    }
                    else
                    {

                        Console.WriteLine(string.Format("Patineur {0}, {1} non trouvé, procéder manuellement à l'inscription", inscrit.FirstName, inscrit.LastName));
                    }

                }
            }
        }
        private class PubSportInscrit
        {
            public string InscriptionType { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Birthday { get; set; }
            public string Gender { get; set; }
            public string RegistrationDate { get; set; }
            public string OrganisationName { get; set; }
            public string TeamDescription { get; set; }
            public string RelativeOneName { get; set; }
            public string Category { get; set; }

        }

        public static void MiseAJourTempsPatineur(DBPatinVitesse db)
        {
            Console.WriteLine("Entrer le code de patineur");
            string codePat = Console.ReadLine();

            while (!string.IsNullOrEmpty(codePat))
            {
                Patineur pat = db.Patineur.SingleOrDefault(z => z.CodePat == codePat);
                if (pat != null)
                {
                    Console.WriteLine("Nom : {0}, Prénom {1}", pat.Nom, pat.Prenom);
                    Console.WriteLine(string.Format("Temps 300 : {0}", pat.Classement1500));
                    Console.WriteLine("Entrer le nouveau temps 300");
                    string newTemps = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newTemps))
                    {
                        double ntemps = double.MinValue;
                        if (!double.TryParse(newTemps, out ntemps))
                        {
                            if (!double.TryParse(newTemps.Replace(",", "."), out ntemps))
                            {
                                double.TryParse(newTemps.Replace(".", ","), out ntemps);
                            }
                        }
                        pat.Classement1500 = ntemps;
                        Console.WriteLine("Nouveau temps 300 {0}", ntemps);
                        db.Update(pat);
                    }
                    Console.WriteLine("Nom : {0}, Prénom {1}", pat.Nom, pat.Prenom);
                    Console.WriteLine(string.Format("Temps 400 : {0}", pat.Classement2000));
                    Console.WriteLine("Entrer le nouveau temps 400");
                    newTemps = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newTemps))
                    {
                        double ntemps = double.MinValue;
                        if (!double.TryParse(newTemps, out ntemps))
                        {
                            if (!double.TryParse(newTemps.Replace(",", "."), out ntemps))
                            {
                                double.TryParse(newTemps.Replace(".", ","), out ntemps);
                            }
                        }
                        pat.Classement2000 = ntemps;
                        Console.WriteLine("Nouveau temps 400 : {0}", ntemps);
                        db.Update(pat);
                    }
                    Console.WriteLine("Nom : {0}, Prénom {1}", pat.Nom, pat.Prenom);
                    Console.WriteLine(string.Format("Temps 800 : {0}", pat.Classement2500));
                    Console.WriteLine("Entrer le nouveau temps 800");
                    newTemps = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newTemps))
                    {
                        double ntemps = double.MinValue;
                        if (!double.TryParse(newTemps, out ntemps))
                        {
                            if (!double.TryParse(newTemps.Replace(",", "."), out ntemps))
                            {
                                double.TryParse(newTemps.Replace(".", ","), out ntemps);
                            }
                        }
                        pat.Classement2500 = ntemps;
                        Console.WriteLine("Nouveau temps 800 : {0}", ntemps);
                        db.Update(pat);
                    }
                }
                Console.WriteLine("Entrer le code de patineur");
                codePat = Console.ReadLine();

            }
        }




        public static void ExtrairePatineur(DBPatinVitesse db)
        {
            Console.WriteLine("Entrer le nom de fichier d'extraction");
            string nomFichier = Console.ReadLine();
            Console.WriteLine("Entrer les numéros de clubs séparés par des virgules");
            db.Club.ToList().ForEach(z => Console.WriteLine("{0}:{1}", z.NoClub, z.NomClub));
            string[] clubs = Console.ReadLine().Split(',');
            List<int> clubsint = new List<int>();
            foreach (string c in clubs)
            {
                clubsint.Add(System.Convert.ToInt32(c));
            }

            using (StreamWriter sr = new StreamWriter(nomFichier))
            {
                foreach (Patineur p in db.Patineur)
                {
                    if (clubsint.Contains(p.NoClub))
                    {
                        Console.WriteLine("{0};{1};{2};{3};{4:yyyy-MM-dd};{5};{6};{7}", p.NoClub, p.CodePat, p.Nom, p.Prenom, p.DateNaissance, p.Classement1500, p.Classement2000, p.Classement2500);
                        sr.WriteLine("{0};{1};{2};{3};{4:yyyy-MM-dd};{5};{6};{7}", p.NoClub, p.CodePat, p.Nom, p.Prenom, p.DateNaissance, p.Classement1500, p.Classement2000, p.Classement2500);
                    }
                }
            }



        }

        public static void SupprimerInter(DBPatinVitesse db)
        {
            Console.WriteLine("Entrer le nom de fichier des patineurs Inter");
            string nomFichier = Console.ReadLine();

            List<int> listC = db.Categorie.Where(z => z.Nom == "12 ans" || z.Nom == "13 ans (Junior C ISU)" || z.Nom == "11 ans"  || z.Nom == "10 ans" || z.Nom == "14 ans (Junior C ISU)").Select(z => z.NoCategorie).ToList<int>();
            
            using (StreamWriter sr = new StreamWriter(nomFichier))
            {
                foreach (Patineur p in db.Patineur.ToList())
                {
                    if (listC.Contains(p.NoCategorie))
                    {
                        if (p.Sexe == "M")
                        {
                            if (p.Classement2000 <= 0.495 || p.Classement2500 <= 1.42)
                            {
                                Console.WriteLine("le patineur {0}, {1} est de niveau interrégional", p.Nom, p.Prenom);
                                sr.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7}", p.NoClub, p.CodePat, p.Nom, p.Prenom, p.DateNaissance, p.Classement1500, p.Classement2000, p.Classement2500);
                                db.Delete<Patineur>(p);
                            }
                        }
                        else {
                            if (p.Classement2000 <= 0.50 || p.Classement2500 <= 1.43)
                            {
                                Console.WriteLine("La patineuse {0}, {1} est de niveau interrégional", p.Nom, p.Prenom);
                                sr.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7}", p.NoClub, p.CodePat, p.Nom, p.Prenom, p.DateNaissance, p.Classement1500, p.Classement2000, p.Classement2500);
                                db.Delete<Patineur>(p);
                            }
                        }
                    }

                }
            }



        }

        /// <summary>
        /// Calculer le rang des patineurs dans la BD
        /// </summary>
        /// <param name="db"></param>
        public static void CalculerRang(DBPatinVitesse db)
        {
            // Assigner aux patineur un classement de 9999;
            List<Patineur> lp0 = db.Patineur.ToList();
            foreach (Patineur p in lp0)
            {
                p.ClassementGeneral = 9999;
                db.Update(p);
            }


            // Trier les patineurs minime et benjamin sur 300 mètres
            Categorie minime6 = db.Categorie.SingleOrDefault(z => z.Nom == "6 ans");
            Categorie benjamin8 = db.Categorie.SingleOrDefault(z => z.Nom == "8 ans" );
            Categorie minime7 = db.Categorie.SingleOrDefault(z =>  z.Nom == "7 ans");
            Categorie benjamin9 = db.Categorie.SingleOrDefault(z =>  z.Nom == "9 ans");

            List<Patineur> lp = db.Patineur.Where(z => (z.NoCategorie == minime6.NoCategorie || z.NoCategorie == benjamin8.NoCategorie || z.NoCategorie == minime7.NoCategorie || z.NoCategorie == benjamin9.NoCategorie)
                && z.Classement1500 < 99
                ).ToList();

            List<Patineur> lp1 = lp.OrderBy(z => z.Classement1500).ToList();
            int i = 1;
            int max = 0;
            foreach (Patineur p in lp1)
            {
                p.ClassementGeneral = i;
                i += 1;
                max = i;
                db.Update(p);
            }

            int departCadet = (max - (max % 100)) + 101;

            // Trier les patineurs Cadet et Juvéniles sur 400 mètres
            Categorie cadet10 = db.Categorie.SingleOrDefault(z => z.Nom == "10 ans" );
            Categorie cadet11 = db.Categorie.SingleOrDefault(z => z.Nom == "11 ans");
            Categorie juvenile12 = db.Categorie.SingleOrDefault(z => z.Nom == "12 ans" );
            Categorie juvenile13 = db.Categorie.SingleOrDefault(z =>  z.Nom == "13 ans (Junior C ISU)");
            Categorie junior = db.Categorie.SingleOrDefault(z => z.Nom == "14 ans (Junior C ISU)");

            List<Patineur> lpcx = db.Patineur.Where(z => (z.NoCategorie == cadet10.NoCategorie || z.NoCategorie == cadet10.NoCategorie || z.NoCategorie == cadet11.NoCategorie || z.NoCategorie == juvenile12.NoCategorie || z.NoCategorie == juvenile13.NoCategorie || z.NoCategorie == junior.NoCategorie)
                && z.Classement2000 < 99
                ).ToList();

            List<Patineur> lp1c = lpcx.OrderBy(z => z.Classement2000).ToList();
            foreach (Patineur p in lp1c)
            {
                p.ClassementGeneral = departCadet;
                departCadet += 1;
                db.Update(p);
            }

            return;
        }

        public static void RechargerTemps(DBPatinVitesse db)
        {
            
                Console.WriteLine("Inscrire fichier CSV");
                string nomFichier = Console.ReadLine();

                if (!string.IsNullOrEmpty(nomFichier) && System.IO.File.Exists(nomFichier))
                {
                    using (StreamReader sr = new StreamReader(nomFichier))
                    {
                        string ligneLue = sr.ReadLine();
                        while (!string.IsNullOrEmpty(ligneLue))
                        {
                            string[] elements = ligneLue.Split(';');
                            DateTime dateNaisse;
                            DateTime.TryParse(elements[4], out dateNaisse);
                            IEnumerable<Patineur> ep = db.Patineur.ToList().Where(z => z.Nom.RemoveDiacritics().ToUpper() == elements[2].RemoveDiacritics().ToUpper()).ToList();
                            //IEnumerable<Patineur> ep = db.Patineur.Where(z => z.Nom.RemoveDiacritics() == elements[0].RemoveDiacritics());
                            IEnumerable<Patineur> ep1 = ep.Where(z => z.Prenom.RemoveDiacritics().ToUpper() == elements[3].RemoveDiacritics().ToUpper()).ToList();
                            IEnumerable<Patineur> ep2 = ep1.Where(z => z.DateNaissance.Day == dateNaisse.Day && z.DateNaissance.Month == dateNaisse.Month && z.DateNaissance.Year == dateNaisse.Year);
                            if (ep2.Count() > 0)
                            {
                                Patineur p = ep2.First();
                                if (p != null)
                                {
                                    Console.WriteLine(string.Format("Patineur {0} {1} du club {2} Inscrit", p.Nom, p.Prenom, db.Club.SingleOrDefault(z => z.NoClub == p.NoClub).NomClub));
                                double temps300;
                                double temps400;
                                double temps800;
                                double.TryParse(elements[5], out temps300);
                                double.TryParse(elements[6], out temps400);
                                double.TryParse(elements[7], out temps800);
                                if (p.Classement1500 > temps300)
                                {
                                    p.Classement1500 = temps300;
                                    db.Update(p);
                                }
                                if (p.Classement2000 > temps400)
                                {
                                    p.Classement2000 = temps400;
                                    db.Update(p);
                                }
                                if (p.Classement2500 > temps800)
                                {
                                    p.Classement2500 = temps800;
                                    db.Update(p);
                                }

                            }
                            else
                                {
                                
                                Console.WriteLine(string.Format("Patineur {0}, {1} non trouvé, procéder manuellement à l'inscription", elements[0], elements[1]));
                                }
                            }
                            else
                            {
                                Console.WriteLine(string.Format("Patineur {0}, {1} non trouvé, procéder manuellement à l'inscription", elements[0], elements[1]));
                            }

                            ligneLue = sr.ReadLine();
                        }
                    }
                }
            
        }


        public static void InscrirePatineurs(DBPatinVitesse db)
        {
            db.Competition.ToList().ForEach(z => Console.WriteLine(string.Format("No : {0}, Nom : {1}", z.NoCompetition, z.Lieu)));
            Console.WriteLine("Entrer le numéro de compétition");
            string noStrCompe = Console.ReadLine();
            int noCompeti;
            if (int.TryParse(noStrCompe, out noCompeti))
            {
                Console.WriteLine("Inscrire fichier CSV");
                string nomFichier = Console.ReadLine();

                if (!string.IsNullOrEmpty(nomFichier) && System.IO.File.Exists(nomFichier))
                {
                    using (StreamReader sr = new StreamReader(nomFichier))
                    {
                        string ligneLue = sr.ReadLine();
                        while (!string.IsNullOrEmpty(ligneLue))
                        {
                            string[] elements = ligneLue.Split(';');
                            DateTime dateNaisse;
                            DateTime.TryParse(elements[3], out dateNaisse);
                            IEnumerable<Patineur> ep = db.Patineur.ToList().Where(z => z.Nom.RemoveDiacritics().ToUpper() == elements[0].RemoveDiacritics().ToUpper()).ToList();
                            //IEnumerable<Patineur> ep = db.Patineur.Where(z => z.Nom.RemoveDiacritics() == elements[0].RemoveDiacritics());
                            IEnumerable<Patineur> ep1 = ep.Where(z => z.Prenom.RemoveDiacritics().ToUpper() == elements[1].RemoveDiacritics().ToUpper()).ToList();
                            IEnumerable<Patineur> ep2 = ep1.Where(z => z.DateNaissance.Day == dateNaisse.Day && z.DateNaissance.Month == dateNaisse.Month && z.DateNaissance.Year == dateNaisse.Year);
                            if (ep2.Count() > 0)
                            {
                                Patineur p = ep2.First();
                                if (p != null)
                                {
                                    // On inscrit le patineur                                    
                                    int max = db.PatineurCompe.Max(z => z.NoPatCompe);
                                    PatineurCompe pc = new PatineurCompe();
                                    pc.NoPatineur = p.NoPatineur;
                                    pc.NoPatCompe = max + 1;
                                    pc.Rang = System.Convert.ToInt32(p.Classement);
                                    pc.Retirer = false;
                                    pc.Si_Regroup_Classement = 2;
                                    pc.Division = "Régional";
                                    pc.NoCompetition = noCompeti;
                                    pc.NoCategorie = p.NoCategorie;
                                    pc.NoClub = p.NoClub;
                                    db.Insert(pc);
                                    Console.WriteLine(string.Format("Patineur {0} {1} du club {2} Inscrit", p.Nom, p.Prenom, db.Club.SingleOrDefault(z => z.NoClub == p.NoClub).NomClub));
                                }
                                else
                                {
                                    Console.WriteLine(string.Format("Patineur {0}, {1} non trouvé, procéder manuellement à l'inscription", elements[0], elements[1]));
                                }
                            }
                            else
                            {
                                Console.WriteLine(string.Format("Patineur {0}, {1} non trouvé, procéder manuellement à l'inscription", elements[0], elements[1]));
                            }

                            ligneLue = sr.ReadLine();
                        }
                    }
                }
            }
        }

        public static void InfoCompeVagues(DBPatinVitesse db)
        {
            db.Competition.ToList().ForEach(z => Console.WriteLine(string.Format("No : {0}, Nom : {1}", z.NoCompetition, z.Lieu)));
            Console.WriteLine("Entrer le numéro de compétition");
            string noStrCompe = Console.ReadLine();
            int noCompeti;
            if (int.TryParse(noStrCompe, out noCompeti))
            {

                var queryCompe =
                    from compe in db.Competition
                    where compe.NoCompetition == noCompeti
                    select compe;
               ;

                JObject programme = new JObject();
                programme.Add("Nom", queryCompe.First().Commentaire);
                JArray ja = new JArray();
                programme.Add("Groupes", ja);
                
                var pc = from progcourse in db.ProgCourses
                         where progcourse.NoCompetition == noCompeti
                         select progcourse.Groupe;

                var grppreced = string.Empty;
                var vgpreced = string.Empty;
                JArray vaguesGroupes = new JArray();
                JArray patineurVague = new JArray();
                JObject vagueJson = null;

                // Sélection de tous les temps des patineurs de la compétition
                var laTotal = from patvag in db.PatVagues
                              join patcmp in db.PatineurCompe on patvag.NoPatCompe equals patcmp.NoPatCompe
                              join patineur in db.Patineur on patcmp.NoPatineur equals patineur.NoPatineur
                              join club in db.Club on patineur.NoClub equals club.NoClub
                              join vag in db.Vagues on patvag.CleTVagues equals vag.CleTVagues
                              join progcrs in db.ProgCourses on vag.CleDistancesCompe equals progcrs.CleDistancesCompe
                              join diststd in db.DistanceStandards on progcrs.NoDistance equals diststd.NoDistance
                              join cmnt in db.Commentaire on patvag.Juge equals cmnt.Code
                              where patvag.NoPatCompe == patcmp.NoPatCompe
                              && patcmp.NoPatineur == patineur.NoPatineur
                              && patineur.NoClub == club.NoClub
                              && patvag.CleTVagues == vag.CleTVagues
                              && vag.CleDistancesCompe == progcrs.CleDistancesCompe
                              && progcrs.NoDistance == diststd.NoDistance
                              && patcmp.NoCompetition == noCompeti
                              && progcrs.NoCompetition == noCompeti
                              select new ResultatObj()
                              {
                                  Nom = patineur.Nom + "," + patineur.Prenom,
                                  Club = club.NomClub,
                                  NoCasque = patvag.NoCasque,
                                  Temps = patvag.Temps,
                                  Point = patvag.Point,
                                  Rang = patvag.Rang,
                                  Code = cmnt.CodeAction.Replace("NIL",string.Empty),
                                  NoVague = vag.NoVague,
                                  Epreuve = vag.Qual_ou_Fin,
                                  Groupe = patcmp.Groupe,
                                  LongueurEpreuve = diststd.LongueurEpreuve,
                                  NoBloc = progcrs.NoBloc
                              };

                var nbp = laTotal.Count();

                var laTotale1 = laTotal.OrderBy(z => z.Groupe + z.NoVague).ThenBy(z => z.NoBloc).ThenBy(z => z.Rang).ThenBy(z => z.Point).ThenBy(z => z.NoCasque);
                nbp = laTotale1.Count();
                var laTotale2 = laTotale1.ToList();
                nbp = laTotale2.Count();
                foreach (var ab in laTotale2)
                {
                    Console.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", ab.Groupe, ab.NoVague, ab.Nom, ab.Rang, ab.Temps, ab.Point, ab.Club, ab.LongueurEpreuve, ab.Nom, ab.Epreuve,ab.Code));
                }

                var noGroupe = 0;
                foreach (var ab in laTotale2)
                {
                    if (grppreced != ab.Groupe)
                    {
                        noGroupe += 1;
                        // Si le groupe change, la vague change
                        if (vagueJson != null)
                        {
                            vaguesGroupes.Add(vagueJson);
                        }
                        vagueChange(out vgpreced, out patineurVague, out vagueJson, ab);
                        //patineurVague = new JArray();
                        //vagueJson = new JObject();
                        //vagueJson.Add("Vague", ab.NoVague);
                        //vagueJson.Add("distance", ab.LongueurEpreuve.ToString() + "m");
                        //vagueJson.Add("etape", ab.Epreuve);
                        //vagueJson.Add("Patineurs", patineurVague);
                        // Converver le groupe précédent
                        if (!string.IsNullOrEmpty(grppreced))
                        {
                            //string groupeData = vaguesGroupes.ToString();
                            CreerFichierResultatGroupe(noGroupe, vaguesGroupes, grppreced, (JArray)programme["Groupes"]);
                        }
                        vaguesGroupes = new JArray();
                        grppreced = ab.Groupe;
                        //vgpreced = ab.NoVague;
                    }
                    else if (vgpreced != ab.NoVague)
                    {
                        if (vagueJson != null)
                        {
                            vaguesGroupes.Add(vagueJson);
                        }
                        vagueChange(out vgpreced, out patineurVague, out vagueJson, ab);
                        //patineurVague = new JArray();
                        //vagueJson = new JObject();
                        //vagueJson.Add("Vague", ab.NoVague);
                        //vagueJson.Add("distance", ab.LongueurEpreuve.ToString() + "m");
                        //vagueJson.Add("etape", ab.Epreuve);
                        //vagueJson.Add("Patineurs", patineurVague);
                        //vgpreced = ab.NoVague;
                    }
                    JObject patineurJson = new JObject();
                    patineurJson.Add("Casque", ab.NoCasque.ToString());
                    patineurJson.Add("Patineurs", ab.Nom);
                    patineurJson.Add("Club", ab.Club);
                    patineurJson.Add("Rang", ab.Rang);
                    patineurJson.Add("Temps", ab.Temps);
                    patineurJson.Add("Commentaire", ab.Code);
                    patineurJson.Add("Point", ab.Point);
                    patineurJson.Add("Date", "");
                    patineurVague.Add(patineurJson);
                    //Console.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", ab.Groupe, ab.NoVague, ab.Nom, ab.Rang, ab.Temps, ab.Point, ab.Club, ab.LongueurEpreuve, ab.Nom, ab.Etape));
                }

                // On termine avec le dernier groupe
                noGroupe += 1;
                if (vagueJson != null)
                {
                    vaguesGroupes.Add(vagueJson);
                }
                if (!string.IsNullOrEmpty(grppreced))
                {
                    //string groupeData = vaguesGroupes.ToString();
                    CreerFichierResultatGroupe(noGroupe, vaguesGroupes, grppreced, (JArray)programme["Groupes"]);
                }

                // Créer le fichier de programme
                string programmeStr = programme.ToString();
                programmeStr = programmeStr.Replace(Environment.NewLine,string.Empty);
                string pathResultat = @"C:\Users\Bruno\Documents\Visual Studio 2015\Projects\WebApplication1\WebApplication1\data";
                string nomFich = System.IO.Path.Combine(pathResultat, "programme.js");
                using (StreamWriter sr = new StreamWriter(nomFich))
                {
                     sr.Write(string.Format("var a = '{0}';", programmeStr));
                }
                List<string> ls = new List<string>()
                { "http://pastebin.com/raw/i5Cx3JNz",
                "http://pastebin.com/raw/kgd6LER3",
                "http://pastebin.com/raw/LwsXQsrJ",
                "http://pastebin.com/raw/JM2Qw1dZ",
                "http://pastebin.com/raw/VtXa66UU",
                "http://pastebin.com/raw/KArz2g3e",
                "http://pastebin.com/raw/zbE3BwdZ",
                "http://pastebin.com/raw/N4vMfii4",
                "http://pastebin.com/raw/nAwDaF4C",
                "http://pastebin.com/raw/cxQHM7Bs",
                "http://pastebin.com/raw/vJb8ZmvY",
                "http://pastebin.com/raw/7WQ4Q5SQ",
                "http://pastebin.com/raw/uajWDZxQ",
                "http://pastebin.com/raw/rbUbZh0D",
                "http://pastebin.com/raw/euVrLJkx",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                ""
                };

            }
        }

        private static void CreerFichierResultatGroupe(int noGroupe, JArray groupeData, string nomGroupe, JArray groupes)
        {
            JObject jogrp = new JObject();
            jogrp.Add("Groupe", nomGroupe);
            jogrp.Add("src", string.Format("data/GR{0}.js", noGroupe));
            groupes.Add(jogrp);
            string groupeDatastr = groupeData.ToString();
            groupeDatastr = groupeDatastr.Replace(Environment.NewLine, string.Empty);
            string pathResultat = @"C:\Users\Bruno\Documents\Visual Studio 2015\Projects\WebApplication1\WebApplication1\data";
            string nomFich = System.IO.Path.Combine(pathResultat, string.Format("GR{0}.js", noGroupe));
            using (StreamWriter sr = new StreamWriter(nomFich))
            {
                sr.Write(string.Format("var a = '{0}';",groupeDatastr));
            }
        }

        private static void vagueChange(out string vgpreced, out JArray patineurVague, out JObject vagueJson, ResultatObj ab)
        {
            patineurVague = new JArray();
            vagueJson = new JObject();
            vagueJson.Add("vague", ab.NoVague);
            vagueJson.Add("distance", ab.LongueurEpreuve.ToString() + "m");
            vagueJson.Add("etape", ab.Epreuve);
            vagueJson.Add("Patineurs", patineurVague);
            vgpreced = ab.NoVague;
        }

        public class ResultatObj
        {
            public string Nom { get; set; }
            public string Club { get; set; }
            public int NoCasque { get; set; }
            public double Temps { get; set; }
            public int Point { get; set; }
            public int Rang { get; set; }
            public string Code { get; set; }
            public string NoVague { get; set; }
            public string Epreuve { get; set; }
            public string Groupe { get; set; }
            public int LongueurEpreuve { get; set; }
            public int NoBloc { get; set; }

        }

        //foreach (var gr in pc.ToList().Distinct())
        //{
        //    var patclub = from patin in db.Patineur
        //                  join club in db.Club on patin.NoClub equals club.NoClub
        //                  where patin.NoClub == club.NoClub
        //                  select new
        //                  {
        //                      NoPatineur = patin.NoPatineur,
        //                      Nom = patin.Nom + "," + patin.Prenom,
        //                      Club = club.NomClub
        //                  };

        //    var patcompgr = from pati in patclub
        //                    join patcomp in db.PatineurCompe on pati.NoPatineur equals patcomp.NoPatineur
        //                    where patcomp.NoPatineur == pati.NoPatineur
        //                    && patcomp.Groupe == gr
        //                    select new
        //                    {
        //                        grp = patcomp.Groupe,
        //                        NoPatineur = patcomp.NoPatineur,
        //                        Nom = pati.Nom,
        //                        Club = pati.Club,
        //                    };
        //    //var nbx = patcompgr.Count();

        //    var patcmpc = from patcomprgrx in patcompgr
        //                  join patcmp in db.PatineurCompe  on patcomprgrx.NoPatineur equals patcmp.NoPatineur
        //                  where patcmp.NoPatineur == patcomprgrx.NoPatineur
        //                  && patcmp.Groupe == gr
        //                  select new
        //                  {
        //                      NoPatCompe = patcmp.NoPatCompe,
        //                      NoPatineur = patcmp.NoPatineur,
        //                      Groupe = patcmp.Groupe,
        //                      Nom = patcomprgrx.Nom,
        //                      Club = patcomprgrx.Club
        //                  };

        //    //var nbptcp = patcmpc.Count();

        //    var patvagc = from vague in db.Vagues
        //                  join patvag in db.PatVagues on vague.CleTVagues equals patvag.CleTVagues
        //                  //join cmd in db.Commentaire on patvag.Juge equals cmd.Code
        //                  where patvag.CleTVagues == vague.CleTVagues //&&
        //                  //patvag.Juge == cmd.Code
        //                  select new
        //                  {

        //                      NoPatCompe = patvag.NoPatCompe,
        //                      CleTPatVagues = patvag.CleTPatVagues,
        //                      CleTVagues = patvag.CleTVagues,
        //                      NoCasque = patvag.NoCasque,
        //                      Temps = patvag.Temps,
        //                      Rang = patvag.Rang,
        //                      Point = patvag.Point,
        //                      nn = patvag.NoPatCompe,
        //                      CleDistancesCompe = vague.CleDistancesCompe,
        //                      Etape = vague.Qual_ou_Fin,
        //                      Cmt = "", // cmd.Description,
        //                      NoVague = vague.NoVague

        //                  };

        //    var nbpatvagc = patvagc.Count();



        //    //var patvagvc = from patvagcc in patvagc
        //    //               join vague in db.Vagues on patvagcc.CleTVagues equals vague.CleTVagues
        //    //               select new
        //    //               {
        //    //                   NoPatCompe = patvagcc.NoPatCompe,
        //    //                   CleTPatVagues = patvagcc.CleTPatVagues,
        //    //                   CleTVagues = patvagcc.CleTVagues,
        //    //                   NoCasque = patvagcc.NoCasque,
        //    //                   Temps = patvagcc.Temps,
        //    //                   Rang = patvagcc.Rang,
        //    //                   Point = patvagcc.Point,
        //    //                   CleDistancesCompe = vague.CleDistancesCompe,
        //    //                   Etape = vague.Qual_ou_Fin,
        //    //                   Cmt = patvagcc.Cmt,
        //    //                   NoVague = vague.NoVague
        //    //               };

        //    var patvagvcpg = from patvagvcc in patvagc
        //                     join prgcrs in db.ProgCourses on patvagvcc.CleDistancesCompe equals prgcrs.CleDistancesCompe
        //                     where prgcrs.NoCompetition == noCompeti
        //                     select new
        //                     {
        //                         NoPatCompe = patvagvcc.NoPatCompe,
        //                         CleTPatVagues = patvagvcc.CleTPatVagues,
        //                         CleTVagues = patvagvcc.CleTVagues,
        //                         NoCasque = patvagvcc.NoCasque,
        //                         Temps = patvagvcc.Temps,
        //                         Rang = patvagvcc.Rang,
        //                         Point = patvagvcc.Point,
        //                         CleDistancesCompe = patvagvcc.CleDistancesCompe,
        //                         NoDistance = prgcrs.NoDistance,
        //                         Cmt = patvagvcc.Cmt,
        //                         Etape = patvagvcc.Etape,
        //                         NoVague = patvagvcc.NoVague
        //                     };

        //    var nbpatvagcz = patvagvcpg.Count();
        //    //var zzxx = from patvagvcpgb in patvagvcpg
        //    //           join distcmp in db.DistanceCompe on patvagvcpgb.CleDistancesCompe equals distcmp.CleDistancesCompe
        //    //           where patvagvcpgb.CleDistancesCompe == distcmp.CleDistancesCompe
        //    //           select new
        //    //           {
        //    //               NoPatCompe = patvagvcpgb.NoPatCompe,
        //    //               CleTPatVagues = patvagvcpgb.CleTPatVagues,
        //    //               CleTVagues = patvagvcpgb.CleTVagues,
        //    //               NoCasque = patvagvcpgb.NoCasque,
        //    //               Temps = patvagvcpgb.Temps,
        //    //               Rang = patvagvcpgb.Rang,
        //    //               Point = patvagvcpgb.Point,
        //    //               CleDistancesCompe = patvagvcpgb.CleDistancesCompe,
        //    //               NoDistance = patvagvcpgb.NoDistance,
        //    //               Cmt = patvagvcpgb.Cmt,
        //    //               Etape = patvagvcpgb.Etape,
        //    //               NoVague = patvagvcpgb.NoVague,
        //    //               Nodistance = distcmp.NoDistance
        //    //           };


        //    var patvagvcpgd = from patvagvcpgb in patvagvcpg
        //                      join dists in db.DistanceStandards on patvagvcpgb.NoDistance equals dists.NoDistance
        //                      where patvagvcpgb.NoDistance == dists.NoDistance
        //                      select new
        //                      {
        //                          NoPatCompe = patvagvcpgb.NoPatCompe,
        //                          CleTPatVagues = patvagvcpgb.CleTPatVagues,
        //                          CleTVagues = patvagvcpgb.CleTVagues,
        //                          NoCasque = patvagvcpgb.NoCasque,
        //                          Temps = patvagvcpgb.Temps,
        //                          Rang = patvagvcpgb.Rang,
        //                          Point = patvagvcpgb.Point,
        //                          CleDistancesCompe = patvagvcpgb.CleDistancesCompe,
        //                          NoDistance = patvagvcpgb.NoDistance,
        //                          LongueurEpreuve = dists.LongueurEpreuve,
        //                          Cmt = patvagvcpgb.Cmt,
        //                          Etape = patvagvcpgb.Etape,
        //                          NoVague = patvagvcpgb.NoVague
        //                      };

        //    var patvagvcpgdz = patvagvcpgd.Count();
        //    var patvagvcpgdpt = from patvagvcpgdb in patvagvcpgd.ToList()
        //                        join patcmpcb in patcmpc.ToList() on patvagvcpgdb.NoPatCompe equals patcmpcb.NoPatCompe
        //                        where patvagvcpgdb.NoPatCompe == patcmpcb.NoPatCompe
        //                        select new
        //                        {
        //                            NoPatCompe = patvagvcpgdb.NoPatCompe,
        //                            CleTPatVagues = patvagvcpgdb.CleTPatVagues,
        //                            CleTVagues = patvagvcpgdb.CleTVagues,
        //                            NoCasque = patvagvcpgdb.NoCasque,
        //                            Temps = patvagvcpgdb.Temps,
        //                            Rang = patvagvcpgdb.Rang,
        //                            Point = patvagvcpgdb.Point,
        //                            CleDistancesCompe = patvagvcpgdb.CleDistancesCompe,
        //                            NoDistance = patvagvcpgdb.NoDistance,
        //                            LongueurEpreuve = patvagvcpgdb.LongueurEpreuve,
        //                            NoPatineur = patcmpcb.NoPatineur,
        //                            Groupe = patcmpcb.Groupe,
        //                            Nom = patcmpcb.Nom,
        //                            Etape = patvagvcpgdb.Etape,
        //                            Cmt = patvagvcpgdb.Cmt,
        //                            Club = patcmpcb.Club,
        //                            NoVague = patvagvcpgdb.NoVague
        //                        };

        //    var patvagvcpgdptz = patvagvcpgdpt.Count();
        //    var zzz = patvagvcpgdpt.OrderBy(z => z.Groupe).ThenBy(z => z.NoVague).ThenBy(z => z.NoCasque).ToList();


        //    vaguesGroupes.Add(vagueJson);
        //    int nbtst = zzz.Count();
        //    foreach (var ab in zzz)
        //    {
        //        if (grppreced != ab.Groupe)
        //        {
        //            // Si le groupe change, la vague change
        //            if (vagueJson != null)
        //            {
        //                vaguesGroupes.Add(vagueJson);
        //            }
        //            patineurVague = new JArray();
        //            vagueJson = new JObject();
        //            vagueJson.Add("Vague", ab.NoVague);
        //            vagueJson.Add("distance", ab.LongueurEpreuve.ToString() + "m");
        //            vagueJson.Add("etape", ab.Etape);
        //            vagueJson.Add("Patineurs", patineurVague);
        //            // Converver le groupe précédent
        //            if (!string.IsNullOrEmpty(grppreced))
        //            {
        //                string groupeData = vaguesGroupes.ToString();
        //            }
        //            vaguesGroupes = new JArray();
        //            grppreced = ab.Groupe;
        //            vgpreced = ab.NoVague;
        //        }
        //        else if (vgpreced != ab.NoVague)
        //        {
        //            if (vagueJson != null)
        //            {
        //                vaguesGroupes.Add(vagueJson);
        //            }
        //            patineurVague = new JArray();
        //            vagueJson = new JObject();
        //            vagueJson.Add("Vague", ab.NoVague);
        //            vagueJson.Add("distance", ab.LongueurEpreuve.ToString() + "m");
        //            vagueJson.Add("etape", ab.Etape);
        //            vagueJson.Add("Patineurs", patineurVague);
        //            vgpreced = ab.NoVague;
        //        }
        //        JObject patineurJson = new JObject();
        //        patineurJson.Add("Casque", ab.NoCasque.ToString());
        //        patineurJson.Add("Patineurs", ab.Nom);
        //        patineurJson.Add("Club", ab.Club);
        //        patineurJson.Add("Rang", ab.Rang);
        //        patineurJson.Add("Temps", ab.Temps);
        //        patineurJson.Add("Commentaire", ab.Cmt);
        //        patineurJson.Add("Date", "");
        //        patineurVague.Add(patineurJson);
        //        Console.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", ab.Groupe, ab.NoVague, ab.Nom, ab.Rang, ab.Temps, ab.Point, ab.Club, ab.LongueurEpreuve, ab.Nom, ab.Etape));

        //    }



        //}






        public static void BilanResultat(DBPatinVitesse db)
        {
            var newQuery =
                from patvag in db.PatVagues
                where patvag.Temps > 0
                select new
                {
                    patvag.NoPatCompe,
                    patvag.Temps,
                    patvag.CleTPatVagues,
                    patvag.CleTVagues
                };
            var nb = newQuery.Count();

            var newQuery1 =
               from patvag in newQuery
               join vague in db.Vagues on patvag.CleTVagues equals vague.CleTVagues
               select new
               {
                   patvag.NoPatCompe,
                   patvag.Temps,
                   patvag.CleTPatVagues,
                   patvag.CleTVagues,
                   vague.CleDistancesCompe
               };
            var nb1 = newQuery1.Count();

            var newQuery1a =
             from patvag in newQuery1
             join vague in db.ProgCourses on patvag.CleDistancesCompe equals vague.CleDistancesCompe

             select new
             {
                 patvag.NoPatCompe,
                 patvag.Temps,
                 patvag.CleTPatVagues,
                 patvag.CleTVagues,
                 patvag.CleDistancesCompe,
                 vague.NoDistance
             };
            var nb1a = newQuery1.Count();

            //var newQuery2 =
            // from patvag in newQuery1
            // join vague in db.DistanceCompe on patvag.CleDistancesCompe equals vague.CleDistancesCompe
            // select new
            // {
            //     patvag.NoPatCompe,
            //     patvag.Temps,
            //     patvag.CleTPatVagues,
            //     patvag.CleTVagues,
            //     patvag.CleDistancesCompe,
            //     vague.NoDistance
            // };
            //var nb2 = newQuery2.Count();
            //var nb2lst = newQuery2.ToList();
            var newQuery3 =
           from patvag in newQuery1a
           join vague in db.DistanceStandards on patvag.NoDistance equals vague.NoDistance
           select new
           {
               patvag.NoPatCompe,
               patvag.Temps,
               patvag.CleTPatVagues,
               patvag.CleTVagues,
               patvag.CleDistancesCompe,
               patvag.NoDistance,
               vague.LongueurEpreuve
           };
            var nb3 = newQuery3.Count();
            var newQuery4 =
        from patvag in newQuery3
        join vague in db.PatineurCompe on patvag.NoPatCompe equals vague.NoPatCompe
        select new
        {
            patvag.NoPatCompe,
            patvag.Temps,
            patvag.CleTPatVagues,
            patvag.CleTVagues,
            patvag.CleDistancesCompe,
            patvag.NoDistance,
            patvag.LongueurEpreuve,
            vague.NoPatineur,
            vague.NoCompetition
        };

            var newQuery5 =
  from patvag in newQuery4
  join vague in db.Patineur on patvag.NoPatineur equals vague.NoPatineur
  select new
  {
      patvag.NoPatCompe,
      patvag.Temps,
      patvag.CleTPatVagues,
      patvag.CleTVagues,
      patvag.CleDistancesCompe,
      patvag.NoDistance,
      patvag.LongueurEpreuve,
      patvag.NoPatineur,
      patvag.NoCompetition,
      vague.Nom,
      vague.Prenom,
      vague.NoClub
  };

            var newQuery6 =
   from patvag in newQuery5
   join vague in db.Club on patvag.NoClub equals vague.NoClub
   select new
   {
       patvag.NoPatCompe,
       patvag.Temps,
       patvag.CleTPatVagues,
       patvag.CleTVagues,
       patvag.CleDistancesCompe,
       patvag.NoDistance,
       patvag.LongueurEpreuve,
       patvag.NoPatineur,
       patvag.NoCompetition,
       patvag.Nom,
       patvag.Prenom,
       patvag.NoClub,
       vague.NomClub
   };

            var newQuery7 =
from patvag in newQuery6
join vague in db.Competition on patvag.NoCompetition equals vague.NoCompetition
let NoClubCompe = vague.NoClub
select new
{
    patvag.NoPatCompe,
    patvag.Temps,
    patvag.CleTPatVagues,
    patvag.CleTVagues,
    patvag.CleDistancesCompe,
    patvag.NoDistance,
    patvag.LongueurEpreuve,
    patvag.NoPatineur,
    patvag.NoCompetition,
    patvag.Nom,
    patvag.Prenom,
    patvag.NoClub,
    patvag.NomClub,
    NoClubCompe
};

            var newQuery8 =
from patvag in newQuery7
join vague in db.Club on patvag.NoClubCompe equals vague.NoClub
let NomClubCompe = vague.NomClub
select new
{
    patvag.NoPatCompe,
    patvag.Temps,
    patvag.CleTPatVagues,
    patvag.CleTVagues,
    patvag.CleDistancesCompe,
    patvag.NoDistance,
    patvag.LongueurEpreuve,
    patvag.NoPatineur,
    patvag.NoCompetition,
    patvag.Nom,
    patvag.Prenom,
    patvag.NoClub,
    patvag.NomClub,
    patvag.NoClubCompe,
    NomClubCompe
};
            var tt = newQuery8.ToList();

            var queryrder =
                from ttx in tt
                orderby ttx.Nom, ttx.Prenom, ttx.NomClub, ttx.NoClubCompe, ttx.LongueurEpreuve
                group new { ttx.Nom, ttx.Prenom, ttx.NomClub, ttx.NomClubCompe, ttx.LongueurEpreuve, ttx.Temps }
                by new { ttx.Nom, ttx.Prenom, ttx.NomClub, ttx.NomClubCompe, ttx.LongueurEpreuve } into pg
                let distance = pg.Min(z => z.Temps)
                let nom = pg.FirstOrDefault().Nom
                let prenom = pg.FirstOrDefault().Prenom
                let nomClub = pg.FirstOrDefault().NomClub
                let nomClubCompe = pg.FirstOrDefault().NomClubCompe
                let longueurEpreuve = pg.FirstOrDefault().LongueurEpreuve
                select new
                {
                    nom,
                    prenom,
                    nomClub,
                    nomClubCompe,
                    longueurEpreuve,
                    distance
                };

            var ttt = queryrder.ToList();
            List<string> listCC = ttt.Select(z => z.nomClubCompe).Distinct().ToList();
            List<int> listDT = ttt.Select(z => z.longueurEpreuve).Distinct().ToList();

            string nomPrecedent = string.Empty;
            string prenomPrecedent = string.Empty;
            string clubPrecedent = string.Empty;
            List<TempsCompil> tcomp = new List<TempsCompil>();

            foreach (var tttt in ttt)
            {
                if (tttt.nom != nomPrecedent || tttt.prenom != prenomPrecedent || tttt.nomClub != clubPrecedent)
                {
                    TempsCompil tcomp1 = new TempsCompil();
                    tcomp1.Nom = tttt.nom;
                    tcomp1.Prenom = tttt.prenom;
                    tcomp1.Club = tttt.nomClub;
                    tcomp.Add(tcomp1);
                    nomPrecedent = tttt.nom;
                    prenomPrecedent = tttt.prenom;
                    clubPrecedent = tttt.nomClub;
                }
                TempsCompil tcompp = tcomp.SingleOrDefault(z => z.Nom == tttt.nom && z.Club == tttt.nomClub && z.Prenom == tttt.prenom);
                if (tcompp == null)
                {
                    System.Diagnostics.Debug.Write("");
                }
                switch (tttt.longueurEpreuve)
                {
                    case 85:
                        if (tcompp.TempsCompe85 == null)
                        {
                            tcompp.TempsCompe85 = new TempsCompil.TempsCompe();
                        }
                        switch (tttt.nomClubCompe)
                        {
                            case "Québec":
                                tcompp.TempsCompe85.Quebec = tttt.distance;
                                break;
                            case "Lévis":
                                tcompp.TempsCompe85.Levis = tttt.distance;
                                break;
                            case "Portneuf":
                                tcompp.TempsCompe85.Portneuf = tttt.distance;
                                break;
                            case "Sainte-Foy":
                                tcompp.TempsCompe85.SainteFoy = tttt.distance;
                                break;
                            case "Sport Étude Québec":
                                break;
                        }
                        break;
                    case 100:
                        tcompp.TempsLP100 = tttt.distance;
                        break;
                    case 170:
                        if (tcompp.TempsCompe170 == null)
                        {
                            tcompp.TempsCompe170 = new TempsCompil.TempsCompe();
                        }
                        switch (tttt.nomClubCompe)
                        {
                            case "Québec":
                                tcompp.TempsCompe170.Quebec = tttt.distance;
                                break;
                            case "Lévis":
                                tcompp.TempsCompe170.Levis = tttt.distance;
                                break;
                            case "Portneuf":
                                tcompp.TempsCompe170.Portneuf = tttt.distance;
                                break;
                            case "Sainte-Foy":
                                tcompp.TempsCompe170.SainteFoy = tttt.distance;
                                break;
                            case "Sport Étude Québec":
                                break;
                        }
                        break;
                    case 200:
                        if (tcompp.TempsCompe200 == null)
                        {
                            tcompp.TempsCompe200 = new TempsCompil.TempsCompe();
                        }
                        switch (tttt.nomClubCompe)
                        {
                            case "Québec":
                                tcompp.TempsCompe200.Quebec = tttt.distance;
                                break;
                            case "Lévis":
                                tcompp.TempsCompe200.Levis = tttt.distance;
                                break;
                            case "Portneuf":
                                tcompp.TempsCompe200.Portneuf = tttt.distance;
                                break;
                            case "Sainte-Foy":
                                tcompp.TempsCompe200.SainteFoy = tttt.distance;
                                break;
                            case "Sport Étude Québec":
                                break;
                        }
                        break;
                    case 300:
                        if (tcompp.TempsCompe300 == null)
                        {
                            tcompp.TempsCompe300 = new TempsCompil.TempsCompe();
                        }
                        switch (tttt.nomClubCompe)
                        {
                            case "Québec":
                                tcompp.TempsCompe300.Quebec = tttt.distance;
                                break;
                            case "Lévis":
                                tcompp.TempsCompe300.Levis = tttt.distance;
                                break;
                            case "Portneuf":
                                tcompp.TempsCompe300.Portneuf = tttt.distance;
                                break;
                            case "Sainte-Foy":
                                tcompp.TempsCompe300.SainteFoy = tttt.distance;
                                break;
                            case "Sport Étude Québec":
                                tcompp.TempsLP300 = tttt.distance;
                                break;
                        }
                        break;
                    case 400:
                        if (tcompp.TempsCompe400 == null)
                        {
                            tcompp.TempsCompe400 = new TempsCompil.TempsCompe();
                        }
                        switch (tttt.nomClubCompe)
                        {
                            case "Québec":
                                tcompp.TempsCompe400.Quebec = tttt.distance;
                                break;
                            case "Lévis":
                                tcompp.TempsCompe400.Levis = tttt.distance;
                                break;
                            case "Portneuf":
                                tcompp.TempsCompe400.Portneuf = tttt.distance;
                                break;
                            case "Sainte-Foy":
                                tcompp.TempsCompe400.SainteFoy = tttt.distance;
                                break;
                            case "Sport Étude Québec":
                                tcompp.TempsLP400 = tttt.distance;
                                break;
                        }
                        break;
                    case 600:
                        tcompp.TempsLP600 = tttt.distance;
                        break;
                    case 800:
                        if (tcompp.TempsCompe800 == null)
                        {
                            tcompp.TempsCompe800 = new TempsCompil.TempsCompe();
                        }
                        switch (tttt.nomClubCompe)
                        {
                            case "Québec":
                                tcompp.TempsCompe800.Quebec = tttt.distance;
                                break;
                            case "Lévis":
                                tcompp.TempsCompe800.Levis = tttt.distance;
                                break;
                            case "Portneuf":
                                tcompp.TempsCompe800.Portneuf = tttt.distance;
                                break;
                            case "Sainte-Foy":
                                tcompp.TempsCompe800.SainteFoy = tttt.distance;
                                break;
                            case "Sport Étude Québec":
                                tcompp.TempsLP300 = tttt.distance;
                                break;
                        }
                        break;
                    case 1500:
                        if (tcompp.TempsCompe1500 == null)
                        {
                            tcompp.TempsCompe1500 = new TempsCompil.TempsCompe();
                        }
                        switch (tttt.nomClubCompe)
                        {
                            case "Québec":
                                tcompp.TempsCompe1500.Quebec = tttt.distance;
                                break;
                            case "Lévis":
                                tcompp.TempsCompe1500.Levis = tttt.distance;
                                break;
                            case "Portneuf":
                                tcompp.TempsCompe1500.Portneuf = tttt.distance;
                                break;
                            case "Sainte-Foy":
                                tcompp.TempsCompe1500.SainteFoy = tttt.distance;
                                break;
                            case "Sport Étude Québec":
                                tcompp.TempsLP300 = tttt.distance;
                                break;
                        }
                        break;
                }
            }

            foreach (TempsCompil tx in tcomp)
            {
                Console.Write(tx.Nom);
                Console.Write(";");
                Console.Write(tx.Prenom);
                Console.Write(";");
                Console.Write(tx.Club);
                Console.Write(";");
                EcrireTemps(tx.TempsCompe85);
                EcrireTemps(tx.TempsCompe170);
                EcrireTemps(tx.TempsCompe200);
                EcrireTemps(tx.TempsCompe300);
                EcrireTemps(tx.TempsCompe400);
                EcrireTemps(tx.TempsCompe800);
                EcrireTemps(tx.TempsCompe1500);
                if (tx.TempsLP100.HasValue)
                {
                    Console.Write(tx.TempsLP100.Value);
                }
                Console.Write(";");
                if (tx.TempsLP300.HasValue)
                {
                    Console.Write(tx.TempsLP300.Value);
                }
                Console.Write(";");
                if (tx.TempsLP400.HasValue)
                {
                    Console.Write(tx.TempsLP400.Value);
                }
                Console.Write(";");
                if (tx.TempsLP600.HasValue)
                {
                    Console.Write(tx.TempsLP600.Value);
                }
                Console.WriteLine(";");
            }


        }

        public static void EcrireTemps(TempsCompil.TempsCompe temp)
        {
            if (temp == null)
            {
                Console.Write(";;;;;");
            }
            else {
                double min = 999;
                if (temp.Levis.HasValue)
                {
                    Console.Write(temp.Levis);
                    if (min > temp.Levis)
                    {
                        min = temp.Levis.Value;
                    }
                }
                Console.Write(";");
                if (temp.Quebec.HasValue)
                {
                    Console.Write(temp.Quebec);
                    if (min > temp.Quebec)
                    {
                        min = temp.Quebec.Value;
                    }
                }
                Console.Write(";");
                if (temp.Portneuf.HasValue)
                {
                    Console.Write(temp.Portneuf);
                    if (min > temp.Portneuf)
                    {
                        min = temp.Portneuf.Value;
                    }
                }
                Console.Write(";");
                if (temp.SainteFoy.HasValue)
                {
                    Console.Write(temp.SainteFoy);
                    if (min > temp.SainteFoy)
                    {
                        min = temp.SainteFoy.Value;
                    }
                }
                Console.Write(";");
                Console.Write(min);
                Console.Write(";");

            }
    }

        public class TempsCompil
        {
            public string Nom { get; set; }
            public string Prenom { get; set; }
            public string Club { get; set; }

            public TempsCompe TempsCompe85 { get; set; }
            public TempsCompe TempsCompe170 { get; set; }
            public TempsCompe TempsCompe200 { get; set; }
            public TempsCompe TempsCompe300 { get; set; }
            public TempsCompe TempsCompe400 { get; set; }
            public TempsCompe TempsCompe800 { get; set; }
            public TempsCompe TempsCompe1500 { get; set; }

            public double? TempsLP100 { get; set; }
            public double? TempsLP300 { get; set; }
            public double? TempsLP400 { get; set; }
            public double? TempsLP600 { get; set; }

            public class TempsCompe
            {
                public double? Quebec { get; set; }
                public double? Levis { get; set; }
                public double? Portneuf { get; set; }
                public double? SainteFoy { get; set; }
            }
        }


        /// <summary>
        /// Compilers les résultats de la compétition
        /// </summary>
        public static void CompilerLesResultats(DBPatinVitesse db)
        {
            db.Competition.ToList().ForEach(z => Console.WriteLine(string.Format("No : {0}, Nom : {1}", z.NoCompetition, z.Lieu)));
            Console.WriteLine("Entrer le numéro de compétition");
            string noStrCompe = Console.ReadLine();
            int noCompeti;
            if (int.TryParse(noStrCompe, out noCompeti))
            {
                IEnumerable<PatineurCompe> patineurs = db.PatineurCompe.Where(z => z.NoCompetition == noCompeti);
                foreach (PatineurCompe patCompe in patineurs.ToList())
                {
                    Dictionary<int, List<double>> dictTemps = new Dictionary<int, List<double>>();
                    int noPatineur = patCompe.NoPatineur;
                    int noPatineurCompe = patCompe.NoPatCompe;
                    Patineur p = db.Patineur.SingleOrDefault(z => z.NoPatineur == noPatineur);
                    IEnumerable<PatVagues> patineursVagues = db.PatVagues.Where(z => z.NoPatCompe == noPatineurCompe);
                    foreach (PatVagues pv in patineursVagues.ToList())
                    {
                        if (pv.Temps != 0)
                        { 
                        //pv.CleTPatVagues;
                        int vague = pv.CleTVagues;
                        Vagues v = db.Vagues.SingleOrDefault(z => z.CleTVagues == vague);
                        int distC = v.CleDistancesCompe;
                        IEnumerable<DistanceCompe> dvs = db.DistanceCompe.Where(z => z.CleDistancesCompe == distC);
                        foreach (DistanceCompe dv in dvs.ToList())
                        {
                            DistanceStandards ds = db.DistanceStandards.SingleOrDefault(z => z.NoDistance == dv.NoDistance);
                            if (dictTemps.ContainsKey(ds.LongueurEpreuve) )
                            {
                                dictTemps[ds.LongueurEpreuve].Add(pv.Temps);
                            }
                            else 
                            {
                                dictTemps.Add(ds.LongueurEpreuve, new List<double>() { pv.Temps });
                            }
                        }
                        }
                    }

                    double update300 = double.MinValue;
                    double update400 = double.MinValue;
                    double update800 = double.MinValue;
                    foreach (KeyValuePair<int, List<Double>> infoTemps in dictTemps)
                    {
                        double minNouveauTemps = infoTemps.Value.Min();
                        if (minNouveauTemps > 0)
                        {
                            if (infoTemps.Key == 300)
                            {

                                if (p.Classement1500 > minNouveauTemps)
                                {
                                    //Console.WriteLine(string.Format("Nom : {0}, Ancien temps {1} : {2} , Nouveau temps : {3}", string.Format("{0},{1}",p.Nom,p.Prenom),infoTemps.Key, p.Classement1500, minNouveauTemps));
                                    update300 = minNouveauTemps;
                                    Console.WriteLine(string.Format("{0};{1};{2};{3}", string.Format("{0},{1}", p.Nom, p.Prenom), infoTemps.Key, p.Classement1500, minNouveauTemps));
                                }
                            }
                            if (infoTemps.Key == 400)
                            {
                                if (p.Classement2000 > minNouveauTemps)
                                {
                                    //Console.WriteLine(string.Format("Nom : {0}, Ancien temps {1} : {2} , Nouveau temps : {3}", string.Format("{0},{1}", p.Nom, p.Prenom), infoTemps.Key, p.Classement2000, minNouveauTemps));
                                    update400 = minNouveauTemps;
                                    Console.WriteLine(string.Format("{0};{1};{2};{3}", string.Format("{0},{1}", p.Nom, p.Prenom), infoTemps.Key, p.Classement2000, minNouveauTemps));
                                }
                            }

                            if (infoTemps.Key == 800)
                            {
                                if (p.Classement2500 > minNouveauTemps)
                                {
                                    //Console.WriteLine(string.Format("Nom : {0}, Ancien temps {1} : {2} , Nouveau temps : {3}", string.Format("{0},{1}", p.Nom, p.Prenom), infoTemps.Key, p.Classement2500, minNouveauTemps));
                                    update800 = minNouveauTemps;
                                    Console.WriteLine(string.Format("{0};{1};{2};{3}", string.Format("{0},{1}", p.Nom, p.Prenom), infoTemps.Key, p.Classement2500, minNouveauTemps));
                                }
                            }
                        }
                    }

                    bool update = false;
                    if (update300 != double.MinValue)
                    {
                        p.Classement1500 = update300;
                        update = true;
                    }

                    if (update400 != double.MinValue)
                    {
                        p.Classement2000 = update400;
                        update = true;
                    }

                    if (update800 != double.MinValue)
                    {
                        p.Classement2500 = update800;
                        update = true;
                    }

                    if (update)
                    {
                        db.Update(p);
                        Console.WriteLine(string.Format("Patineur {0},{1} mis à jour", p.Nom, p.Prenom));
                    }
                }
            }
        }

        /// <summary>
        /// Compilers les résultats de la compétition
        /// </summary>
        public static void CompilerLesResultatsInter(DBPatinVitesse db)
        {
            db.Competition.ToList().ForEach(z => Console.WriteLine(string.Format("No : {0}, Nom : {1}", z.NoCompetition, z.Lieu)));
            Console.WriteLine("Entrer le numéro de compétition");
            string noStrCompe = Console.ReadLine();
            int noCompeti;
            if (int.TryParse(noStrCompe, out noCompeti))
            {
                IEnumerable<PatineurCompe> patineurs = db.PatineurCompe.Where(z => z.NoCompetition == noCompeti);
                foreach (PatineurCompe patCompe in patineurs.ToList())
                {
                    Dictionary<int, List<double>> dictTemps = new Dictionary<int, List<double>>();
                    int noPatineur = patCompe.NoPatineur;
                    int noPatineurCompe = patCompe.NoPatCompe;
                    Patineur p = db.Patineur.SingleOrDefault(z => z.NoPatineur == noPatineur);
                    IEnumerable<PatVagues> patineursVagues = db.PatVagues.Where(z => z.NoPatCompe == noPatineurCompe);
                    foreach (PatVagues pv in patineursVagues.ToList())
                    {
                        //pv.CleTPatVagues;
                        int vague = pv.CleTVagues;
                        Vagues v = db.Vagues.SingleOrDefault(z => z.CleTVagues == vague);
                        int distC = v.CleDistancesCompe;
                        IEnumerable<DistanceCompe> dvs = db.DistanceCompe.Where(z => z.CleDistancesCompe == distC);
                        foreach (DistanceCompe dv in dvs.ToList())
                        {
                            DistanceStandards ds = db.DistanceStandards.SingleOrDefault(z => z.NoDistance == dv.NoDistance);
                            if (dictTemps.ContainsKey(ds.LongueurEpreuve))
                            {
                                dictTemps[ds.LongueurEpreuve].Add(pv.Temps);
                            }
                            else
                            {
                                dictTemps.Add(ds.LongueurEpreuve, new List<double>() { pv.Temps });
                            }
                        }
                    }

                    double update400 = double.MinValue;
                    double update500 = double.MinValue;
                    double update1000 = double.MinValue;
                    double update1500 = double.MinValue;
                    foreach (KeyValuePair<int, List<Double>> infoTemps in dictTemps)
                    {
                        double minNouveauTemps = infoTemps.Value.Min();
                        if (minNouveauTemps > 0)
                        {
                            if (infoTemps.Key == 400)
                            {

                                if (p.Classement > minNouveauTemps)
                                {
                                    //Console.WriteLine(string.Format("Nom : {0}, Ancien temps {1} : {2} , Nouveau temps : {3}", string.Format("{0},{1}",p.Nom,p.Prenom),infoTemps.Key, p.Classement1500, minNouveauTemps));
                                    update400 = minNouveauTemps;
                                    Console.WriteLine(string.Format("{0};{1};{2};{3}", string.Format("{0},{1}", p.Nom, p.Prenom), infoTemps.Key, p.Classement, minNouveauTemps));
                                }
                            }
                            if (infoTemps.Key == 500)
                            {

                                if (p.Classement > minNouveauTemps)
                                {
                                    //Console.WriteLine(string.Format("Nom : {0}, Ancien temps {1} : {2} , Nouveau temps : {3}", string.Format("{0},{1}",p.Nom,p.Prenom),infoTemps.Key, p.Classement1500, minNouveauTemps));
                                    update500 = minNouveauTemps;
                                    Console.WriteLine(string.Format("{0};{1};{2};{3}", string.Format("{0},{1}", p.Nom, p.Prenom), infoTemps.Key, p.Classement, minNouveauTemps));
                                }
                            }
                            if (infoTemps.Key == 1000)
                            {
                                if (p.Classement1000 > minNouveauTemps)
                                {
                                    //Console.WriteLine(string.Format("Nom : {0}, Ancien temps {1} : {2} , Nouveau temps : {3}", string.Format("{0},{1}", p.Nom, p.Prenom), infoTemps.Key, p.Classement2000, minNouveauTemps));
                                    update1000 = minNouveauTemps;
                                    Console.WriteLine(string.Format("{0};{1};{2};{3}", string.Format("{0},{1}", p.Nom, p.Prenom), infoTemps.Key, p.Classement1000, minNouveauTemps));
                                }
                            }

                            if (infoTemps.Key == 1500)
                            {
                                if (p.Classement1500 > minNouveauTemps)
                                {
                                    //Console.WriteLine(string.Format("Nom : {0}, Ancien temps {1} : {2} , Nouveau temps : {3}", string.Format("{0},{1}", p.Nom, p.Prenom), infoTemps.Key, p.Classement2500, minNouveauTemps));
                                    update1500 = minNouveauTemps;
                                    Console.WriteLine(string.Format("{0};{1};{2};{3}", string.Format("{0},{1}", p.Nom, p.Prenom), infoTemps.Key, p.Classement1500, minNouveauTemps));
                                }
                            }
                        }
                    }

                    bool update = false;

                    if (update400 != double.MinValue)
                    {
                        p.Classement2000 = update400;
                        update = true;
                    }

                    if (update500 != double.MinValue)
                    {
                        p.Classement = update500;
                        update = true;
                    }

                    if (update1000 != double.MinValue)
                    {
                        p.Classement1000 = update1000;
                        update = true;
                    }

                    if (update1500 != double.MinValue)
                    {
                        p.Classement1500 = update1500;
                        update = true;
                    }

                    if (update)
                    {
                        db.Update(p);
                        Console.WriteLine(string.Format("Patineur {0},{1} mis à jour", p.Nom, p.Prenom));
                    }
                }
            }
        }

        public static void AjouterCompetiteur(OleDbConnection connection, int noCompe, string prenom, string nom, List<Patineur> lp)
        {

            Patineur p = lp.SingleOrDefault(z => z.Nom.ToUpper().RemoveDiacritics() == nom.ToUpper().RemoveDiacritics() && z.Prenom.ToUpper().RemoveDiacritics() == prenom.ToUpper().RemoveDiacritics());
            if (p == null)
            {
                IEnumerable<Patineur> enump = lp.Where(z => z.CodePat.StartsWith(nom.Substring(0, 3).ToUpper().RemoveDiacritics()));
                if (enump.Count() == 1)
                {
                    p = enump.First();
                    Console.WriteLine(string.Format("{0} {1} substitué à {2} {3} de la liste d'inscription", p.Nom, p.Prenom, nom, prenom));
                }
                else
                {
                    foreach (Patineur p1 in enump)
                    {
                        if (p1.Nom.Substring(0, 3).ToUpper().RemoveDiacritics() == nom.Substring(0, 3).ToUpper().RemoveDiacritics() &&
                            p1.Prenom.Substring(0, 3).ToUpper().RemoveDiacritics() == prenom.Substring(0, 3).ToUpper().RemoveDiacritics())
                        {
                            p = p1;
                            Console.WriteLine(string.Format("{0} {1} substitué à {2} {3} de la liste d'inscription", p.Nom, p.Prenom, nom, prenom));
                            break;
                        }
                    }

                }
            }

            if (p != null)
            {
                bool present = false;
                string strSQL2 = string.Format("Select * from TPatineur_Compe where UCase(NoPatineur) = UCase({0}) and NoCompetition = {1}", p.NoPatineur, noCompe);
                using (OleDbCommand CmdSql = new OleDbCommand(strSQL2, connection))
                {
                    CmdSql.CommandType = CommandType.Text;
                    OleDbDataReader reader = CmdSql.ExecuteReader();
                    while (reader.Read())
                    {
                        present = true;
                        Console.WriteLine(string.Format("Compétiteur déjà présent : {0} {1} ", prenom, nom));
                        break;
                    }
                }
                if (!present)
                {
                    string strSQL = "Insert into [TPatineur_Compe] ";
                    strSQL += "(NoCompetition, NoPatineur, Division, NoCategorie, NoClub, Rang, Groupe, Si_Regroup_Classement, Retirer)";
                    strSQL += " values (@NoCompetition,@NoPatineur,@Division,@NoCategorie,@NoClub,@Rang,@Groupe,@Si_Regroup_Classement, @Retirer)";
                    using (OleDbCommand CmdSql = new OleDbCommand(strSQL, connection))
                    {
                        CmdSql.CommandType = CommandType.Text;

                        CmdSql.Parameters.Add("@NoCompetition", OleDbType.Numeric).Value = noCompe;
                        CmdSql.Parameters.Add("@NoPatineur", OleDbType.Numeric).Value = p.NoPatineur;
                        CmdSql.Parameters.Add("@Division", OleDbType.VarChar).Value = p.Division;
                        CmdSql.Parameters.Add("@NoCategorie", OleDbType.Numeric).Value = p.NoCategorie;
                        CmdSql.Parameters.Add("@NoClub", OleDbType.Numeric).Value = p.NoClub;
                        CmdSql.Parameters.Add("@Rang", OleDbType.Numeric).Value = 0;
                        CmdSql.Parameters.Add("@Groupe", OleDbType.VarChar).Value = "Pas dans un groupe";
                        CmdSql.Parameters.Add("@Si_Regroup_Classement", OleDbType.Numeric).Value = 0;
                        CmdSql.Parameters.Add("@Retirer", OleDbType.Boolean).Value = false;
                        try
                        {
                            CmdSql.ExecuteNonQuery();
                            Console.WriteLine(string.Format("Compétiteur ajouté : {0} {1} ", prenom, nom));
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                            Console.WriteLine(string.Format("Ajouter le patineur : {0} {1}", prenom, nom));
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine(string.Format("Ajouter le patineur : {0} {1}", prenom, nom));
            }
        }

        public static List<Categorie> ObtenirCategories(OleDbConnection connection)
        {
            List<Categorie> lc = new List<Categorie>();
            // Provide the query string with a parameter placeholder.
            string queryString =
                "SELECT NoCategorie, Nom, AgeMin, AgeMax, AgeMinF, AgeMaxF FROM TCategorie";

            // Create the Command and Parameter objects.
            using (OleDbCommand command = new OleDbCommand(queryString, connection))
            {
                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Categorie c = new Categorie();
                            //Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
                            //    reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6]);
                            c.NoCategorie = (int)reader[0];
                            c.Nom = (string)reader[1];
                            c.AgeMin = (int)reader[2];
                            c.AgeMax = (int)reader[3];
                            c.AgeMinF = (int)reader[4];
                            c.AgeMaxF = (int)reader[5];
                            lc.Add(c);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return lc;
            }
        }


        public static List<Club> ObtenirClub(OleDbConnection connection)
        {
            List<Club> lc = new List<Club>();
            // Provide the query string with a parameter placeholder.
            string queryString =
                "SELECT NoClub, [Nom du Club], Adresse, Responsable, Tel, Commentaire, NoRegion, Abreviation, Selection FROM TClubs";

            // Create the Command and Parameter objects.
            using (OleDbCommand command = new OleDbCommand(queryString, connection))
            {
                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Club c = new Club();
                            //Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
                            //    reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6]);
                            c.NoClub = (int)reader[0];
                            c.NomClub = (string)reader[1];
                            c.Adresse = (string)reader[2];
                            c.Responsable = (string)reader[3];
                            c.Tel = (string)reader[4];
                            c.Commentaire = (string)reader[5];
                            c.NoRegion = (int)reader[6];
                            c.Abreviation = (string)reader[7];
                            c.Selection = (bool)reader[8];
                            lc.Add(c);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return lc;
            }
        }

        public static List<Patineur> ObtenirPatineurs(OleDbConnection connection)
        {

            List<Patineur> lp = new List<Patineur>();
            // Provide the query string with a parameter placeholder.
            string queryString =
                "SELECT NoPatineur, CodePat, Prenom, Nom, Division, NoCategorie, NoClub FROM TPatineurs";

            // Create the Command and Parameter objects.
            using (OleDbCommand command = new OleDbCommand(queryString, connection))
            {
                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Patineur p = new Patineur();
                            //Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
                            //    reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6]);
                            p.NoPatineur = (int)reader[0];
                            p.CodePat = (string)reader[1];
                            p.Prenom = (string)reader[2];
                            p.Nom = (string)reader[3];
                            p.Division = (string)reader[4];
                            p.NoCategorie = (int)reader[5];
                            p.NoClub = (int)reader[6];
                            lp.Add(p);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return lp;
            }
        }

        public static Patineur ObtenirPatineur(OleDbConnection connection, string prenom, string nom)
        {

            Patineur p = new Patineur();
            // Provide the query string with a parameter placeholder.
            string queryString =
                string.Format("SELECT NoPatineur, CodePat, Prenom, Nom, Division, NoCategorie, NoClub FROM TPatineurs where Prenom = '{0}' and Nom = '{1}' ", prenom, nom);

            // Create the Command and Parameter objects.
            using (OleDbCommand command = new OleDbCommand(queryString, connection))
            {
                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
                            //    reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6]);
                            p.NoPatineur = (int)reader[0];
                            p.CodePat = (string)reader[1];
                            p.Prenom = (string)reader[2];
                            p.Nom = (string)reader[3];
                            p.Division = (string)reader[4];
                            p.NoCategorie = (int)reader[5];
                            p.NoClub = (int)reader[6];
                            return p;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return null;
            }
        }


        public static void AfficherPatineursCompe(OleDbConnection connection, int noCompe, List<Patineur> lp, List<Categorie> lc, List<Club> lclb)
        {
            // Provide the query string with a parameter placeholder.
            string queryString =
                string.Format("SELECT NoPatCompe, NoCompetition, NoPatineur, Division, NoCategorie, NoClub, Rang, Retirer, Groupe, Si_Regroup_Classement, NoCasque FROM TPatineur_compe where noCompetition = {0} ", noCompe);

            // Create the Command and Parameter objects.
            using (OleDbCommand command = new OleDbCommand(queryString, connection))
            {
                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        int nb = 0;
                        using (StreamWriter sr = new StreamWriter("C:\\Users\\Bruno\\Documents\\PVData\\Devoir4\\PatineurCompe.txt"))
                        {

                            while (reader.Read())
                            {
                                int noPatCompet = (int)reader[0];
                                int noCompet = (int)reader[1];
                                int noPatineur = (int)reader[2];
                                string Division = (string)reader[3];
                                int noCategorie = (int)reader[4];
                                int noClub = (int)reader[5];
                                short rang = -1;
                                if (!reader.IsDBNull(6))
                                {
                                    rang = (short)reader[6];
                                }

                                bool retirer = (bool)reader[7];
                                string Groupe = (string)reader[8];
                                short Si_Regroup_Classement = (short)reader[9];
                                int noCasque = -1;
                                if (!reader.IsDBNull(10))
                                {
                                    noCasque = (int)reader[10];
                                }
                                Patineur p = lp.SingleOrDefault(z => z.NoPatineur == noPatineur);
                                Categorie c = lc.SingleOrDefault(z => z.NoCategorie == noCategorie);
                                Club clb = lclb.SingleOrDefault(z => z.NoClub == noClub);
                                sr.WriteLine(string.Format("Nom : {0}\tPrenom : {1}\tClub : {2}\tCategorie : {3}", p.Nom, p.Prenom, clb.NomClub, c.Nom));
                                nb += 1;
                                //Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}",
                                //    reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[8], reader[9]);
                            }
                            sr.WriteLine("Total : {0} inscriptions", nb);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Statistiques d'une compétition
        /// </summary>
        public static void statistiques255(DBPatinVitesse db, int noCompe)
        {
            string distanceStat = "300";

            List<Categorie> lc = db.Categorie.Where(z => z.Nom == "Benjamin" || z.Nom == "Minime").ToList();

            List<GroupesCompe> lgc = db.GroupesCompe.Where(z => (z.Groupe == "Minime Benjamin 7" ||
                z.Groupe == "Minime Benjamin 8" ||
                z.Groupe == "Minime Benjamin 9"
                ) && z.NoCompetition == noCompe).ToList();

            //lgc.First().IDGroupesCompe



            // On fait juste pour le 255 mètres (faux 300)
            List<DistanceStandards> lds = db.DistanceStandards.Where(z => z.Distance == distanceStat).ToList();
            List<DistanceStandards> lds1 = lds.Where(z => lc.Select(k => k.NoCategorie).Contains(z.NoCategorie)).ToList();

            string distance = string.Join(",", lds1.Select(z => z.NoDistance));


            List<DistanceCompe> ldc1 = new List<DistanceCompe>();

            foreach (DistanceCompe dc in db.DistanceCompe)
            {
                foreach (DistanceStandards ds in lds1)
                {
                    if (ds.NoDistance == dc.NoDistance)
                    {
                        ldc1.Add(dc);
                        break;
                    }
                }
            }

            //ldc1.First().IDDistancesCompe;
            // ldc1.First().CleDistancesCompe;



            List<Vagues> lv = new List<Vagues>();

            foreach (Vagues v in db.Vagues)
            {
                foreach (DistanceCompe dc in ldc1)
                {
                    if (v.CleDistancesCompe == dc.CleDistancesCompe)
                    {
                        lv.Add(v);
                        break;
                    }
                }
            }



            List<PatVagues> lpv = new List<PatVagues>();
            foreach (PatVagues pv in db.PatVagues)
            {
                foreach (Vagues v in lv)
                {
                    if (v.CleTVagues == pv.CleTVagues)
                    {
                        lpv.Add(pv);
                        break;
                    }
                }
            }


            List<PatVagues> lpv1 = lpv.Where(z => z.Temps > 0).ToList();


            var queryReulstat = from patVague in lpv1
                                join pcompe in db.PatineurCompe on patVague.NoPatCompe equals pcompe.NoPatCompe
                                join patineurx in db.Patineur on pcompe.NoPatineur equals patineurx.NoPatineur
                                select new
                                {
                                    Nom = patineurx.Nom,
                                    Prenom = patineurx.Prenom,
                                    Classement1000 = patineurx.Classement1000,
                                    Classement1500 = patineurx.Classement1500,
                                    Classement2000 = patineurx.Classement2000,
                                    Classement2500 = patineurx.Classement2500,
                                    ClassementGeneral = patineurx.ClassementGeneral,
                                    CodePat = patineurx.CodePat,
                                    NouveauTemps = patVague.Temps,
                                    NomCategorie = pcompe.Categorie.Nom,
                                    Meilleur = patineurx.Classement1500 >= patVague.Temps
                                };

            var queryResultat2 = queryReulstat.Where(z => z.Meilleur == true).ToList();

            var xxxx = queryResultat2.GroupBy(z => z.CodePat).Select(y => new
            {
                ABC = y.Key,
                DEV = y.Min(k => k.NouveauTemps)
            });

            var queryResultat3 = from r1 in xxxx
                                 join patineurx in db.Patineur on r1.ABC equals patineurx.CodePat
                                 join clb in db.Club on patineurx.NoClub equals clb.NoClub
                                 join cat in db.Categorie on patineurx.NoCategorie equals cat.NoCategorie
                                 select new
                                 {
                                     Nom = patineurx.Nom,
                                     Prenom = patineurx.Prenom,
                                     Classement1000 = patineurx.Classement1000,
                                     Classement1500 = patineurx.Classement1500,
                                     Classement2000 = patineurx.Classement2000,
                                     Classement2500 = patineurx.Classement2500,
                                     ClassementGeneral = patineurx.ClassementGeneral,
                                     CodePat = patineurx.CodePat,
                                     NouveauTemps = r1.DEV,
                                     NomCategorie = cat.Nom,
                                     NomClub = clb.NomClub
                                 };




            queryResultat3.OrderBy(z => z.CodePat);
            queryResultat3.ToList();

            List<Tmp> listTmp = new List<Tmp>();
            foreach (dynamic e in queryResultat3)
            {
                Console.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6}", e.Nom, e.Prenom, e.CodePat, e.NouveauTemps, e.Classement1500, e.NomCategorie, e.NomClub));
                string leCodePat = e.CodePat;
                listTmp.Add(new Tmp() { CodePat = e.CodePat, tempsRef = e.NouveauTemps });
                //Patineur p = db.Patineur.SingleOrDefault(z => z.CodePat == leCodePat);
                //p.Classement1500 = e.NouveauTemps;
                //db.Update(p);
            }

            List<Tmp> listTmp1 = listTmp.OrderBy(z => z.tempsRef).ToList();


            // Trier les compétiteurs selon le rang
            List<Categorie> lcat = db.Categorie.Where(z => z.Nom == "Benjamin" || z.Nom == "Minime").ToList();
            //z => z.Nom == "Cadet" || z.Nom == "Juvénile"
            int premier = lcat.Select(z => z.NoCategorie).First();
            int deuxième = lcat.Select(z => z.NoCategorie).Last();
            // Ceux qui ne sont pas classés par temps
            var abc = db.Patineur.Where(z => z.Classement1500 > 50 && (z.NoCategorie == premier || z.NoCategorie == deuxième));
            // Ceux qui étaient présent dans la compétition

            List<Patineur> lpx = abc.OrderBy(z => z.Classement1500).ToList();
            int dep1 = 200;
            int nbUpdate = 0;

            Console.WriteLine("Patineur sans temps mis à jour");
            foreach (Tmp t in listTmp1)
            {
                Patineur p = db.Patineur.SingleOrDefault(z => z.CodePat == t.CodePat);
                if (p.Classement1500 > 50)
                {
                    p.ClassementGeneral = dep1;
                    dep1 += 1;
                    Console.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6}", p.Nom, p.Prenom, p.CodePat, dep1, p.Classement1500, p.NoCategorie, p.NoClub));
                    db.Update(p);
                    nbUpdate += 1;
                }
            }

            Console.WriteLine(nbUpdate);


        }

        public class Tmp
        {
            public string CodePat { get; set; }
            public double tempsRef { get; set; }
            public int rang { get; set; }
        }

        /// <summary>
        /// Statistiques d'une compétition
        /// </summary>
        public static void statistiques300(DBPatinVitesse db, int noCompe)
        {
            string distanceStat = "300";

            List<PatineurCompe> xxx = db.PatineurCompe.ToList();
            PatineurCompe px = xxx.First();


            List<Categorie> lc = db.Categorie.Where(z => z.Nom == "Benjamin" || z.Nom == "Minime" || z.Nom == "Cadet" || z.Nom == "Juvénile").ToList();

            List<GroupesCompe> lgc = db.GroupesCompe.Where(z => (z.Groupe == "Minime Benjamin 1" ||
                z.Groupe == "Minime Benjamin 2" ||
                z.Groupe == "Minime Benjamin 3" ||
                z.Groupe == "Minime Benjamin 4" ||
                z.Groupe == "Minime Benjamin 5" ||
                z.Groupe == "Minime Benjamin 6" ||
                z.Groupe == "Minime Benjamin 7" ||
                z.Groupe == "Minime Benjamin 8" ||
                z.Groupe == "Minime Benjamin 9" ||
                z.Groupe == "Minime Benjamin 10" ||
                z.Groupe == "Cadet Juvénile 1" ||
                z.Groupe == "Cadet Juvénile 2" ||
                z.Groupe == "Cadet Juvénile 3"
                ) && z.NoCompetition == noCompe).ToList();

            List<string> listeGroupe = lgc.Select(z => z.Groupe).ToList();



            //List<DistanceStandards> lds = db.DistanceStandards.Where(z => (z.Distance == "300" || z.Distance == "400")).ToList();
            // On fait juste pour le 400 mètres
            List<DistanceStandards> lds = db.DistanceStandards.Where(z => z.Distance == distanceStat).ToList();
            List<DistanceStandards> lds1 = lds.Where(z => lc.Select(k => k.NoCategorie).Contains(z.NoCategorie)).ToList();

            string distance = string.Join(",", lds1.Select(z => z.NoDistance));


            List<DistanceCompe> ldc1 = new List<DistanceCompe>();

            foreach (DistanceCompe dc in db.DistanceCompe)
            {
                foreach (DistanceStandards ds in lds1)
                {
                    if (ds.NoDistance == dc.NoDistance)
                    {
                        ldc1.Add(dc);
                        break;
                    }
                }
            }

            //ldc1.First().IDDistancesCompe;
            // ldc1.First().CleDistancesCompe;



            List<Vagues> lv = new List<Vagues>();

            foreach (Vagues v in db.Vagues)
            {
                foreach (DistanceCompe dc in ldc1)
                {
                    if (v.CleDistancesCompe == dc.CleDistancesCompe)
                    {
                        lv.Add(v);
                        break;
                    }
                }
            }



            List<PatVagues> lpv = new List<PatVagues>();
            foreach (PatVagues pv in db.PatVagues)
            {
                foreach (Vagues v in lv)
                {
                    if (v.CleTVagues == pv.CleTVagues)
                    {
                        lpv.Add(pv);
                        break;
                    }
                }
            }


            List<PatVagues> lpv1 = lpv.Where(z => z.Temps > 0).ToList();


            var queryReulstat = from patVague in lpv1
                                join pcompe in db.PatineurCompe on patVague.NoPatCompe equals pcompe.NoPatCompe
                                join patineurx in db.Patineur on pcompe.NoPatineur equals patineurx.NoPatineur
                                where listeGroupe.Contains(pcompe.Groupe)
                                select new
                                {
                                    Nom = patineurx.Nom,
                                    Prenom = patineurx.Prenom,
                                    Classement1000 = patineurx.Classement1000,
                                    Classement1500 = patineurx.Classement1500,
                                    Classement2000 = patineurx.Classement2000,
                                    Classement2500 = patineurx.Classement2500,
                                    ClassementGeneral = patineurx.ClassementGeneral,
                                    CodePat = patineurx.CodePat,
                                    NouveauTemps = patVague.Temps,
                                    NomCategorie = pcompe.Categorie.Nom,
                                    Meilleur = patineurx.Classement1500 >= patVague.Temps
                                }
                                ;

            var queryResultat2 = queryReulstat.Where(z => z.Meilleur == true).ToList();

            var xxxx = queryResultat2.GroupBy(z => z.CodePat).Select(y => new
            {
                ABC = y.Key,
                DEV = y.Min(k => k.NouveauTemps)
            });

            var queryResultat3 = from r1 in xxxx
                                 join patineurx in db.Patineur on r1.ABC equals patineurx.CodePat
                                 join clb in db.Club on patineurx.NoClub equals clb.NoClub
                                 join cat in db.Categorie on patineurx.NoCategorie equals cat.NoCategorie
                                 select new
                                 {
                                     Nom = patineurx.Nom,
                                     Prenom = patineurx.Prenom,
                                     Classement1000 = patineurx.Classement1000,
                                     Classement1500 = patineurx.Classement1500,
                                     Classement2000 = patineurx.Classement2000,
                                     Classement2500 = patineurx.Classement2500,
                                     ClassementGeneral = patineurx.ClassementGeneral,
                                     CodePat = patineurx.CodePat,
                                     NouveauTemps = r1.DEV,
                                     NomCategorie = cat.Nom,
                                     NomClub = clb.NomClub
                                 };




            queryResultat3.OrderBy(z => z.CodePat);
            foreach (dynamic e in queryResultat3)
            {
                Console.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6}", e.Nom, e.Prenom, e.CodePat, e.NouveauTemps, e.Classement1500, e.NomCategorie, e.NomClub));
                string leCodePat = e.CodePat;
                Patineur p = db.Patineur.SingleOrDefault(z => z.CodePat == leCodePat);
                p.Classement1500 = e.NouveauTemps;
                db.Update(p);
            }

        }


        /// <summary>
        /// Statistiques d'une compétition
        /// </summary>
        public static void statistiques400(DBPatinVitesse db, int noCompe)
        {
            string distanceStat = "400";

            List<Categorie> lc = db.Categorie.Where(z => z.Nom == "Benjamin" || z.Nom == "Minime" || z.Nom == "Cadet" || z.Nom == "Juvénile").ToList();

            List<GroupesCompe> lgc = db.GroupesCompe.Where(z => (z.Groupe == "Minime Benjamin 1" ||
                z.Groupe == "Minime Benjamin 2" ||
                z.Groupe == "Minime Benjamin 3" ||
                z.Groupe == "Minime Benjamin 4" ||
                z.Groupe == "Minime Benjamin 5" ||
                z.Groupe == "Minime Benjamin 6" ||
                z.Groupe == "Minime Benjamin 7" ||
                z.Groupe == "Minime Benjamin 8" ||
                z.Groupe == "Cadet Juvénile 1" ||
                z.Groupe == "Cadet Juvénile 2" ||
                z.Groupe == "Cadet Juvénile 3"
                ) && z.NoCompetition == noCompe).ToList();

            List<string> listeGroupe = lgc.Select(z => z.Groupe).ToList();

            //lgc.First().IDGroupesCompe

            //List<DistanceStandards> lds = db.DistanceStandards.Where(z => (z.Distance == "300" || z.Distance == "400")).ToList();
            // On fait juste pour le 400 mètres
            List<DistanceStandards> lds = db.DistanceStandards.Where(z => z.Distance == distanceStat).ToList();
            List<DistanceStandards> lds1 = lds.Where(z => lc.Select(k => k.NoCategorie).Contains(z.NoCategorie)).ToList();

            string distance = string.Join(",", lds1.Select(z => z.NoDistance));


            List<DistanceCompe> ldc1 = new List<DistanceCompe>();

            foreach (DistanceCompe dc in db.DistanceCompe)
            {
                foreach (DistanceStandards ds in lds1)
                {
                    if (ds.NoDistance == dc.NoDistance)
                    {
                        ldc1.Add(dc);
                        break;
                    }
                }
            }

            //ldc1.First().IDDistancesCompe;
            // ldc1.First().CleDistancesCompe;



            List<Vagues> lv = new List<Vagues>();

            foreach (Vagues v in db.Vagues)
            {
                foreach (DistanceCompe dc in ldc1)
                {
                    if (v.CleDistancesCompe == dc.CleDistancesCompe)
                    {
                        lv.Add(v);
                        break;
                    }
                }
            }



            List<PatVagues> lpv = new List<PatVagues>();
            foreach (PatVagues pv in db.PatVagues)
            {
                foreach (Vagues v in lv)
                {
                    if (v.CleTVagues == pv.CleTVagues)
                    {
                        lpv.Add(pv);
                        break;
                    }
                }
            }


            List<PatVagues> lpv1 = lpv.Where(z => z.Temps > 0).ToList();


            var queryReulstat = from patVague in lpv1
                                join pcompe in db.PatineurCompe on patVague.NoPatCompe equals pcompe.NoPatCompe
                                join patineurx in db.Patineur on pcompe.NoPatineur equals patineurx.NoPatineur
                                where listeGroupe.Contains(pcompe.Groupe)
                                select new
                                {
                                    Nom = patineurx.Nom,
                                    Prenom = patineurx.Prenom,
                                    Classement1000 = patineurx.Classement1000,
                                    Classement1500 = patineurx.Classement1500,
                                    Classement2000 = patineurx.Classement2000,
                                    Classement2500 = patineurx.Classement2500,
                                    ClassementGeneral = patineurx.ClassementGeneral,
                                    CodePat = patineurx.CodePat,
                                    NouveauTemps = patVague.Temps,
                                    NomCategorie = pcompe.Categorie.Nom,
                                    Meilleur = patineurx.Classement2000 >= patVague.Temps
                                };

            var queryResultat2 = queryReulstat.Where(z => z.Meilleur == true).ToList();

            var xxxx = queryResultat2.GroupBy(z => z.CodePat).Select(y => new
            {
                ABC = y.Key,
                DEV = y.Min(k => k.NouveauTemps)

            });

            var queryResultat3 = from r1 in xxxx
                                 join patineurx in db.Patineur on r1.ABC equals patineurx.CodePat
                                 join clb in db.Club on patineurx.NoClub equals clb.NoClub
                                 join cat in db.Categorie on patineurx.NoCategorie equals cat.NoCategorie
                                 select new
                                 {
                                     Nom = patineurx.Nom,
                                     Prenom = patineurx.Prenom,
                                     Classement1000 = patineurx.Classement1000,
                                     Classement1500 = patineurx.Classement1500,
                                     Classement2000 = patineurx.Classement2000,
                                     Classement2500 = patineurx.Classement2500,
                                     ClassementGeneral = patineurx.ClassementGeneral,
                                     CodePat = patineurx.CodePat,
                                     NouveauTemps = r1.DEV,
                                     NomCategorie = cat.Nom,
                                     NomClub = clb.NomClub
                                 };




            queryResultat3.OrderBy(z => z.CodePat);
            foreach (dynamic e in queryResultat3)
            {
                Console.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6}", e.Nom, e.Prenom, e.CodePat, e.NouveauTemps, e.Classement2000, e.NomCategorie, e.NomClub));
                string leCodePat = e.CodePat;
                Patineur p = db.Patineur.SingleOrDefault(z => z.CodePat == leCodePat);
                p.Classement2000 = e.NouveauTemps;
                db.Update(p);
            }

        }

        /// <summary>
        /// Statistiques d'une compétition
        /// </summary>
        public static void statistiques800(DBPatinVitesse db, int noCompe)
        {
            string distanceStat = "800";

            List<Categorie> lc = db.Categorie.Where(z => z.Nom == "Benjamin" || z.Nom == "Minime" || z.Nom == "Cadet" || z.Nom == "Juvénile").ToList();

            List<GroupesCompe> lgc = db.GroupesCompe.Where(z => (z.Groupe == "Minime Benjamin 1" ||
                z.Groupe == "Minime Benjamin 2" ||
                z.Groupe == "Minime Benjamin 3" ||
                z.Groupe == "Minime Benjamin 4" ||
                z.Groupe == "Minime Benjamin 5" ||
                z.Groupe == "Minime Benjamin 6" ||
                z.Groupe == "Minime Benjamin 7" ||
                z.Groupe == "Minime Benjamin 8" ||
                z.Groupe == "Cadet Juvénile 1" ||
                z.Groupe == "Cadet Juvénile 2" ||
                z.Groupe == "Cadet Juvenile 3"
                ) && z.NoCompetition == noCompe).ToList();

            List<string> listeGroupe = lgc.Select(z => z.Groupe).ToList();

            //lgc.First().IDGroupesCompe

            //List<DistanceStandards> lds = db.DistanceStandards.Where(z => (z.Distance == "300" || z.Distance == "400")).ToList();
            // On fait juste pour le 400 mètres
            List<DistanceStandards> lds = db.DistanceStandards.Where(z => z.Distance.StartsWith(distanceStat)).ToList();
            List<DistanceStandards> lds1 = lds.Where(z => lc.Select(k => k.NoCategorie).Contains(z.NoCategorie)).ToList();

            string distance = string.Join(",", lds1.Select(z => z.NoDistance));


            List<DistanceCompe> ldc1 = new List<DistanceCompe>();

            foreach (DistanceCompe dc in db.DistanceCompe)
            {
                foreach (DistanceStandards ds in lds1)
                {
                    if (ds.NoDistance == dc.NoDistance)
                    {
                        ldc1.Add(dc);
                        break;
                    }
                }
            }

            List<Vagues> lv = new List<Vagues>();

            foreach (Vagues v in db.Vagues)
            {
                foreach (DistanceCompe dc in ldc1)
                {
                    if (v.CleDistancesCompe == dc.CleDistancesCompe)
                    {
                        lv.Add(v);
                        break;
                    }
                }
            }

            List<PatVagues> lpv = new List<PatVagues>();
            foreach (PatVagues pv in db.PatVagues)
            {
                foreach (Vagues v in lv)
                {
                    if (v.CleTVagues == pv.CleTVagues)
                    {
                        lpv.Add(pv);
                        break;
                    }
                }
            }

            List<PatVagues> lpv1 = lpv.Where(z => z.Temps > 0).ToList();

            var queryReulstat = from patVague in lpv1
                                join pcompe in db.PatineurCompe on patVague.NoPatCompe equals pcompe.NoPatCompe
                                join patineurx in db.Patineur on pcompe.NoPatineur equals patineurx.NoPatineur
                                where listeGroupe.Contains(pcompe.Groupe)
                                select new
                                {
                                    Nom = patineurx.Nom,
                                    Prenom = patineurx.Prenom,
                                    Classement1000 = patineurx.Classement1000,
                                    Classement1500 = patineurx.Classement1500,
                                    Classement2000 = patineurx.Classement2000,
                                    Classement2500 = patineurx.Classement2500,
                                    ClassementGeneral = patineurx.ClassementGeneral,
                                    CodePat = patineurx.CodePat,
                                    NouveauTemps = patVague.Temps,
                                    NomCategorie = pcompe.Categorie.Nom,
                                    Meilleur = patineurx.Classement2500 >= patVague.Temps
                                };

            var queryResultat2 = queryReulstat.Where(z => z.Meilleur == true).ToList();

            var xxxx = queryResultat2.GroupBy(z => z.CodePat).Select(y => new
            {
                ABC = y.Key,
                DEV = y.Min(k => k.NouveauTemps)

            });

            var queryResultat3 = from r1 in xxxx
                                 join patineurx in db.Patineur on r1.ABC equals patineurx.CodePat
                                 join clb in db.Club on patineurx.NoClub equals clb.NoClub
                                 join cat in db.Categorie on patineurx.NoCategorie equals cat.NoCategorie
                                 select new
                                 {
                                     Nom = patineurx.Nom,
                                     Prenom = patineurx.Prenom,
                                     Classement1000 = patineurx.Classement1000,
                                     Classement1500 = patineurx.Classement1500,
                                     Classement2000 = patineurx.Classement2000,
                                     Classement2500 = patineurx.Classement2500,
                                     ClassementGeneral = patineurx.ClassementGeneral,
                                     CodePat = patineurx.CodePat,
                                     NouveauTemps = r1.DEV,
                                     NomCategorie = cat.Nom,
                                     NomClub = clb.NomClub
                                 };




            queryResultat3.OrderBy(z => z.CodePat);
            foreach (dynamic e in queryResultat3)
            {
                Console.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6}", e.Nom, e.Prenom, e.CodePat, e.NouveauTemps, e.Classement2500, e.NomCategorie, e.NomClub));
                string leCodePat = e.CodePat;
                Patineur p = db.Patineur.SingleOrDefault(z => z.CodePat == leCodePat);
                p.Classement2500 = e.NouveauTemps;
                db.Update(p);
            }
        }


        public static void AfficherCompetition(OleDbConnection connection)
        {
            // Provide the query string with a parameter placeholder.
            string queryString =
                "SELECT * FROM TCompetition";

            // Create the Command and Parameter objects.
            using (OleDbCommand command = new OleDbCommand(queryString, connection))
            {
                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                                reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    /// <summary>
    /// Contains approximate string matching
    /// </summary>
    static class LevenshteinDistance
    {
        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        public static int Compute(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
    }

}
