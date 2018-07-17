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

    [TestMethod]
    public void CanWeCompareTwoDoctorswithSameinfo()
    {
      //Arrange/act
      Doctor firstDoctor = new Doctor("Ai", "Brain Damage");
      Doctor secondDoctor = new Doctor("Ai", "Brain Damage");

      //Assert
      Assert.AreEqual(firstDoctor, secondDoctor);
    }

    [TestMethod]
    public void GetaListBackwithGETALL()
    {
      //Arrange
      Doctor testDoctorOne = new Doctor("YU", "Family Doctor");
      testDoctorOne.Save();
      Doctor testDoctorTwo = new Doctor("Mi", "Family Doctor");
      testDoctorTwo.Save();
      //Act
      List<Doctor> testlist = Doctor.GetAll();
      List<Doctor> testlistTwo = new List<Doctor> {testDoctorOne, testDoctorTwo};

      //Assert
      CollectionAssert.AreEqual(testlist, testlistTwo);
    }

    [TestMethod]
    public void Save_DatabaseAssignIdToCategory_id()
    {
      //Arrange
      Doctor testDoctor = new Doctor("Hisato", "Cardiology");
      testDoctor.Save();

      //act
      Doctor savedDoctor = Doctor.GetAll()[0];

      int result = savedDoctor.GetId();
      int testId = testDoctor.GetId();

      //Assert
      Assert.AreEqual(result, testId);
    }
  }
}
