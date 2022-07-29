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
    public class financeController : Controller
    {
        public IActionResult babsraporu(string bastarih, string bistarih)
        {
            if (bastarih.isNull() || bistarih.isNull())
            {
                var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                bastarih = firstDayOfMonth.ToString("yyyy-MM-dd");
                bistarih = DateTime.Now.ToString("yyyy-MM-dd");
            }

            dynamic model = string.Format(@"SELECT ea.LOGICALREF, ea.INVOICEREF, ea.TCKNO, ea.NAME, ea.SURNAME,ea.DEFINITION_, ea.CITYCODE,ea.ADDR1, i.GRPCODE, i.FICHENO, i.DATE_, i.TIME_, i.GROSSTOTAL, i.SPECODE, i.CLIENTREF,i.CANCELLED
FROM LG_017_01_EARCHIVEDET ea INNER JOIN LG_017_01_INVOICE i ON ea.INVOICEREF = i.LOGICALREF
WHERE (((i.DATE_) Between '{0}' And '{1}') AND ((i.CLIENTREF)=24698) AND ((i.CANCELLED)=0));", bastarih, bistarih).GetDynamicQuery("SCSlogo");
            return View(model);
        }
           public IActionResult payplanlist()
        {
            return View();
        }

        public IActionResult invoicenoaccountcodelist(string bastarih, string bistarih)
        {
            List<Invoicenoaccountcode> model = string.Format(@"SELECT ACCOUNTCODE,INVOICENO from LG_017_01_EMFLINE where INVOICENO!='' AND DATE_ 
       BETWEEN '{0}' AND '{1}' Group by INVOICENO,ACCOUNTCODE", bastarih, bistarih).GetQuery<Invoicenoaccountcode>("SCSlogo");


            return View(model);
        }
        
        public IActionResult payplandetail()
        {
            return View();
        }

        [Route("{controller}/addPayplan")]
        [HttpPost]
        public Result<string> addPayplan([FromBody]Payplan obj)
        {
            Result<string> result = new Result<string>();

            try
            {

                SqlCommand cmd = new GenericDataAccess().CreateCommand();

                cmd.Parameters.Clear();
                cmd.CommandText = @"INSERT INTO Payplan(institution,paymentmodeid,startdate,enddate,day,bankrate,creditmainmoney,creditinterest,creditbsmv,continuationrate,continuationmainmoney,continuationbsmv,continuationtotal,generaltotal) 
                 OUTPUT INSERTED.id VALUES(@institution,@paymentmodeid,@startdate,@enddate,@day,@bankrate,@creditmainmoney,@creditinterest,@creditbsmv,@continuationrate,@continuationmainmoney,@continuationbsmv,@continuationtotal,@generaltotal)";
                cmd.Parameters.AddWithValue("@institution", obj.institution);
                cmd.Parameters.AddWithValue("@paymentmodeid", obj.paymentmodeid);
                cmd.Parameters.AddWithValue("@startdate", obj.startdate);
                cmd.Parameters.AddWithValue("@enddate", obj.enddate);

                cmd.Parameters.AddWithValue("@day", obj.day);
                cmd.Parameters.AddWithValue("@bankrate", obj.bankrate);
                cmd.Parameters.AddWithValue("@creditmainmoney", obj.creditmainmoney);
                cmd.Parameters.AddWithValue("@creditinterest", obj.creditinterest);
                cmd.Parameters.AddWithValue("@creditbsmv", obj.creditbsmv);
                cmd.Parameters.AddWithValue("@continuationrate", obj.continuationrate);
                cmd.Parameters.AddWithValue("@continuationmainmoney", obj.continuationmainmoney);
                cmd.Parameters.AddWithValue("@continuationbsmv", obj.continuationbsmv);
                cmd.Parameters.AddWithValue("@continuationtotal", obj.continuationtotal);
                cmd.Parameters.AddWithValue("@generaltotal", obj.generaltotal);

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



        [Route("{controller}/getPayplanbyid")]
        [HttpPost]
        public dynamic getPayplanbyid([FromBody]Payplan obj)
        {
            return string.Format("SELECT * From Payplan WHERE id={0}", obj.id).GetDynamicQuery();
        }

        [Route("{controller}/updateUser")]
        [HttpPost]
        public Result<string> updateUser([FromBody] Payplan obj)
        {
            Result<string> result = new Result<string>();

            try
            {
                SqlCommand cmd = new GenericDataAccess().CreateCommand();
                cmd.Parameters.Clear();
                cmd.CommandText = @"UPDATE Payplan Set institution=@institution , paymentmodeid=@paymentmodeid , startdate=@startdate , enddate=@enddate , day=@day , bankrate=@bankrate ,
                 creditmainmoney=@creditmainmoney , creditinterest=@creditinterest,creditbsmv=@creditbsmv,continuationrate=@continuationrate,
             continuationmainmoney=@continuationmainmoney,continuationbsmv=@continuationbsmv,continuationtotal=@continuationtotal,generaltotal=@generaltotal WHERE id=@id";
                cmd.Parameters.AddWithValue("@institution", obj.institution);
                cmd.Parameters.AddWithValue("@paymentmodeid", obj.paymentmodeid);
                cmd.Parameters.AddWithValue("@startdate", obj.startdate);
                cmd.Parameters.AddWithValue("@enddate", obj.enddate);

                cmd.Parameters.AddWithValue("@day", obj.day);
                cmd.Parameters.AddWithValue("@bankrate", obj.bankrate);
                cmd.Parameters.AddWithValue("@creditmainmoney", obj.creditmainmoney);
                cmd.Parameters.AddWithValue("@creditinterest", obj.creditinterest);
                cmd.Parameters.AddWithValue("@creditbsmv", obj.creditbsmv);
                cmd.Parameters.AddWithValue("@continuationrate", obj.continuationrate);
                cmd.Parameters.AddWithValue("@continuationmainmoney", obj.continuationmainmoney);
                cmd.Parameters.AddWithValue("@continuationbsmv", obj.continuationbsmv);
                cmd.Parameters.AddWithValue("@continuationtotal", obj.continuationtotal);
                cmd.Parameters.AddWithValue("@generaltotal", obj.generaltotal);
                cmd.Parameters.AddWithValue("@id", obj.id);

                var returnobj = (int)new GenericDataAccess().ExecuteNonQuery(cmd);

                if (returnobj > 0)
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
        public Result<string> deleteUser([FromBody] Appuser obj)
        {
            Result<string> result = new Result<string>();

            try
            {

                SqlCommand cmd = new GenericDataAccess().CreateCommand();

                cmd.Parameters.Clear();
                cmd.CommandText = "UPDATE appusers Set isdel=@isdel WHERE id=@id";
                cmd.Parameters.AddWithValue("@isdel", 1);
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
        public IActionResult datatables([FromBody] DatatablesJS.DatatablesObject requestobj)
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
