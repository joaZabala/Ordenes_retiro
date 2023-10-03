using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Entidades
{
    public class OrdenRetiro
    {
        public int Nro_orden { get; set; }
        public DateTime Fecha { get; set; }
        public string Responsable { get; set; }

        public List<DetalleOrden> Detalles { get; set; }

        public OrdenRetiro()
        {
              Detalles=new List<DetalleOrden>();
        }

        public void AgregarDetalle(DetalleOrden detalle)
        {
            Detalles.Add(detalle);
        }

        public void EliminarDetalle(int index)
        {
            Detalles.RemoveAt(index);
        }



    }
}
