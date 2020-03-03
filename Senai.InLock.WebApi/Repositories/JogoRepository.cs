using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private string stringConexao = "Data Source=DEV18\\SQLEXPRESS; initial catalog=Inlock_Games_Tarde; integrated security=true;";

        public void Atualizar(int id, JogoDomain JogoAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string queryUpdate = "UPDATE Jogo " +
                                    "SET NomeJogo = @NomeJogo, Descricao = @Descricao, DataLancamento = @DataLancamento, Valor = @Valor " +
                                    "WHERE IdJogo = @ID";

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    // Passa os valores dos parâmetros
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Nome", JogoAtualizado.NomeJogo);
                    cmd.Parameters.AddWithValue("@Sobrenome", JogoAtualizado.Descricao);
                    cmd.Parameters.AddWithValue("@DataNascimento", JogoAtualizado.DataLancamento);
                    cmd.Parameters.AddWithValue("@Valor", JogoAtualizado.Valor);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public JogoDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Define a query a ser executada no banco
                string querySelect = "SELECT IdJogo, NomeJogo, Descricao,DataLancamento,Valor" +
                                     "WHERE IdJogo = @ID";

                // Define o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    // Define o valor dos parâmetros
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Abre a conexão com o banco
                    con.Open();

                    // Executa o comando e armazena os dados no objeto rdr
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // Caso o resultado da query possua registro
                    if (rdr.Read())
                    {
                        // Instancia um objeto usuario 
                        JogoDomain jogo = new JogoDomain
                        {
                            // Atribui às propriedades os valores das colunas da tabela do banco
                            IdJogo = Convert.ToInt32(rdr["IdJogo"])
                            ,
                            Descricao = rdr["Descricao"].ToString()
                            ,
                            DataLancamento = Convert.ToDateTime(rdr["DataLancamento"])
                            ,
                            Valor = rdr["Valor"].ToString()
                        };

                        // Retorna o jogo buscado
                        return jogo;
                    }
                }

                // Caso não encontre um email e senha correspondente, retorna null
                return null;
            }
        }

        public void Cadastrar(JogoDomain novoJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string queryInsert = "INSERT INTO Jogo(NomeJogo,Descricao,DataLancamento,Valor)" +
                                         "VALUES (@NomeJogo, @Descricao, @DataLancamento, @Valor)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@Nome", novoJogo.NomeJogo);
                    cmd.Parameters.AddWithValue("@Sobrenome", novoJogo.Descricao);
                    cmd.Parameters.AddWithValue("@DataLancamento", novoJogo.DataLancamento);
                    cmd.Parameters.AddWithValue("@Valor", novoJogo.Valor);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada passando o valor como parâmetro
                string queryDelete = "DELETE FROM Jogo WHERE IdJogo = @ID";

                // Declara o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<JogoDomain> Listar()
        {
            List<JogoDomain> jogos = new List<JogoDomain>();

            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a instrução a ser executada
                string querySelectAll = "SELECT IdJogo, NomeJogo, Descricao, DataLancamento, Valor FROM Jogo";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader para receber os dados do banco de dados
                SqlDataReader rdr;

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    // Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    // Enquanto houver registros para serem lidos no rdr, o laço se repete
                    while (rdr.Read())
                    {
                        // Instancia um objeto funcionario 
                        JogoDomain jogo = new JogoDomain
                        {
                            // Atribui à propriedade IdFuncionario o valor da coluna "IdFuncionario" da tabela do banco
                            IdJogo = Convert.ToInt32(rdr["IdJogo"])

                            // Atribui à propriedade Nome o valor da coluna "Nome" da tabela do banco
                            ,
                            NomeJogo = rdr["Nome"].ToString()

                            // Atribui à propriedade Sobrenome o valor da coluna "Sobrenome" da tabela do banco
                            ,
                            Descricao = rdr["Descricao"].ToString()

                            // Atribui à propriedade DataNascimento o valor da coluna "DataNascimento" da tabela do banco
                            ,
                            DataLancamento = Convert.ToDateTime(rdr["DataLancamento"])
                        };

                        // Adiciona o funcionario criado à lista funcionarios
                        jogos.Add(jogo);
                    }
                }
            }

            // Retorna a lista de funcionarios
            return jogos;
        }
    }
}

