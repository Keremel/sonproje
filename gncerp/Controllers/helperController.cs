using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ExcelDataReader;
using gncerp.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace gncerp.Controllers
{
    public class helperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [Route("{controller}/exceladdquestion")]
        [HttpPost]
        public async Task<IActionResult> exceladdquestion(IFormFile file)
        {
            Result<string> resultobj = new Result<string>();

            string dirpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/" + "Excelsoruformat");
            if (!Directory.Exists(dirpath))
            {
                DirectoryInfo dir = Directory.CreateDirectory(dirpath);
            }

            string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            filename = EnsureCorrectFilename(filename);

            using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(dirpath + "/" + filename)))
            {
                await file.CopyToAsync(output);
            }

            Stream output1 = System.IO.File.Open(this.GetPathAndFilename(dirpath + "/" + filename), FileMode.Open, FileAccess.Read);
            var excelContent = ParseExcel(output1);
            dynamic array = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(excelContent));


            try
            {
                string query = "";
                int i = 0;


                foreach (var item in array)
                {

                   
                }

                SqlCommand cmd = new GenericDataAccess().CreateCommand();
                cmd = new GenericDataAccess().CreateCommand();
                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO exam_questions(companyid,difficulty,categoryid,supcategoryid,questiontype,question,a,b,c,d,e,answer,information)" + query;
                int returnId = new GenericDataAccess().ExecuteNonQuery(cmd);





                resultobj.status = true;
                resultobj.message = "";

            }
            catch (Exception e)
            {
                resultobj.status = false;
                resultobj.message = e.Message;


            }

            return Ok(resultobj);

        }
        private string GetPathAndFilename(string filename)
        {
            return Path.Combine(GetPathZip, filename);
        }

        [HttpGet]
        [Route("{controller}/data_from_excel_file")]
        public dynamic data_from_excel_file()
        {
            //string filePath ="RaporDetayı.xlsx".GetPathAndFilename();
            string dirpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/" + "RaporDetayı.xlsx");

            Stream output1 = System.IO.File.Open(dirpath, FileMode.Open, FileAccess.Read);
            var excelContent = ParseExcel(output1);
            string json = JsonConvert.SerializeObject(excelContent);


            return JsonConvert.SerializeObject(excelContent);
        }

        public static IEnumerable<Dictionary<string, object>> ParseExcel(Stream document)
        {
            using (var reader = ExcelDataReader.ExcelReaderFactory.CreateReader(document))
            {
                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    UseColumnDataType = true,
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true,
                    }
                });
                return MapDatasetData(result.Tables.Cast<DataTable>().First());
            }
        }

        public static IEnumerable<Dictionary<string, object>> MapDatasetData(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                var row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                yield return row;
            }
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }
        private string GetPathZip
        {
            get
            {
                return Path.Combine(
                            Directory.GetCurrentDirectory(), filePath + @"TEMP");
            }
        }
        private string filePath
        {
            get
            {
                return "wwwroot/uploads/";
            }
        }

    }
}