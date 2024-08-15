using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPISucursal.DTOs;
using WebAPISucursal.Models;

namespace WebAPISucursal.Controllers
{
    [ApiController]
    [Route("api/monedas")]
    public class MonedasController: ControllerBase
    {
        private readonly TestDBContext context;
        public IMapper Mapper { get; }
        public MonedasController(TestDBContext context, IMapper Mapper)
        {
            this.context = context;
            this.Mapper = Mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<MonedasConsultaDTO>>> Get()
        {
            var monedas = await context.TblMonedaAhs.Include(x => x.TblSucursalAhs).ToListAsync();
            return Mapper.Map<List<MonedasConsultaDTO>>(monedas);
        }
    }
}
