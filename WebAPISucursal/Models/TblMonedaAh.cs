using System;
using System.Collections.Generic;

namespace WebAPISucursal.Models
{
    public partial class TblMonedaAh
    {
        public TblMonedaAh()
        {
            TblSucursalAhs = new HashSet<TblSucursalAh>();
        }

        public int MonId { get; set; }
        public string MonDescripcion { get; set; }
        public bool? MonActivo { get; set; }

        public virtual ICollection<TblSucursalAh> TblSucursalAhs { get; set; }
    }
}
