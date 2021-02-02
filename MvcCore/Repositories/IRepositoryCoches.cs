using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public interface IRepositoryCoches
    {
        List<Coche> GetCoches();

        Coche BuscarCoche(int idcoche);

        List<Coche> BuscarCoche(string nombre);

        void EliminarCoche(int idcoche);

        void InsertarCoche(Coche coche);

        void ModificarCoche(Coche coche);
    }
}
