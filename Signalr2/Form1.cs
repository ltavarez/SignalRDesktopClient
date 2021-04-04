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

namespace Signalr2
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

            _hub.On("SendInfo", x => {

                DtoContacto contacto = JsonConvert.DeserializeObject<DtoContacto>(x);

                TxtName.Invoke((MethodInvoker)delegate {
                    // Running on the UI thread
                    TxtName.Text = contacto.Nombre; 
                });

                TxtLastName.Invoke((MethodInvoker)delegate {
                    // Running on the UI thread
                    TxtLastName.Text = contacto.Apellido;
                });

                TxtPhone.Invoke((MethodInvoker)delegate {
                    // Running on the UI thread
                    TxtPhone.Text = contacto.Telefono;
                });             
            });

            _hub.On("SendNotify", x => {

                MessageBox.Show("Nueva orden");

            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
