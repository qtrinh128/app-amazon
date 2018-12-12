using Chilkat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace AmazonApp
{
    public partial class Form1 : Form
    {
        void PrintLog(string s, Color color)
        {
            DateTime date = DateTime.Now;
            txtResult.Invoke(new MethodInvoker(() =>
            {
                txtResult.SelectionStart = txtResult.TextLength;
                txtResult.SelectionLength = 0;

                txtResult.SelectionColor = color;
                txtResult.AppendText($"{date.ToString()}: {s} {Environment.NewLine}");
                txtResult.SelectionColor = txtResult.ForeColor;
            }));
        }
        string GetUserAgent()
        {
            Bogus.Faker faker = new Bogus.Faker();
            string userAgentChorme = $"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/{faker.Random.Int(100000, 999999)}.{faker.Random.Int(50, 999)} (KHTML, like Gecko) Chrome/{faker.Random.Int(50, 999)}.{faker.Random.Int(50, 999)}.{faker.Random.Int(50, 999)}.{faker.Random.Int(50, 999)} Safari/{faker.Random.Int(50, 999)}.{faker.Random.Int(50, 999)}";
            string userAgentFirefox = $"Mozilla/5.0 (Windows NT 5.1; rv:7.0.1) Gecko/20100101 Firefox/{faker.Random.Int(100, 1000)}.{faker.Random.Int(10, 100)}.{faker.Random.Int(100, 1000)}";
            string userAgentSafari = $"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/{faker.Random.Int(1000, 10000)}.{faker.Random.Int(100, 1000)}.{faker.Random.Int(10, 100)} (KHTML, like Gecko) Version/{faker.Random.Int(1, 10)}.{faker.Random.Int(1, 10)}.{faker.Random.Int(1, 10)} Safari/7046A194A";
            List<string> userAgent = new List<string>();
            userAgent.Add(userAgentChorme);
            userAgent.Add(userAgentFirefox);
            userAgent.Add(userAgentSafari);
            int rd = faker.Random.Int(0, userAgent.Count - 1);
            return userAgent[rd];
        }
        /// <summary>
        /// write file log account reg success
        /// </summary>
        /// <param name="log"></param>
        void WriteLog(string log)
        {
            using (StreamWriter sw = new StreamWriter("LogAmazon.txt", true))
            {
                sw.WriteLine(log);
            }
        }
        /// <summary>
        /// create List<string> from richtextbox
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        List<string> CreateListFromRichTextBox(RichTextBox txt)
        {
            List<string> list = new List<string>(txt.Text.Split('\n'));
            return list;
        }
        /// <summary>
        /// Create string from list<string>
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        string CreateString(List<string> list)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string item in list)
            {
                sb.Append(item);
                sb.AppendLine();
            }
            return sb.ToString();
        }
        /// <summary>
        /// Get sock live add to list
        /// </summary>
        /// <returns></returns>
        void GetListSock()
        {
            List<string> listSockCheck = CreateListFromRichTextBox(txtListSock);
            //var cts = new CancellationTokenSource();
            foreach (var item in listSockCheck)
            {
                ThreadPool.QueueUserWorkItem(o =>
                {
                    //CancellationToken token = (CancellationToken)o;
                    //while (!token.IsCancellationRequested)
                    //{
                    Http http = new Http();
                    bool success = http.UnlockComponent("Anything for 30-day trial");
                    if (success != true)
                    {
                        MessageBox.Show("Vui lòng thử lại sau!!!", "Thông  báo");
                        return;
                    }
                    http.SocksVersion = 5;
                    string[] sock = item.Split(':');
                    http.SocksHostname = sock[0];
                    http.SocksPort = int.Parse(sock[1]);
                    PrintLog($"Checking {sock[0]} ...", Color.Aqua);
                    string showMyIP = http.QuickGetStr("https://api.ipify.org/?format=json");
                    if (String.IsNullOrEmpty(showMyIP))
                    {
                        listSockCheck.Remove(item);
                        PrintLog($"{sock[0]} not working", Color.Red);
                        txtListSock.Invoke(new MethodInvoker(delegate ()
                        {
                            txtListSock.Text = CreateString(listSockCheck);
                        }));
                    }
                    else
                    {
                        CreateAccount(http);
                    }
                    //}
                }); //cts.Token);
            }
        }

        void CreateAccount(Http http)
        {
            const string GET_URL_REGISTER = "https://www.amazon.com/ap/register?_encoding=UTF8&openid.assoc_handle=usflex&openid.claimed_id=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.identity=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.mode=checkid_setup&openid.ns=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0&openid.ns.pape=http%3A%2F%2Fspecs.openid.net%2Fextensions%2Fpape%2F1.0&openid.pape.max_auth_age=0&openid.return_to=https%3A%2F%2Fwww.amazon.com%2Fgp%2Fyourstore%2Fhome%3Fie%3DUTF8%26ref_%3Dnav_custrec_newcust";
            const string URL_LOGIN_SUCCESS = "https://www.amazon.com/gp/yourstore/home?ie=UTF8&action=sign-out&path=%2Fgp%2Fyourstore%2Fhome&ref_=nav_youraccount_signout&signIn=1&useRedirectOnSuccess=1&claim_type=EmailAddress&new_account=1&";
            const string POST_URL_REGISTER = "https://www.amazon.com/ap/register";
            Bogus.Faker faker = new Bogus.Faker();
            string fakerName = faker.Name.FullName();
            string fakerPassword = faker.Internet.Password();
            string fakerEmail = faker.Internet.Email();
            string register = "REGISTER";

            http.CookieDir = "memory";
            http.SaveCookies = true;
            http.SendCookies = true;
            http.UserAgent = GetUserAgent().ToString();

            string htmlLogin = http.QuickGetStr(GET_URL_REGISTER);
            if (String.IsNullOrEmpty(htmlLogin))
            {
                return;
            }
            else
            {
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
                HttpResponse responseLogin = http.PostUrlEncoded(POST_URL_REGISTER, requestLogin);
                if (responseLogin == null)
                {
                    PrintLog($"responseLogin {fakerEmail} error", Color.Red);
                    return;
                }
                else
                {
                    string htmlLoginSuccess = http.QuickGetStr(URL_LOGIN_SUCCESS);
                    if (String.IsNullOrEmpty(htmlLoginSuccess))
                    {
                        PrintLog($"htmlLoginSuccess {fakerEmail} error", Color.Red);
                        return;
                    }
                    else
                    {
                        string checkEmailExists = Regex.Match(htmlLoginSuccess, "<title dir=\"ltr\">(.+?)</title>").Groups[1].Value.Trim();
                        bool check = Equals(checkEmailExists, "Amazon Sign In");
                        if (check)
                        {
                            PrintLog($"{fakerEmail} exists", Color.Linen);
                            return;
                        }
                        SendMess(http, fakerEmail);
                        string log = fakerName + "|" + fakerEmail + "|" + fakerPassword;
                        WriteLog(log);

                    }
                }
            }
        }
        void SendMess(Http http, string mail)
        {
            txtListID.Invoke(new MethodInvoker(delegate ()
            {
                List<string> listID = CreateListFromRichTextBox(txtListID);
                txtListMessage.Invoke(new MethodInvoker(delegate ()
                {
                    List<string> listMessages = CreateListFromRichTextBox(txtListMessage);
                    foreach (var mess in listMessages)
                    {
                        foreach (var id in listID)
                        {
                            ThreadPool.QueueUserWorkItem((o) =>
                            {
                                HttpRequest requestSendMess = new HttpRequest();
                                requestSendMess.AddParam("comment", mess);
                                HttpResponse responseLogin = http.PostUrlEncoded($"https://www.amazon.com/ss/help/contact/submitMessage?writeButton=submit&subject=3&orderID=&sellerID={id}&asin=&marketplaceID=ATVPDKIKX0DER&language=en_US", requestSendMess);
                                txtResult.Invoke(new MethodInvoker(delegate ()
                                {
                                    PrintLog($"{mail} send to {id} success", Color.Green);
                                }));
                            });
                        }
                    }
                }));

            }));
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            GetListSock();
        }

        private void txtListSock_TextChanged(object sender, EventArgs e)
        {
            List<string> list = CreateListFromRichTextBox(txtListSock);
            lblSockLive.Text = list.Count.ToString();
        }

    }
}
