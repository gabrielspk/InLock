using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string stringConexao = "Data Source=DEV18\\SQLEXPRESS; initial catalog=Inlock_Games_Tarde; integrated security=true;";

        public void Atualizar(int id, UsuarioDomain UsuarioAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE Usuarios" +
                                     "Set Email = @Email, Senha = @Senha, IdTipoUsuario = @IdTipoUsuario" +
                                     "WHERE IdTipoUsuario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Email", UsuarioAtualizado.Email);
                    cmd.Parameters.AddWithValue("@Senha", UsuarioAtualizado.Senha);
                    cmd.Parameters.AddWithValue("@IdTipoUsuario", UsuarioAtualizado.TipoUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UsuarioDomain BuscarPorEmailSenha(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelect = "SELECT Usuario.Email, TipoUsuario.Titulo FROM Usuario INNER JOIN TipoUsuario ON TipoUsuario.IdTipoUsuario = Usuario.IdTipoUsuario WHERE Email = @Email AND Senha = @Senha";
                                     
                                     

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        UsuarioDomain usuario = new UsuarioDomain
                        {
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"])
                            ,
                            Email = rdr["Email"].ToString()
                            ,
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])
                            ,
                            TipoUsuario = new TipoUsuarioDomain
                            {
                                IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])
                                ,
                                Titulo = rdr["Titulo"].ToString()
                            }
                        };
                        return usuario;
                    }
                }

                return null;
            }
        }

        public UsuarioDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT IdtipoUsuario,Email,Senha FROM Usuario" +
                                         "WHERE Idusuario = @ID";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        // Instancia um objeto usuario 
                        UsuarioDomain usuario = new UsuarioDomain
                        {
                            // Atribui às propriedades os valores das colunas da tabela do banco
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"])
                            ,
                            Email = rdr["Email"].ToString()
                            ,
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])
                            ,
                            TipoUsuario = new TipoUsuarioDomain
                            {
                                IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])
                                ,
                                Titulo = rdr["Titulo"].ToString()
                            }
                        };

                        // Retorna o usuario buscado
                        return usuario;
                    }

                    // Caso o resultado da query não possua registros, retorna null
                    return null;
                }
            }
        }

        public void Cadastrar(UsuarioDomain novoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Usuario(Email, Senha, IdTipoUsuario)" +
                                     "VALUES (@Email, @Senha, @IdTipoUsuario)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Email", novoUsuario.Email);
                    cmd.Parameters.AddWithValue("@Senha", novoUsuario.Senha);
                    cmd.Parameters.AddWithValue("@IdTipoUsuario", novoUsuario.IdTipoUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {


                string queryDelete = "DELETE FROM Usuarios WHERE IdUsuario = @ID";

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

        public List<UsuarioDomain> Listar()
        {
            List<UsuarioDomain> usuarios = new List<UsuarioDomain>();

            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a instrução a ser executada
                string querySelectAll = "SELECT U.IdUsuario, U.Email, U.IdTipoUsuario, TU.Titulo FROM Usuarios U INNER JOIN TiposUsuario TU ON U.IdTipoUsuario = TU.IdTipoUsuario";

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
                        // Instancia um objeto usuario 
                        UsuarioDomain usuario = new UsuarioDomain
                        {
                            // Atribui às propriedades os valores das colunas da tabela do banco
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"])
                            ,
                            Email = rdr["Email"].ToString()
                            ,
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),
                            TipoUsuario = new TipoUsuarioDomain
                            {
                                IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])
                                ,
                                Titulo = rdr["Titulo"].ToString()
                            }
                        };

                        // Adiciona o usuario criado à lista usuarios
                        usuarios.Add(usuario);
                    }
                }
            }

            // Retorna a lista de usuários
            return usuarios;
        }
    }
}
