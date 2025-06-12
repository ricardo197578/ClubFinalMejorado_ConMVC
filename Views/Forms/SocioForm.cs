using System;
using System.Windows.Forms;
using ClubMinimal.Services;
using ClubMinimal.Repositories;
using ClubMinimal.Models;

namespace ClubMinimal.Views.Forms
{
    public class SocioForm : Form
    {
        private readonly SocioService _socioService;
        private readonly TextBox txtNombre;
        private readonly TextBox txtApellido;
        private readonly ListBox listBox;

        public SocioForm()
        {
            // Configuración inicial
            this.Text = "Gestión de Socios";
            this.Width = 400;
            this.Height = 400;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Inicialización de dependencias
            var dbHelper = new DatabaseHelper();
            var socioRepo = new SocioRepository(dbHelper);
            _socioService = new SocioService(socioRepo);

            // Inicializar los campos readonly aquí
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            listBox = new ListBox();

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
            listBox.Top = 170;
            listBox.Width = 350;
            listBox.Height = 180;

            // Controles
            var lblNombre = new Label { Text = "Nombre:", Left = 20, Top = 20, Width = 80 };
            var lblApellido = new Label { Text = "Apellido:", Left = 20, Top = 50, Width = 80 };

            var btnGuardar = new Button { Text = "Guardar Socio", Left = 100, Top = 90, Width = 120 };
            var btnListar = new Button { Text = "Ver Socios", Left = 100, Top = 130, Width = 120 };

            // Eventos
            btnGuardar.Click += btnGuardar_Click;
            btnListar.Click += btnListar_Click;

            // Agregar controles al formulario actual (this)
            this.Controls.AddRange(new Control[] {
                lblNombre, txtNombre,
                lblApellido, txtApellido,
                btnGuardar, btnListar,
                listBox
            });
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNombre.Text) && !string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                _socioService.RegistrarSocio(txtNombre.Text, txtApellido.Text);
                MessageBox.Show("Socio registrado!");
                txtNombre.Clear();
                txtApellido.Clear();
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            var socios = _socioService.ObtenerSocios();
            foreach (var socio in socios)
            {
                listBox.Items.Add(string.Format("{0}: {1} {2}",
                    socio.Id,
                    socio.Nombre,
                    socio.Apellido));
            }
        }
    }
}