namespace Service_order_service
{
    abstract class User
    {
        private int UserId;
        private string? Name;
        private string? Surname;
        private string? Email;
        private DateTime DateOfBirth;
        private string? Password;
        private string? PhoneNumber;
        private double Balance;

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public string GetSurname()
        {
            throw new NotImplementedException();
        }

        public string GetEmail()
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateOfBirth()
        {
            throw new NotImplementedException();
        }

        public string GetPassword()
        {
            throw new NotImplementedException();
        }

        public string GetPhoneNumber()
        {
            throw new NotImplementedException();
        }

        public double GetBalance()
        {
            throw new NotImplementedException();
        }

        public void Login()
        {
            throw new NotImplementedException();
        }

        public void Register()
        {
            throw new NotImplementedException();
        }

        public void AddFunds()
        {
            throw new NotImplementedException();
        }
    }
}
