using System;
using System.Windows.Forms;
using ClubMinimal.Services;
using ClubMinimal.Repositories;
using ClubMinimal.Models;

namespace ClubMinimal.Views.Forms
{
    public class MenuPrincipalForm : Form
    {
        private readonly DatabaseHelper _dbHelper;

        public MenuPrincipalForm()
        {
            _dbHelper = new DatabaseHelper();

            this.Text = "Menú Principal";
            this.Width = 300;
            this.Height = 250;
            this.StartPosition = FormStartPosition.CenterScreen;

            var btnSocios = new Button { Text = "Gestión de Socios", Left = 50, Top = 30, Width = 200 };
            var btnNoSocios = new Button { Text = "Gestión de No Socios", Left = 50, Top = 80, Width = 200 };
            var btnPagos = new Button { Text = "Pago de Actividades", Left = 50, Top = 130, Width = 200 };

            btnSocios.Click += (s, e) => new SocioForm().Show();
            btnNoSocios.Click += (s, e) => new NoSocioForm().Show();

            btnPagos.Click += (s, e) =>
            {
                var noSocioRepo = new NoSocioRepository(_dbHelper);
                var actividadRepo = new ActividadRepository(_dbHelper);
                var pagoRepo = new PagoRepository(_dbHelper);

                var noSocioService = new NoSocioService(noSocioRepo);
                var actividadService = new ActividadService(actividadRepo);
                var pagoService = new PagoService(pagoRepo, actividadRepo, noSocioRepo);

                new PagoActividadForm(noSocioService, actividadService, pagoService).Show();
            };

            this.Controls.AddRange(new Control[] { btnSocios, btnNoSocios, btnPagos });
        }
    }
}