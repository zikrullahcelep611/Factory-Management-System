using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace FabrikaYonetimSistemi.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ad gereklidir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad gereklidir.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "E-posta alanı gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Parola gereklidir.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Parola en az {2} karakter ve en fazla {1} karakter olmalıdır.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Parola doğrulama gereklidir.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Parola ve parola doğrulama eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }
}
