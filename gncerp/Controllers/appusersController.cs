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
    public class appusersController : Controller
    {
        public IActionResult profile()
        {

            Appuser _user = string.Format("SELECT * FROM appusers WHERE id={0}", CurrentSession.id).GetQuery<Appuser>().FirstOrDefault();
            return View(_user);
        }


        public IActionResult userlist()
        {
            return View();
        }

        [Route("{controller}/addUser")]
        [HttpPost]
        public Result<string> addUser([FromBody]Appuser obj)
        {
            Result<string> result = new Result<string>();

            try
            {
              
                    SqlCommand cmd = new GenericDataAccess().CreateCommand();

                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO appusers(name,surname,username,role,email,tel,password,ispsv) OUTPUT INSERTED.id VALUES(@name,@surname,@username,@role,@email,@tel,@password,@ispsv)";
                    cmd.Parameters.AddWithValue("@name", obj.name);
                    cmd.Parameters.AddWithValue("@surname", obj.surname);
                    cmd.Parameters.AddWithValue("@username", obj.username);
                    cmd.Parameters.AddWithValue("@role", obj.role);
                    cmd.Parameters.AddWithValue("@email", obj.email);
                    cmd.Parameters.AddWithValue("@tel", obj.tel);
                    cmd.Parameters.AddWithValue("@password", obj.password);
                    cmd.Parameters.AddWithValue("@ispsv", obj.ispsv);

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



        [Route("{controller}/getUserbyid")]
        [HttpPost]
        public dynamic getUserbyid([FromBody]Appuser obj)
        {
            return string.Format("SELECT * From appusers WHERE id={0}", obj.id).GetDynamicQuery();
        }

        [Route("{controller}/updateUser")]
        [HttpPost]
        public Result<string> updateUser([FromBody]Appuser obj)
        {
            Result<string> result = new Result<string>();

            try
            {
                    SqlCommand cmd = new GenericDataAccess().CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.CommandText = "UPDATE appusers Set name=@name , surname=@surname , username=@username , role=@role , email=@email , tel=@tel , password=@password , ispsv=@ispsv WHERE id=@id";
                    cmd.Parameters.AddWithValue("@name", obj.name);
                    cmd.Parameters.AddWithValue("@surname", obj.surname);
                    cmd.Parameters.AddWithValue("@username", obj.username);
                    cmd.Parameters.AddWithValue("@role", obj.role);
                    cmd.Parameters.AddWithValue("@email", obj.email);
                    cmd.Parameters.AddWithValue("@tel", obj.tel);
                    cmd.Parameters.AddWithValue("@password", obj.password);
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    cmd.Parameters.AddWithValue("@ispsv", obj.ispsv);
                   var returnobj =(int) new GenericDataAccess().ExecuteNonQuery(cmd);

                if (returnobj>0)
                {
                    var jsonObj = new { returnId = returnobj };
                    result.status = true;
                    result.jsonObj = JsonConvert.SerializeObject(jsonObj);
                    return result;
                }

                
                result.status = true;
                result.jsonObj = JsonConvert.SerializeObject(returnobj);
                return result;

            }
            catch (Exception ex)
            {
                result.status = false;
                result.message = ex.ToString();
                return result;
            }

        }

        [Route("{controller}/deleteUser")]
        [HttpPost]
        public Result<string> deleteUser([FromBody]Appuser obj)
        {
            Result<string> result = new Result<string>();

            try
            {
              
                    SqlCommand cmd = new GenericDataAccess().CreateCommand();

                    cmd.Parameters.Clear();
                    cmd.CommandText = "UPDATE appusers Set isdel=@isdel WHERE id=@id";
                    cmd.Parameters.AddWithValue("@isdel",1);
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