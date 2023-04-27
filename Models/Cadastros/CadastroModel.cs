using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaCadastroEleitoral.Models.Admin;
using SistemaCadastroEleitoral.Models.Contato;
using SistemaCadastroEleitoral.Models.Endereco;
using SistemaCadastroEleitoral.Models.Observacao;
using SistemaCadastroEleitoral.Models.Profissao;

namespace SistemaCadastroEleitoral.Models.Cadastros
{
    public class CadastroModel
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Apelido { get; set; }

        public string CPF { get; set; }

        public bool Filhos { get; set; }

        public string Nascimento { get; set; }

        public string Fone { get; set; }   

        public virtual List<ContatoModel> ContatosLinks { get; set; }

        public virtual List<EnderecoModel> Enderecos { get; set; }
        
        public virtual List<ObservacaoModel> Observacoes { get; set; }

        public virtual List<ProfissaoModel> Profissoes { get; set; }

        [ForeignKey("Admin")]
        public int AdminId { get; set; }
        public AdminModel Admin { get; set; }
    }
}