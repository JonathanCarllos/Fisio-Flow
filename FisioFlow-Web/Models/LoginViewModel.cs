using System.ComponentModel.DataAnnotations;

namespace FisioFlow_Web.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Informe o usuário.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a senha.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}