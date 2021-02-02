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
    public class RepositoryCochesMySQL : IRepositoryCoches
    {
        CochesContext context;
        public RepositoryCochesMySQL(CochesContext context)
        {
            this.context = context;
        }
        public Coche BuscarCoche(int idcoche)
        {
            return this.context.Coches.Where(x => x.IdCoche == idcoche).FirstOrDefault();
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
            this.context.Coches.Add(coche);
            this.context.SaveChanges();
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
