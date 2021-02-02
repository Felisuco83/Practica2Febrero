using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCore.Data;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public class RepositoryCochesSQL : IRepositoryCoches
    {
        CochesContext context;
        public RepositoryCochesSQL (CochesContext context)
        {
            this.context = context;
        }
        public Coche BuscarCoche(int idcoche)
        {
            return this.context.Coches.Where(x=> x.IdCoche == idcoche).FirstOrDefault();
        }

        public List<Coche> BuscarCoche(string nombre)
        {
            return this.context.Coches.Where(x => x.Modelo.ToLower().Contains(nombre.ToLower())).ToList();
        }

        public void EliminarCoche(int idcoche)
        {
            Coche coche = this.BuscarCoche(idcoche);
            this.context.Coches.Remove(coche);
            this.context.SaveChanges();
        }

        public List<Coche> GetCoches()
        {
            return this.context.Coches.ToList();
        }

        public void InsertarCoche(Coche coche)
        {
            string sql = "insertarcoche @idcoche, @marca, @modelo, @conductor, @imagen";
            SqlParameter pamidcoche = new SqlParameter("@idcoche", coche.IdCoche);
            SqlParameter pammarca = new SqlParameter("@marca", coche.Marca);
            SqlParameter pammodelo = new SqlParameter("@modelo", coche.Modelo);
            SqlParameter pamconductor = new SqlParameter("@conductor", coche.Conductor);
            SqlParameter pamimagen = new SqlParameter("@imagen", coche.Imagen);
            this.context.Database.ExecuteSqlRaw(sql, pamidcoche, pammarca, pammodelo, pamconductor, pamimagen);
            //create procedure insertarcoche(@idcoche int, @marca NVARCHAR(30), @modelo NVARCHAR(50), @conductor NVARCHAR(50), @imagen NVARCHAR(500))
            //as
            //INSERT INTO COCHES values(@idcoche, @marca, @modelo, @conductor, @imagen)
            //go
        }

        public void ModificarCoche(Coche coche)
        {
            Coche carro = this.BuscarCoche(coche.IdCoche);
            carro.Marca = coche.Marca;
            carro.Modelo = coche.Modelo;
            carro.Conductor = coche.Conductor;
            carro.Imagen = coche.Imagen;
            this.context.SaveChanges();
        }
    }
}
