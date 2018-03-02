using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using Bitnet.Client;
using Microsoft.Extensions.Configuration;

namespace MasterNodesManager
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Менеджер Мастернод начал арбайтен =)))))\n");
            Console.WriteLine("Загрузка настроек\n");
            Configuration = Settings.Load();


            CheckMnSendMoney();


            while (true)
            {
                CheckMnSendMoney();

                Thread.Sleep(Int32.Parse(Configuration["TimeReqwestNode"]) * 1000);
            } 
        }
    

    private static void CheckMnSendMoney()
        {
            var valuesSection = Configuration.GetSection("Nodes").GetChildren();

            Console.WriteLine("----------------------------------------------------");

            foreach (var item in valuesSection)
            {
                var bc = new BitnetClient("http://" + Configuration[item.Path + ":Server"] + ":" +
                                          Configuration[item.Path + ":Port"])
                {
                    Credentials = new NetworkCredential(Configuration[item.Path + ":RpcUser"],
                        Configuration[item.Path + ":RpcPasword"])
                };

                try
                {
                    Console.WriteLine("БАЛАНС {0} РАВЕН {1}", Configuration[item.Path + ":Symbol"],
                        bc.GetBalance() + "\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine("ЗАПИСАЛ ОШИБКУ ЗАПРОСА В ЛОГ");
                }

                if (bc.GetBalance() > 1001)
                {
                    var sendmoney = bc.GetBalance() - 1001;
                    Console.WriteLine("Отправка денег на биржу {0} монет", sendmoney);
                    bc.SendToAddress(Configuration[item.Path + ":WalletAddress"], sendmoney, "MNManager", "");
                }
                
            }
            Console.WriteLine("----------------------------------------------------");
        }
    }
}
