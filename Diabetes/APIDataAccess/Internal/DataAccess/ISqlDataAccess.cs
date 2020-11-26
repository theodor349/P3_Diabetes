using System.Collections.Generic;

namespace APIDataAccess.Internal.DataAccess
{
    public interface ISqlDataAccess
    {
        void CommitTransaction();
        int DeleteData<T>(string storedProcedure, T parameters, string connectionStringName);
        void DeleteDataInTransaction<T>(string storedProcedure, T parameters);
        void Dispose();
        string GetConnectionString(string name);
        List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters);
        void RollbackTransaction();
        int SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
        void SaveDataInTransaction<T>(string storedProcedure, T parameters);
        void StartTransaction(string connectionStringName);
    }
}