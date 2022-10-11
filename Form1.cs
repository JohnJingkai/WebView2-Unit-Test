using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;

namespace Winforms_Webs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //webView.Source = new Uri(@"C:\Users\byang\source\repos\Winforms_Webs\Test1.html");
            //this.Resize += new System.EventHandler(this.Form_Resize);
            webView.Source = new Uri(@"C:\Users\jwang\Desktop\working folder\GitHub\Winforms_Webs10072022\Test1.html");
            
            this.Resize += new System.EventHandler(this.Form_Resize);

        }
        private void Form_Resize(object sender, EventArgs e)
        {
            webView.Size = this.ClientSize - new System.Drawing.Size(webView.Location);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (webView != null && webView.CoreWebView2 != null) { }
                webView.CoreWebView2.Navigate(@"C:\Users\jwang\Desktop\working folder\GitHub\Winforms_Webs10072022\Test1.html");
            //UpdateMessage(null,null);
            InitializeAsync();
        }

        async void InitializeAsync()
        {
            //var env = await CoreWebView2Environment.CreateAsync(userDataFolder: Path.Combine(Path.GetTempPath(), "MarkdownMonster_Browser"));
            await webView.EnsureCoreWebView2Async(null);
            //webView.CoreWebView2.NavigationCompleted += UpdateMessage;
            webView.CoreWebView2.NavigationCompleted += UpdateTable;
            
            
            //await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.chrome.webview.postMessage(window.document.URL);");
            //await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.chrome.webview.addEventListener(\'message\', event => alert(event.data));");
        }
        private async void UpdateTable(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            if (webView != null && webView.CoreWebView2 != null)
            {
                List<tblTest> list = new List<tblTest>();
                list.Add(new tblTest { userId = "2022", id = "M", title = "John King MVP", completed = "1232131231" });
                list.Add(new tblTest { userId = "321", id = "M", title = "John ", completed = "33" });
                list.Add(new tblTest { userId = "65", id = "M", title = "MVP", completed = "645" });
                string strserialize = JsonConvert.SerializeObject(list);
                string res = $"renderDataInTheTable({strserialize})";
                await webView.ExecuteScriptAsync(res);
            }


        }
        void UpdateMessage(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            //String uri = args.TryGetWebMessageAsString();
            //txtToHtml.Text = uri;
            if (webView != null && webView.CoreWebView2 != null)
            {
                //object[] o = new object[1];
                computer com = new computer();
                com.DOB = "2022";
                com.GENDER = "M";
                com.NAME = "John King MVP";
                com.SSN = "1232131231";
                string response = JsonConvert.SerializeObject(new
                {
                    NAME = com.NAME,
                    SSN = com.SSN,
                    GENDER = com.GENDER,
                    DOB = com.DOB
                });
                //o[0] = response;
                //var json = JsonConvert.SerializeObject(s);
                webView.CoreWebView2.PostWebMessageAsString(response);
            }            
        }

        public class computer
        {
            public string NAME { get; set; }
            public string DOB { get; set; }
            public string SSN { get; set; }
            public string GENDER { get; set; }
        }


        public class tblTest
        {
            public string userId { get; set; }
            public string id { get; set; }
            public string title { get; set; }
            public string completed { get; set; }
        }
        private void webView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string cur = e.TryGetWebMessageAsString();
            txtToHtml.Text = cur;
            computer mdl = Newtonsoft.Json.JsonConvert.DeserializeObject<computer>(cur);
        }
    }
}
