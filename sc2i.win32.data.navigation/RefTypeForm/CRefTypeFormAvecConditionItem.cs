using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.win32.common.customizableList;

namespace sc2i.win32.data.navigation.RefTypeForm
{
    public class CRefTypeFormAvecConditionItem : CCustomizableListItem
    {
        private CReferenceTypeFormAvecCondition.CParametreTypeForm m_parametre;
        private Type m_typeObjetPourForm = null;

        //-----------------------------------------------------------
        public CRefTypeFormAvecConditionItem()
            : this(null, null)
        {
        }

        //-----------------------------------------------------------
        public CRefTypeFormAvecConditionItem(
            Type typeObjetPourForm,
            CReferenceTypeFormAvecCondition.CParametreTypeForm parametre)
            : base()
        {
            m_parametre = parametre;
            m_typeObjetPourForm = typeObjetPourForm;
        }

        //-----------------------------------------------------------
        public CReferenceTypeFormAvecCondition.CParametreTypeForm Parametre
        {
            get
            {
                return m_parametre;
            }
            set
            {
                m_parametre = value;
            }
        }

        //-----------------------------------------------------------
        public Type TypeObjetPourForm
        {
            get
            {
                return m_typeObjetPourForm;
            }
            set
            {
                m_typeObjetPourForm = value;
            }
        }

        //-----------------------------------------------------------
        public override int Index
        {
            get
            {
                if ( m_parametre != null )
                    return m_parametre.Index;
                return 0;
            }
            set
            {
                if (m_parametre != null)
                    m_parametre.Index = value;
            }
        }
    }
}
