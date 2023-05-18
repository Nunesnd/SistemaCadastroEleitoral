using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaCadastroEleitoral.Models.Cadastro;
using SistemaCadastroEleitoral.Enum.TipoLink;

namespace SistemaCadastroEleitoral.Models.Contato
{
    public class ContatoModel
    {
        [Required]
        [Key]
        public int IdContato { get; set; }

        public string NomeContato { get; set; }
        public string LinkContato { get; set; }
        //public TipoLink TipoLink { get; set; } // Propriedade do tipo Enum

        [ForeignKey("Cadastro")]
        public int cadastroId { get; set; }
        public CadastroModel Cadastro { get; set; }


    }
}