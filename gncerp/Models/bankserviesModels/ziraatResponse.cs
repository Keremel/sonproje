using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Models.bankserviesModels
{
    public class ziraatResponse
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2003/05/soap-envelope", IsNullable = false)]
        public partial class Envelope
        {

            private EnvelopeBody bodyField;

            /// <remarks/>
            public EnvelopeBody Body
            {
                get
                {
                    return this.bodyField;
                }
                set
                {
                    this.bodyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public partial class EnvelopeBody
        {

            private HesapEkstreResponse hesapEkstreResponseField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://tempuri.org/")]
            public HesapEkstreResponse HesapEkstreResponse
            {
                get
                {
                    return this.hesapEkstreResponseField;
                }
                set
                {
                    this.hesapEkstreResponseField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/", IsNullable = false)]
        public partial class HesapEkstreResponse
        {

            private HesapEkstreResponseHesapEkstreResult hesapEkstreResultField;

            /// <remarks/>
            public HesapEkstreResponseHesapEkstreResult HesapEkstreResult
            {
                get
                {
                    return this.hesapEkstreResultField;
                }
                set
                {
                    this.hesapEkstreResultField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/")]
        public partial class HesapEkstreResponseHesapEkstreResult
        {

            private byte bankaKoduField;

            private string bankaAdiField;

            private string bankaVergiDairesiField;

            private ulong bankaVergiNumarasiField;

            private byte hataKoduField;

            private string hataAciklamasiField;

            private Bakiye bakiyeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public byte BankaKodu
            {
                get
                {
                    return this.bankaKoduField;
                }
                set
                {
                    this.bankaKoduField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public string BankaAdi
            {
                get
                {
                    return this.bankaAdiField;
                }
                set
                {
                    this.bankaAdiField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public string BankaVergiDairesi
            {
                get
                {
                    return this.bankaVergiDairesiField;
                }
                set
                {
                    this.bankaVergiDairesiField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public ulong BankaVergiNumarasi
            {
                get
                {
                    return this.bankaVergiNumarasiField;
                }
                set
                {
                    this.bankaVergiNumarasiField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public byte hataKodu
            {
                get
                {
                    return this.hataKoduField;
                }
                set
                {
                    this.hataKoduField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public string hataAciklamasi
            {
                get
                {
                    return this.hataAciklamasiField;
                }
                set
                {
                    this.hataAciklamasiField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public Bakiye Bakiye
            {
                get
                {
                    return this.bakiyeField;
                }
                set
                {
                    this.bakiyeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class Bakiye
        {

            private ushort subeKoduField;

            private string subeAdiField;

            private string hesapNoField;

            private string dovizTipiField;

            private decimal acilisBakiyeField;

            private decimal cariBakiyeField;

            private BakiyeHareketlerDetay[] detayField;

            /// <remarks/>
            public ushort subeKodu
            {
                get
                {
                    return this.subeKoduField;
                }
                set
                {
                    this.subeKoduField = value;
                }
            }

            /// <remarks/>
            public string SubeAdi
            {
                get
                {
                    return this.subeAdiField;
                }
                set
                {
                    this.subeAdiField = value;
                }
            }

            /// <remarks/>
            public string HesapNo
            {
                get
                {
                    return this.hesapNoField;
                }
                set
                {
                    this.hesapNoField = value;
                }
            }

            /// <remarks/>
            public string DovizTipi
            {
                get
                {
                    return this.dovizTipiField;
                }
                set
                {
                    this.dovizTipiField = value;
                }
            }

            /// <remarks/>
            public decimal AcilisBakiye
            {
                get
                {
                    return this.acilisBakiyeField;
                }
                set
                {
                    this.acilisBakiyeField = value;
                }
            }

            /// <remarks/>
            public decimal CariBakiye
            {
                get
                {
                    return this.cariBakiyeField;
                }
                set
                {
                    this.cariBakiyeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("HareketlerDetay", IsNullable = false)]
            public BakiyeHareketlerDetay[] Detay
            {
                get
                {
                    return this.detayField;
                }
                set
                {
                    this.detayField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class BakiyeHareketlerDetay
        {

            private uint islemTarihiField;

            private uint valorField;

            private byte referansField;

            private string aciklamaField;

            private decimal tutarField;

            private string borcAlacakField;

            private string islemTipiField;

            private string timeStampField;

            private string musteriVergiNumarasiField;

            private decimal bakiyeField;

            /// <remarks/>
            public uint IslemTarihi
            {
                get
                {
                    return this.islemTarihiField;
                }
                set
                {
                    this.islemTarihiField = value;
                }
            }

            /// <remarks/>
            public uint Valor
            {
                get
                {
                    return this.valorField;
                }
                set
                {
                    this.valorField = value;
                }
            }

            /// <remarks/>
            public byte Referans
            {
                get
                {
                    return this.referansField;
                }
                set
                {
                    this.referansField = value;
                }
            }

            /// <remarks/>
            public string Aciklama
            {
                get
                {
                    return this.aciklamaField;
                }
                set
                {
                    this.aciklamaField = value;
                }
            }

            /// <remarks/>
            public decimal Tutar
            {
                get
                {
                    return this.tutarField;
                }
                set
                {
                    this.tutarField = value;
                }
            }

            /// <remarks/>
            public string BorcAlacak
            {
                get
                {
                    return this.borcAlacakField;
                }
                set
                {
                    this.borcAlacakField = value;
                }
            }

            /// <remarks/>
            public string IslemTipi
            {
                get
                {
                    return this.islemTipiField;
                }
                set
                {
                    this.islemTipiField = value;
                }
            }

            /// <remarks/>
            public string TimeStamp
            {
                get
                {
                    return this.timeStampField;
                }
                set
                {
                    this.timeStampField = value;
                }
            }

            /// <remarks/>
            public string musteriVergiNumarasi
            {
                get
                {
                    return this.musteriVergiNumarasiField;
                }
                set
                {
                    this.musteriVergiNumarasiField = value;
                }
            }

            /// <remarks/>
            public decimal Bakiye
            {
                get
                {
                    return this.bakiyeField;
                }
                set
                {
                    this.bakiyeField = value;
                }
            }
        }


    }
}
