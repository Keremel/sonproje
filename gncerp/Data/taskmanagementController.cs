using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using gncerp.Entities;
using gncerp.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace gncerp.Controllers
{
    public class taskmanagementController : Controller
    {
        public IActionResult userlist()
        {



            return View();
        } 

        public IActionResult tasklist(int id)
        {
            Appuser _user = string.Format("SELECT * FROM appusers WHERE id={0}  AND ispsv=0 AND isdel=0", id).GetQuery<Appuser>()[0];

            ViewBag.userid = id;
            return View(_user);
        }


        [Route("{controller}/gettaskbyid")]
        [HttpPost]
        public dynamic gettaskbyid([FromBody]Taskdb obj)
        {
            return string.Format("SELECT * From tasks WHERE id={0}", obj.id).GetDynamicQuery();
        }

        [Route("{controller}/addtask")]
        [HttpPost]
        public Result<string> addtask([FromBody]Taskdb obj)
        {
            Result<string> result = new Result<string>();

            try
            {
                SqlCommand cmd = new GenericDataAccess().CreateCommand();

                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO tasks(title,information,startdate,completedate,appuserid,appuserassignid,taskstatus) OUTPUT INSERTED.id VALUES(@title,@information,@startdate,@completedate,@appuserid,@appuserassignid,@taskstatus)";
                cmd.Parameters.AddWithValue("@title", obj.title);
                cmd.Parameters.AddWithValue("@information", obj.information);
                cmd.Parameters.AddWithValue("@startdate", obj.startdate);
                cmd.Parameters.AddWithValue("@completedate", obj.completedate);
                cmd.Parameters.AddWithValue("@appuserid", obj.appuserid);
                cmd.Parameters.AddWithValue("@appuserassignid", obj.appuserassignid);
                cmd.Parameters.AddWithValue("@taskstatus", obj.taskstatus);

                int returnId = new GenericDataAccess().ExecuteNonQuery(cmd);

                if (returnId > 0)
                {
                    var jsonObj = new { returnId = returnId };

                    result.status = true;
                    result.jsonObj = JsonConvert.SerializeObject(jsonObj);
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.status = false;
                result.message = ex.ToString();
                return result;
            }



            return result;
        }
        
        [Route("{controller}/addCommenttask")]
        [HttpPost]
        public Result<string> addCommenttask([FromBody]Commenttask obj)
        {
            Result<string> result = new Result<string>();

            try
            {
                SqlCommand cmd = new GenericDataAccess().CreateCommand();

                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO comments(text,taskid,appuserid) OUTPUT INSERTED.id VALUES(@text,@taskid,@appuserid)";
                cmd.Parameters.AddWithValue("@text", obj.text);
                cmd.Parameters.AddWithValue("@taskid", obj.taskid);
                cmd.Parameters.AddWithValue("@appuserid", obj.appuserid);

                int returnId = new GenericDataAccess().ExecuteNonQuery(cmd);

                if (returnId > 0)
                {
                    var jsonObj = new { returnId = returnId };

                    result.status = true;
                    result.jsonObj = JsonConvert.SerializeObject(jsonObj);
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.status = false;
                result.message = ex.ToString();
                return result;
            }



            return result;
        }



        [Route("{controller}/deleteCommenttask")]
        [HttpPost]
        public Result<string> deleteCommenttask([FromBody]Commenttask obj)
        {
            Result<string> result = new Result<string>();

            try
            {

                SqlCommand cmd = new GenericDataAccess().CreateCommand();

                cmd.Parameters.Clear();
                cmd.CommandText = "DELETE comments WHERE id=@id";
                cmd.Parameters.AddWithValue("@id", obj.id);

                int returnId = new GenericDataAccess().ExecuteNonQuery(cmd);

                if (returnId > 0)
                {
                    var jsonObj = new { returnId = returnId };

                    result.status = true;
                    result.jsonObj = JsonConvert.SerializeObject(jsonObj);
                    return result;
                }


            }
            catch (Exception ex)
            {
                result.status = false;
                result.message = ex.ToString();
                return result;
            }



            return result;
        }  
        
        [Route("{controller}/deletetask")]
        [HttpPost]
        public Result<string> deletetask([FromBody]Taskdb obj)
        {
            Result<string> result = new Result<string>();

            try
            {

                SqlCommand cmd = new GenericDataAccess().CreateCommand();

                cmd.Parameters.Clear();
                cmd.CommandText = "DELETE tasks WHERE id=@id";
                cmd.Parameters.AddWithValue("@id", obj.id);

                int returnId = new GenericDataAccess().ExecuteNonQuery(cmd);
                
                cmd.Parameters.Clear();
                cmd.CommandText = "DELETE comments WHERE taskid=@taskid";
                cmd.Parameters.AddWithValue("@taskid", obj.id);

                 returnId = new GenericDataAccess().ExecuteNonQuery(cmd);

                if (returnId > 0)
                {
                    var jsonObj = new { returnId = returnId };

                    result.status = true;
                    result.jsonObj = JsonConvert.SerializeObject(jsonObj);
                    return result;
                }


            }
            catch (Exception ex)
            {
                result.status = false;
                result.message = ex.ToString();
                return result;
            }



            return result;
        }    



        
        [Route("{controller}/updatetaskstatus")]
        [HttpPost]
        public Result<string> updatetaskstatus([FromBody]Taskdb obj)
        {
            Result<string> result = new Result<string>();

            try
            {

                SqlCommand cmd = new GenericDataAccess().CreateCommand();

                cmd.Parameters.Clear();
                cmd.CommandText = "UPDATE tasks Set taskstatus=@taskstatus,completedate=@completedate WHERE id=@id";
                cmd.Parameters.AddWithValue("@taskstatus", obj.taskstatus);
                cmd.Parameters.AddWithValue("@completedate", DateTime.Now);
                cmd.Parameters.AddWithValue("@id", obj.id);
                int returnId = new GenericDataAccess().ExecuteNonQuery(cmd);

                if (returnId > 0)
                {
                   
                    result.status = true;
                    //result.jsonObj = JsonConvert.SerializeObject(jsonObj);
                    return result;
                }


            }
            catch (Exception ex)
            {
                result.status = false;
                result.message = ex.ToString();
                return result;
            }



            return result;
        }




        [Route("{controller}/datatables")]
        [HttpPost]
        public IActionResult datatables([FromBody]DatatablesJS.DatatablesObject requestobj)
        {

            try
            {
                //var results = await _demoService.GetDataAsync(param);
                return new JsonResult(new DatatablesJS.DataTablesObjectResult().getresults(requestobj));
                //return new JsonResult(new { error = "aaa" });
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }

        }

    }
}