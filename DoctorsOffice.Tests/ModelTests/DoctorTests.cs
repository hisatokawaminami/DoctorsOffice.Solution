using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using DoctorsOffice.Models;

namespace DoctorsOffice.Tests
{
  [TestClass]
  public class DoctorTests : IDisposable
  {
    public void Dispose()
    {
      Doctor.DeleteAll();
    }
    public DoctorTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=doctors_office_test;";
    }


    [TestMethod]
    public void GetAll_DatabaseStartswithNothing()
    {
      //act
      int result = Doctor.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
  }
}
