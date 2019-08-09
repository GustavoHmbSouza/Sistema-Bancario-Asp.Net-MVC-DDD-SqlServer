using ProjTeste.Domain.ConexaoBancoDados;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ProjTeste.Repository.Infra
{
    public class ConexaoBancoDados : IConexaoBancoDados
    {
        // String de conexão
        private string _connectionString = "Data Source=GUSTAVO-MATOS\\SQLEXPRESS;Initial Catalog=ProjTesteBd;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // Variável que irá guardar sua conexão atual com banco de dados para utilizar para consultas
        private readonly SqlConnection _connection;

        // Variável que irá guardar todos os comandos que você irá fazer no banco de dados
        // Exemplo Adicionar variável, definir qual procedimento você irá buscar
        private SqlCommand Command { get; set; }

        // Construtor para criar objeto da conexão
        public ConexaoBancoDados()
        {
            _connection = Connect();
        }

        // Método que irá adicionar parâmetro da procedure que será executada conforme o método acima
        // Os parâmetros serão adicionados na propriedade Command.
        public void AddParametro(string nomeParametro, object valor) // object pois todos os tipos herdão de object, portanto todos os tipos serão válidos aqui
        {
            // adicionando parametro
            // caso o valor vindo como parâmetro for NULO, irá passar o valor de nulo padrão do banco de dados
            // ?? significa que se o valor a esquerda for nulo, ele usará o da direita, se não usa o primeiro
            // Ex: null ?? 1 (retorna 1)
            //     1 ?? 2    (retorna 1)
            Command.Parameters.Add(new SqlParameter(nomeParametro, valor ?? DBNull.Value));
        }

        // Método para conectar no banco de dados
        public SqlConnection Connect()
        {
            // cria conexão
            var connection = new SqlConnection(_connectionString);

            // verifica se conexão está com problema
            if (connection.State == ConnectionState.Broken)
            {
                connection.Close();
                connection.Open();
            }

            // se a conexão não estiver aberta, abrir
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }

        // Método para executar procedure
        // Este método receberá o nome da sua procedure e irá configurar na propriedade command esta procedure
        public void ExecutarProcedure(string nomeProcedure)
        {
            Command = new SqlCommand(nomeProcedure, _connection)
            {
                CommandType = CommandType.StoredProcedure
            };
        }

        public void ExecutarSemRetorno()
        {
            Command.ExecuteNonQuery();
        }

        // Encapsulando ExecuteReader para utilizado nas classes que irão utilizar a DatabaseConnect
        public SqlDataReader ExecuteReader()
        {
            return Command.ExecuteReader();
        }

        public void AddParametroReturn(string parameterName = "@RETURN_VALUE", DbType paramterType = DbType.Int16)
        {
            Command.Parameters.Add(new SqlParameter
            {
                ParameterName = parameterName,
                Direction = ParameterDirection.ReturnValue,
                DbType = paramterType
            });
        }

        public int ExecuteNoQueryWithReturn()
        {
            try
            {
                AddParametroReturn();
                Connect();
                Command.ExecuteNonQuery();
                return int.Parse(Command.Parameters["@RETURN_VALUE"].Value.ToString());
            }
            catch (SqlException ex)
            {
                return -1;
            }
        }
    }

}
