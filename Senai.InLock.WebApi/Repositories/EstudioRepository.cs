using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        private string stringConexao = "Data Source=DEV18\\SQLEXPRESS; initial catalog=Inlock_Games_Tarde; integrated security=true;";

        public void Atualizar(int id, EstudioDomain EstudioAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string queryUpdate = "UPDATE Estudio " +
                                    "SET NomeEstudio = @NomeEstudio";

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    // Passa os valores dos parâmetros
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Nome", EstudioAtualizado.NomeEstudio);
                    
                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public EstudioDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string querySelectById = "SELECT IdEstudio, NomeEstudio" +
                                        " WHERE IdEstudio = @ID";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader para receber os dados do banco de dados
                SqlDataReader rdr;

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    // Caso o resultado da query possua registro
                    if (rdr.Read())
                    {
                        // Instancia um objeto funcionario 
                        EstudioDomain estudio = new EstudioDomain
                        {
                            // Atribui à propriedade IdFuncionario o valor da coluna "IdFuncionario" da tabela do banco
                            IdEstudio = Convert.ToInt32(rdr["IdEstudio"])

                            // Atribui à propriedade Nome o valor da coluna "Nome" da tabela do banco
                            ,
                            NomeEstudio = rdr["NomeEstudio"].ToString()

                        };

                        // Retorna o funcionário buscado
                        return estudio;
                    }

                    // Caso o resultado da query não possua registros, retorna null
                    return null;
                }
            }
        }

        public void Cadastrar(EstudioDomain novoEstudio)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string queryInsert = "INSERT INTO Estudio(NomeEstudio) " +
                                     "VALUES (@NomeEstudio)";

                // Declara o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@Nome", novoEstudio.NomeEstudio);

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
                string queryDelete = "DELETE FROM Estudio WHERE IdEstudio = @ID";

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

        public List<EstudioDomain> Listar()
        {
            List<EstudioDomain> estudios = new List<EstudioDomain>();

            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a instrução a ser executada
                string querySelectAll = "SELECT IdEstudio, NomeEstudio";

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
                        EstudioDomain estudio = new EstudioDomain
                        {
                            // Atribui à propriedade IdFuncionario o valor da coluna "IdFuncionario" da tabela do banco
                            IdEstudio = Convert.ToInt32(rdr["IdEstudio"])

                            // Atribui à propriedade Nome o valor da coluna "Nome" da tabela do banco
                            ,
                            NomeEstudio = rdr["NomeEstudio"].ToString()
                        };

                        // Adiciona o funcionario criado à lista funcionarios
                        estudios.Add(estudio);
                    }
                }
            }

            // Retorna a lista de funcionarios
            return estudios;
        }
    }
}
