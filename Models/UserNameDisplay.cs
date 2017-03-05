namespace exam4.Models
{
    public class UserNameDisplay
    {
        public int id { get; set; }
        public string username { get; set; }
        public UserNameDisplay(){}
        public UserNameDisplay(int ident, string fullname){
            id = ident;
            username = fullname;
        }

    }
}