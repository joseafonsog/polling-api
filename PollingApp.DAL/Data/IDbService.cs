using System;
using System.Collections.Generic;

namespace PollingApp.DAL.Data
{
    public interface IDbService
    {
        void DeleteRecord<T>(string collection, string id) where T : class;
        void InsertRecord<T>(string collection, T record) where T : class;
        void InsertManyRecords<T>(string collection, IList<T> records) where T : class;
        T LoadRecordById<T>(string collection, string id) where T : class;
        List<T> LoadRecords<T>(string collection, int limit, int offset, string filter) where T : class;
        void UpsertRecord<T>(string collection, string id, T record) where T : class;
    }
}