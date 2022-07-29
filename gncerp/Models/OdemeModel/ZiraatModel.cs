using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Models.OdemeModel
{
    public class ZiraatModel
    {


        public string clientId { get; set; } //Banka tarafından verilen işyeri numarası
        public string amount { get; set; } //İşlem tutarı
        public string oid { get; set; }   //Sipariş Numarası
        public string failUrl { get; set; } //İşlem başarısızsa dönülecek sayfa
        public string okUrl { get; set; }//İşlem başarılıysa dönülecek sayfa
        public string rnd { get; set; } //Kontrol ve güvenlik amaçlı sürekli değişen bir değer tarih gibi

        public string hash { get; set; } //Güvenlik amaçlı hash değeri
        public string storetype { get; set; } //İş yeri anahtarı
        public string lang { get; set; }
        public string islemtipi { get; set; } //İşlem tipi

        public string taksit { get; set; } //taksit
        public string storekey { get; set; } //taksit


        public string pan { get; set; } //Kredi Kart Numarası
        public string cv2 { get; set; } //Güvenlik Kodu
        public string cardType { get; set; } //Güvenlik Kodu
        public string Ecom_Payment_Card_ExpDate_Year { get; set; } //Son Kullanma Yılı
        public string Ecom_Payment_Card_ExpDate_Month { get; set; } //Son Kullanma Ayı



    }
}
