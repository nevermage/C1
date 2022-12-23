using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maksym_lb1_c
{
    class Program
    {
        static void Main(string[] args)
        {
            Rahunok rah = new Rahunok();
            string action="";
            while (action!="0")
            {
                Console.WriteLine("Choose action:");
                Console.WriteLine("Make a call - 1");
                Console.WriteLine("History calls - 2");
                Console.WriteLine("Change tariff - 3");
                Console.WriteLine("Replenish the balance - 4");
                Console.WriteLine("Balance - 5");
                Console.WriteLine("Services - 6");
                action = Console.ReadLine();
                Console.Clear();

                if (action=="1") {
                Console.WriteLine("amount minutes");
                string min = Console.ReadLine();
                Console.WriteLine("Enter call number, 10 symbols");
                string number = Console.ReadLine();

                    if (number.Length == 10) {
                        Single.TryParse(min, out float res);
                        Console.WriteLine($"Cost of call: {rah.makeCall(res, number)}");
                        Console.ReadKey();
                        Console.Clear();

                    } else {
                        Console.WriteLine("Number must be 10 digits");
                        Console.WriteLine("Try again");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }

                if (action=="2") {
                    Console.WriteLine("History calls");
                    rah.history();
                    Console.ReadKey();
                    Console.Clear();
                }

                if (action=="3") {
                    Console.WriteLine($"Current tariff : {rah.tarif}");
                    Console.WriteLine("Choose next tariff and write in line: ");
                    Console.WriteLine("SuperNet_Start - cost 125");
                    Console.WriteLine("SuperNet_Unlim - cost 220");
                    Console.WriteLine("SuperNet_Pro - cost 180");
                    string name = Console.ReadLine();
                    rah.changeTarif(name);
                    Console.ReadKey();
                    Console.Clear();

                }

                if (action=="5") {
                    rah.bal();
                }

                if (action == "4") {
                    rah.replenishBalance();
                }
                
                if (action == "6")
                {
                    Console.WriteLine("Enable or disable the service?");
                    Console.WriteLine("Enable - 1");
                    Console.WriteLine("Disable - 2");
                    string answer = Console.ReadLine();

                    if (answer == "1") {
                        Console.WriteLine("Choose next service and write in line: ");
                        Console.WriteLine("VideoPass - cost 50");
                        Console.WriteLine("OnlinePass - cost 60");
                        Console.WriteLine("SocialPass - cost 40");
                        string answer2 = Console.ReadLine();
                        rah.connServ(answer2);
                        Console.ReadKey();
                        Console.Clear();
                    }
                    
                    if (answer=="2") {
                        rah.DisServ();
                        Console.ReadKey();
                        Console.Clear();
                    }
                }

            }
        }
    }

    class Rahunok
    {
        string h;
        List<string> historyCall = new List<string>();
        List<string> connServices = new List<string>();
        enum nameTar
        {
            SuperNet_Start=125,
            SuperNet_Unlim=220,
            SuperNet_Pro=180
        };
        public string tarif;
        float costPerMin;
        float balance;
        string number;

        enum services
        {
            VideoPass=50,
            SocialPass=40,
            OnlinePass=60
        };
        services service = new services();
        
        public Rahunok()
        {
            tarif = nameTar.SuperNet_Pro.ToString();
            costPerMin = 0.75f;
            balance = 30;
            number = "3809516871";
        }
       
        public void changeTarif(string futureTarif)
        {
            if (tarif!=futureTarif)
            {
                if (futureTarif == nameTar.SuperNet_Start.ToString())
                {
                    if (balance > (int)nameTar.SuperNet_Start)
                    {
                        tarif = futureTarif;
                        balance -= (int)nameTar.SuperNet_Start;
                    }
                    else
                        Console.WriteLine("Not enough balance");
                }
                if (futureTarif == nameTar.SuperNet_Pro.ToString())
                {
                    if (balance > (int)nameTar.SuperNet_Pro)
                    {
                        tarif = futureTarif;
                        balance -= (int)nameTar.SuperNet_Pro;
                    }
                    else
                        Console.WriteLine("Not enough balance");
                }
                if (futureTarif == nameTar.SuperNet_Unlim.ToString())
                {
                    if (balance > (int)nameTar.SuperNet_Unlim)
                    {
                        tarif = futureTarif;
                        balance -= (int)nameTar.SuperNet_Unlim;
                    }
                    else
                        Console.WriteLine("Not enough balance");
                }

            }
            else
            {
                Console.WriteLine("You already have his tariff");
            }
           
        }

        public float makeCall(float minutes, string callNumber)
        {
            if (balance > (minutes * costPerMin))
            {
                h = "Minites: " + minutes.ToString() + "| " + "Call number: " + callNumber;
                historyCall.Add(h);
                balance -= minutes * costPerMin;
                Console.WriteLine("Call made");
                return minutes * costPerMin;
            }
            else
            {
                Console.WriteLine("Not enough balance");
                return 0;
            }
                
            
            
        }
        public void history()
        {
            if (historyCall.Count==0)
            {
                Console.WriteLine("Сall history is empty");
            }
            else
            {
                for (int i = 0; i < historyCall.Count; i++)
                    Console.WriteLine(historyCall[i]);
            }
            
        }
        public void bal()
        {
            Console.WriteLine($"Current balance {balance}");
            Console.ReadKey();
            Console.Clear();
        }

        public void replenishBalance()
        {
            string line;
            Console.WriteLine("Enter the replenishment amount :");
            line=Console.ReadLine();
            Single.TryParse(line,out float b);
            balance += b;
            Console.ReadKey();
            Console.Clear();
        }
        public void connServ(string nameServ)
        {
            
            int check = 0;
            for (int i=0;i<connServices.Count;i++)
            {
                if (nameServ==connServices[i])
                {
                    Console.WriteLine("Services is already connected");
                    check = 1;
                }
            }
            if (check==0)
            {
                if (nameServ == services.VideoPass.ToString())
                {
                    if (balance > (int)services.VideoPass)
                    {
                        connServices.Add(nameServ);
                        balance -= (int)services.VideoPass;
                        Console.WriteLine("Service activated");
                    }
                    else Console.WriteLine("Not enough balance");

                }
                if (nameServ == services.SocialPass.ToString())
                {
                    if (balance > (int)services.VideoPass)
                    {
                        connServices.Add(nameServ);
                        balance -= (int)services.SocialPass;
                        Console.WriteLine("Service activated");
                    }
                    else Console.WriteLine("Not enough balance");


                }
                if (nameServ == services.OnlinePass.ToString()  )
                {
                    if (balance > (int)services.OnlinePass)
                    {
                        connServices.Add(nameServ);
                        balance -= (int)services.OnlinePass;
                        Console.WriteLine("Service activated");
                    }
                    else Console.WriteLine("Not enough balance");

                }
            }    

        }
        public void DisServ()
        {
            if (connServices.Count!=0)
            {
                Console.WriteLine("Connected services: ");
                for (int i = 0; i < connServices.Count; i++)
                {
                    Console.WriteLine(connServices[i]);
                }
                Console.WriteLine("Enter the service you want to disable:");
                string answer = Console.ReadLine();
                if (!connServices.Remove(answer))
                {
                    Console.WriteLine("Incorrect name of service");
                }
                else
                    Console.WriteLine("Service disable");
            }
            else
            {
                Console.WriteLine("Connected services: empty");
            }
            



        }

    }
}
