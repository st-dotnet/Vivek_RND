namespace WebForm.Models
{
    public class DeleteUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public long PhoneNumber { get; set; }
        public long AlternatePhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
    }
}
