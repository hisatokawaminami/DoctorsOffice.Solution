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
    public void GetAll_DbStartsEmpty()
    {
      //Arrange
      //Act
      int result = Patient.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void CanYouCompareTWOObjects()
    {
      //arrange //act
      DateTime temp = new DateTime(1991,06,06);
      Patient firstPatient = new Patient("Monkey", 1991,06,06);
      Patient secondPatient = new Patient("Monkey", temp);

      //Assert
      Assert.AreEqual(firstPatient, secondPatient);
    }

    [TestMethod]
    public void ComparePatientToDate()
    {
      //arrange //act
      DateTime temp = new DateTime(1991,06,06);
      Patient firstPatient = new Patient("MonkeyOne", 1991,06,06);
      temp.AddDays(1);
      Patient secondPatient = new Patient("MonkeyTwo", temp);
      DateTime frist = firstPatient.GetBirthday().Date;
      frist.AddDays(1);

      //Assert
      Assert.AreEqual(firstPatient.GetBirthday().Date, temp.Date);
      Assert.AreEqual(temp.Date, frist.Date);
    }

    [TestMethod]
    public void WillYouGetListBackwithGETALL()
    {
      //Arrange
      DateTime temp = new DateTime(1991,06,06);
      Patient firstPatient = new Patient("Monkey", temp);
      firstPatient.Save();
      Patient secondPatient = new Patient("Monkey", temp);
      secondPatient.Save();
      //Act
      List<Patient> testList = Patient.GetAll();
      List<Patient> test2List = new List<Patient>{firstPatient, secondPatient};
      //Assert
      CollectionAssert.AreEqual(testList, test2List);
    }
  }
}
