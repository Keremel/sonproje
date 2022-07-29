using gncerp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Models
{
    public class KarzararraporuModel
    {
        public List<TGelir> tGelir { get; set; }    
        public List<TKar> tKar { get; set; }
        public List<TMaliyet> tMaliyet { get; set; }
        public List<TGelirgider> teletFiyatfarki { get; set; }  
        public List<GelirgiderraporuModel> gelirgiderraporuModel { get; set; }    
        public List<GelirgiderraporuModel> personel_giderler { get; set; } 
        public List<GelirgiderraporuModel> buro_sabit_giderler { get; set; } 
        public List<GelirgiderraporuModel> genel_idare_giderleri { get; set; }
          public List<GelirgiderraporuModel> satis_dagitim_giderleri { get; set; }
        public List<GelirgiderraporuModel> bagis_ve_yardimlar { get; set; }
        public List<GelirgiderraporuModel> amortisman_giderleri { get; set; }  
        
        public List<GelirgiderraporuModel> toplam_giderler { get; set; } 
        
        
        public List<GelirgiderraporuModel> pos_kom_gelirleri { get; set; }
        public List<GelirgiderraporuModel> pos_kom_giderleri { get; set; }    
        public List<GelirgiderraporuModel> faiz_kar_payi_giderleri { get; set; }
        public List<GelirgiderraporuModel> teminat_mektubu_masraf_yansitma { get; set; }  

        public List<GelirgiderraporuModel> kira_gelirleri { get; set; }  
        public List<GelirgiderraporuModel> fiyat_farki_gelirleri { get; set; }  
        public List<GelirgiderraporuModel> fiyat_farki_giderleri { get; set; }
        public List<Toplamay> diger_gelirler_toplam { get; set; }

        public List<Toplamay> finasmantoplam { get; set; }
        public List<Toplamay> brut_kar { get; set; }

        public List<Toplamay> big_toplam_gider { get; set; }  
        
        public List<Toplamay> aylik_kar_zarar { get; set; }
        public List<Toplamay> kar_zarar { get; set; } 

        public List<Toplamay> vergi_karsiligi { get; set; }

        public List<Toplamay> vs_aylik_kar_zarar { get; set; }  
        public List<Toplamay> kumulatif_aylik_kar_zarar { get; set; }

        public List<Toplamay> toplam_brut_kar { get; set; }





    }
}
