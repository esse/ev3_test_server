using Nancy;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ev3_test_server
{

    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = parameters =>
            {
                Console.WriteLine("/");
                return "Robot locked for: " + Program.myLock;
            };

            Get["/proximity"] = parameters =>
            {
                return "100";
                //return Program.brick.Ports[InputPort.Four].PercentValue.ToString();
            };


            Get["/{apiID}/lock"] = _ =>
            {
                if (Program.myLock != "0")
                {
                    return "API locked by " + Program.myLock;
                }
                Console.WriteLine("Locking for: " + _.apiID.Value);
                Program.myLock = _.apiID.Value;
                Program.Unlock();
                return "OK";
            };

            Get["/{apiID}/forward"] = parameters =>
            {
                if (parameters.apiID.Value != Program.myLock)
                {
                    return "API Needs to be locked first";
                }
                Console.WriteLine("FORWARD by " + parameters.apiID.Value);
                //Program.brick.BatchCommand.TurnMotorAtSpeedForTime(OutputPort.B, 100, 1000, true);
                //Program.brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.C, 100, 1000, true);
                //Program.brick.BatchCommand.SendCommandAsync();
                return "OK";
            };

            Get["/{apiID}/slow_forward"] = parameters =>
            {
                if (parameters.apiID.Value != Program.myLock)
                {
                    return "API Needs to be locked first";
                }
                Console.WriteLine("SLOW FORWARD by " + parameters.apiID.Value);

                return "OK";
            };

            Get["/{apiID}/left"] = parameters =>
            {
                if (parameters.apiID.Value != Program.myLock)
                {
                    return "API Needs to be locked first";
                }
                Console.WriteLine("LEFT by " + parameters.apiID.Value);

                return "OK";
            };

            Get["/{apiID}/right"] = parameters =>
            {
                if (parameters.apiID.Value != Program.myLock)
                {
                    return "API Needs to be locked first";
                }
                Console.WriteLine("RIGHT by " + parameters.apiID.Value);

                return "OK";
            };

            Get["/{apiID}/backward"] = parameters =>
            {
                if (parameters.apiID.Value != Program.myLock)
                {
                    return "API Needs to be locked first";
                }
                Console.WriteLine("BACKWARDS by " + parameters.apiID.Value);

                return "OK";
            };

            Get["/{apiID}/slow_backward"] = parameters =>
            {
                if (parameters.apiID.Value != Program.myLock)
                {
                    return "API Needs to be locked first";
                }
                Console.WriteLine("SLOW BACKWARD by " + parameters.apiID.Value);

                return "OK";
            };

            Get["/{apiID}/attack"] = parameters =>
            {
                if (parameters.apiID.Value != Program.myLock)
                {
                    return "API Needs to be locked first";
                }
                Console.WriteLine("ATTACK by " + parameters.apiID.Value);

                return "OK";
            };

            Get["/{apiID}/run"] = parameters =>
            {
                if (parameters.apiID.Value != Program.myLock)
                {
                    return "API Needs to be locked first";
                }
                Console.WriteLine("RUN ENGINE A - test");

                return "OK";
            };
        }
    }
    class Program : NancyModule
    {


        public static String myLock = "0";

        public static async void Unlock()
        {
            await Task.Delay(60 * 1000);
            System.Console.WriteLine("Unlocking api...");
            myLock = "0";
        }

        static void Main(string[] args)
        {

            System.Console.WriteLine("Starting test API");
            Uri url = new Uri("http://" +
                Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork) +
                ":" + args[0]);
                //":1235");
            using (var host = new NancyHost(url))
            {
                host.Start();
                System.Console.WriteLine("Started HTTP Server on " + url);
                while (true)
                {
                    Console.ReadLine();
                }
            }


        }


    }
}
