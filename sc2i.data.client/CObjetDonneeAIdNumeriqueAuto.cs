using System;
using System.Collections.Generic;
using System.Data;

using sc2i.common;
using System.Collections;
using System.Text;

namespace sc2i.data
{
	/// <summary>
	/// Tous les Objets donné qui ont un champ unique identifiant qui est un numérique auto
	/// </summary>
	[DynamicClass("<Entity with auto ID>")]
	public abstract class CObjetDonneeAIdNumeriqueAuto : CObjetDonneeAIdNumerique, IObjetDonneeAIdNumeriqueAuto, IAllocateurSupprimeurElements
	{
#if PDA
		/// //////////////////////////////////////////////////////////////
		public CObjetDonneeAIdNumeriqueAuto( )
			:base()
		{
		}
#endif

		/// //////////////////////////////////////////////////////////////
		public CObjetDonneeAIdNumeriqueAuto( CContexteDonnee ctx)
			:base(ctx)
		{
		}

		/// //////////////////////////////////////////////////////////////
		public CObjetDonneeAIdNumeriqueAuto ( DataRow row )
			:base(row)
		{
		}

		/// //////////////////////////////////////////////////////////////
		public void CreateNew()
		{
			base.CreateNew(null);
		}

		/// //////////////////////////////////////////////////////////////
		public void CreateNewInCurrentContexte()
		{
			base.CreateNewInCurrentContexte(null);
		}

		/// //////////////////////////////////////////////////////////////
		public override bool IsNew()
		{
			return Id < 0;
		}

		#region IAllocateurSupprimeurElements Membres

		public CResultAErreur AlloueElement(Type tp)
		{
			CResultAErreur result = CResultAErreur.True;
			if (typeof(CObjetDonneeAIdNumeriqueAuto).IsAssignableFrom(tp))
			{
				try
				{
					CObjetDonneeAIdNumeriqueAuto retour = Activator.CreateInstance(tp, new object[] { ContexteDonnee }) as CObjetDonneeAIdNumeriqueAuto;
					if (retour != null)
						retour.CreateNewInCurrentContexte();
					result.Data = retour;
				}
				catch (Exception e)
				{
					result.EmpileErreur(new CErreurException(e));
				}
			}
			if (result.Data == null)
			{
				result.EmpileErreur(I.T("Can not allocate object @1|30005", DynamicClassAttribute.GetNomConvivial(tp)));
			}
			return result;
		}

		public CResultAErreur SupprimeElement(object elt)
		{
			CResultAErreur result = CResultAErreur.True;
			CObjetDonneeAIdNumeriqueAuto obj = elt as CObjetDonneeAIdNumeriqueAuto;
			if (obj != null)
			{
				result = obj.Delete();
			}
			return result;
		}

		#endregion



	}
}
