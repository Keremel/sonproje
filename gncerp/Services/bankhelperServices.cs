using gncerp.Entities;
using gncerp.Models.bankserviesModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace gncerp.Services
{


    public class bankhelperServices
    {
        private bankServices _bankServices = new bankServices();
        public void Ziraatbank_Accountactivitiy_Control(string no,int accountnoid)
        {
            List<Accountactivity> modellist = new List<Accountactivity>();

            string xml = _bankServices.getziraataccountactivity(no);

            try
            {
                ziraatResponse.Envelope model = DeserializeFromXmlString<ziraatResponse.Envelope>(xml);

                if (model.Body.HesapEkstreResponse.HesapEkstreResult.hataKodu == 00)
                {
                    for (int i = 0; i < model.Body.HesapEkstreResponse.HesapEkstreResult.Bakiye.Detay.Length; i++)
                    {
                        try
                        {
                            Accountactivity accountactivity = new Accountactivity();

                            accountactivity.info = model.Body.HesapEkstreResponse.HesapEkstreResult.Bakiye.Detay[i].Aciklama;
                            accountactivity.amount = model.Body.HesapEkstreResponse.HesapEkstreResult.Bakiye.Detay[i].Tutar.ToString();
                            accountactivity.activititype = model.Body.HesapEkstreResponse.HesapEkstreResult.Bakiye.Detay[i].BorcAlacak;
                            accountactivity.balance = model.Body.HesapEkstreResponse.HesapEkstreResult.Bakiye.Detay[i].Bakiye.ToString();
                            accountactivity.reference = model.Body.HesapEkstreResponse.HesapEkstreResult.Bakiye.Detay[i].Referans.ToString();
                            accountactivity.taxnumber = model.Body.HesapEkstreResponse.HesapEkstreResult.Bakiye.Detay[i].musteriVergiNumarasi;
                            accountactivity.transactiondate = model.Body.HesapEkstreResponse.HesapEkstreResult.Bakiye.Detay[i].IslemTarihi.ToString();
                            accountactivity.valuedate = model.Body.HesapEkstreResponse.HesapEkstreResult.Bakiye.Detay[i].Valor.ToString();
                            accountactivity.timestamp = model.Body.HesapEkstreResponse.HesapEkstreResult.Bakiye.Detay[i].TimeStamp.ToString();
                            accountactivity.processtype = model.Body.HesapEkstreResponse.HesapEkstreResult.Bakiye.Detay[i].IslemTipi.ToString();

                            modellist.Add(accountactivity);

                        }
                        catch (Exception e)
                        {
                            //AddLog(0, 0, "salexml", "GetSales", salexml, e.Message);
                        }


                    }

                }

          
            SqlCommand cmd = new GenericDataAccess().CreateCommand();


            cmd.Parameters.Clear();
            cmd.CommandText = "UPDATE Accountnos Set acilisbakiye=@acilisbakiye , caribakiye=@caribakiye WHERE id=@id";
            cmd.Parameters.AddWithValue("@acilisbakiye", model.Body.HesapEkstreResponse.HesapEkstreResult.Bakiye.AcilisBakiye.ToString());
            cmd.Parameters.AddWithValue("@caribakiye", model.Body.HesapEkstreResponse.HesapEkstreResult.Bakiye.CariBakiye.ToString());
            cmd.Parameters.AddWithValue("@id", accountnoid);



                int renId = new GenericDataAccess().ExecuteNonQuery(cmd);

                if (modellist.Count > 0)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "DELETE Accountactivities WHERE transactiondate=@transactiondate";
                cmd.Parameters.AddWithValue("@transactiondate", modellist.First().transactiondate);

                int returnId = new GenericDataAccess().ExecuteNonQuery(cmd);

                string query = " INSERT INTO Accountactivities(info,amount,activititype,balance,taxnumber,reference,transactiondate,valuedate,timestamp,processtype,accountnoid) ";
                int i = 0;
                foreach (var item in modellist)
                {
                    if (i > 0)
                        query += " UNION ALL ";

                    query = query + string.Format("SELECT '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10}",
                        item.info, item.amount, item.activititype, item.balance, item.taxnumber, item.reference, item.transactiondate, item.valuedate,item.timestamp,item.processtype,accountnoid);

                    i++;
                }

                cmd = new GenericDataAccess().CreateCommand();
                cmd.Parameters.Clear();
                cmd.CommandText = query;

                int recid = new GenericDataAccess().ExecuteNonQuery(cmd);
            }
            }
            catch (Exception e)
            {
                //AddLog(0, 0, "buyerxml", "Getbuyer", buyerxml, e.Message);
            }

        }



        public T DeserializeFromXmlString<T>(string xml) where T : new()
        {
            T xmlObject = new T();                                   // HERE you created a NEW 'T' and assigned to variable xmlObject.
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xml))
            {
                xmlObject = (T)xmlSerializer.Deserialize(reader);
            }

            return xmlObject;
        }


    }
}
