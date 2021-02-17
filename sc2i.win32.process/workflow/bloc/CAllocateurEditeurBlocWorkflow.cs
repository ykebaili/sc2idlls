using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.process.workflow.blocs;
using System.Windows.Forms;

namespace sc2i.win32.process.workflow.bloc
{

    public interface IEditeurBlocWorkflow
    {
        bool EditeBloc ( CBlocWorkflow bloc );
    }

    public static class CAllocateurEditeurBlocWorkflow
    {
        private static Dictionary<Type, Type> m_dicTypeBlocToTypeEditeur = new Dictionary<Type, Type>();

        public static void RegisterEditeur(Type typeBloc, Type typeEditeur)
        {
            m_dicTypeBlocToTypeEditeur[typeBloc] = typeEditeur;
        }

        public static bool EditeBloc(CBlocWorkflow bloc)
        {
            if (bloc == null)
                return false;
            Type tpForm = null;
            if (m_dicTypeBlocToTypeEditeur.TryGetValue(bloc.GetType(), out tpForm))
            {
                IEditeurBlocWorkflow editeur = Activator.CreateInstance(tpForm, new object[0]) as IEditeurBlocWorkflow;
                if (editeur != null)
                {
                    return editeur.EditeBloc(bloc);
                }
            }
            return false;
        }

    }
}
