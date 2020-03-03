using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private string stringConexao = "Data Source=DEV18\\SQLEXPRESS; initial catalog=Inlock_Games_Tarde; integrated security=true;";

        public void Atualizar(int id, TipoUsuarioDomain tipoUsuarioAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string queryUpdate = "UPDATE TiposUsuario SET Titulo = @Titulo WHERE IdTipoUsuario = @ID";

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    // Passa os valores dos parâmetros
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Titulo", tipoUsuarioAtualizado.Titulo);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public TipoUsuarioDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string querySelectById = "SELECT IdTipoUsuario, Titulo FROM TiposUsuario WHERE IdTipoUsuario = @ID";

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
                        // Instancia um objeto tipoUsuario 
                        TipoUsuarioDomain tipoUsuario = new TipoUsuarioDomain
                        {
                            // Atribui às propriedades os valores das colunas da tabela do banco
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])
                            ,
                            Titulo = rdr["Titulo"].ToString()
                        };

                        // Retorna o tipoUsuario buscado
                        return tipoUsuario;
                    }

                    // Caso o resultado da query não possua registros, retorna null
                    return null;
                }
            }
        }

        public void Cadastrar(TipoUsuarioDomain novoTipoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string queryInsert = "INSERT INTO TiposUsuario(Titulo) VALUES (@Titulo)";

                // Declara o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@Titulo", novoTipoUsuario.Titulo);

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
                string queryDelete = "DELETE FROM TiposUsuario WHERE IdTipoUsuario = @ID";

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

        public List<TipoUsuarioDomain> Listar()
        {
            // Cria uma lista tipos de usuário onde serão armazenados os dados
            List<TipoUsuarioDomain> tiposUsuario = new List<TipoUsuarioDomain>();

            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a instrução a ser executada
                string querySelectAll = "SELECT IdTipoUsuario, Titulo FROM TiposUsuario";

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
                        // Instancia um objeto tipoUsuario 
                        TipoUsuarioDomain tipoUsuario = new TipoUsuarioDomain
                        {
                            // Atribui às propriedades os valores das colunas da tabela do banco
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])
                            ,
                            Titulo = rdr["Titulo"].ToString()
                        };

                        // Adiciona o tipoUsuario criado à lista tiposUsuario
                        tiposUsuario.Add(tipoUsuario);
                    }
                }
            }

            // Retorna a lista de tipos de usuário
            return tiposUsuario;
        }
    }
}
