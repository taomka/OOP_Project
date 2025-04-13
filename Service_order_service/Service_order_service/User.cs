namespace Service_order_service
{
    public abstract class User
    {
        public int UserId;
        public string? Name;
        public string? Surname;
        public string? Email;
        public DateTime DateOfBirth;
        public string? Password;
        public string? PhoneNumber;
        public double Balance;

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
