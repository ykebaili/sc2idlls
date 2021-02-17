using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using System.IO;
using System.Runtime.Remoting.Lifetime;
using sc2i.multitiers.client;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Permet d'interroger les valeurs d'un IElementAVariables à 
	/// partir d'un accès distant, même si les valeurs de ces variables ne sont pas
	/// sérializable (par exemple des cobjetDonnee).
	/// Utilisation : appeller sur le IElementAVariable la fonction GetProxyElementAVariables,
	/// si elle existe.
	/// ce qui retourne un CProxyElementAVariables qui est serializable.
	/// PENSER à initialiser le contexte de donnée local du CProxyElementAVariables
	/// </summary>
	[Serializable]
	public class CProxyElementAVariables : IElementAVariables
	{
		private CDistantElementAVariables m_distantElementAVariables = null;
		private Dictionary<string, IVariableDynamique> m_dicVariables = new Dictionary<string, IVariableDynamique>();

        [NonSerialized]
		private CContexteDonnee m_contexteDonneeLocal = null;

		//--------------------------------------------------------------
		public CProxyElementAVariables(IElementAVariablesDynamiques element, CContexteDonnee contexteDonneeDistant)
		{
			m_distantElementAVariables = new CDistantElementAVariables(element, contexteDonneeDistant);
			foreach (IVariableDynamique variable in element.ListeVariables)
				m_dicVariables[variable.IdVariable] = variable;
		}

        //--------------------------------------------------------------
        public CProxyElementAVariables(IElementAVariablesDynamiques element, CContexteDonnee contexteDonneeDistant, C2iSponsor sponsor)
        {
            m_distantElementAVariables = new CDistantElementAVariables(element, contexteDonneeDistant);
            sponsor.Register(m_distantElementAVariables);
            foreach (IVariableDynamique variable in element.ListeVariables)
                m_dicVariables[variable.IdVariable] = variable;
        }

		//--------------------------------------------------------------
		public CContexteDonnee ContexteDonneeLocal
		{
			get
			{
				return m_contexteDonneeLocal;
			}
			set
			{
				m_contexteDonneeLocal = value;
			}
		}

		//--------------------------------------------------------------
		public CResultAErreur SetValeurChamp(IVariableDynamique variable, object valeur)
		{
			CResultAErreur result = CDistantElementAVariables.WriteValeur ( valeur, variable, m_contexteDonneeLocal );
			if ( result )
				return m_distantElementAVariables.SetValeurChamp ( variable, (byte[])result.Data);
			return result;
		}

		//--------------------------------------------------------------
		public CResultAErreur SetValeurChamp(string strIdVariable, object valeur)
		{
			IVariableDynamique variable = null;
            if (m_dicVariables.TryGetValue(strIdVariable, out variable))
				return SetValeurChamp(variable, valeur);
			CResultAErreur result = CResultAErreur.True;
			result.EmpileErreur(I.T("Unknown variable (@1)|20032", strIdVariable.ToString() ));
			return result;
		}

		//--------------------------------------------------------------
		public object GetValeurChamp(IVariableDynamique variable)
		{
			byte[] data = m_distantElementAVariables.GetValeurChamp(variable);
			if (data == null)
				return data;
			CResultAErreur result = CDistantElementAVariables.ReadValeur ( data, variable, m_contexteDonneeLocal );
			if ( result )
				return result.Data;
			return null;
		}
		
		//--------------------------------------------------------------
        public object GetValeurChamp(string strIdVariable)
        {
            IVariableDynamique variable = null;
            if (m_dicVariables.TryGetValue(strIdVariable, out variable))
                return GetValeurChamp(variable);
            return null;
        }
	}

	public class CDistantElementAVariables : MarshalByRefObject
	{
		private IElementAVariables m_elementAVariables = null;
		private CContexteDonnee m_contexeDonneeDistant = null;

		public CDistantElementAVariables(IElementAVariables elementLocalAVariables, CContexteDonnee contexteDonneeDistant)
		{
			m_elementAVariables = elementLocalAVariables;
			m_contexeDonneeDistant = contexteDonneeDistant;
		}

		//------------------------------------------------------------------------------
		//Le data du result contient un byte[] correspondant à la valeur sérializée
		public static CResultAErreur WriteValeur ( 
			object valeur, 
			IVariableDynamique variable,
			CContexteDonnee contexte )
		{
			MemoryStream stream = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(stream);
			CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
			CResultAErreur result = CSerializerValeursVariables.SerializeValeurVariable(
				ref valeur, 
				variable,
				serializer, contexte);
			writer.Close();
			if ( result )
				result.Data = stream.ToArray();
            stream.Close();
			return result;
		}

		//------------------------------------------------------------------------------
		//Le data du result contient la valeur déserializée
		public static CResultAErreur ReadValeur (
			byte[] data,
			IVariableDynamique variable,
			CContexteDonnee contexteDonnee )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( data == null )
				return result;
			MemoryStream stream = new MemoryStream(data);
			BinaryReader reader = new BinaryReader(stream);
			CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
			object valeur = null;
			result = CSerializerValeursVariables.SerializeValeurVariable(
				ref valeur,
				variable,
				serializer,
				contexteDonnee);
			if ( result  )
				result.Data = valeur;

            reader.Close();
            stream.Close();

			return result;
		}


		//------------------------------------------------------------------------------
		public CResultAErreur SetValeurChamp(IVariableDynamique variable, byte[] valeurSerializee)
		{
			CResultAErreur result = CResultAErreur.True;
			//Déserialise la valeur
			if (valeurSerializee == null)
				m_elementAVariables.SetValeurChamp(variable, null);
			else
			{
				result = ReadValeur(valeurSerializee, variable, m_contexeDonneeDistant);
				if (result)
					m_elementAVariables.SetValeurChamp(variable, result.Data);
			}
            result.Data = null;//Car le result.data risque de contenir un élément non sérializable
			return result;

		}
		//------------------------------------------------------------------------------
		public byte[] GetValeurChamp(IVariableDynamique variable)
		{
			object valeur = m_elementAVariables.GetValeurChamp(variable);
			if (valeur == null)
				return null;
			CResultAErreur result = WriteValeur(valeur, variable, m_contexeDonneeDistant);
			if (result)
				return (byte[])result.Data;
			return null;
		}


	}
}
