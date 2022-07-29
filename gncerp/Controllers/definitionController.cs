using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using gncerp.Entities;
using gncerp.Models;
using gncerp.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace gncerp.Controllers
{
    public class definitionController : Controller
    {
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

        public IActionResult productprice()
        {

            dynamic model = string.Format(@" SELECT ml.Specode, erpp.name as fiyatlistname, ml.[Malzeme Grup Kodu] tip,
    ml.[LOGICALREF] id, ml.Malz_Kodu kod, ml.[Marka Kodu] marka, ml.[Model] model_kodu, ml.Malz_Adı model, 
    ROUND([MIN_Fiyat]*(1+([KDV_Oranı]/100)),0) mal_min, ROUND([MAX_Fiyat]*(1+([KDV_Oranı]/100)),0) mal_max, 
    ROUND([Son_Alış_Fiyatı]*(1+([KDV_Oranı]/100)),0) mal_son, ROUND([Ortalama_Değer]*(1+([KDV_Oranı]/100)),0) mal_ort, 
    ml.[Fiili_Stok] stok, kk.FiyatListe, kk.Guncelleme 
    FROM 
    ARY_017_MALZEME_LISTESI ml LEFT JOIN 
    ARY_XXX_STOK_MALIYET_TB tb ON ml.[LOGICALREF]=tb.MalzRef
    LEFT JOIN KK_Fiyat_Listesi kk ON kk.MalzRef=ml.[LOGICALREF]
    LEFT JOIN ERP_Product erpp ON erpp.MalzRef=ml.LOGICALREF WHERE  ml.ACTIVE
 = 0 order by ml.[Fiili_Stok] DESC").GetDynamicQuery("SCSlogo");
                           
//            (ml.[Malzeme Grup Kodu] = 'CEP TELEFONU' and ml.Fiili_Stok >= 0) 
//or(ml.[Malzeme Grup Kodu] != 'CEP TELEFONU' and )

            return View(model);

        }


        [Route("{controller}/updateRate")]
        [HttpPost]
        public Result<string> updateRate([FromBody] dynamic obj)
        {
            Result<string> result = new Result<string>();

            try
            {

                decimal rate = obj.rate;

                SqlCommand cmd = new GenericDataAccess().CreateCommand();
                cmd.Parameters.Clear();
                cmd.CommandText = "UPDATE Appdefinition Set cardrate=@cardrate WHERE id=@id";
                cmd.Parameters.AddWithValue("@cardrate", rate);
                cmd.Parameters.AddWithValue("@id",1);

                var returudata = (int)new GenericDataAccess().ExecuteNonQuery(cmd);

                if (returudata > 0)
                {

                    result.status = true;
                    return result;
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

        [Route("{controller}/updateProductname")]
        [HttpPost]
        public Result<string> updateProductname([FromBody] updateProductpriceModel obj)
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
                cmd.CommandText = "UPDATE ERP_Product Set name=@name WHERE MalzRef=@MalzRef";
                cmd.Parameters.AddWithValue("@MalzRef", obj.MalzRef);
                cmd.Parameters.AddWithValue("@name", obj.fiyatlistname);

                var returudata = (int)new GenericDataAccess().ExecuteNonQuery(cmd);

                if (returudata == 0)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO ERP_Product(MalzRef,name) OUTPUT INSERTED.id VALUES(@MalzRef,@name)";
                    cmd.Parameters.AddWithValue("@MalzRef", obj.MalzRef);
                    cmd.Parameters.AddWithValue("@name", obj.fiyatlistname);

                    var rc = (int)new GenericDataAccess().ExecuteNonQuery(cmd);

                    if (rc > 0)
                    {

                        result.status = true;
                        return result;
                    }


                }
             

                result.status = true;


                return result;



            }
            catch (Exception ex)
            {
                result.status = false;
                result.message = ex.ToString();
                return result;
            }

        }

        [Route("{controller}/updateProductprice")]
        [HttpPost]
        public Result<string> updateProductprice([FromBody]updateProductpriceModel obj)
        {
            Result<string> result = new Result<string>();
            int returudata = 1;
            try
            { if (obj.MalzRef <= 0)
                {
                    result.status = false;
                    result.message = "MalzRef 0 Olamaz";
                    return result;
                }
               
                dynamic mo = string.Format("SELECT * FROM KK_Fiyat_Listesi WHERE MalzRef={0}", obj.MalzRef).GetDynamicQuery("SCSlogo");
                SqlCommand cmd = new GenericDataAccess().CreateCommand("SCSlogo");
                if (mo.Count > 0)
                {
                   
                    cmd.Parameters.Clear();
                    cmd.CommandText = "DELETE KK_Fiyat_Listesi WHERE MalzRef=@MalzRef";
                    cmd.Parameters.AddWithValue("@MalzRef", obj.MalzRef);
                 
                     returudata = (int)new GenericDataAccess().ExecuteNonQuery(cmd);

                }




                if (returudata > 0)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO KK_Fiyat_Listesi(MalzRef,Guncelleme,FiyatListe,del) OUTPUT INSERTED.id VALUES(@MalzRef,@Guncelleme,@FiyatListe,@del)";
                    cmd.Parameters.AddWithValue("@MalzRef", obj.MalzRef);
                    cmd.Parameters.AddWithValue("@FiyatListe", obj.price);
                    cmd.Parameters.AddWithValue("@Guncelleme", DateTime.Now.ToString(Formats.dateFormatsql));
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


        #region BankIban



        private string GetPathAndFilename(string filename)
        {
            return Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + @"TEMP"), filename);
        }
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        [Route("{controller}/imageupload")]
        [HttpPost]
        public async Task<IActionResult> educationimageupload(IFormFile imgfile, int id)
        {
            Result<string> resultobj = new Result<string>();


            string dirpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/bank/" + id.ToString());
            if (!Directory.Exists(dirpath))
            {
                DirectoryInfo dir = Directory.CreateDirectory(dirpath);
            }

            string filename = ContentDispositionHeaderValue.Parse(imgfile.ContentDisposition).FileName.Trim('"');

            filename = EnsureCorrectFilename(filename);

            if (new string[] { ".jpg", ".jpeg", ".png", ".gif" }.Contains(Path.GetExtension(imgfile.FileName)))
            {
                using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(dirpath + "/" + filename)))
                {
                    await imgfile.CopyToAsync(output);
                    resultobj.status = true;
                    //createEducationDirectory(source.FileName);

                }
            }


            return Ok(new
            {
                resultobj
            });
        }

        public IActionResult bankIbanlist()
        {
            return View();
        }


        [Route("{controller}/addBankIban")]
        [HttpPost]
        public Result<string> addBankIban([FromBody]BankIban obj)
        {
            Result<string> result = new Result<string>();

            try
            {

                SqlCommand cmd = new GenericDataAccess().CreateCommand();

                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO BankIbans(bankname,subecodu,iban,accountno) OUTPUT INSERTED.id VALUES(@bankname,@subecodu,@iban,@accountno)";
                cmd.Parameters.AddWithValue("@bankname", obj.bankname);
                cmd.Parameters.AddWithValue("@subecodu", obj.subecodu);
                cmd.Parameters.AddWithValue("@iban", obj.iban);
                cmd.Parameters.AddWithValue("@accountno", obj.accountno);

                int returnId = new GenericDataAccess().ExecuteNonQuery(cmd);

                if (returnId > 0)
                {
                    var jsonObj = new { returnId = returnId };

                    result.status = true;
                    result.jsonObj = JsonConvert.SerializeObject(jsonObj);
                    return result;
                }

                result.status = false;
                result.jsonObj = JsonConvert.SerializeObject(returnId);
                return result;
            }
            catch (Exception ex)
            {
                result.status = false;
                result.message = ex.ToString();
                return result;
            }



           
        }


        [Route("{controller}/getBankIban")]
        [HttpPost]
        public dynamic getBankIban([FromBody]BankIban obj)
        {
            return string.Format("SELECT * From BankIbans WHERE id={0}", obj.id).GetDynamicQuery();
        }

        [Route("{controller}/updateBankIban")]
        [HttpPost]
        public Result<string> updateBankIban([FromBody]BankIban obj)
        {
            Result<string> result = new Result<string>();

            try
            {
                SqlCommand cmd = new GenericDataAccess().CreateCommand();
                cmd.Parameters.Clear();
                cmd.CommandText = "UPDATE BankIbans Set bankname=@bankname ,iban=@iban,subecodu=@subecodu,accountno=@accountno WHERE id=@id";
                cmd.Parameters.AddWithValue("@bankname", obj.bankname);
                cmd.Parameters.AddWithValue("@iban", obj.iban);
                cmd.Parameters.AddWithValue("@subecodu", obj.subecodu);
                cmd.Parameters.AddWithValue("@accountno", obj.accountno);
                cmd.Parameters.AddWithValue("@id", obj.id);

                var returnobj = (int)new GenericDataAccess().ExecuteNonQuery(cmd);

                if (returnobj > 0)
                {
                    var jsonObj = new { returnId = returnobj };
                    result.status = true;
                    result.jsonObj = JsonConvert.SerializeObject(jsonObj);
                    return result;
                }


                result.status = false;
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


        [Route("{controller}/deleteBankIban")]
        [HttpPost]
        public Result<string> deleteBankIban([FromBody]BankIban obj)
        {
            Result<string> result = new Result<string>();

            try
            {

                SqlCommand cmd = new GenericDataAccess().CreateCommand();

                cmd.Parameters.Clear();
                cmd.CommandText = "DELETE BankIbans WHERE id=@id";
                cmd.Parameters.AddWithValue("@id", obj.id);

                int returnId = new GenericDataAccess().ExecuteNonQuery(cmd);

                var jsonObj = new { returnId = returnId };
                if (returnId > 0)
                {
                    result.status = true;
                    result.jsonObj = JsonConvert.SerializeObject(jsonObj);
                    return result;
                }
            

                result.status = false;
                result.jsonObj = JsonConvert.SerializeObject(jsonObj);
                return result;

            }
            catch (Exception ex)
            {
                result.status = false;
                result.message = ex.ToString();
                return result;
            }



            return result;
        }
        #endregion

        #region salestargets
        public IActionResult salestargetslist()
        {
            return View();
        }

        [Route("{controller}/addSalestargets")]
        [HttpPost]
        public Result<string> addSalestargets([FromBody]Salestarget obj)
        {
            Result<string> result = new Result<string>();

            try
            {

                SqlCommand cmd = new GenericDataAccess().CreateCommand();

                cmd.Parameters.Clear();
                cmd.CommandText = " INSERT INTO salestargets(year,month,target,appuserid) OUTPUT INSERTED.id VALUES(@year,@month,@target,@appuserid) ";
                cmd.Parameters.AddWithValue("@year", obj.year);
                cmd.Parameters.AddWithValue("@month", obj.month);
                cmd.Parameters.AddWithValue("@target",obj.target);
                cmd.Parameters.AddWithValue("@appuserid", obj.appuserid);

                int returnId = new GenericDataAccess().ExecuteNonQuery(cmd);

                if (returnId > 0)
                {
                    var jsonObj = new { returnId = returnId };

                    result.status = true;
                    result.jsonObj = JsonConvert.SerializeObject(jsonObj);
                    return result;
                }

                result.status = false;
                result.jsonObj = JsonConvert.SerializeObject(returnId);
                return result;
            }
            catch (Exception ex)
            {
                result.status = false;
                result.message = ex.ToString();
                return result;
            }




        }


        [Route("{controller}/deleteSalestargets")]
        [HttpPost]
        public Result<string> deleteSalestargets([FromBody]BankIban obj)
        {
            Result<string> result = new Result<string>();

            try
            {

                SqlCommand cmd = new GenericDataAccess().CreateCommand();

                cmd.Parameters.Clear();
                cmd.CommandText = "DELETE salestargets WHERE id=@id";
                cmd.Parameters.AddWithValue("@id", obj.id);

                int returnId = new GenericDataAccess().ExecuteNonQuery(cmd);

                var jsonObj = new { returnId = returnId };
                if (returnId > 0)
                {
                    result.status = true;
                    result.jsonObj = JsonConvert.SerializeObject(jsonObj);
                    return result;
                }

                result.status = false;
                result.jsonObj = JsonConvert.SerializeObject(jsonObj);
                return result;

            }
            catch (Exception ex)
            {
                result.status = false;
                result.message = ex.ToString();
                return result;
            }


        }
        #endregion

    }
}