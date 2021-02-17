using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.easyquery
{
    /// <summary>
    /// Pour tous les objets de query qui se calculent à partir d'autres objets
    /// </summary>
    [Serializable]
    public abstract class CODEQFromObjetsSource : CODEQBase
    {
        List<string> m_lstObjetsSource = new List<string>();

        public CODEQFromObjetsSource()
            : base()
        {
        }

        //---------------------------------------------------
        //Nombre d'éléments sources requis
        public abstract int NbSourceRequired { get; }

        //---------------------------------------------------
        public virtual IEnumerable<string> NomsSources
        {
            get
            {
                List<string> lst = new List<string>();
                for ( int n = 0; n < NbSourceRequired; n++ )
                {
                    lst.Add(I.T("Table n°@1|20021", (n + 1).ToString()));
                }
                return lst.AsReadOnly();
            }
        }

        //---------------------------------------------------
        public virtual IObjetDeEasyQuery[] ElementsSource
        {
            get
            {
                CEasyQuery q = Query;
                List<IObjetDeEasyQuery> lst = new List<IObjetDeEasyQuery>();
                if (q != null)
                {
                    foreach (string strObjetSource in m_lstObjetsSource)
                    {
                        IObjetDeEasyQuery obj = q.GetObjet(strObjetSource);
                        if (obj != null)
                            lst.Add(obj);
                    }
                }
                return lst.ToArray();
            }
            set
            {
                m_lstObjetsSource = new List<string>();
                if (value != null)
                {
                    foreach (IObjetDeEasyQuery obj in value)
                    {
                        if (obj != null)
                            m_lstObjetsSource.Add(obj.Id);
                    }
                }
            }
        }

        //---------------------------------------------------
        protected virtual CResultAErreur MyRemplaceSource ( IObjetDeEasyQuery ancienneSource, IObjetDeEasyQuery nouvelleSource )
        {
            return CResultAErreur.True;
        }

        //---------------------------------------------------
        public virtual CResultAErreur SetSource(int nSource, IObjetDeEasyQuery nouvelleSource)
        {
            CResultAErreur result = CResultAErreur.True;

           
            if (nouvelleSource == null || nSource > NbSourceRequired)
            {
                result.EmpileErreur(I.T("Invalid parameters to replace source|20019"));
                return result;
            }

            //On ne peut ajouter les sources que dans l'ordre
            if ( nSource > ElementsSource.Length )
            {
                result.EmpileErreur(I.T("You should add previous sources before this one|20021"));
                return result;
            }

            List<string> lstIds = new List<string>();
            foreach (IObjetDeEasyQuery source in ElementsSource)
                lstIds.Add(source.Id);
            IObjetDeEasyQuery ancienneSource = ElementsSource.Length > nSource ? ElementsSource[nSource] : null;
            if (nSource >= lstIds.Count())
                lstIds.Add(nouvelleSource.Id);
            else
                lstIds[nSource] = nouvelleSource.Id;

            

            if (ancienneSource != null)
            {

                //Vérifie que la nouvelle source est compatible
                foreach (IColumnDeEasyQuery col in ancienneSource.Columns)
                {
                    IColumnDeEasyQuery newCol = nouvelleSource.Columns.FirstOrDefault(c => c.ColumnName == col.ColumnName);
                    if (newCol == null)
                    {
                        result.EmpileErreur(I.T("The new source should contains the column @1|20018", col.ColumnName));
                    }
                }
                if (!result)
                    return result;

                for (int n = 0; n < m_lstObjetsSource.Count; n++)
                {
                    if (m_lstObjetsSource[n] == ancienneSource.Id)
                        m_lstObjetsSource[n] = nouvelleSource.Id;
                }


                //Traite les CColumnEQFromSource
                foreach (IColumnDeEasyQuery oldCol in ancienneSource.Columns)
                {
                    IColumnDeEasyQuery newCol = nouvelleSource.Columns.FirstOrDefault(c => c.ColumnName == oldCol.ColumnName);
                    foreach (IColumnDeEasyQuery colDeThis in Columns)
                    {
                        result = colDeThis.OnRemplaceColSource(oldCol, newCol);
                    }
                }
            }
            if ( result )
                m_lstObjetsSource = lstIds;
            return result;
        }

        //---------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //---------------------------------------------------
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.MySerialize(serializer);
            if (!result)
                return result;
            int nNb = m_lstObjetsSource.Count;
            serializer.TraiteInt(ref nNb);
            switch (serializer.Mode)
            {
                case ModeSerialisation.Ecriture:
                    foreach (string strId in m_lstObjetsSource)
                    {
                        string strTmp = strId;
                        serializer.TraiteString(ref strTmp);
                    }
                    break;
                case ModeSerialisation.Lecture:
                    m_lstObjetsSource = new List<string>();
                    for (int i = 0; i < nNb; i++)
                    {
                        string strTmp = "";
                        serializer.TraiteString(ref strTmp);
                        m_lstObjetsSource.Add(strTmp);
                    }
                    break;
            }
            return result;
        }
    }
}
