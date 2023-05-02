namespace DotMessenger.Core.Model.Abstract
{
    public interface IUser
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}