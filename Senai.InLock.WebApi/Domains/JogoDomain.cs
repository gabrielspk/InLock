using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Domains
{
    public class JogoDomain
    {
        public int IdJogo { get; set; }

        public string NomeJogo { get; set; }

        public string Descrição { get; set; }

        public DateTime DataLancamento { get; set; }

        public int Preço { get; set; }

        public int IdTipoUsuario { get; set; }

        public EstudioDomain Estudio { get; set; }
    }
}
