using System;
using System.Windows.Forms;

namespace ClubMinimal.Views.Forms
{
    public class MenuPrincipalForm : Form
    {
        public MenuPrincipalForm()
        {
            this.Text = "Menú Principal";
            this.Width = 300;
            this.Height = 200;
            this.StartPosition = FormStartPosition.CenterScreen;

            var btnSocios = new Button 
            { 
                Text = "Gestión de Socios", 
                Left = 50, 
                Top = 30, 
                Width = 200 
            };

            var btnNoSocios = new Button 
            { 
                Text = "Gestión de No Socios", 
                Left = 50, 
                Top = 80, 
                Width = 200 
            };

            btnSocios.Click += (s, e) => new SocioForm().Show();
            btnNoSocios.Click += (s, e) => new NoSocioForm().Show();

            this.Controls.AddRange(new Control[] { btnSocios, btnNoSocios });
        }
    }
}