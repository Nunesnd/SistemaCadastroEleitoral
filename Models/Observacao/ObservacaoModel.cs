using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaCadastroEleitoral.Models.Admin;
using SistemaCadastroEleitoral.Models.Cadastro;

namespace SistemaCadastroEleitoral.Models.Observacao
{
    public class ObservacaoModel
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public string Conteudo { get; set; }

        public DateTime DataObservacao { get; set; }

        public ObservacaoModel()
        {
            DataObservacao = DateTime.Now;
        }


        [ForeignKey("Cadastro")]
        public int cadastroId { get; set; }
        public CadastroModel Cadastro { get; set; }
    }
}