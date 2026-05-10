using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using MTesting.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MTesting.Controllers
{
    public class HomeController : Controller
    {
       
        // GET: /Home/

        string sqlcon=ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

        public ActionResult Index()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection con = new SqlConnection(sqlcon))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("tblStudentSelect", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Student student = new Student();

                    student.Id = Convert.ToInt32(reader["Id"]);
                    student.Name = Convert.ToString(reader["Name"]);
                    student.Salary = Convert.ToDecimal(reader["Salary"]);
                    student.IsFullTime = Convert.ToString(reader["IsfullTime"]);
                    string Skillstring = Convert.ToString(reader["Skills"]);
                    student.Skills = Skillstring.Split(',').ToList();
                    student.Department = Convert.ToString(reader["Department"]);

                   
                    students.Add(student);
                }
            }
            return View(students);
        }

        public ActionResult Create(){

            ViewBag.DeptList=GetDept();
            return View();
        }


        public ActionResult Edit(int id)
        {
            Student student = new Student();
            using (SqlConnection con = new SqlConnection(sqlcon))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("tblStudentEdit", con);
                cmd.CommandType =System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    student.Id = Convert.ToInt32(reader["Id"]);
                    student.Name = Convert.ToString(reader["Name"]);
                    student.Salary = Convert.ToDecimal(reader["Salary"]);
                    student.IsFullTime = Convert.ToString(reader["IsfullTime"]);
                    string Skillstring = Convert.ToString(reader["Skills"]);
                    student.Skills = Skillstring.Split(',').ToList();
                    student.Department = Convert.ToString(reader["Department"]);

                   

                }
            }
            ViewBag.DeptList = GetDept();
            return View(student);
        }
        public ActionResult Delete(int id)
        {
            Student student = new Student();
            using (SqlConnection con = new SqlConnection(sqlcon))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("tblStudentDelete", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Create(Student student)
        {
            string Skillstring = student.Skills != null ? string.Join(",", student.Skills) : "";
            using (SqlConnection con = new SqlConnection(sqlcon))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("tblStudentInsert", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Department", student.Department);
                cmd.Parameters.AddWithValue("@Salary", student.Salary);
                cmd.Parameters.AddWithValue("@IsfullTime", student.IsFullTime);
                cmd.Parameters.AddWithValue("@Skills", Skillstring);
                cmd.ExecuteNonQuery();
                con.Close();
            }
           
            return Content("<script>alert ('Data Save Successfully'); window.location.href='/Home/Index';</Script>");
        }
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            string Skillstring = student.Skills != null ? string.Join(",", student.Skills) : "";
            using (SqlConnection con = new SqlConnection(sqlcon))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("tblStudentUpdate", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", student.Id);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Department", student.Department);
                cmd.Parameters.AddWithValue("@Salary", student.Salary);
                cmd.Parameters.AddWithValue("@IsfullTime", student.IsFullTime);
                cmd.Parameters.AddWithValue("@Skills", Skillstring);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return Content("<script>alert ('Data Update Successfully'); window.location.href='/Home/Index';</Script>");
        }

        private List<SelectListItem> GetDept(){
        
         List<SelectListItem> list=new List<SelectListItem> ();
         using (SqlConnection con = new SqlConnection(sqlcon)) {
             con.Open();
             SqlCommand cmd = new SqlCommand("select * from Department", con);
             SqlDataReader reader=cmd.ExecuteReader();
             list.Add(new SelectListItem { Text = "------Select----",Value="" });
             while(reader.Read()){

                 list.Add(new SelectListItem { Text = reader["DeptName"].ToString(), Value = reader["DeptId"].ToString() });
             
             }
         }
         return list;
        }






    }
}
