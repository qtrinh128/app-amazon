using Chilkat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmazonApp
{
    public partial class Form1 : Form
    {
        void writeLog(string log)
        {
            using (StreamWriter sw = new StreamWriter("LogAmazon.txt", true))
            {
                sw.WriteLine(log);
            }
        }
        void createAccount()
        {
            const string URL_LOGIN = "https://www.amazon.com/ap/register?_encoding=UTF8&openid.assoc_handle=usflex&openid.claimed_id=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.identity=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.mode=checkid_setup&openid.ns=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0&openid.ns.pape=http%3A%2F%2Fspecs.openid.net%2Fextensions%2Fpape%2F1.0&openid.pape.max_auth_age=0&openid.return_to=https%3A%2F%2Fwww.amazon.com%2Fgp%2Fyourstore%2Fhome%3Fie%3DUTF8%26ref_%3Dnav_custrec_newcust";
            const string URL_LOGIN_SUCCESS = "https://www.amazon.com/gp/yourstore/home?ie=UTF8&action=sign-out&path=%2Fgp%2Fyourstore%2Fhome&ref_=nav_youraccount_signout&signIn=1&useRedirectOnSuccess=1&claim_type=EmailAddress&new_account=1&";
            const string URL_REGISTER = "https://www.amazon.com/ap/register";
            Bogus.Faker faker = new Bogus.Faker();
            string fakerName = faker.Name.FullName();
            string fakerPassword = faker.Internet.Password();
            string fakerEmail = faker.Internet.Email();
            //string fakerUserAgent = faker.Internet.UserAgent();
            string fakerUserAgent = $"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/{faker.Random.Int(100000, 999999)}.{faker.Random.Int(50, 999)} (KHTML, like Gecko) Chrome/{faker.Random.Int(50, 999)}.{faker.Random.Int(50, 999)}.{faker.Random.Int(50, 999)}.{faker.Random.Int(50, 999)} Safari/{faker.Random.Int(50, 999)}.{faker.Random.Int(50, 999)}";
            string register = "REGISTER";

            Http http = new Http();
            bool success = http.UnlockComponent("Anything for 30-day trial");
            if (success != true)
            {
                txtResult.Text = http.LastErrorText.ToString();
                return;
            }

            // Using sock 5
            http.SocksVersion = 5;
            //  Set the SocksHostname to the SOCKS proxy domain name or IP address,
            //  which may be IPv4 (dotted notation) or IPv6.
            http.SocksHostname = "103.240.161.109";
            http.SocksPort = 6667;

            http.CookieDir = "memory";
            http.SaveCookies = true;
            http.SendCookies = true;
            http.UserAgent = fakerUserAgent;

            string showMyIP = http.QuickGetStr("http://ip-api.com/json");
            if (true)
            {

            }

            string htmlLogin = http.QuickGetStr(URL_LOGIN);

            string appActionToken = Regex.Match(htmlLogin, "name=\"appActionToken\" value=\"(.+?)\" />").Groups[1].Value.Trim();
            string openidReturnTo = Regex.Match(htmlLogin, "name=\"openid.return_to\" value=\"(.+?)\" />").Groups[1].Value.Trim();
            string prevRID = Regex.Match(htmlLogin, "name=\"prevRID\" value=\"(.+?)\" />").Groups[1].Value.Trim();
            string workflowState = Regex.Match(htmlLogin, "name=\"workflowState\" value=\"(.+?)\" />").Groups[1].Value.Trim();

            HttpRequest requestLogin = new HttpRequest();
            requestLogin.AddParam("appActionToken", appActionToken);
            requestLogin.AddParam("appAction", register);
            requestLogin.AddParam("openid.return_to", openidReturnTo);
            requestLogin.AddParam("prevRID	", prevRID);
            requestLogin.AddParam("workflowState", workflowState);
            requestLogin.AddParam("customerName", fakerName);
            requestLogin.AddParam("email", fakerEmail);
            requestLogin.AddParam("password", fakerPassword);
            requestLogin.AddParam("passwordCheck", fakerPassword);
            HttpResponse responseLogin = http.PostUrlEncoded(URL_REGISTER, requestLogin);
            if (responseLogin == null)
            {
                return;
            }
            string htmlLoginSuccess = http.QuickGetStr(URL_LOGIN_SUCCESS);
            string checkEmailExists = Regex.Match(htmlLoginSuccess, "<title dir=\"ltr\">(.+?)</title>").Groups[1].Value.Trim();
            bool check = Equals(checkEmailExists, "Amazon Sign In");
            if (check)
            {
                txtResult.Invoke(new MethodInvoker(delegate() {
                    txtResult.Text += "Reg error\n";
                }));
               
                return;
            }
            sendMess(http);
            string log = fakerName + "|" + fakerEmail + "|" + fakerPassword;
            writeLog(log);
        }
        void sendMess(Http http)
        {
            while (true)
            {
                HttpRequest requestSendMess = new HttpRequest();
                requestSendMess.AddParam("comment", " hello hellohello hellohello hellohello hellohello hello hello hello hello ");
                string idSeler = "ANSF0RE9FUP82";
                HttpResponse responseLogin = http.PostUrlEncoded($"https://www.amazon.com/ss/help/contact/submitMessage?writeButton=Gửi+truy+vấn&subject=3&orderID=&sellerID={idSeler}&asin=&marketplaceID=ATVPDKIKX0DER&language=en_US", requestSendMess);
                if (true)
                {

                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            List<string> lstMessage = new List<string>(richTextBox1.Text.Split('\n'));

            for (int i = 0; i < 8; i++)
            {
                ThreadStart ts = new ThreadStart(createAccount);
                Thread th = new Thread(ts);
                th.IsBackground = true;
                th.Start();
            }
       
            ////createAccount();
        }
    }
}
