using ProjTeste.Domain.ConexaoBancoDados;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ProjTeste.Repository.Infra
{
    public class ConexaoBancoDados : IConexaoBancoDados
    {
        public SqlConnection SqlConnection { get; }
        public SqlTransaction SqlTransaction { get; set; }

        // String de conexão
        private string _connectionString = "Data Source=GUSTAVO-MATOS\\SQLEXPRESS;Initial Catalog=ProjTesteBd;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ConexaoBancoDados(Parameter parameters)
        {
            SqlConnection = new SqlConnection(_connectionString);
        }

        private void OpenConnection()
        {
            if (SqlConnection.State == ConnectionState.Broken)
            {
                SqlConnection.Close();
                SqlConnection.Open();
            }

            if (SqlConnection.State == ConnectionState.Closed)
                SqlConnection.Open();
        }

        public void OpenTransaction()
        {
            OpenConnection();
            SqlTransaction = SqlConnection.BeginTransaction();
        }

        public void OpenTransaction(IsolationLevel isolationLevel)
        {
            OpenConnection();
            SqlTransaction = SqlConnection.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            SqlTransaction.Commit();
            SqlTransaction.Dispose();
        }

        public void Rollback()
        {
            SqlTransaction.Rollback();
            SqlTransaction.Dispose();
        }

        public void Dispose()
        {
            SqlConnection.Close();
        }
    }
}
