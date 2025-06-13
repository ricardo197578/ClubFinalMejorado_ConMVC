using ClubMinimal.Repositories;
using ClubMinimal.Models;
using ClubMinimal.Services;
using System;
using System.Windows.Forms;

namespace ClubMinimal.Views.Forms
{
    public class SocioForm : Form
    {
        private readonly SocioService _socioService;
        private readonly TextBox txtNombre;
        private readonly TextBox txtApellido;
        private readonly ListBox listBox;
        private readonly CheckBox chkAptoFisico;
        private readonly Button btnEntregarCarnet;
        private Socio _socioSeleccionado;

        public SocioForm()
        {
            // Configuración inicial
            this.Text = "Gestión de Socios";
            this.Width = 450; // Aumentamos el ancho para los nuevos controles
            this.Height = 500; // Aumentamos la altura
            this.StartPosition = FormStartPosition.CenterScreen;

            // Inicialización de dependencias
            var dbHelper = new DatabaseHelper();
            var socioRepo = new SocioRepository(dbHelper);
            _socioService = new SocioService(socioRepo);

            // Inicializar los campos
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            listBox = new ListBox();
            chkAptoFisico = new CheckBox();
            btnEntregarCarnet = new Button();

            // Crear controles
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Configurar los controles ya inicializados
            txtNombre.Left = 100;
            txtNombre.Top = 20;
            txtNombre.Width = 200;

            txtApellido.Left = 100;
            txtApellido.Top = 50;
            txtApellido.Width = 200;

            listBox.Left = 20;
            listBox.Top = 220; // Bajamos la lista para los nuevos controles
            listBox.Width = 400;
            listBox.Height = 200;
            listBox.SelectionMode = SelectionMode.One;
            listBox.DisplayMember = "NombreCompleto";

            // CheckBox para Apto Físico
            chkAptoFisico.Text = "Apto Físico Aprobado";
            chkAptoFisico.Left = 100;
            chkAptoFisico.Top = 90;
            chkAptoFisico.Width = 150;
            chkAptoFisico.CheckedChanged += (s, e) =>
            {
                btnEntregarCarnet.Enabled = chkAptoFisico.Checked;
            };

            // Botón para entregar carnet (inicialmente deshabilitado)
            btnEntregarCarnet.Text = "Entregar Carnet";
            btnEntregarCarnet.Left = 100;
            btnEntregarCarnet.Top = 120;
            btnEntregarCarnet.Width = 120;
            btnEntregarCarnet.Enabled = false;
            btnEntregarCarnet.Click += BtnEntregarCarnet_Click;

            // Controles existentes
            var lblNombre = new Label { Text = "Nombre:", Left = 20, Top = 20, Width = 80 };
            var lblApellido = new Label { Text = "Apellido:", Left = 20, Top = 50, Width = 80 };

            var btnGuardar = new Button { Text = "Guardar Socio", Left = 100, Top = 160, Width = 120 };
            var btnListar = new Button { Text = "Ver Socios", Left = 230, Top = 160, Width = 120 };

            // Eventos
            btnGuardar.Click += BtnGuardar_Click;
            btnListar.Click += BtnListar_Click;
            listBox.SelectedIndexChanged += ListBox_SelectedIndexChanged;

            // Agregar controles al formulario
            this.Controls.AddRange(new Control[] {
                lblNombre, txtNombre,
                lblApellido, txtApellido,
                chkAptoFisico,
                btnEntregarCarnet,
                btnGuardar, btnListar,
                listBox
            });
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNombre.Text) && !string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                var socio = new Socio
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    AptoFisicoAprobado = chkAptoFisico.Checked
                };

                _socioService.RegistrarSocio(socio);
                MessageBox.Show("Socio registrado!");
                LimpiarFormulario();
                BtnListar_Click(sender, e); // Actualizar la lista
            }
        }

        private void BtnListar_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            var socios = _socioService.ObtenerSocios();
            foreach (var socio in socios)
            {
                listBox.Items.Add(socio);
            }
        }

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _socioSeleccionado = listBox.SelectedItem as Socio;
            if (_socioSeleccionado != null)
            {
                txtNombre.Text = _socioSeleccionado.Nombre;
                txtApellido.Text = _socioSeleccionado.Apellido;
                chkAptoFisico.Checked = _socioSeleccionado.AptoFisicoAprobado;
                btnEntregarCarnet.Enabled = _socioSeleccionado.AptoFisicoAprobado;
            }
        }

        private void BtnEntregarCarnet_Click(object sender, EventArgs e)
        {
            if (_socioSeleccionado == null || !_socioSeleccionado.AptoFisicoAprobado)
            {
                MessageBox.Show("Seleccione un socio con apto físico aprobado");
                return;
            }

            // Configurar servicios para el carnet
            var dbHelper = new DatabaseHelper();
            var carnetRepo = new CarnetRepository(dbHelper);
            var socioRepo = new SocioRepository(dbHelper);
            var carnetService = new CarnetService(carnetRepo, socioRepo);
            var socioService = new SocioService(socioRepo);

            // Mostrar formulario de carnet
            var formCarnet = new EntregaCarnetForm(socioService, carnetService, _socioSeleccionado.Id);
            formCarnet.ShowDialog();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            chkAptoFisico.Checked = false;
            _socioSeleccionado = null;
        }
    }
}