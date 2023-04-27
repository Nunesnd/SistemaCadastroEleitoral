using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SistemaCadastroEleitoral.Models.Admin;
using SistemaCadastroEleitoral.Models.Cadastro;
using SistemaCadastroEleitoral.Models.Contato;
using SistemaCadastroEleitoral.Models.Endereco;
using SistemaCadastroEleitoral.Models.Observacao;
using SistemaCadastroEleitoral.Models.Profissao;

namespace SistemaCadastroEleitoral.Infraestrutura.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base (options){}

        public BancoContext(){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            JToken jAppSettings = JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json")));
            optionsBuilder.UseSqlServer(jAppSettings["ConnectionStrings"]["MinhaConexao"].ToString());
        }

        public DbSet<AdminModel> Admins {get; set;}
        public DbSet<CadastroModel> Cadastros {get; set;}
        public DbSet<ContatoModel> Contatos {get; set;}        
        public DbSet<EnderecoModel> Enderecos {get; set;}            
        public DbSet<ObservacaoModel> Observacoes {get; set;}       
        public DbSet<ProfissaoModel> Profissoes {get; set;}
    }
}