using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPISucursal.Models
{
    public partial class TblSucursalAh
    {
        public int SucId { get; set; }

        [Required(ErrorMessage = "el Campo {0} es requerido")]
        public int? SucCodigo { get; set; }

        [Required(ErrorMessage = "el Campo {0} es requerido")]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres")]
        public string SucDescripcion { get; set; }

        [Required(ErrorMessage = "el Campo {0} es requerido")]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres")]
        public string SucDirrecion { get; set; }

        [Required(ErrorMessage = "el Campo {0} es requerido")]
        [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres")]
        public string SucIdentificacion { get; set; }

        public DateTime? SucFechaCreacion { get; set; }
        public DateTime? SucFechaModificacion { get; set; }

        [Required(ErrorMessage = "el Campo {0} es requerido")]
        public int? MonId { get; set; }
        public virtual TblMonedaAh Mon { get; set; }
    }
}
