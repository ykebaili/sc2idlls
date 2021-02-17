using System;
using sc2i.expression;
using sc2i.common;
using System.Reflection;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de RelationFilleAttribute.
	/// </summary>
	public class RelationFilleAttribute : Attribute
	{
		public readonly Type TypeFille;
		public readonly string ProprieteFille;

		/// ///////////////////////////////////////////////
		public RelationFilleAttribute( Type typeFille, string strProprieteFille)
		{
			TypeFille = typeFille;
			ProprieteFille = strProprieteFille;
		}
	}

    /// ///////////////////////////////////////////////
    [AutoExec("Autoexec")]
    public class CFournisseurPropInverseRelationFille : IFournisseurProprieteDynamiqueInverse
    {
        /// ///////////////////////////////////////////////
        public static void Autoexec()
        {
            CFournisseurProprieteDynamiqueInverse.RegisterFournisseur ( typeof(CDefinitionProprieteDynamiqueDotNet), typeof(CFournisseurPropInverseRelationFille) );
        }

        /// ///////////////////////////////////////////////
        public CDefinitionProprieteDynamique GetProprieteInverse(Type typePortantLaPropriete, CDefinitionProprieteDynamique def)
        {
            CDefinitionProprieteDynamiqueDotNet defDotNet = def as CDefinitionProprieteDynamiqueDotNet;
            if ( typePortantLaPropriete == null || defDotNet == null )
                return null;
            PropertyInfo info = typePortantLaPropriete.GetProperty ( defDotNet.NomProprieteSansCleTypeChamp );
            if ( info != null )
            {
                object[] attrs = info.GetCustomAttributes ( typeof( RelationFilleAttribute), true );
                if ( attrs.Length > 0 )
                {
                    string strProp = ((RelationFilleAttribute)attrs[0]).ProprieteFille;
                    if ( strProp.Length != 0 )
                        return new CDefinitionProprieteDynamiqueDotNet ( strProp, strProp, 
                            new CTypeResultatExpression ( info.PropertyType, false ), true, false, "");
                }
            }
            return null;
        }
    }
}
