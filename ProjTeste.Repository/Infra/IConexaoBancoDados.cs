using System.Data.SqlClient;

namespace ProjTeste.Domain.ConexaoBancoDados
{
    public interface IConexaoBancoDados
    {
        SqlConnection SqlConnection { get; }
        SqlTransaction SqlTransaction { get; }
        void OpenTransaction();
        void Commit();
        void Rollback();
        void Dispose();
    }
}
