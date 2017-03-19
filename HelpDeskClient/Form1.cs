using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using SymHelper;


namespace HelpDeskClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = Environment.UserName;
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
           
            WebRequest request = WebRequest.Create("http://25.54.215.161:49709/api/request/postTest");
            request.Method = "POST";
            string postData = "username=" + textBox1.Text + "&password=" + textBox2.Text;// + "&Image="; // + Convert.ToBase64String(File.ReadAllBytes(path));
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);           
            dataStream.Close();

            WebResponse response = request.GetResponse();           
            label1.Text = (((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            //label1.Text = (responseFromServer);
            reader.Close();
            dataStream.Close();
            response.Close();

            //Конвертация ответа из Json в объект AuthObject

            AuthObject RObject = JsonConvert.DeserializeObject<AuthObject>(responseFromServer);
            request.Headers.Add("Authorization", "Bearer" + RObject.access_token);

            // label1.Text = RObject.access_token; Вывод токена для проверки работоспособности.

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Инвентаризация и получение xml документа со списком параметров. 
            Inventory pcinventory;
            pcinventory = new Inventory(); 
            pcinventory.StartInventory();
            label1.Text = pcinventory.IdProcessor.ToString(); 
            Inventory.GetStringXMLFile(pcinventory);
        }
    }
}
