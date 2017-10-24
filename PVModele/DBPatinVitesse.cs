using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;
using LinqToDB;
using PVModele.Tables;

namespace PVModele
{
    /// <summary>
    /// Tables de la base de donnée patin de vitesse
    /// </summary>
    public class DBPatinVitesse : LinqToDB.Data.DataConnection
    {
        public DBPatinVitesse() : base("PatinVitesse") { }

        public ITable<Erreurs> Erreurs { get { return GetTable<Tables.Erreurs>(); } }
        public ITable<Tables.Action> Action { get { return GetTable<Tables.Action>(); } }
        public ITable<Blocs> Blocs { get { return GetTable<Blocs>(); } }
        public ITable<CateCompil> CateCompil { get { return GetTable<CateCompil>(); } }
        public ITable<Categorie> Categorie { get { return GetTable<Categorie>(); } }
        public ITable<Club> Club { get { return GetTable<Club>(); } }
        public ITable<Commentaire> Commentaire { get { return GetTable<Commentaire>(); } }
        public ITable<Competition> Competition { get { return GetTable<Competition>(); } }
        public ITable<CompilationPoints> CompilationPoints { get { return GetTable<CompilationPoints>(); } }
        public ITable<CompilationTemps> CompilationTemps { get { return GetTable<CompilationTemps>(); } }
        public ITable<CompilationTempsTempo> CompilationTempsTempo { get { return GetTable<CompilationTempsTempo>(); } }
        public ITable<DistanceCompe> DistanceCompe { get { return GetTable<DistanceCompe>(); } }
        public ITable<DistanceStandards> DistanceStandards { get { return GetTable<DistanceStandards>(); } }
        public ITable<DistancesCompilations> DistancesCompilations { get { return GetTable<DistancesCompilations>(); } }
        public ITable<Division> Division { get { return GetTable<Division>(); } }
        public ITable<DivisionCompe> DivisionCompe { get { return GetTable<DivisionCompe>(); } }
        public ITable<EpreuvesCompilation> EpreuvesCompilation { get { return GetTable<EpreuvesCompilation>(); } }
        public ITable<FormatCompe> FormatCompe { get { return GetTable<FormatCompe>(); } }
        public ITable<GroupesCompe> GroupesCompe { get { return GetTable<GroupesCompe>(); } }
        public ITable<LdCd> LdCd { get { return GetTable<LdCd>(); } }
        public ITable<ListePoints> ListePoints { get { return GetTable<ListePoints>(); } }
        public ITable<LpCp> LpCp { get { return GetTable<LpCp>(); } }
        public ITable<NombPatFormat2> NombPatFormat2 { get { return GetTable<NombPatFormat2>(); } }
        public ITable<NombPatFormat3> NombPatFormat3 { get { return GetTable<NombPatFormat3>(); } }
        public ITable<Parametres> Parametres { get { return GetTable<Parametres>(); } }
        public ITable<PatineurCompe> PatineurCompe { get { return GetTable<PatineurCompe>(); } }
        public ITable<Patineur> Patineur { get { return GetTable<Patineur>(); } }
        public ITable<PatVagues> PatVagues { get { return GetTable<PatVagues>(); } }
        public ITable<PointageEssaisC> PointageEssaisC { get { return GetTable<PointageEssaisC>(); } }
        public ITable<PointageEssaisCISU> PointageEssaisCISU { get { return GetTable<PointageEssaisCISU>(); } }
        public ITable<Points> Points { get { return GetTable<Points>(); } }
        public ITable<ProgCourses> ProgCourses { get { return GetTable<ProgCourses>(); } }
        public ITable<ProgCourses1> ProgCourses1 { get { return GetTable<ProgCourses1>(); } }
        public ITable<Regions> Regions { get { return GetTable<Regions>(); } }
        public ITable<Secteurs> Secteurs { get { return GetTable<Secteurs>(); } }
        public ITable<Sexe> Sexe { get { return GetTable<Sexe>(); } }
        public ITable<SommeDePoints> SommeDePoints { get { return GetTable<SommeDePoints>(); } }
        public ITable<Vague1> Vague1 { get { return GetTable<Vague1>(); } }
        public ITable<Vagues> Vagues { get { return GetTable<Vagues>(); } }
        public ITable<VaguesFormatOuvert> VaguesFormatOuvert { get { return GetTable<VaguesFormatOuvert>(); } }
        public ITable<PatVaguesFormatOuvert> PatVaguesFormatOuvert { get { return GetTable<PatVaguesFormatOuvert>(); } }
        public ITable<PointsCumulFormatOuvert> PointsCumulFormatOuvert { get { return GetTable<PointsCumulFormatOuvert>(); } }
        public ITable<ProgCoursesFormatOuvert> ProgCoursesFormatOuvert { get { return GetTable<ProgCoursesFormatOuvert>(); } }
        public ITable<SeqCoursesFormatOuvert> SeqCoursesFormatOuvert { get { return GetTable<SeqCoursesFormatOuvert>(); } }
        public ITable<EpreuvesFormatOuvert> EpreuvesFormatOuvert { get { return GetTable<EpreuvesFormatOuvert>(); } }
    }
}