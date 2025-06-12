using System.Collections.Generic;
using System.Data.SQLite;
using ClubMinimal.Interfaces;
using ClubMinimal.Models;

namespace ClubMinimal.Repositories
{
    public class SocioRepository : ISocioRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public SocioRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS Socios (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nombre TEXT NOT NULL,
                        Apellido TEXT NOT NULL)";
            _dbHelper.ExecuteNonQuery(sql);
        }

        public void Agregar(Socio socio)
        {
            var sql = "INSERT INTO Socios (Nombre, Apellido) VALUES (@nombre, @apellido)";
            _dbHelper.ExecuteNonQuery(sql,
                new SQLiteParameter("@nombre", socio.Nombre),
                new SQLiteParameter("@apellido", socio.Apellido));
        }

        public List<Socio> ObtenerTodos()
        {
            var socios = new List<Socio>();
            var sql = "SELECT Id, Nombre, Apellido FROM Socios";
            var dt = _dbHelper.ExecuteQuery(sql);

            foreach (System.Data.DataRow row in dt.Rows)
            {
                socios.Add(new Socio
                {
                    Id = (int)(long)row["Id"],
                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString()
                });
            }
            return socios;
        }
    }
}