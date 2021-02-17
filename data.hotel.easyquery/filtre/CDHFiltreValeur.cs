using data.hotel.client.query;
using futurocom.easyquery;
using sc2i.common;
using sc2i.expression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.easyquery.filtre
{
    public class CDHFiltreValeur : IDHFiltre
    {
        private string m_strHotelColonneId = "";
        private EOperateurComparaisonMassStorage m_operateur = EOperateurComparaisonMassStorage.Superieur;
        private C2iExpression m_formuleValeur = null;

        private C2iExpression m_formuleApplication = null;

        //--------------------------------------------------
        public CDHFiltreValeur()
        {

        }

        //--------------------------------------------------
        public string GetLibelle(IObjetDeEasyQuery table)
        {
            CColumnEQFromSource col = table.Columns.FirstOrDefault(c => c is CColumnEQFromSource && ((CColumnEQFromSource)c).IdColumnSource == m_strHotelColonneId) as CColumnEQFromSource;
            return (col != null ? col.ColumnName : "?") +
                new COperateurComparaisonMassStorage(m_operateur).Libelle +
                (m_formuleValeur != null ? m_formuleValeur.GetString() : "?");
        }

        //--------------------------------------------------
        public string ColumnHotelId
        {
            get
            {
                return m_strHotelColonneId;
            }
            set
            {
                m_strHotelColonneId = value;
            }
        }

        //--------------------------------------------------
        public EOperateurComparaisonMassStorage Operateur
        {
            get
            {
                return m_operateur;
            }
            set 
            { 
                m_operateur = value;
            }
        }

        //--------------------------------------------------
        public C2iExpression FormuleValeur
        {
            get
            {
                return m_formuleValeur;
            }
            set
            {
                m_formuleValeur = value;
            }
        }

        //--------------------------------------------------
        public C2iExpression FormuleApplication
        {
            get
            {
                return m_formuleApplication;
            }
            set
            {
                m_formuleApplication = value;
            }
        }

        //--------------------------------------------------
        public ITestDataHotel GetTestFinal(object objetPourSousProprietes )
        {
            CResultAErreur result = CResultAErreur.True;
            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(objetPourSousProprietes);
            if ( FormuleApplication != null )
            {
                result = FormuleApplication.Eval ( ctx );
                if ( !result || result.Data == null)
                    return null;
                if ( !CUtilBool.BoolFromObject(result.Data ))
                    return null;
            }
            double? valRef = null;
            if ( FormuleValeur != null )
            {
                result = FormuleValeur.Eval ( ctx );
                if ( !result )
                    return null;
                if (result.Data is double)
                    valRef = (double)result.Data;
                else
                {
                    try
                    {
                        valRef = Convert.ToDouble(result.Data);
                    }
                    catch { }
                }
            }
            if ( valRef == null )
                return null;
            
            CTestDataHotelValue test = new CTestDataHotelValue();
            test.IdChamp = m_strHotelColonneId;
            test.Operateur = Operateur;
            test.ValeurReference = valRef.Value;
            return test;
        }

        //--------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------------
        public CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            serializer.TraiteEnum<EOperateurComparaisonMassStorage>(ref m_operateur);
            result = serializer.TraiteObject<C2iExpression>(ref m_formuleValeur);
            if ( result )
                result = serializer.TraiteObject<C2iExpression>( ref m_formuleApplication) ;
            if (result)
                serializer.TraiteString(ref m_strHotelColonneId);
            if ( !result )
                return result;
            return result;
        }
   

    }
}
