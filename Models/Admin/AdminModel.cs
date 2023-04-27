
using System.ComponentModel.DataAnnotations;
using SistemaCadastroEleitoral.Models.Cadastro;

namespace SistemaCadastroEleitoral.Models.Admin
{
public class AdminModel
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string NomeAdmin { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string CPF { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string Fone { get; set; }        
        public virtual List<CadastroModel> Cadastros { get; set; }
        public string Acesso { get{ return "Admin";} }// caso houver erro nesta linha tentar outras opções de retunr

    }
}