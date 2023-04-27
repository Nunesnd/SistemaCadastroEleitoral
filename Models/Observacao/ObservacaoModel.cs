using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaCadastroEleitoral.Models.Admin;
using SistemaCadastroEleitoral.Models.Cadastros;

namespace SistemaCadastroEleitoral.Models.Observacao
{
    public class ObservacaoModel
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public string Conteudo { get; set; }

        public string Data { get; set; }

        [ForeignKey("Cadastro")]
        public int cadastroId { get; set; }
        public CadastroModel Cadastro { get; set; }
    }
}