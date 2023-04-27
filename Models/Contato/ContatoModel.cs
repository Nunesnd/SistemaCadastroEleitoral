using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaCadastroEleitoral.Models.Cadastro;

namespace SistemaCadastroEleitoral.Models.Contato
{
    public class ContatoModel
    {
        [Required]
        [Key]
        public int IdContato { get; set; }

        [Required]
        public string NomeContato { get; set; }

        public string LinkContato { get; set; }

        
        [ForeignKey("Cadastro")]
        public int cadastroId { get; set; }
        public CadastroModel Cadastro { get; set; }


    }
}