using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.FtpClient;
using MinimalisticTelnet;

namespace mantis_tests
{
    public class JamesHelper : HelperBase
    {

        public JamesHelper(ApplicationManager manager) : base(manager) { }

        public void Add(AccountData account)
        {
            if (Verify(account))
            {
                return;
            }
            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("adduser " + account.Name + " " + account.Password);
            WaitTelnet(telnet);

            //Ждем появления пользователя
            int attempt = 0;
            while (!Verify(account) && attempt < 10000)
            {
                System.Threading.Thread.Sleep(2);
                attempt++;
            }
        }

        public void Delete(AccountData account)
        {
            if (!Verify(account))
            {
                return;
            }
            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("deluser " + account.Name);
            WaitTelnet(telnet);
        }
        public bool Verify(AccountData account)
        {
            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("verify " + account.Name);
            
            return !WaitTelnet(telnet).Contains("does not exist");
        }

        private string WaitTelnet(TelnetConnection telnet)
        {
            //string s = null;            
            //int attempt = 0;

            //while (s == null && attempt < 10)
            //{
            //    s = telnet.Read();
            //    System.Threading.Thread.Sleep(100);                
            //    attempt++;
            //}
            //System.Console.Out.WriteLine(attempt);
            string  s = telnet.Read();
            System.Threading.Thread.Sleep(1000);
            System.Console.Out.WriteLine(s);
            
            return s;
        }

        private TelnetConnection LoginToJames()
        {
            TelnetConnection telnet = new TelnetConnection("localhost", 4555);
            System.Console.Out.WriteLine(telnet.Read());
            telnet.WriteLine("root");
            System.Console.Out.WriteLine(telnet.Read());
            telnet.WriteLine("root");
            System.Console.Out.WriteLine(telnet.Read());
            return telnet;
        }

    }
}
