using System.ComponentModel.DataAnnotations;

namespace Demo_Product.Models
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "Lütfen isim giriniz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen Soyisim giriniz.")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Lütfen mail giriniz.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen mail giriniz.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi tekrar  giriniz.")]
        [Compare("Password", ErrorMessage = "Lütfen şifrenin doğru oldğundan emin olun")]
        public string ConfirmPassword { get; set; }
    }
}
