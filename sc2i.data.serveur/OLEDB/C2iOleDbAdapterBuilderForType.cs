using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Reflection;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Fabrique automatiquement les commandes pour un SqlDataAdapter
	/// </summary>
    public class C2iOleDbAdapterBuilderForType : C2iDbAdapterBuilderForType
	{

		private int m_nNumeroVariable = 1;
		//Nom de champ->Numero
		private Hashtable m_tableChampToNumero = new Hashtable();
		public C2iOleDbAdapterBuilderForType( Type tp, COleDbDatabaseConnexion connexion )
			:base ( tp, connexion )
		{
		}

		////////////////////////////////////////////////////
		public override IDataAdapter GetNewAdapter(DataRowState etatsAPrendreEnCompte, bool bDisableIdAuto, params string[] champsExclus)
		{
			IDataAdapter adapter = base.GetNewAdapter (etatsAPrendreEnCompte, bDisableIdAuto, champsExclus);
			if ( adapter is OleDbDataAdapter )
				((OleDbDataAdapter)adapter).RowUpdating += new OleDbRowUpdatingEventHandler(OnRowUpdating);
			return adapter;
		}


		////////////////////////////////////////////////////
		private void OnRowUpdating ( object sender, OleDbRowUpdatingEventArgs args )
		{
			if ( args.StatementType == StatementType.Delete && 
				(EtatsAPrendreEnCompte & DataRowState.Deleted) == 0 )
				args.Status = UpdateStatus.SkipCurrentRow;
			
			if ( args.StatementType == StatementType.Insert && 
				(EtatsAPrendreEnCompte & DataRowState.Added) == 0 )
				args.Status = UpdateStatus.SkipCurrentRow;

			if ( args.StatementType == StatementType.Update && 
				(EtatsAPrendreEnCompte & DataRowState.Modified) == 0 )
				args.Status = UpdateStatus.SkipCurrentRow;
		}

		////////////////////////////////////////////////////
		protected override string GetNomParametreFor ( CInfoChampTable champ, DataRowVersion version )
		{
			int nNumChamp = 0;
			if ( m_tableChampToNumero[champ.NomChamp] == null )
			{
				nNumChamp = m_nNumeroVariable++;
				m_tableChampToNumero[champ.NomChamp] = nNumChamp;
			}
			else
				nNumChamp = (int)m_tableChampToNumero[champ.NomChamp];

			string strNom = m_connexion.GetNomParametre("A"+nNumChamp.ToString());
			switch ( version )
			{
				case DataRowVersion.Current :
					strNom+="_NEW";
					break;
				case DataRowVersion.Original : 
					strNom += "_OLD";
					break;
				default :
					strNom += (int)version;
					break;
			}
			return strNom;
		}

		
	}
}
