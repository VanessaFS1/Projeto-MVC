using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoWeb.Models
{
    public class UsuarioModel
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório", AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O sobrenome do usuário é obrigatório", AllowEmptyStrings = false)]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O CPF do usuário é obrigatório", AllowEmptyStrings = false)]
        public long CPF { get; set; }

        [Required(ErrorMessage = "O email do usuário é obrigatório", AllowEmptyStrings = false)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O login do usuário é obrigatório", AllowEmptyStrings = false)]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha do usuário é obrigatório", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O status do usuário é obrigatório", AllowEmptyStrings = false)]
        public string Status { get; set; }
    }
}