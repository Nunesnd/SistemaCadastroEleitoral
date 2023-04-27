using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaCadastroEleitoral.Models.Admin;
using SistemaCadastroEleitoral.Models.Cadastros;

namespace SistemaCadastroEleitoral.Models.Profissao
{
    public class ProfissaoModel
    {
        [Required]
        [Key]
        public int IdProfissao { get; set; }

        public string NomeProfissao { get; set; }

        [ForeignKey("Cadastro")]
        public int cadastroId { get; set; }
        public CadastroModel Cadastro { get; set; }

    }
}