using System;
using System.Data.SQLite;
using ClubMinimal.Interfaces;
using ClubMinimal.Models;

namespace ClubMinimal.Repositories
{
    public class CarnetRepository : ICarnetRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public CarnetRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS Carnets (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        SocioId INTEGER NOT NULL,
                        Codigo TEXT NOT NULL UNIQUE,
                        FechaEmision TEXT NOT NULL,
                        FechaVencimiento TEXT NOT NULL,
                        Activo INTEGER NOT NULL DEFAULT 1,
                        FOREIGN KEY(SocioId) REFERENCES Socios(Id))";
            _dbHelper.ExecuteNonQuery(sql);
        }

        public void GenerarCarnet(Carnet carnet)
        {
            var sql = @"INSERT INTO Carnets 
                       (SocioId, Codigo, FechaEmision, FechaVencimiento, Activo) 
                       VALUES (@socioId, @codigo, @fechaEmision, @fechaVencimiento, @activo)";
            
            _dbHelper.ExecuteNonQuery(sql,
                new SQLiteParameter("@socioId", carnet.SocioId),
                new SQLiteParameter("@codigo", carnet.Codigo),
                new SQLiteParameter("@fechaEmision", carnet.FechaEmision.ToString("yyyy-MM-dd")),
                new SQLiteParameter("@fechaVencimiento", carnet.FechaVencimiento.ToString("yyyy-MM-dd")),
                new SQLiteParameter("@activo", carnet.Activo ? 1 : 0));
        }

        public Carnet ObtenerCarnetPorSocioId(int socioId)
        {
            var sql = "SELECT * FROM Carnets WHERE SocioId = @socioId AND Activo = 1 ORDER BY FechaVencimiento DESC LIMIT 1";
            var dt = _dbHelper.ExecuteQuery(sql, new SQLiteParameter("@socioId", socioId));

            if (dt.Rows.Count == 0) return null;

            var row = dt.Rows[0];
            return new Carnet
            {
                Id = (int)(long)row["Id"],
                SocioId = (int)(long)row["SocioId"],
                Codigo = row["Codigo"].ToString(),
                FechaEmision = DateTime.Parse(row["FechaEmision"].ToString()),
                FechaVencimiento = DateTime.Parse(row["FechaVencimiento"].ToString()),
                Activo = (int)(long)row["Activo"] == 1
            };
        }

        public void DesactivarCarnet(int carnetId)
        {
            var sql = "UPDATE Carnets SET Activo = 0 WHERE Id = @id";
            _dbHelper.ExecuteNonQuery(sql, new SQLiteParameter("@id", carnetId));
        }
    }
}
