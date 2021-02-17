using data.hotel.client.query;
using sc2i.common;
using sc2i.common.memorydb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.client
{
    public interface IDataRoomServer
    {
        CResultAErreur UpdateConfig(CMemoryDb database);

        CMemoryDb GetConfig();

        bool Ping();

        string RoomId { get; }

        //Envoie un packet de données
        CResultAErreur SendData(List<CDataHotelClient.SDataToSend> data);

        //envoie des données avec dispatch vers autres room
        CResultAErreur SendData(string strIdTable, string strIdChamp, string strIdEntite, DateTime dateDate, double fValue);

        //Envoie des données sans dispatch vers autre room
        //Id table et Id champs peuvent être des noms de table et de champ
        CResultAErreur SendDataDirect(string strIdTable, string strIdChamp, string strIdEntite, DateTime dateDate, double fValue);

        
        //envoie des données sans dispatch. 
        //Id table et IdChamp ne sont pas interpretés, la donnée est stockée sans contrôle sur IdTable et IdChamp
        void SendDataDirectWithTableAndFieldId(string strIdTable, string strIdChamp, string strIdEntite, DateTime dateDate, double fValue);

        //Récupère la liste des entités qui ont des données entre deux dates (dans toutes les rooms)
        IEnumerable<string> GetEntities(string strIdTable, DateTime dateDebut, DateTime dateFin);

        //Récupère la liste des entités qui ont des données entre deux dates (local)
        IEnumerable<string> GetEntitiesDirect(string strIdTable, DateTime dateDebut, DateTime dateFin);

        //Récupére des données avec interrogation multirooms
        CResultAErreurType<CDataTableFastSerialize> GetData(CDataHotelQuery query);

        //Récupére des données sans interrogation multirooms (directement room appellée)
        CResultAErreurType<CDataTableFastSerialize> GetDataDirect(CDataHotelQuery query);

        ////Retourne le premier élément d'un série se terminant à la date demandée
        //et pour laquelle toutes les valeurs correspondent au filtre.
        IDataRoomEntry GetFirstNotInSerie(
            string strTableId,
            string strEntityId,
            string strFieldId,
            DateTime dateRecherche,
            ITestDataHotel filtre);

        //Retourne le premier élément d'un série se terminant à la date demandée
        //et pour laquelle toutes les valeurs correspondent au filtre.
        IDataRoomEntry GetFirstNotInSerieDirect(
            string strTableId,
            string strEntityId,
            string strFieldId,
            DateTime dateRecherche,
            ITestDataHotel filtre);

        //Retourne depuis combien de temps (à la date demandée), la valeur
        //spécifiée correspond au filtre
        double GetDepuisCombienDeTempsEnSDirect(
           string strTableId,
           string strEntityId,
           string strFieldId,
           DateTime dateRecherche,
           ITestDataHotel filtre);

        CDataSetFastSerialize GetDataSetModele();
    }
}
