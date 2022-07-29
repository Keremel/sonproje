using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

//public interface IFormFile
//{
//    string ContentType { get; }
//    string ContentDisposition { get; }
//    IHeaderDictionary Headers { get; }
//    long Length { get; }
//    string Name { get; }
//    string FileName { get; }
//    Stream OpenReadStream();
//    void CopyTo(Stream target);
//    Task CopyToAsync(Stream target, CancellationToken cancellationToken);
//}

public class Entities
{

    public class Admin
    {
        public class AdminUsers
        {
            public int id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string fullname { get; set; }
        }

        public class EducationAssignRequest
        {


            public int id { get; set; }

            public int before_x_day { get; set; }
            public int before_x_day_time { get; set; }
            public int on_assigned { get; set; }
            public int before_x_day_one_time { get; set; }
            public string[] itemarray { get; set; }
            public string[] itemarray2 { get; set; }

            public string[] bayiler { get; set; }
            public string[] gorevler { get; set; }
        }
        public class ExamAssignRequest
        {
            public int id { get; set; }
            public int before_x_day { get; set; }
            public int before_x_day_time { get; set; }
            public int on_assigned { get; set; }
            public int before_x_day_one_time { get; set; }
            public string[] itemarray { get; set; }
            public string[] itemarray2 { get; set; }

            public string[] bayiler { get; set; }
            public string[] gorevler { get; set; }



        }
    }

    public class AjaxRequestObject
    {
        public List<TableObject> table { get; set; }
        public int id { get; set; }
        public string tablename { get; set; }
        public List<Entities.DefaultColumns> defaults { get; set; }
    }

    public class MailRequest
    {
        public string name { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string text { get; set; }
    }

    public class TableObject
    {
        public string columnname { get; set; }
        public string columnvalue { get; set; }
        public string columntype { get; set; }
        public string columnmaxlength { get; set; }
        public string columnclasses { get; set; }
        public string columndisplayname { get; set; }
        public string columncheck { get; set; }
        public string columnstatus { get; set; }
        public string columnpercent { get; set; }
        public string columncontrolgroup { get; set; }
    }

    public class CatalogFilters
    {
        public bool joined { get; set; }
        public bool optional { get; set; }
        public bool mandatory { get; set; }
    }

    public class AutoComplete
    {
        public class Request
        {
            public string q { get; set; }
            public string queryname { get; set; }
        }
    }

    public class FileRequest
    {
        public int[] ids { get; set; }
        public string searchtext { get; set; }
        public int maxCharacter { get; set; }
    }
    public class DefaultColumns
    {
        public string source { get; set; }
        public string target { get; set; }
        public bool controlgroup { get; set; }
    }

    public class UpdateModel
    {
        public object dataobj { get; set; }
        public string[] columnlist { get; set; }
        public string[] identitycolumns { get; set; }
    }

    public class Language
    {
        public int id { get; set; }
        public string languagename { get; set; }
        public string languagecode { get; set; }
        public string direction { get; set; }
    }

    public class LanguageTranslate
    {
        public int id { get; set; }
        public string keyword { get; set; }
        public string translate { get; set; }
        public string languagecode { get; set; }
    }

    public class Log
    {
        public int id { get; set; }
        public int recordid { get; set; }
        public string processname { get; set; }
        public string processtype { get; set; }
        public string information { get; set; }
        public DateTime recorddate { get; set; }
    }

    public class Member
    {
        public int id { get; set; }
        public string accountid { get; set; }
        public int companyid { get; set; }
        public int titleid { get; set; }
        public int dealerid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string username { get; set; }
        public string role { get; set; }
        public string password { get; set; }
        public int status { get; set; }
        public int membertype { get; set; }
        public int? supervisorid { get; set; }
        public string supervisorname { get; set; }
        public DateTime? lastlogindate { get; set; }
        public DateTime recorddate { get; set; }

        //public Guid userid { get; set; }

        public Member()
        {
            id = 0;
        }
    }

    public class EducationProcess
    {
        public int id { get; set; }
        public int assignid { get; set; }
        public int educationid { get; set; }
        public string serialkey { get; set; }
        public float rating { get; set; }
        public string processinfo { get; set; }
        public DateTime recorddate { get; set; }
    }
    public class SurveysRequest
    {
        public int id { get; set; }
        public string title { get; set; }
        public string code { get; set; }
        public string information { get; set; }
        public List<Surveys_polls> surveys_polls { get; set; }
    }

    public class Surveys_polls
    {
        public int id { get; set; }
        public int surveyid { get; set; }
        public int order_no { get; set; }
        public int polltype { get; set; }
        public string poll { get; set; }
        public string pollcontent { get; set; }
    }
    public class Surveys_answersRequest
    {
        public int surveyid { get; set; }
        public List<Surveys_answers> surveys_answers { get; set; }

    }
    public class Surveys_answers
    {
        public int id { get; set; }
        public int memberid { get; set; }
        public int surveyid { get; set; }
        public int pollid { get; set; }
        public int polltype { get; set; }
        public string answer { get; set; }

    }
    public class ExamLookupsRequest
    {
        public List<ExamLookup> request { get; set; }
    }

    public class exam_lookups_smarttype
    {
        public int examid { get; set; }
        public int questioncategoryid { get; set; }
        public int questioncount { get; set; }
        public int questiontype { get; set; }
        public int questiondiffuculty { get; set; }
    }
    public class ExamLookup
    {
        public int examid { get; set; }
        public decimal point { get; set; }
        public int questionid { get; set; }
    }

    public class Exam_answersRequest
    {
        public int examid { get; set; }

        public int examtype { get; set; }
        public List<Exam_answers> answers { get; set; }
    }

    public class Exam_answers
    {
        public int memberid { get; set; }

        public int questionid { get; set; }
        public int examid { get; set; }
        public decimal point { get; set; }

        public string answer { get; set; }
    }
    public class Files
    {
        public int id { get; set; }
        public string title { get; set; }

        public string information { get; set; }
        public string titlecat { get; set; }


    }
    public class EducationProgramme
    {
        public int id { get; set; }
        public string title { get; set; }
        public string code { get; set; }
        public string information { get; set; }
        public int participationcertificateid { get; set; }
        public int achievementcertificateid { get; set; }

        public List<Programmesection> section { get; set; }

    }
    public class Programmesection
    {
        public int id { get; set; }
        public string uniqueId { get; set; }
        public string ownerkey { get; set; }
        public string title { get; set; }

        public int orderid { get; set; }
        public List<Programmecontent> content { get; set; }
    }

    public class System_settings
    {
        public int id { get; set; }
        public string slogan { get; set; }
        public string firmaadi { get; set; }
        public string sitename { get; set; }
        public string logourl { get; set; }
        public string siteurl { get; set; }
        public string mailusername { get; set; }
        public string mailpassword { get; set; }
        public string mailport { get; set; }
        public string mailhosting { get; set; }
        public string mailsendername { get; set; }
        public string mailrecieveraddress { get; set; }
        public bool mailsll { get; set; }


    }
    public class Programmecontent
    {
        public int id { get; set; }
        public string ownerkey { get; set; }
        public int sectionid { get; set; }
        public int content_type { get; set; }
        public int contentid { get; set; }
        public int success_rate { get; set; }
        public int completion_rate { get; set; }
        public string endedrating { get; set; }
        public string title { get; set; }
        public int orderid { get; set; }
    }
    public class ProgrammeContentTypes
    {
        public static Dictionary<int, string> ContentTypes
        {
            get
            {
                Dictionary<int, string> result = new Dictionary<int, string>();
                result.Add(0, "Scorm");
                result.Add(1, "Sınıf Eğitimi");
                result.Add(2, "Video");
                result.Add(3, "Sınav");
                result.Add(4, "Anket");
                result.Add(5, "Dosya");
                result.Add(6, "Eğitim Programı");
                return result;
            }
        }

        public enum EduTypes
        {
            Scorm = 0,
            Classroom = 1,
            Video = 2,
            Exam = 3,
            Survey = 4,
            File = 5,
            Programme = 6
        }

    }

    public static Dictionary<int, string> NotificationMailTypes
    {
        get
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            result.Add(0, "İlk Atama");
            result.Add(1, "Hatırlatma");
            return result;
        }
    }

    public static Dictionary<string, int> questionType
    {
        get
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            result.Add("Coktan-Secmeli", 0);
            result.Add("Dogru-Yanlıs", 1);
            return result;
        }
    }
    public static Dictionary<string, int> questiondifficulty
    {
        get
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            result.Add("Kolay", 0);
            result.Add("Orta", 1);
            result.Add("Zor", 2);


            return result;
        }
    }


    public static Dictionary<int, string> QuestionTypeObject
    {
        get
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            result.Add(0, "Çoktan Seçmeli");
            result.Add(1, "Doğru-Yanlış");
            return result;
        }
    }
    public static Dictionary<int, string> QuestionDifficultyObject
    {
        get
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            result.Add(0, "Kolay");
            result.Add(1, "Orta");
            result.Add(2, "Zor");

            return result;
        }
    }

    public class Programmeassigns
    {
        public int id { get; set; }
        public int memberid { get; set; }
        public int programmeid { get; set; }
        public DateTime startdate { get; set; }
        public DateTime finishdate { get; set; }
        public float rating { get; set; }
        public int status { get; set; }
        public DateTime lastupdatedate { get; set; }
        public DateTime recorddate { get; set; }


    }
    public class Exam_assigns
    {
        public int id { get; set; }
        public int memberid { get; set; }
        public int examid { get; set; }
        public int exammark { get; set; }

        public int status { get; set; }
        public DateTime startdate { get; set; }
        public DateTime finishdate { get; set; }

        public DateTime examstarteddate { get; set; }
        public DateTime examfinisheddate { get; set; }

        public DateTime recorddate { get; set; }
    }

    public class Classroomsession_assingns
    {
        public int id { get; set; }
        public int memberid { get; set; }
        public int classroomsessionid { get; set; }
        public int applymemberid { get; set; }
        public int quota { get; set; }

        public string cancelinformation { get; set; }

        public int status { get; set; }
        public DateTime canceldate { get; set; }

        public DateTime recorddate { get; set; }
    }

    public class title_education_responsibilities
    {
        public int itemid { get; set; }
        public int eduitemtype { get; set; }
        public int titleid { get; set; }
        public int orderid { get; set; }
        public int companyid { get; set; }
    }

    public class MemberDashboard
    {
        public int Videorating { get; set; }
        public int Scormrating { get; set; }
        public int Classroomassingn { get; set; }
        public int progassingn { get; set; }
        public int surveyassingn { get; set; }
        public int examassingn { get; set; }

        public MemberDashboard()
        {
            Videorating = 0;
            Scormrating = 0;
            Classroomassingn = 0;
            progassingn = 0;
            surveyassingn = 0;
            examassingn = 0;
        }
    }
    public enum EducationType
    {
        Scorm = 0,
        Classroom = 1,
        Video = 2
    }
}



