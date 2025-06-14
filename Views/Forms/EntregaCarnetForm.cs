using System;
using System.Drawing;
using System.Windows.Forms;
using ClubMinimal.Models;
using ClubMinimal.Interfaces;

namespace ClubMinimal.Views.Forms
{
    public class EntregaCarnetForm : Form
    {
        private readonly ISocioService _socioService;
        private readonly ICarnetService _carnetService;
        private int _socioId;

        private Label lblTitulo;
        private Label lblInfoSocio;
        private Label lblEstadoApto;
        private Label lblCodigoCarnet;
        private Label lblVencimiento;
        private Button btnGenerarCarnet;
        private PictureBox picCarnet;

        public EntregaCarnetForm(ISocioService socioService, ICarnetService carnetService, int socioId)
        {
            _socioService = socioService;
            _carnetService = carnetService;
            _socioId = socioId;

            InitializeComponent();
            CargarDatosSocio();
        }

        private void InitializeComponent()
        {
            this.Text = "Entrega de Carnet";
            this.Width = 400;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitulo = new Label();
            lblTitulo.Text = "Carnet de Socio";
            lblTitulo.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblTitulo.Top = 20;
            lblTitulo.Width = this.ClientSize.Width - 40;
            lblTitulo.Left = 20;

            lblInfoSocio = new Label();
            lblInfoSocio.Top = 70;
            lblInfoSocio.Left = 20;
            lblInfoSocio.Width = this.ClientSize.Width - 40;

            lblEstadoApto = new Label();
            lblEstadoApto.Top = 100;
            lblEstadoApto.Left = 20;
            lblEstadoApto.Width = this.ClientSize.Width - 40;
            lblEstadoApto.Font = new Font("Arial", 10, FontStyle.Bold);

            picCarnet = new PictureBox();
            picCarnet.Top = 140;
            picCarnet.Left = 50;
            picCarnet.Width = 300;
            picCarnet.Height = 180;
            picCarnet.BorderStyle = BorderStyle.FixedSingle;
            picCarnet.BackColor = Color.LightGray;

            lblCodigoCarnet = new Label();
            lblCodigoCarnet.Top = 340;
            lblCodigoCarnet.Left = 20;
            lblCodigoCarnet.Width = this.ClientSize.Width - 40;

            lblVencimiento = new Label();
            lblVencimiento.Top = 370;
            lblVencimiento.Left = 20;
            lblVencimiento.Width = this.ClientSize.Width - 40;

            btnGenerarCarnet = new Button();
            btnGenerarCarnet.Text = "Generar Carnet";
            btnGenerarCarnet.Top = 410;
            btnGenerarCarnet.Left = 120;
            btnGenerarCarnet.Width = 150;
            btnGenerarCarnet.Click += new EventHandler(BtnGenerarCarnet_Click);

            this.Controls.AddRange(new Control[] {
                lblTitulo,
                lblInfoSocio,
                lblEstadoApto,
                picCarnet,
                lblCodigoCarnet,
                lblVencimiento,
                btnGenerarCarnet
            });
        }

        private void CargarDatosSocio()
        {
            Socio socio = _socioService.ObtenerPorId(_socioId);
            if (socio == null)
            {
                MessageBox.Show("Socio no encontrado");
                this.Close();
                return;
            }

            lblInfoSocio.Text = "Socio: " + socio.Nombre + " " + socio.Apellido + "\nDNI: " + socio.Dni;

            if (socio.AptoFisicoAprobado)
            {
                lblEstadoApto.Text = "APTO FÍSICO: APROBADO";
                lblEstadoApto.ForeColor = Color.Green;
                btnGenerarCarnet.Enabled = true;
            }
            else
            {
                lblEstadoApto.Text = "APTO FÍSICO: NO APROBADO";
                lblEstadoApto.ForeColor = Color.Red;
                btnGenerarCarnet.Enabled = false;
            }

            Carnet carnet = _carnetService.ObtenerCarnetVigente(_socioId);
            if (carnet != null)
            {
                MostrarCarnet(carnet);
                btnGenerarCarnet.Text = "Renovar Carnet";
            }
        }

        private void MostrarCarnet(Carnet carnet)
        {
            lblCodigoCarnet.Text = "Código: " + carnet.Codigo;
            lblVencimiento.Text = "Válido hasta: " + carnet.FechaVencimiento.ToString("dd/MM/yyyy");

            using (Graphics g = picCarnet.CreateGraphics())
            {
                g.Clear(Color.White);
                g.DrawString("Club Minimal", new Font("Arial", 16, FontStyle.Bold), Brushes.Blue, 50, 20);
                g.DrawString("Carnet de Socio", new Font("Arial", 12), Brushes.Black, 80, 50);

                Socio socio = _socioService.ObtenerPorId(_socioId);
                g.DrawString(socio.Nombre + " " + socio.Apellido, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 30, 90);
                g.DrawString("DNI: " + socio.Dni, new Font("Arial", 10), Brushes.Black, 30, 120);
                g.DrawString("Código: " + carnet.Codigo, new Font("Arial", 8), Brushes.Black, 30, 150);
            }
        }

        private void BtnGenerarCarnet_Click(object sender, EventArgs e)
        {
            try
            {
                Carnet carnet = _carnetService.GenerarNuevoCarnet(_socioId);
                MostrarCarnet(carnet);
                MessageBox.Show("Carnet generado exitosamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar carnet: " + ex.Message);
            }
        }
    }
}
