
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using sc2i.expression;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.common.inventaire;
using sc2i.drawing;
using sc2i.formulaire;

namespace sc2i.test.classes
{
    
    
    /// <summary>
    ///Classe de test pour CFournisseurInventaireObjetGeneriqueTest, destinée à contenir tous
    ///les tests unitaires CFournisseurInventaireObjetGeneriqueTest
    ///</summary>
    [TestClass()]
    public class CFournisseurInventaireObjetGeneriqueTest
    {
        
        private TestContext testContextInstance;
        private static CContexteDonnee m_contexte;
        private CTestInventaireHelper m_helper = new CTestInventaireHelper();

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        // 
        //Vous pouvez utiliser les attributs supplémentaires suivants lors de l'écriture de vos tests :
        //
        //Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test dans la classe
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            m_contexte = new CContexteDonnee();
        }
        //
        //Utilisez ClassCleanup pour exécuter du code après que tous les tests ont été exécutés dans une classe
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            if (m_contexte != null)
                m_contexte.Dispose();
            m_contexte = null;
        }
        //
        //Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        //------------------------------------------------- TEST 1 --------------------------------------------------
        [TestMethod()]
        public void FillInventaireNonRecursifTest1()
        {
            // Test Inventaire d'un objet contenant un C2iExpression
            CObjetSimpleTestS1 objetTest = new CObjetSimpleTestS1();

            m_helper.FillInventaireNonRecursifTestHelper<C2iExpression>(objetTest);

            // Asserts, déroulement des tests unitaires
            //Assert.IsTrue(true); // Ne sert à rien, juste pour vérifier que je passe bien ici
            Assert.AreEqual(3, m_helper.ListeResultInventaire.Count);
            foreach (object item in m_helper.ListeResultInventaire)
            {
                // Test que les valeur de la liste sont du bon Type
                Assert.IsInstanceOfType(item, typeof(C2iExpression));

            }
            
        }

        //------------------------------------------------- TEST 2 --------------------------------------------------
        [TestMethod()]
        public void FillInventaireNonRecursifTest2()
        {
            // Test Inventaire d'un objet contenant un CFilreData
            CListeObjetsDonnees listeTest = new CListeObjetsDonnees(m_contexte, typeof(CDroitUtilisateur));
            listeTest.Filtre = new CFiltreDataImpossible();
            listeTest.FiltrePrincipal = new CFiltreDataImpossible();

            m_helper.FillInventaireNonRecursifTestHelper<CFiltreData>(listeTest);
           
            // Asserts
            Assert.AreEqual(3, m_helper.ListeResultInventaire.Count);
            
        }


        //------------------------------------------------- TEST 3 --------------------------------------------------
        [TestMethod()]
        public void FillInventaireNonRecursifTest3()
        {
            // Test Inventaire d'un objet contenant un C2iExpression

            CObjetComplexeTestC1 complexe = new CObjetComplexeTestC1("TEST3_C1");
            m_helper.FillInventaireNonRecursifTestHelper<C2iExpression>(complexe);

            // Assert
            Assert.AreEqual(0, m_helper.ListeResultInventaire.Count);
            Assert.AreEqual(5, m_helper.ListeResultHorsInventaire.Count);
            foreach (object item in m_helper.ListeResultHorsInventaire)
            {
                Assert.IsInstanceOfType(item, typeof(CObjetSimpleTestS1));
            }


        }

        //------------------------------------------------- TEST 4 --------------------------------------------------
        [TestMethod()]
        public void FillInventaireNonRecursifTest4()
        {
            // Test Inventaire d'un objet contenant un C2iExpression

            CObjetComplexeTestC1 complexe = new CObjetComplexeTestC1("TEST4_C1");
            
            complexe.Objetsfils.Add(new CObjetComplexeTestC1("TEST4_F1"));
            complexe.Objetsfils.Add(new CObjetComplexeTestC1("TEST4_F2"));

            m_helper.FillInventaireNonRecursifTestHelper<C2iExpression>(complexe);

            // Assert
            Assert.AreEqual(0, m_helper.ListeResultInventaire.Count);
            Assert.AreEqual(7, m_helper.ListeResultHorsInventaire.Count);
            foreach (object item in m_helper.ListeResultHorsInventaire)
            {
                //Assert.IsInstanceOfType(item, typeof(CObjetComplexeTestC1));
            }

        }

        //------------------------------------------------- TEST 5 --------------------------------------------------
        [TestMethod()]
        public void FillInventaireNonRecursifTest5()
        {
            // Test Inventaire d'un objet 
            CFiltreDynamique filtreDyn = new CFiltreDynamique();

            m_helper.FillInventaireNonRecursifTestHelper<C2iExpression>(filtreDyn);
            // Asserts
            Assert.AreEqual(3, m_helper.ListeResultInventaire.Count); // 3 Expressions constante false
            Assert.AreEqual(1, m_helper.ListeResultHorsInventaire.Count); // 1 Formulaire C2iWndFenetre qui utilise des C2iExpression

            m_helper.FillInventaireNonRecursifTestHelper<CFiltreData>(filtreDyn);
            Assert.AreEqual(0, m_helper.ListeResultInventaire.Count); // 
            Assert.AreEqual(0, m_helper.ListeResultHorsInventaire.Count); // 

            C2iWndFenetre fenetre = filtreDyn.FormulaireEdition as C2iWndFenetre;
            m_helper.FillInventaireNonRecursifTestHelper<C2iExpression>(fenetre);
            Assert.AreEqual(0, m_helper.ListeResultInventaire.Count); 
            Assert.AreEqual(18, m_helper.ListeResultHorsInventaire.Count);

            
            m_helper.FillInventaireNonRecursifTestHelper<CFormuleNommee>(fenetre);
            Assert.AreEqual(3, m_helper.ListeResultInventaire.Count);
            Assert.AreEqual(0, m_helper.ListeResultHorsInventaire.Count);
            m_helper.FillInventaireNonRecursifTestHelper<C2iExpression>(fenetre);
            Assert.AreEqual(0, m_helper.ListeResultInventaire.Count);
            Assert.AreEqual(24, m_helper.ListeResultHorsInventaire.Count); // 18 + 3 Formules Nommees + 3 Def Methode Dynamique

            // Ajout de variables
            CVariableDynamiqueSaisie varX = new CVariableDynamiqueSaisie();
            varX.Nom = "varX";
            filtreDyn.AddVariable(varX);
            CVariableDynamiqueSaisie varY = new CVariableDynamiqueSaisie();
            varX.Nom = "varY";
            filtreDyn.AddVariable(varY);
            m_helper.FillInventaireNonRecursifTestHelper<IVariableDynamique>(filtreDyn);
            Assert.AreEqual(2, m_helper.ListeResultInventaire.Count);
            Assert.AreEqual(1, m_helper.ListeResultHorsInventaire.Count);
            m_helper.FillInventaireNonRecursifTestHelper<C2iExpression>(filtreDyn);
            Assert.AreEqual(3, m_helper.ListeResultInventaire.Count);
            Assert.AreEqual(5, m_helper.ListeResultHorsInventaire.Count);

        }

        //------------------------------------------------- TEST 6 --------------------------------------------------
        [TestMethod()]
        public void FillInventaireNonRecursifTest6()
        {
            // Test Inventaire d'un objet 

            //FillInventaireNonRecursifTestHelper<CFormulaire>();
        }


        //-------------------------------------------------- Test Helper -----------------------------------
        private class CTestInventaireHelper
        {
            CInventaire m_inventaire = null;
            private List<object> m_listeResultInventaire;
            private List<object> m_listeResultHorsInventaire;

            /// <summary>
            ///Test pour FillInventaireNonRecursif, retourne la liste de l'inventaire
            ///</summary>
            public void FillInventaireNonRecursifTestHelper<TypeRecherche>(object objAInventorier)
            {
                CFournisseurInventaireObjetGenerique<TypeRecherche> fournisseur = new CFournisseurInventaireObjetGenerique<TypeRecherche>(); // TODO : initialisez à une valeur appropriée
                //object obj = null; ; // TODO : initialisez à une valeur appropriée

                m_inventaire = new CInventaire();
                fournisseur.FillInventaireNonRecursif(objAInventorier, m_inventaire);

                m_listeResultInventaire = new List<object>( m_inventaire.GetObjects(false));
                m_listeResultHorsInventaire = new List<object>( m_inventaire.GetObjects(true));
            }

            public List<object> ListeResultInventaire
            {
                get { return m_listeResultInventaire; }
            }
            public List<object> ListeResultHorsInventaire
            {
                get { return m_listeResultHorsInventaire; }
            }

        }

        //---------------------------------------------- Objets de test à inventorier -------------------------------------------
        /// <summary>
        /// Fournit des objets à inventorier
        /// </summary>
        class CObjetSimpleTestS1
        {
            string m_strNomIdentifiant = "";
            List<C2iExpression> m_lstFormules = new List<C2iExpression>();
                        
            public CObjetSimpleTestS1()
            {
                m_lstFormules.Add(new C2iExpressionNull());
                m_lstFormules.Add( new C2iExpressionVrai());
                m_lstFormules.Add( new C2iExpressionFaux());
            }

            public CObjetSimpleTestS1(string strId)
            {
                m_strNomIdentifiant = strId;
                m_lstFormules.Add(new C2iExpressionNull());
                m_lstFormules.Add(new C2iExpressionVrai());
                m_lstFormules.Add(new C2iExpressionFaux());
            }

            public override string ToString()
            {
                return m_strNomIdentifiant;
            }
            public Array ArrayFormules
            {
                get { return m_lstFormules.ToArray(); }
            }

            public C2iExpression[] TableauFormules
            {
                get { return m_lstFormules.ToArray(); }
            }

            public List<C2iExpression> ListeFormules
            {
                get { return m_lstFormules; }
                set { }
            }

            public ArrayList ArrayListeFormules
            {
                get { return new ArrayList(m_lstFormules); }
            }
              
        }

        //---------------------------------------------------------------------------
        /// <summary>
        /// Fournit des objets un peu plus complexe que les premiers à inventorier
        /// </summary>
        class CObjetComplexeTestC1
        {
            string m_strNomIdentifiant = "";
            private List<CObjetComplexeTestC1> m_listeObjetsFils;

            CObjetSimpleTestS1 m_simple1;
            CObjetSimpleTestS1 m_simple2;

            public CObjetComplexeTestC1(string strId)
            {
                m_strNomIdentifiant = strId;
                m_listeObjetsFils = new List<CObjetComplexeTestC1>();
                m_simple1 = new CObjetSimpleTestS1(m_strNomIdentifiant + "_Simple 1");
                m_simple2 = new CObjetSimpleTestS1(m_strNomIdentifiant + "_Simple 2");
            }

            public override string ToString()
            {
                return m_strNomIdentifiant;
            }
            public CObjetSimpleTestS1 ObjetSimple1
            {
                get
                {
                    return m_simple1;
                }
            }

            public CObjetSimpleTestS1 ObjetSimple2
            {
                get
                {
                    return m_simple2;
                }
            }


            public CObjetSimpleTestS1[] GetListeObjetsSimples()
            {
                return new CObjetSimpleTestS1[] {
                    new CObjetSimpleTestS1(m_strNomIdentifiant + "_Simple dans liste A"),
                    new CObjetSimpleTestS1(m_strNomIdentifiant + "_Simple dans liste B"),
                    new CObjetSimpleTestS1(m_strNomIdentifiant + "_Simple dans liste C")};
            }

            public List<CObjetComplexeTestC1> Objetsfils
            {
                get
                {
                    return m_listeObjetsFils;
                }
            }


        }

    }
}
