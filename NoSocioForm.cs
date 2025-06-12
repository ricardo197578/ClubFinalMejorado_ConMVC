using System;
using System.Windows.Forms;
using ClubMinimal.Services;
using ClubMinimal.Repositories;
using ClubMinimal.Models;

namespace ClubMinimal.Views.Forms
{
    public class NoSocioForm : Form
    {
        private readonly NoSocioService _noSocioService;
        private readonly TextBox txtNombre;
        private readonly TextBox txtApellido;
        private readonly ListBox listBox;

        public NoSocioForm()
        {
            // Configuración inicial del formulario
            this.Text = "Gestión de No Socios";
            this.Width = 450;
            this.Height = 450;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Inicialización de dependencias
            var dbHelper = new DatabaseHelper();
            var repo = new NoSocioRepository(dbHelper);
            _noSocioService = new NoSocioService(repo);

            // Initialize readonly fields here
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            listBox = new ListBox();

            // Crear controles
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Configuración de los controles ya inicializados
            txtNombre.Left = 120;
            txtNombre.Top = 20;
            txtNombre.Width = 300;

            txtApellido.Left = 120;
            txtApellido.Top = 60;
            txtApellido.Width = 300;

            listBox.Left = 20;
            listBox.Top = 180;
            listBox.Width = 400;
            listBox.Height = 200;
            listBox.Font = new System.Drawing.Font("Consolas", 9.75f);

            // Controles para Nombre
            var lblNombre = new Label
            {
                Text = "Nombre:",
                Left = 20,
                Top = 20,
                Width = 80
            };

            // Controles para Apellido
            var lblApellido = new Label
            {
                Text = "Apellido:",
                Left = 20,
                Top = 60,
                Width = 80
            };

            // Botones
            var btnGuardar = new Button
            {
                Text = "Registrar No Socio",
                Left = 120,
                Top = 100,
                Width = 150
            };

            var btnListar = new Button
            {
                Text = "Listar No Socios",
                Left = 120,
                Top = 140,
                Width = 150
            };

            // Configuración de eventos
            btnGuardar.Click += BtnGuardar_Click;
            btnListar.Click += BtnListar_Click;

            // Agregar controles al formulario
            this.Controls.AddRange(new Control[]
            {
                lblNombre, txtNombre,
                lblApellido, txtApellido,
                btnGuardar, btnListar,
                listBox
            });
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
                {
                    MessageBox.Show("Debe completar nombre y apellido", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _noSocioService.RegistrarNoSocio(txtNombre.Text, txtApellido.Text);

                MessageBox.Show("No Socio registrado exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNombre.Clear();
                txtApellido.Clear();
                txtNombre.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al registrar: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnListar_Click(object sender, EventArgs e)
        {
            try
            {
                listBox.Items.Clear();
                var noSocios = _noSocioService.ObtenerNoSocios();

                // Encabezado
                listBox.Items.Add("ID\tNombre\t\tApellido\tFecha Registro");
                listBox.Items.Add(new string('-', 70));

                foreach (var ns in noSocios)
                {
                    listBox.Items.Add(
                        string.Format("{0}\t{1}\t\t{2}\t{3}",
                        ns.Id,
                        ns.Nombre.PadRight(10).Substring(0, 10),
                        ns.Apellido.PadRight(10).Substring(0, 10),
                        ns.FechaRegistro.ToShortDateString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al listar: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}