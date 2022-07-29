using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using gncerp.Entities;
using gncerp.Models;
using gncerp.Utility;
using Microsoft.AspNetCore.Mvc;

namespace gncerp.Controllers
{
    public class accountcodeController : Controller
    {
        public IActionResult accountcodelist()
        {
            List<Accountcodemapping> modellist = new List<Accountcodemapping>();

            modellist = string.Format(@"SELECT * FROM accountcodemapping").GetQuery<Accountcodemapping>();

            return View(modellist);
        }

        public IActionResult accountcodeadd()
        {
            List<Accountcodemapping> modellist = new List<Accountcodemapping>();

            modellist = string.Format(@"SELECT * FROM accountcodemapping").GetQuery<Accountcodemapping>();

            return View(modellist);
        }


        [Route("{controller}/accountcodeadd")]
        [HttpPost]
        public Result<string> accountcodeadd([FromBody]accountcodeaddModel request)
        {
            Result<string> result = new Result<string>();

            try
            {
                SqlCommand cmd = new GenericDataAccess().CreateCommand();
                cmd.Parameters.Clear();

                cmd.CommandText = "INSERT INTO accountcodemapping(title,accountcode) OUTPUT INSERTED.id VALUES(@title,@accountcode)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@title", request.title));
                cmd.Parameters.Add(new SqlParameter("@accountcode", string.Join(",", request.codes)));

             int   recid = (int)new GenericDataAccess().ExecuteScalar(cmd);
                if (recid > 0)
                {
                    result.status = true;
                }


            }
            catch (Exception Ex)
            {
                result.message = Ex.Message;
                result.status = false;
            }

            return result;

        }



   
    
        public IActionResult accountcodeedit(int id)
        {
            
                return View(string.Format(@"SELECT * FROM accountcodemapping where id={0}", id).GetQuery<Accountcodemapping>()[0]);

            
        } 


        public IActionResult accountcodedelete(int id)
        {
            try
            {
                SqlCommand cmd = new GenericDataAccess().CreateCommand();
                cmd.Parameters.Clear();

                cmd.CommandText = "DELETE FROM accountcodemapping WHERE id=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@id",id));

                int recid = (int)new GenericDataAccess().ExecuteNonQuery(cmd);
                if (recid > 0)
                {
                   
                }


            }
            catch (Exception Ex)
            {
                
            }


            return View("accountcodelist", string.Format(@"SELECT * FROM accountcodemapping").GetQuery<Accountcodemapping>());

            
        }

        [Route("{controller}/accountcodeedit")]
        [HttpPost]
        public Result<string> accountcodeedit([FromBody]accountcodeaddModel request)
        {
            Result<string> result = new Result<string>();

            try
            {
                SqlCommand cmd = new GenericDataAccess().CreateCommand();
                cmd.Parameters.Clear();

                cmd.CommandText = "UPDATE  accountcodemapping Set title=@title,accountcode=@accountcode Where id=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@title", request.title));
                cmd.Parameters.Add(new SqlParameter("@accountcode", string.Join(",", request.codes)));
                cmd.Parameters.Add(new SqlParameter("@id", request.id));

                int recid  = (int)new GenericDataAccess().ExecuteNonQuery(cmd);
                if (recid > 0)
                {
                    result.status = true;
                }


            }
            catch (Exception Ex)
            {
                result.message = Ex.Message;
                result.status = false;
            }

            return result;

        }

    }
}