using System.Data.SqlClient;

namespace ProjTeste.Domain.ConexaoBancoDados
{
    public interface IConexaoBancoDados
    {
        SqlConnection Connect();

        void ExecutarProcedure(string nomeProcedure);

        void AddParametro(string nomeParametro, object valor);

        void ExecutarSemRetorno();

        SqlDataReader ExecuteReader();

        int ExecuteNoQueryWithReturn();
    }
}
