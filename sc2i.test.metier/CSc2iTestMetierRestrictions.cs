using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using NUnit.Framework;

using sc2i.common;
using sc2i.multitiers.client;

namespace sc2i.test.metier
{
	/// <summary>
	/// Tests métiers sur la combinaison des restrictions essentiellement
	/// </summary>
	[TestFixture]
	public class CSc2iTestMetierRestrictions
	{
		[SetUp]
		public void Init()
		{
			Console.WriteLine("            ----- [ 717 TESTS ] -----");
		}

		[Test]
		public void TestDominanceRestrictionDansCombine()
		{
			Console.WriteLine("");
			Console.WriteLine("----- COMBINE DES RESTRICTION -----");
			Console.WriteLine("     ----- [ 15 TESTS ] -----");
			Console.WriteLine("");
			CRestrictionUtilisateurSurType restriction = null;
			
			CRestrictionUtilisateurSurType restriction1 = new CRestrictionUtilisateurSurType(typeof(int));
			CRestrictionUtilisateurSurType restriction2 = new CRestrictionUtilisateurSurType(typeof(int));
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			#region HIDE
			restriction1.RestrictionUtilisateur = ERestriction.Hide;

			Console.WriteLine("----- HIDE - HIDE -----");
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Console.WriteLine("");
			Console.WriteLine("----- HIDE - READONLY -----");
			restriction2.RestrictionUtilisateur = ERestriction.ReadOnly;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Console.WriteLine("");
			Console.WriteLine("----- HIDE - NOCREATE -----");
            restriction2.RestrictionUtilisateur = ERestriction.NoCreate;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Console.WriteLine("");
			Console.WriteLine("----- HIDE - NODELETE -----");
			restriction2.RestrictionUtilisateur = ERestriction.NoDelete;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Console.WriteLine("");
			Console.WriteLine("----- HIDE - AUCUNE -----");
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region NOCREATE
			restriction1.RestrictionUtilisateur = ERestriction.NoCreate;

			Console.WriteLine("----- NOCREATE - NOCREATE -----");
			restriction2.RestrictionUtilisateur = ERestriction.NoCreate;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.NoCreate);
			Console.WriteLine(">>> RESULTAT >>> Restriction : NOCREATE");
			Console.WriteLine("");
			Console.WriteLine("----- NOCREATE - NODELETE -----");
			restriction2.RestrictionUtilisateur = ERestriction.NoDelete;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.NoCreate | ERestriction.NoDelete);
			Console.WriteLine(">>> RESULTAT >>> Restriction : NOCREATE | NODELETE");
			Console.WriteLine("");
			Console.WriteLine("----- NOCREATE - READONLY -----");
			restriction2.RestrictionUtilisateur = ERestriction.ReadOnly;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.ReadOnly);
			Console.WriteLine(">>> RESULTAT >>> Restriction : READONLY");
			Console.WriteLine("");
			Console.WriteLine("----- NOCREATE - AUCUNE -----");
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.NoCreate);
			Console.WriteLine(">>> RESULTAT >>> Restriction : NOCREATE");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region NODELETE
			restriction1.RestrictionUtilisateur = ERestriction.NoDelete;

			Console.WriteLine("----- NODELETE - NODELETE -----");
			restriction2.RestrictionUtilisateur = ERestriction.NoDelete;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.NoDelete);
			Console.WriteLine(">>> RESULTAT >>> Restriction : NODELETE");
			Console.WriteLine("");
			Console.WriteLine("----- NODELETE - READONLY -----");
			restriction2.RestrictionUtilisateur = ERestriction.ReadOnly;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.ReadOnly);
			Console.WriteLine(">>> RESULTAT >>> Restriction : READONLY");
			Console.WriteLine("");
			Console.WriteLine("----- NODELETE - AUCUNE -----");
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.NoDelete);
			Console.WriteLine(">>> RESULTAT >>> Restriction : NODELETE");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region READONLY
			restriction1.RestrictionUtilisateur = ERestriction.ReadOnly;

			Console.WriteLine("----- READONLY - READONLY -----");
			restriction2.RestrictionUtilisateur = ERestriction.ReadOnly;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.ReadOnly);
			Console.WriteLine(">>> RESULTAT >>> Restriction : READONLY");
			Console.WriteLine("");
			Console.WriteLine("----- READONLY - AUCUNE -----");
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.ReadOnly);
			Console.WriteLine(">>> RESULTAT >>> Restriction : READONLY");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region AUCUNE
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;

			Console.WriteLine("----- AUCUNE - AUCUNE -----");
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			#endregion
		}

		[Test]
		public void TestPrioriteDansCombine()
		{
			Console.WriteLine("");
			Console.WriteLine("----- COMBINE AVEC PRIORITES -----");
			Console.WriteLine("     ----- [ 18 TESTS ] -----");
			Console.WriteLine("");

			CRestrictionUtilisateurSurType restriction = null;
			CRestrictionUtilisateurSurType restriction1 = new CRestrictionUtilisateurSurType(typeof(int));
			CRestrictionUtilisateurSurType restriction2 = new CRestrictionUtilisateurSurType(typeof(int));

			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			#endregion
		}

		[Test]
		public void TestPrioriteEtSurchargeDansCombine()
		{
			Console.WriteLine("");
			Console.WriteLine("----- COMBINE AVEC SURCHARGES ET PRIORITES -----");
			Console.WriteLine("            ----- [ 108 TESTS ] -----");
			Console.WriteLine("");

			CRestrictionUtilisateurSurType restriction = null;
			CRestrictionUtilisateurSurType restriction1 = new CRestrictionUtilisateurSurType(typeof(int));
			CRestrictionUtilisateurSurType restriction2 = new CRestrictionUtilisateurSurType(typeof(int));

			#region SURCHARGE NON NON
			Console.WriteLine("----- SURCHARGE Non - Non -----");
			restriction1.SurchageComplete = false;
			restriction2.SurchageComplete = false;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			#endregion
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region SURCHARGE NON OUI
			Console.WriteLine("----- SURCHARGE Non - Oui -----");
			restriction1.SurchageComplete = false;
			restriction2.SurchageComplete = true;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			#endregion
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region SURCHARGE OUI NON
			Console.WriteLine("----- SURCHARGE Oui - Non -----");
			restriction1.SurchageComplete = true;
			restriction2.SurchageComplete = false;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			#endregion
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");	
			#endregion
			#region SURCHARGE OUI OUI
			Console.WriteLine("----- SURCHARGE Oui - Oui -----");
			restriction1.SurchageComplete = true;
			restriction2.SurchageComplete = true;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
            restriction1.RestrictionUtilisateur = ERestriction.Aucune;
            restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
            restriction1.RestrictionUtilisateur = ERestriction.Aucune;
            restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
            restriction1.RestrictionUtilisateur = ERestriction.Hide;
            restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			#endregion
			#endregion
		}

		[Test]
		public void TestRestrictionSysteme()
		{
			Console.WriteLine("");
			Console.WriteLine("----- COMBINE AVEC SYSTEME, SURCHAGES ET PRIORITES -----");
			Console.WriteLine("            ----- [ 576 TESTS ] -----");
			Console.WriteLine("");

			CRestrictionUtilisateurSurType restriction = null;
			CRestrictionUtilisateurSurType restriction1 = null;
			CRestrictionUtilisateurSurType restriction2 = null;

			#region RESTRICTIONS SYSTEME SYSTEME
			Console.WriteLine("----- [ RESTRICTION SYSTEME CONTRE RESTRICTION SYSTEME ] -----");
			Console.WriteLine("");
			restriction1 = new CRestrictionUtilisateurSurType(typeof(int), ERestriction.Aucune);
            restriction2 = new CRestrictionUtilisateurSurType(typeof(int), ERestriction.Aucune);
			#region SURCHARGE NON NON
			Console.WriteLine("----- SURCHARGE Non - Non -----");
			restriction1.SurchageComplete = false;
			restriction2.SurchageComplete = false;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
            restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
            restriction1.RestrictionUtilisateur = ERestriction.Aucune;
            restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
            restriction1.RestrictionUtilisateur = ERestriction.Hide;
            restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
            restriction1.RestrictionUtilisateur = ERestriction.Aucune;
            restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
            restriction1.RestrictionUtilisateur = ERestriction.Aucune;
            restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
            restriction1.RestrictionUtilisateur = ERestriction.Hide;
            restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
            restriction1.RestrictionUtilisateur = ERestriction.Aucune;
            restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
            restriction1.RestrictionUtilisateur = ERestriction.Aucune;
            restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			#endregion
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region SURCHARGE NON OUI
			Console.WriteLine("----- SURCHARGE Non - Oui -----");
			restriction1.SurchageComplete = false;
			restriction2.SurchageComplete = true;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			#endregion
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region SURCHARGE OUI NON
			Console.WriteLine("----- SURCHARGE Oui - Non -----");
			restriction1.SurchageComplete = true;
			restriction2.SurchageComplete = false;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			#endregion
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region SURCHARGE OUI OUI
			Console.WriteLine("----- SURCHARGE Oui - Oui -----");
			restriction1.SurchageComplete = true;
			restriction2.SurchageComplete = true;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			#endregion
			#endregion
			Console.WriteLine("----- [ FIN RESTRICTION SYSTEME CONTRE RESTRICTION SYSTEME ] -----");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region RESTRICTIONS NONSYSTEME SYSTEME
			Console.WriteLine("----- [ RESTRICTION NON SYSTEME CONTRE RESTRICTION SYSTEME ] -----");
			Console.WriteLine("");
			restriction1 = new CRestrictionUtilisateurSurType(typeof(int), ERestriction.Aucune);
			restriction2 = new CRestrictionUtilisateurSurType(typeof(int), ERestriction.Aucune);
			#region SURCHARGE NON NON
			Console.WriteLine("----- SURCHARGE Non - Non -----");
			restriction1.SurchageComplete = false;
			restriction2.SurchageComplete = false;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			#endregion
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region SURCHARGE NON OUI
			Console.WriteLine("----- SURCHARGE Non - Oui -----");
			restriction1.SurchageComplete = false;
			restriction2.SurchageComplete = true;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			#endregion
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region SURCHARGE OUI NON
			Console.WriteLine("----- SURCHARGE Oui - Non -----");
			restriction1.SurchageComplete = true;
			restriction2.SurchageComplete = false;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			#endregion
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region SURCHARGE OUI OUI
			Console.WriteLine("----- SURCHARGE Oui - Oui -----");
			restriction1.SurchageComplete = true;
			restriction2.SurchageComplete = true;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			#endregion
			#endregion
			Console.WriteLine("----- [ FIN RESTRICTION NON SYSTEME CONTRE RESTRICTION SYSTEME ] -----");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region RESTRICTIONS SYSTEME NONSYSTEME
			Console.WriteLine("----- [ RESTRICTION SYSTEME CONTRE RESTRICTION NON SYSTEME ] -----");
			Console.WriteLine("");
			restriction1 = new CRestrictionUtilisateurSurType(typeof(int), ERestriction.Aucune);
			restriction2 = new CRestrictionUtilisateurSurType(typeof(int), ERestriction.Aucune);
			#region SURCHARGE NON NON
			Console.WriteLine("----- SURCHARGE Non - Non -----");
			restriction1.SurchageComplete = false;
			restriction2.SurchageComplete = false;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			#endregion
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region SURCHARGE NON OUI
			Console.WriteLine("----- SURCHARGE Non - Oui -----");
			restriction1.SurchageComplete = false;
			restriction2.SurchageComplete = true;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			#endregion
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region SURCHARGE OUI NON
			Console.WriteLine("----- SURCHARGE Oui - Non -----");
			restriction1.SurchageComplete = true;
			restriction2.SurchageComplete = false;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			#endregion
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region SURCHARGE OUI OUI
			Console.WriteLine("----- SURCHARGE Oui - Oui -----");
			restriction1.SurchageComplete = true;
			restriction2.SurchageComplete = true;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : OUI");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			#endregion
			#endregion
			Console.WriteLine("----- [ FIN RESTRICTION SYSTEME CONTRE RESTRICTION NON SYSTEME ] -----");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region RESTRICTIONS NONSYSTEME NONSYSTEME
			Console.WriteLine("----- [ RESTRICTION NON SYSTEME CONTRE RESTRICTION NON SYSTEME ] -----");
			Console.WriteLine("");
			restriction1 = new CRestrictionUtilisateurSurType(typeof(int), ERestriction.Aucune);
            restriction2 = new CRestrictionUtilisateurSurType(typeof(int), ERestriction.Aucune);
			#region SURCHARGE NON NON
			Console.WriteLine("----- SURCHARGE Non - Non -----");
			restriction1.SurchageComplete = false;
			restriction2.SurchageComplete = false;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			#endregion
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region SURCHARGE NON OUI
			Console.WriteLine("----- SURCHARGE Non - Oui -----");
			restriction1.SurchageComplete = false;
			restriction2.SurchageComplete = true;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			#endregion
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region SURCHARGE OUI NON
			Console.WriteLine("----- SURCHARGE Oui - Non -----");
			restriction1.SurchageComplete = true;
			restriction2.SurchageComplete = false;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, false);
			Console.WriteLine("             >>> SurchargeComplete : NON");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			#endregion
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region SURCHARGE OUI OUI
			Console.WriteLine("----- SURCHARGE Oui - Oui -----");
			restriction1.SurchageComplete = true;
			restriction2.SurchageComplete = true;
			#region PRIORITE =
			restriction1.Priorite = 0;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 0 (=) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 0);
			Console.WriteLine("             >>> Priorité : 0");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE <
			restriction1.Priorite = 0;
			restriction2.Priorite = 1;

			Console.WriteLine("----- PRIORITES 0 (<) 1 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
			#region PRIORITE >
			restriction1.Priorite = 1;
			restriction2.Priorite = 0;

			Console.WriteLine("----- PRIORITES 1 (>) 0 -----");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (=) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : AUCUNE (<) HIDE");
			restriction1.RestrictionUtilisateur = ERestriction.Aucune;
			restriction2.RestrictionUtilisateur = ERestriction.Hide;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Aucune);
			Console.WriteLine(">>> RESULTAT >>> Restriction : AUCUNE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			Console.WriteLine("");
			Console.WriteLine(" - RESTRICTIONS : HIDE (>) AUCUNE");
			restriction1.RestrictionUtilisateur = ERestriction.Hide;
			restriction2.RestrictionUtilisateur = ERestriction.Aucune;
			restriction = CRestrictionUtilisateurSurType.Combine(restriction1, restriction2);
			Assert.AreEqual(restriction.RestrictionGlobale, ERestriction.Hide);
			Console.WriteLine(">>> RESULTAT >>> Restriction : HIDE");
			Assert.AreEqual(restriction.Priorite, 1);
			Console.WriteLine("             >>> Priorité : 1");
			Assert.AreEqual(restriction.SurchageComplete, true);
			Console.WriteLine("             >>> SurchargeComplete : OUI");
			 
			Console.WriteLine("             >>> IsRestrictionSysteme : NON");
			#endregion
			#endregion
			Console.WriteLine("----- [ FIN RESTRICTION NON SYSTEME CONTRE RESTRICTION NON SYSTEME ] -----");
			Console.WriteLine("");
			Console.WriteLine("");
			#endregion
		}
	}
}
