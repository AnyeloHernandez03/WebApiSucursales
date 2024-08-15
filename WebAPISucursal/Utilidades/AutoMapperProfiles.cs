using AutoMapper;
using WebAPISucursal.DTOs;
using WebAPISucursal.Models;

namespace WebAPISucursal.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<SucursalCreacionDTO, TblSucursalAh>();
            CreateMap<SucursalActualizacionDTO, TblSucursalAh>().ReverseMap();
            CreateMap<TblSucursalAh, SucursalConsultaDTO>();
            CreateMap<TblMonedaAh, MonedasConsultaDTO>();
        }
    }
}
