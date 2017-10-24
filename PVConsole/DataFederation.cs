using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVConsole
{
    public class DataFederation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<PatFedeTemps> ObtenirTempsCSV()
        {
            List<PatFedeTemps> liste = new List<PatFedeTemps>();
            int depart = 1;
            int no;
            string prenom;
            string nom;
            string club;
            double temps400;
            double temps1000;

            //C:\Users\Bruno\Documents\Arpvq 2016-2017\QualifJQ\ClassementInter.csv format CSV
            //@"C:\Users\Bruno\Documents\Arpvq 2016-2017\QualifJQ\gars.txt" format bizare
            using (System.IO.StreamReader sr = new System.IO.StreamReader(@"C:\Users\Bruno\Documents\Arpvq 2016-2017\QualifJQ\ClassementInter.csv"))
            {
                string ligne = sr.ReadLine();
                // on saute la première ligne
                ligne = sr.ReadLine();
                PatFedeTemps patfedetemps = null;
                                int nblu = 0;
                while (!sr.EndOfStream || !string.IsNullOrEmpty(ligne))
                {
                    nblu += 1;
                    string[] elements = ligne.Split(';');
                    patfedetemps = new PatFedeTemps();
                    if (int.TryParse(elements[0], out no))
                    {
                        patfedetemps.No = no;
      
                    }
                        // prenom
                        patfedetemps.Prenom = elements[1];
                    patfedetemps.Nom = elements[2];
                    int age;
                    if (int.TryParse(elements[3], out age))
                    {
                        patfedetemps.Age = age;
                    }
                    patfedetemps.Club = elements[4];
                    string t400tmp = elements[5];
                    if (double.TryParse(t400tmp.Replace(".", string.Empty).Replace(":", "."), System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temps400))
                    {
                        if (elements[11] == "I")
                        { 
                            patfedetemps.Temps400 = temps400;
                        }
                        if (elements[11] == "P")
                        {
                            patfedetemps.Temps500 = temps400;
                        }
                    }
                    string t1000tmp = elements[7];
                    if (double.TryParse(t1000tmp.Replace(".", string.Empty).Replace(":", "."), System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temps1000))
                    {
                            patfedetemps.Temps1000 = temps1000;
                    }
                    patfedetemps.Division = elements[11];
                    patfedetemps.Genre = elements[10];

                    liste.Add(patfedetemps);
                    ligne = sr.ReadLine();
                }
            }
            return liste;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<PatFedeTemps> ObtenirTemps()
        {
            List<PatFedeTemps> liste = new List<PatFedeTemps>();
            int depart = 1;
            int no;
            string prenom;
            string nom;
            string club;
            double temps400;
            double temps1000;

            //C:\Users\Bruno\Documents\Arpvq 2016-2017\QualifJQ\ClassementInter.csv format CSV
            //@"C:\Users\Bruno\Documents\Arpvq 2016-2017\QualifJQ\gars.txt" format bizare
            using (System.IO.StreamReader sr = new System.IO.StreamReader(@"C:\Users\Bruno\Documents\Arpvq 2016-2017\QualifJQ\gars.txt"))
            {
                string ligne = sr.ReadLine();
                PatFedeTemps patfedetemps = null;
                int nblu = 0;
                while (!sr.EndOfStream || !string.IsNullOrEmpty(ligne))
                {
                    nblu += 1;
                    if (int.TryParse(ligne, out no))
                    {
                        if (no == depart)
                        {
                            if (patfedetemps != null)
                            {
                                liste.Add(patfedetemps);
                            }
                            patfedetemps = new PatFedeTemps();
                            depart += 1;
                            nblu = 1;
                        }
                    }
                    if (nblu == 2)
                    {
                        // prenom
                        patfedetemps.Prenom = ligne;
                    }
                    if (nblu == 3)
                    {
                        string[] x = ligne.Split(',');
                        patfedetemps.Nom = x[0];
                        int age;
                        if (int.TryParse(x[1].Replace("(", string.Empty).Replace(")", string.Empty).TrimEnd().TrimStart(), out age))
                        {
                            patfedetemps.Age = age;
                        }
                    }
                    if (nblu == 4)
                    {
                        patfedetemps.Club = ligne;
                    }
                    if (nblu == 5)
                    {
                        if (double.TryParse(ligne.Replace(".", string.Empty).Replace(":", "."), System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temps400 ))
                        {
                            patfedetemps.Temps400 = temps400;
                        }
                    }
                    if (nblu > 5 && nblu < 99)
                    {
                        if (double.TryParse(ligne.Replace(".", string.Empty).Replace(":", "."), System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temps1000))
                        {
                            patfedetemps.Temps1000 = temps1000;
                            nblu = 100;
                        }
                    }
                    ligne = sr.ReadLine();


                }
            }
            return liste;

        }


    }

    public class PatFedeTemps
    {
        public int No { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public int Age { get; set; }
        public string Club { get; set; }
        public Double Temps400 { get; set; }
        public Double Temps500 { get; set; }
        public Double Temps1000 { get; set; }
        public string Genre { get; set; }
        public string Division { get; set; }
    }
}
