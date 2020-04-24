using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test1APBD.Models;

namespace Test1APBD.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TeamMemberController : ControllerBase
    {
        private string ConnString = "Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=s16500;Integrated Security=True";





        [HttpGet]
        public IActionResult GetTasks()
        {


            var result = new List<Tasks>();
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;

                com.CommandText = "select tm.FirstName,t.Name,t.Description,t.Deadline,ty.Name,p.Name from TeamMember tm, Task t, TaskType ty, project p where ty.IdTaskType = t.IdTask AND p.IdProject = t.IdProject";
                con.Open();
                try
                {
                    SqlDataReader dr = com.ExecuteReader();


                    while (dr.Read())
                    {
                        var ts = new Tasks();
                      
                        ts.firstname = dr[0].ToString();
                        ts.taskname = dr[1].ToString();
                        ts.description = dr[2].ToString();
                        ts.deadline = dr[3].ToString();
                        ts.tasktype = dr[4].ToString();
                        ts.projectname = dr[5].ToString();

                        result.Add(ts);
                    }
                }
                catch (Exception e)
                {
                    return BadRequest("404");
                }

                return Ok(result);
            }
        }

        [HttpGet("{idTeamMember}")]
        public IActionResult GetTeamMember(string idTeamMember)
        {

            var result = new List<TeamMember>();
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select tm.FirstName,t.Name,t.Description,t.Deadline,ty.Name,p.Name from TeamMember tm, Task t, TaskType ty, project p where ty.IdTaskType = t.IdTask AND p.IdProject = t.IdProject AND tm.IdTeamMember=@id";

                SqlParameter par1 = new SqlParameter();
                par1.ParameterName = "id";
                par1.Value = idTeamMember;

                com.Parameters.Add(par1);
               
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    var tm = new TeamMember();
                   
                    tm.firstname = dr[0].ToString();
                    tm.taskname = dr[1].ToString();
                    tm.description = dr[2].ToString();
                    tm.deadline = DateTime.Parse(dr[3].ToString());
                    tm.tasktype = dr[4].ToString();
                    tm.projectname = dr[5].ToString();
                    return Ok(tm);
                }

                return Ok();
            }

        }

        [HttpDelete("{idDelete}")]
        public IActionResult DeleteTask(string idDelete)
        {

            var result = new List<TeamMember>();
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "delete from Task where task.IdTask = @idtask";
                SqlParameter par1 = new SqlParameter();
                par1.ParameterName = "idtask";
                par1.Value = idDelete;

                com.Parameters.Add(par1);

                con.Open();
                Console.WriteLine ("Task has been deleted.");
                

                return Ok();
            }

        }
    }
}