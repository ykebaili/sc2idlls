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
    public class CDHFiltreDate : IDHFiltre
    {
        private EOperateurComparaisonMassStorage m_operateur = EOperateurComparaisonMassStorage.Superieur;
        private C2iExpression m_formuleDate = null;

        private C2iExpression m_formuleApplication = null;

        //--------------------------------------------------
        public CDHFiltreDate()
        {

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
        public C2iExpression FormuleDate
        {
            get
            {
                return m_formuleDate;
            }
            set
            {
                m_formuleDate = value;
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
        public string GetLibelle(IObjetDeEasyQuery table)
        {
                return I.T("Date|20009") + " " +
                    new COperateurComparaisonMassStorage(m_operateur).Libelle + " " +
                    (FormuleDate != null ? FormuleDate.GetString() : "?");
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
            DateTime? dateRef = null;
            if ( FormuleDate != null )
            {
                result = FormuleDate.Eval ( ctx );
                if ( !result )
                    return null;
                if ( result.Data is DateTime || result.Data is CDateTimeEx)
                    dateRef = (DateTime)result.Data;
            }
            if ( dateRef == null )
                return null;
            CTestDataHotelDate test = new CTestDataHotelDate();
            test.Operateur = Operateur;
            test.DateTest = dateRef.Value;
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
            result = serializer.TraiteObject<C2iExpression>(ref m_formuleDate);
            if ( result )
                result = serializer.TraiteObject<C2iExpression>( ref m_formuleApplication) ;
            if ( !result )
                return result;
            return result;
        }
   

    }
}
