using System.ComponentModel.DataAnnotations;

namespace RongProject1.ViewModels
{
    public class RegisterUserViewModel
    {
        //Set length
        [Required, MaxLength(256)]
        public string Username { get; set; }

        //Set data type
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        //Set data type & compare with password
        //Compare("Password") / Compare(nameof(Password))
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
