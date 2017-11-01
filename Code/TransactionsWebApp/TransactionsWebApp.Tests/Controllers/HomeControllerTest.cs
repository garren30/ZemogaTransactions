﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactionsWebApp;
using TransactionsWebApp.Controllers;
using TransactionsWebApp.Models;

namespace TransactionsWebApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Login_usernameEmpty()
        {
            Users usr = new Users() { username = string.Empty, password="12345"};

           
        }
        
    }
}
