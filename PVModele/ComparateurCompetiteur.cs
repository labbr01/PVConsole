using System.Collections;
using System;
using PVModele.Tables;
using System.Collections.Generic;
namespace PVModele
{
    public class ComparateurCompetiteur : IComparer<PatineurCompe>
    {
        /// <summary>
        /// Gets or sets le mode du comparateur, 0 = 300 1 = 400, 2=300/400, 
        /// </summary>
        public int Mode { get; set; }

        //public int Compare(Object q, Object r)
        //{
        //    return this.Compare((PatineurCompe)q, (PatineurCompe)r);
        //}

        public int Compare(PatineurCompe q, PatineurCompe r)
        {
            if (this.Mode == 0)
            {
                return this.TrierSur300(q,r);
            }
            else if (this.Mode == 1)
            {
                return this.TrierSur400(q, r);
            }

            else if (this.Mode == 2)
            {
                // On compare les deux temps
                if (q.Patineur.Classement2000 < 10 && r.Patineur.Classement2000 < 10 &&
                    q.Patineur.Classement1500 < 10 && r.Patineur.Classement1500 < 10)
                {
                    // Les deux ont des temps au 300 et au 400 mètres
                    if (q.Patineur.Classement1500 < r.Patineur.Classement1500 &&
                        q.Patineur.Classement2000 < r.Patineur.Classement2000)
                    {
                        // Le classement 300 et 400 du patineur q a un meilleur temps
                        return -1;
                    }
                    else if (q.Patineur.Classement1500 > r.Patineur.Classement1500 &&
                      q.Patineur.Classement2000 > r.Patineur.Classement2000)
                    {
                        // Le classement 300 et 400 du patineur r a un meilleur temps
                        return 1;
                    }
                    // Ici, les deux on un des deux classement meilleur que l'autre, alors
                    // on va prioriser le classement du 400;
                    else if (q.Patineur.Classement2000 < r.Patineur.Classement2000)
                    {
                        return -1;
                    }
                    else if (q.Patineur.Classement2000 > r.Patineur.Classement2000)
                    {
                        return 1;
                    }

                    return 0; // Les patineurs sont pareil               
                }
                else if (q.Patineur.Classement2000 < 10 && r.Patineur.Classement2000 < 10)
                {
                    // Les deux patineurs ont un classement 400, mais il manque un patineur au classement 300
                    return this.TrierSur400(q, r);
                }
                else if (q.Patineur.Classement1500 < 10 && r.Patineur.Classement1500 < 10)
                {
                    // Les deux patineurs ont un classement 300, mais il manque un patineur au classement 400
                    return this.TrierSur300(q, r);
                }

                // Les patineurs sont équivalents
                return 0;
            }

            return 0;
        }

        private int TrierSur300(PatineurCompe q, PatineurCompe r)
        {
            // On compare uniquement le 300 mètres
            if (q.Patineur.Classement1500 < 10 && r.Patineur.Classement1500 < 10)
            {
                // Les deux ont un temps au 300 mètres
                if (q.Patineur.Classement1500 < r.Patineur.Classement1500)
                {
                    // le temps du partineur q est inférieur
                    return -1;
                }
                else if (q.Patineur.Classement1500 > r.Patineur.Classement1500)
                {
                    // le temps du partineur r est inférieur
                    return 1;
                }

                // Les deux patineurs ont le même temps
                return 0;
            }
            else if (q.Patineur.Classement1500 < 10)
            {
                // le patineur q va avant car l'autre n'a pas de temps et le q
                return -1;
            }
            else if (r.Patineur.Classement1500 < 10)
            {
                // Le patineur r va avant car l'autre n'a pas de temps et le r en a un
                return 1;
            }

            // Aucun des deux patineurs n'a un temps au 300 (on y va par la date)
            if (q.Patineur.DateNaissance > r.Patineur.DateNaissance)
            {
                return -1;
            }
            else if (q.Patineur.DateNaissance < r.Patineur.DateNaissance)
            {
                return 1;
            }

            // La date de naissance est pareil, et ils n'ont pas de temps
            return 0;
        }

        private int TrierSur400(PatineurCompe q, PatineurCompe r)
        {
            // On compare uniquement le 400 mètres
            if (q.Patineur.Classement2000 < 10 && r.Patineur.Classement2000 < 10)
            {
                // Les deux ont un temps au 400 mètres
                if (q.Patineur.Classement2000 < r.Patineur.Classement2000)
                {
                    // le temps du partineur q est inférieur
                    return -1;
                }
                else if (q.Patineur.Classement2000 > r.Patineur.Classement2000)
                {
                    // le temps du partineur r est inférieur
                    return 1;
                }

                // Les deux patineurs ont le même temps
                return 0;
            }
            else if (q.Patineur.Classement2000 < 10)
            {
                // le patineur q va avant car l'autre n'a pas de temps et le q
                return -1;
            }
            else if (r.Patineur.Classement2000 < 10)
            {
                // Le patineur r va avant car l'autre n'a pas de temps et le r en a un
                return 1;
            }

            // Aucun des deux patineurs n'a un temps au 400 (on y va par la date)
            if (q.Patineur.DateNaissance > r.Patineur.DateNaissance)
            {
                return -1;
            }
            else if (q.Patineur.DateNaissance < r.Patineur.DateNaissance)
            {
                return 1;
            }

            // La date de naissance est pareil, et ils n'ont pas de temps
            return 0;
        }
    }
}