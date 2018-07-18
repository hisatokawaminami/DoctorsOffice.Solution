using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace DoctorsOffice.Models
{
  public class Doctor
  {
    private int _id;
    private string _name;
    private string _specialty;

    public Doctor(string name, string specialty, int id = 0)
    {
      _id = id;
      _name = name;
      _specialty  = specialty;
    }
    public override bool Equals(System.Object otherDoctor)
    {
      if (!(otherDoctor is Doctor))
      {
        return false;
      }
      else
      {
        Doctor newDoctor = (Doctor) otherDoctor;
        return this.GetId().Equals(newDoctor.GetId());
      }
    }
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetSpecialty()
    {
      return _specialty;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM doctors;";

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

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO doctors (name, specialty) VALUES (@name, @specialty);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter specialty = new MySqlParameter();
      specialty.ParameterName = "@specialty";
      specialty.Value = this._specialty;
      cmd.Parameters.Add(specialty);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Doctor> GetAll()
    {
      List<Doctor> allDoctors = new List<Doctor> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM doctors;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int DoctorId = rdr.GetInt32(0);
        string DoctorName = rdr.GetString(1);
        string DoctorSpecialty = rdr.GetString(2);
        Doctor newDoctor = new Doctor(DoctorName, DoctorSpecialty, DoctorId);
        allDoctors.Add(newDoctor);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allDoctors;
    }

    public static Doctor Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM doctors WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int DoctorId = 0;
      string DoctorName = "";
      string DoctorSpecialty = "";

      while(rdr.Read())
      {
        DoctorId = rdr.GetInt32(0);
        DoctorName = rdr.GetString(1);
        DoctorSpecialty = rdr.GetString(2);
      }
      Doctor newDoctor = new Doctor(DoctorName, DoctorSpecialty, DoctorId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newDoctor;
    }
    public List<Patient> GetPatients()
    {
      MySqlConnection conn =DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT patients.* FROM doctors JOIN doctors_patients ON (doctors.id = doctors_patients.doctor_id) JOIN patients ON (doctors_patients.patient_id = patients.id) WHERE doctors.id = @DoctorId;";

      MySqlParameter doctorIdParameter = new MySqlParameter();
      doctorIdParameter.ParameterName = "@DoctorId";
      doctorIdParameter.Value = _id;
      cmd.Parameters.Add(doctorIdParameter);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Patient> patients = new List<Patient>{};

      while(rdr.Read())
      {
        int patientId = rdr.GetInt32(0);
        string patientName = rdr.GetString(1);
        DateTime patientBirthday = rdr.GetDateTime(2);
        Patient newPatient = new Patient(patientName, patientBirthday, patientId);
        patients.Add(newPatient);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return patients;
    }
  }
}
