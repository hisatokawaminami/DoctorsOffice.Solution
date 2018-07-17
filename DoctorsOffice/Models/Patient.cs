using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DoctorsOffice;
using System;

namespace DoctorsOffice.Models
{
  public class Patient
  {
    private int _id;
    private string _name;
    private int _birthday;

    public Patient (string Name, int Birthday, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _birthday = Birthday;
    }

    public override bool Equals(System.Object otherPatient)
    {
      if (!(otherPatient is Patient))
      {
        return false;
      }
      else
      {
        Patient newPatient = (Patient) otherPatient;
        bool idEquality = (this.GetId() == newPatient.GetId());
        bool nameEquality = (this.GetName() == newPatient.GetName());
        bool birthdayEquality = (this.GetBirthday() == newPatient.GetBirthday());
        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public int GetBirthday()
    {
      return _birthday;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM patients;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd  = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO patients (name, birthday) VALUES (@PatientName, @PatientBirthday);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@PatientName";
      name.Value = _name;
      cmd.Parameters.Add(name);

      MySqlParameter birthday = new MySqlParameter();
      birthday.ParameterName = "@PatientBirthday";
      birthday.Value = _birthday;
      cmd.Parameters.Add(birthday);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Patient> GetAll()
    {
      List<Patient> allPatients = new List<Patient> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM patients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int PatientId = rdr.GetInt32(0);
        string PatientName = "";
        if(!rdr.IsDBNull(1))
        {
          PatientName = rdr.GetString(1);
        }
        int PatientBirthday = 0;
        if(!rdr.IsDBNull(2))
        {
          PatientBirthday = rdr.GetInt32(2);
        }
        Patient newPatient = new Patient(PatientName, PatientBirthday, PatientId);
        allPatients.Add(newPatient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allPatients;
    }
    public static Patient Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM patients WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int PatientId = 0;
      string PatientName = "";
      int PatientBirthday = 0;

      while(rdr.Read())
      {
        PatientId = rdr.GetInt32(0);
        PatientName = rdr.GetString(1);
        PatientBirthday = rdr.GetInt32(2);
      }

      Patient foundPatient = new Patient(PatientName, PatientBirthday, PatientId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundPatient;
    }
  }
}
