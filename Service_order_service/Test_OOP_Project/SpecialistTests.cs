namespace Test_OOP_Project
{
    [TestClass]
    public sealed class SpecialistTests
    {
        private Specialist specialist;

        [TestInitialize]
        public void Setup()
        {
            specialist = new Specialist(5, "John", "Doe", "john.doe@example.com", new DateTime(1988, 5, 22), "securePass789", "+38(067)-9876543", 500.00);
        }

        [TestMethod]
        public void GetName_ReturnsCorrectName()
        {
            Assert.AreEqual("John", specialist.GetName());
        }

        [TestMethod]
        public void GetSurname_ReturnsCorrectSurname()
        {
            Assert.AreEqual("Doe", specialist.GetSurname());
        }

        [TestMethod]
        public void GetEmail_ReturnsCorrectEmail()
        {
            Assert.AreEqual("john.doe@example.com", specialist.GetEmail());
        }

        [TestMethod]
        public void GetDateOfBirth_ReturnsCorrectDate()
        {
            Assert.AreEqual(new DateTime(1988, 5, 22), specialist.GetDateOfBirth());
        }

        [TestMethod]
        public void GetPassword_ReturnsCorrectPassword()
        {
            Assert.AreEqual("securePass789", specialist.GetPassword());
        }

        [TestMethod]
        public void GetPhoneNumber_ReturnsCorrectPhoneNumber()
        {
            Assert.AreEqual("+38(067)-9876543", specialist.GetPhoneNumber());
        }

        [TestMethod]
        public void GetBalance_ReturnsCorrectBalance()
        {
            Assert.AreEqual(500.00, specialist.GetBalance());
        }
    }
}
