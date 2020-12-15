namespace API.Models {
    public class AccountUpdate 
    {
        public AccountUpdate()
        {

        }
        public AccountUpdate(string firstName, string lastName, bool isEU)
        {
            FirstName = firstName;
            LastName = lastName;
            IsEU = isEU;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsEU { get; set; }
    }
}
