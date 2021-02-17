using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using sc2i.expression;

namespace sc2i.formulaire
{
	[Serializable]
	public class C2iWndSousFormulaire : C2iWndFenetre
	{
		private bool m_bAdjustToContent = false;
        private IWndAContainer m_container = null;
        private bool m_bAutoscroll = true;

		public override bool CanBeUseOnType(Type tp)
		{
			return false;
		}

		//---------------------------------
		public bool AdjustToContent
		{
			get
			{
				return m_bAdjustToContent;
			}
			set
			{
				m_bAdjustToContent = value;
			}
		}

        //---------------------------------
        public bool AutoScroll
        {
            get
            {
                return m_bAutoscroll;
            }
            set
            {
                m_bAutoscroll = value;
            }
        }

		//---------------------------------
		private int GetNumVersion()
		{
            /*1 : Ajout de Autoscroll: en effet pb si on fait apparaitre des cadenas 
                * readonly que autoscroll et à true sur les fils et qu'un fils a 
                * cadenas est docké à droite*/
			return 1;
		}

        //---------------------------------
        protected override sc2i.common.CResultAErreur MySerialize(sc2i.common.C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			result = base.MySerialize(serializer);
			if (!result)
				return result;
			serializer.TraiteBool(ref m_bAdjustToContent);

            if (nVersion >= 1)
                serializer.TraiteBool(ref m_bAutoscroll);
			return result;		
		}

        public override IWndAContainer WndContainer
        {
            get
            {
                return m_container;
            }
            set
            {
                m_container = value;
            }
        }
 


	}
}
