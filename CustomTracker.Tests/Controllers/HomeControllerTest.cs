using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomTracker;
using CustomTracker.Controllers;
using CustomTracker.Models.Concrete;
using CustomTracker.Models;
using Moq;
using CustomTracker.Models.Abstract;
using CustomTracker.Tests.Moq.Repository;
using System.Web;

namespace CustomTracker.Tests.Controllers
{
    //[TestClass]

    [TestFixture]
    public class HomeControllerTest
    {
        
        public HomeControllerTest()
        {
            Console.WriteLine("=====INIT======");
            mock = new Mock<IRepository>();
            GenerateUsers();
            GenerateTickets();
            
            //listTickets = new List<Tickets>();
            mock.Setup(x => x.Users).Returns(listUsers.AsQueryable());
            mock.Setup(x => x.Tickets).Returns(listTickets.AsQueryable());
            homeController = new HomeController(mock.Object);
            
            
         //   ticketsController = new TicketsController();
            Console.WriteLine("======ACT======");
        }
        IQueryProcessor queryProc = new EmailQueryProcessor(new EmailSettings());
        public List<Users> listUsers;
        public List<Tickets> listTickets;
        public Mock<IRepository> mock;
        public HomeController homeController;
        //public TicketsController ticketsController;
        public string CreateNewTicket(Tickets ticket)
        {
            listTickets.Add(ticket);
            return "AAA-999111";
        }
        public void GenerateTickets()
        {
            Random rnd = new Random();
            listTickets = new List<Tickets>();
            for (int iCount = 1; iCount <= 10; iCount++)
            {
                Tickets ticket = new Tickets
                {
                    Id = iCount,
                    DepartmentId = rnd.Next(1, 5),
                    UserId = rnd.Next(1, 5),
                    Email = "mailN" + iCount.ToString() + "@mail.com.ua",
                    Subject = "Subject #" + iCount.ToString(),
                    Reference = "AAA-00000" + iCount.ToString(),
                    Body = "something issue №" + iCount.ToString()
                };
                listTickets.Add(ticket);
            }
        }
        public void GenerateUsers()
        {
            listUsers = new List<Users>();
            Users First = new Users
            {
                Id = 0,
                Name = "Rondo",
                Password = "123qwe",
                Cookies = ""

            };
            listUsers.Add(First);
            Users Second = new Users
            {
                Id = 0,
                Name = "Ivanov",
                Password = "123qwe",
                Cookies = ""

            };
            listUsers.Add(Second);
        }
        [Test]
        public void Can_Paginate()
        {
            // create a controller and make the page size 3 items
            StaffController staffController = new StaffController(mock.Object, queryProc);
            staffController.PageSize = 3;
            // Action
            //TicketsListViewModel result = (TicketsListViewModel)staffController.ViewTickets().Model;
            var result = staffController.ViewTickets("",-1,2);
            var resultModel = ((ViewResult)result).Model;
            
            TicketsListViewModel model = (TicketsListViewModel)resultModel;
            // Assert
            Tickets[] prodArray = model.Tickets.ToArray();
            Assert.IsTrue(prodArray.Length > 0);
            Assert.AreEqual(prodArray[0].Id, 3);
            
        }
        [Test]
        public void Open_Custom_Query()
        {
            CustomController controller = new CustomController(mock.Object,queryProc);
            //Tickets ticket = listTickets.Find(x => x.Id == 1);
            var result = controller.CreateQuery();
            Assert.IsTrue(result != null);

        }
        [Test]
        public void Open_Edit_Ticket()
        {
            //listTickets.Find(x => x.Id == 1)
            StaffController staffController = new StaffController(mock.Object, queryProc);
            var result = staffController.Edit(1);
            Tickets resultModel = (Tickets)((ViewResult)result).Model;
            Assert.IsTrue(resultModel.Id == 1);
            result = staffController.Edit(100);
            Assert.IsInstanceOf<HttpNotFoundResult>(result);
        }
        [Test]
        public void Find_Incorrect_Reference_InTickets()
        {
            StaffController staffController = new StaffController(mock.Object, queryProc);
            var result = staffController.ViewTickets("TTTFFF");
            var resultModel = ((ViewResult)result).Model;
            TicketsListViewModel model = (TicketsListViewModel)resultModel;
            Assert.IsTrue(model.Tickets.ToList().Count() == 0);

        }
        [Test]
        public void Find_Correct_Reference_InTickets()
        {
            StaffController staffController = new StaffController(mock.Object, queryProc);
            var result = staffController.ViewTickets("AAA-000001");
            var resultModel = ((ViewResult)result).Model;
            TicketsListViewModel model = (TicketsListViewModel)resultModel;
            
            Assert.IsTrue(model.Tickets.ToList().Count() > 0);
        }
        [Test]
        public void OpenTicketsList()
        {
            var result = new StaffController(mock.Object, queryProc);
            Assert.IsNotNull(result);
        }
        [Test]
        public void User_LogIn_CorrectName()
        {
            RedirectToRouteResult result = homeController.LogIn("Ivanov", "123qwe") as RedirectToRouteResult;
            Assert.AreNotEqual(result.RouteValues.Where(x => x.Key == "Action"), null);
            

            //Assert.AreEqual(, result.RouteName);
        }
        [Test]
        public void User_LogIn_IncorrectName()
        {
            var result2 = homeController.LogIn("Rondsso", "123qwe");
            Assert.IsInstanceOf<LogOnModel>(((ViewResult)result2).Model);
        }
    }
}
