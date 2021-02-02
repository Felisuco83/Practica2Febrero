using MvcCore.Helpers;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCore.Repositories
{
    public class RepositoryCochesXML : IRepositoryCoches
    {
        PathProvider pathprovider;
        private XDocument docxml;
        private string path;
        public RepositoryCochesXML(PathProvider pathprovider)
        {
            this.pathprovider = pathprovider;
            this.path = this.pathprovider.MapPath("coches.xml", Folders.Documents);
            this.docxml = XDocument.Load(path);
        }

        public List<Coche> GetCoches()
        {
            var consulta = from datos in this.docxml.Descendants("coche")
                           select new Coche
                           {
                               IdCoche = int.Parse(datos.Element("idcoche").Value),
                               Marca = datos.Element("marca").Value,
                               Modelo = datos.Element("modelo").Value,
                               Conductor = datos.Element("conductor").Value,
                               Imagen = datos.Element("imagen").Value,
                           };
            return consulta.ToList();
        }

        public Coche BuscarCoche(int idcoche)
        {
            var consulta = from datos in this.docxml.Descendants("coche")
                           where datos.Element("idcoche").Value == idcoche.ToString()
                           select new Coche
                           {
                               IdCoche = int.Parse(datos.Element("idcoche").Value),
                               Marca = datos.Element("marca").Value,
                               Modelo = datos.Element("modelo").Value,
                               Conductor = datos.Element("conductor").Value,
                               Imagen = datos.Element("imagen").Value,
                           };
            return consulta.FirstOrDefault();
        }

        public List<Coche> BuscarCoche(string nombre)
        {
            var consulta = from datos in this.docxml.Descendants("coche")
                           where datos.Element("modelo").Value.ToLower().Contains(nombre.ToLower())
                           select new Coche
                           {
                               IdCoche = int.Parse(datos.Element("idcoche").Value),
                               Marca = datos.Element("marca").Value,
                               Modelo = datos.Element("modelo").Value,
                               Conductor = datos.Element("conductor").Value,
                               Imagen = datos.Element("imagen").Value,
                           };
            return consulta.ToList();
        }

        public void EliminarCoche(int idcoche)
        {
            var consulta = from datos in this.docxml.Descendants("coche")
                           where datos.Element("idcoche").Value == idcoche.ToString()
                           select datos;
            XElement elementalumno = consulta.FirstOrDefault();
            elementalumno.Remove();
            this.docxml.Save(this.path);
        }
        public void InsertarCoche(Coche coche)
        {
            XElement elementdepartamento = new XElement("coche");
            XElement idcoche = new XElement("idcoche", coche.IdCoche);
            XElement marca = new XElement("marca", coche.Marca);
            XElement modelo = new XElement("modelo", coche.Modelo);
            XElement conductor = new XElement("conductor", coche.Conductor);
            XElement imagen = new XElement("imagen", coche.Imagen);
            elementdepartamento.Add(idcoche);
            elementdepartamento.Add(marca);
            elementdepartamento.Add(modelo);
            elementdepartamento.Add(conductor);
            elementdepartamento.Add(imagen);
            this.docxml.Element("coches").Add(elementdepartamento);
            this.docxml.Save(this.path);
        }

        public void ModificarCoche(Coche coche)
        {
            var consulta = from datos in this.docxml.Descendants("coche")
                           where datos.Element("idcoche").Value == coche.IdCoche.ToString()
                           select datos;
            XElement element = consulta.FirstOrDefault();
            element.Element("idcoche").Value = coche.IdCoche.ToString();
            element.Element("marca").Value = coche.Marca;
            element.Element("modelo").Value = coche.Modelo;
            element.Element("conductor").Value = coche.Conductor;
            element.Element("imagen").Value = coche.Imagen;
            this.docxml.Save(this.path);
        }
    }
}
