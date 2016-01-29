using CustomTracker.Models;
using CustomTracker.Models.Abstract;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomTracker.Tests.Moq.Repository
{
    public partial class MockEFRepository : Mock<IRepository>
    {
        public MockEFRepository(MockBehavior mockBehavior = MockBehavior.Strict)
            : base(mockBehavior)
        {
            GenerateUsers();
        }
        public List<Users> Users { get; set; }

        private List<Users> listUsers;
        public void GenerateUsers()
        {
            listUsers = new List<Users>();
            var First = new Users
            {
                Id = 1,
                Name = "Rondo",
                Password = "123qwe",
                Cookies = ""

            };
            listUsers.Add(First);
            var Second = new Users
            {
                Id = 1,
                Name = "Petrenko",
                Password = "123qwe",
                Cookies = ""

            };
            listUsers.Add(Second);
            this.Setup(p => p.Users).Returns(listUsers.AsQueryable());
            this.Setup(p => p.GetUser(It.IsAny<string>())).Returns((string Name) =>
                Users.FirstOrDefault(p => string.Compare(p.Name, Name, 0) == 0));
            this.Setup(p => p.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((string name, string password) =>
                       Users.FirstOrDefault(p => string.Compare(p.Name, Name, 0) == 0));

        }
    }
}
