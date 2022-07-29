using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gncerp.Entities;
using Microsoft.AspNetCore.Mvc;
using gncerp.Models;
using System.Data.SqlClient;
using gncerp.Utility;

namespace gncerp.Controllers
{
    public class satisController : Controller
    {
        public IActionResult siparislistesi()
        {
            return View();
        }
     

        public IActionResult siparislistesisatis()
        {
            return View();
        }
        public IActionResult cariekstresi()
        {
            dynamic model = string.Format(@"SELECT [Cari Kodu] kodu
      ,[Cari Ünvanı] unvan
  FROM ARY_017_CARI_MUSTER_TEMSILCI ").GetDynamicQuery("SCSlogo");
            return View(model);
        } 

        public IActionResult cariekstresisatis()
        {
            dynamic model = string.Format(@"SELECT [Cari Kodu] kodu
      ,[Cari Ünvanı] unvan
  FROM ARY_017_CARI_MUSTER_TEMSILCI c where c.[Satış Temsilcisi]='{0}'", CurrentSession.Username).GetDynamicQuery("SCSlogo");
            return View(model);
        }

        public IActionResult fiyatlistesi(int? id)
        {

            fiyatlistesiModel fiyatlistesiModel = new fiyatlistesiModel();


            fiyatlistesiModel.fiyatlistesis= string.Format(@"SELECT  erpp.name as fiyatlistname, ml.Malz_Kodu malzemekodu, ml.Malz_Adı malzemeadi, ml.[Malzeme Grup Kodu] malzemedrupkodu, ml.[Marka Kodu] markakodu, ml.Model model,
                    kk.FiyatListe fiyatlist, ml.Fiili_Stok stok FROM 
                  ARY_017_MALZEME_LISTESI ml LEFT JOIN 
                  ARY_XXX_STOK_MALIYET_TB tb ON ml.[LOGICALREF]=tb.MalzRef
                  LEFT JOIN KK_Fiyat_Listesi kk ON kk.MalzRef=ml.[LOGICALREF]
                  LEFT JOIN ERP_Product erpp ON erpp.MalzRef=ml.LOGICALREF WHERE ml.[Malzeme Grup Kodu] IN('CEP TELEFONU','TABLET','AKSESUAR') AND kk.FiyatListe>0 AND ml.ACTIVE=0  order by  ml.Malz_Kodu DESC").GetQuery<Fiyatlistesi>("SCSlogo");
                


            foreach (var item in fiyatlistesiModel.fiyatlistesis)
            {
                if (!item.fiyatlistname.isNull())
                {
                    item.firstchart = item.fiyatlistname.Trim().Substring(0, 1);
                }
                else {
                    item.firstchart = item.malzemeadi.Trim().Substring(0, 1);
                }
              
            }
     
            fiyatlistesiModel.bankIbans = string.Format("SELECT * FROM BankIbans").GetQuery<BankIban>();

            return View(fiyatlistesiModel);
        }


        [Route("{controller}/updateProductprice")]
        [HttpPost]
        public Result<string> updateProductprice([FromBody] updateProductpriceModel obj)
        {
            Result<string> result = new Result<string>();

            try
            {
                if (obj.MalzRef <= 0)
                {
                    result.status = false;
                    result.message = "MalzRef 0 Olamaz";
                    return result;
                }


                SqlCommand cmd = new GenericDataAccess().CreateCommand("SCSlogo");
                cmd.Parameters.Clear();
                cmd.CommandText = "UPDATE KK_Fiyat_Listesi Set del=@del WHERE MalzRef=@MalzRef AND del=0";
                cmd.Parameters.AddWithValue("@MalzRef", obj.MalzRef);
                cmd.Parameters.AddWithValue("@del", 1);

                var returudata = (int)new GenericDataAccess().ExecuteNonQuery(cmd);

                if (returudata > 0)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO KK_Fiyat_Listesi(MalzRef,FiyatListe,del) OUTPUT INSERTED.id VALUES(@MalzRef,@FiyatListe,@del)";
                    cmd.Parameters.AddWithValue("@MalzRef", obj.MalzRef);
                    cmd.Parameters.AddWithValue("@FiyatListe", obj.price);
                    cmd.Parameters.AddWithValue("@del", 0);

                    var rc = (int)new GenericDataAccess().ExecuteNonQuery(cmd);

                    if (rc > 0)
                    {

                        result.status = true;
                        return result;
                    }


                }


                result.status = false;
                return result;



            }
            catch (Exception ex)
            {
                result.status = false;
                result.message = ex.ToString();
                return result;
            }

        }


        [Route("{controller}/datatables")]
        [HttpPost]
        public IActionResult datatables([FromBody]DatatablesJS.DatatablesObject requestobj)
        {
            requestobj.dbtype = "SCSlogo";
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