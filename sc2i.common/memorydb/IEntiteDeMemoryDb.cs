using System;
namespace sc2i.common.memorydb
{
    public interface IEntiteDeMemoryDb
    {
        void CreateNew(string strId);
        CMemoryDb Database { get; }
        sc2i.common.CResultAErreur Delete();
        string GetChampId();
        string GetChampTriParDefaut();
        string GetNomTable();
        void MyInitValeursParDefaut();
        bool ReadIfExist(string strId, bool bUtiliserLesSourcesExternes);
        bool ReadIfExist(string strId);
        bool ReadIfExist(CFiltreMemoryDb filtre);
        sc2i.common.CResultAErreur Serialize(sc2i.common.C2iSerializer serializer);
    }
}
