using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public void Atualizar(int id, UsuarioDomain UsuarioAtualizado)
        {

        }

        public UsuarioDomain BuscarPorEmailSenha(string email, string senha)
        {
            
        }

        public UsuarioDomain BuscarPorId(int id)
        {
            
        }

        public void Cadastrar(UsuarioDomain novoUsuario)
        {
            
        }

        public void Deletar(int id)
        {
            
        }

        public List<UsuarioDomain> Listar()
        {
            
        }
    }
}
