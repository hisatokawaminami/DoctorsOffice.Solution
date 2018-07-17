using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DoctorsOffice.Models;
using System;

namespace DoctorsOffice.Tests
{
  [TestClass]
  public class PatientTests : IDisposable
  {
    public void Dispose()
    {
      Patient.DeleteAll();
      Doctor.DeleteAll();
    }
    public PatientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=doctors_office_test;";
    }
    [TestMethod]
    public void GetAll DbStartsEmpty
    {
      //Arrange
      //Act
      int result = Patient.Getall().Count;
    }
  }
}
