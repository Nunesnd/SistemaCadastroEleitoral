using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaCadastroEleitoral.Models.Admin;
using SistemaCadastroEleitoral.Models.Cadastro;

namespace SistemaCadastroEleitoral.Models.Endereco
{
    public class EnderecoModel
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public string Logradouro { get; set; }

        public string Número { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string UF { get; set; }

        public int CEP { get; set; } 

        [ForeignKey("Cadastro")]
        public int cadastroId { get; set; }
        public CadastroModel Cadastro { get; set; }

    }
}