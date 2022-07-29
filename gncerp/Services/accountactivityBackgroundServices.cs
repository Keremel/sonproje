using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
namespace gncerp.Services
{
    public class accountactivityBackgroundServices : BackgroundService
    {
        bankhelperServices _bankhelperServices = new bankhelperServices();
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ziraatbank();

                await Task.Delay((Convert.ToInt32(ConfigurationManager.AppSettings["BackgroundServiceDelayMinute"])) * 60 * 1000, stoppingToken);
            }

        

        }

        public void ziraatbank()
        {

            dynamic modellist = string.Format("SELECT * FROM Accountnos WHERE bankid=1").GetDynamicQuery();

            foreach (var item in modellist)
            {

                _bankhelperServices.Ziraatbank_Accountactivitiy_Control(item.anumber, item.id);

            }
        }
    }
}
