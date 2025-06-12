using System;
using System.Collections.Generic;
using System.Data.SQLite;
using ClubMinimal.Interfaces;
using ClubMinimal.Models;

namespace ClubMinimal.Repositories
{
    public class NoSocioRepository : INoSocioRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public NoSocioRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS NoSocios (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nombre TEXT NOT NULL,
                        Apellido TEXT NOT NULL,
                        FechaRegistro TEXT NOT NULL)";
            _dbHelper.ExecuteNonQuery(sql);
        }

        public void Agregar(NoSocio noSocio)
        {
            var sql = "INSERT INTO NoSocios (Nombre, Apellido, FechaRegistro) VALUES (@nombre, @apellido, @fecha)";
            _dbHelper.ExecuteNonQuery(sql,
                new SQLiteParameter("@nombre", noSocio.Nombre),
                new SQLiteParameter("@apellido", noSocio.Apellido),
                new SQLiteParameter("@fecha", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        }

        public List<NoSocio> ObtenerTodos()
        {
            var noSocios = new List<NoSocio>();
            var sql = "SELECT Id, Nombre, Apellido, FechaRegistro FROM NoSocios";
            var dt = _dbHelper.ExecuteQuery(sql);

            foreach (System.Data.DataRow row in dt.Rows)
            {
                noSocios.Add(new NoSocio
                {
                    Id = (int)(long)row["Id"],
                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString(),
                    FechaRegistro = DateTime.Parse(row["FechaRegistro"].ToString())
                });
            }
            return noSocios;
        }
    }
}