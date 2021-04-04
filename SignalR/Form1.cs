using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dto;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;

namespace SignalR
{
    public partial class Form1 : Form
    {

        IHubProxy _hub;
        public Form1()
        {
            InitializeComponent();

           
            string url = @"http://localhost:8080/";
            var connection = new HubConnection(url);
            _hub = connection.CreateHubProxy("TestHub");
            connection.Start().Wait();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DtoContacto contacto = new DtoContacto();

            contacto.Nombre = textBox1.Text;
            contacto.Apellido = textBox2.Text;
            contacto.Telefono = textBox3.Text;

            string output = JsonConvert.SerializeObject(contacto);

            _hub.Invoke("Notify", output).Wait();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            _hub.Invoke("Notify2",true).Wait();
        }
    }
}
