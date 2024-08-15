using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPISucursal.DTOs;
using WebAPISucursal.Models;

namespace WebAPISucursal.Controllers
{
    [ApiController]
    [Route("api/sucursales")]
    public class SucursalesController : ControllerBase
    {
        private readonly TestDBContext context;
        public IMapper Mapper { get; }

        public SucursalesController(TestDBContext context, IMapper Mapper)
        {
            this.context = context;
            this.Mapper = Mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<SucursalConsultaDTO>>> Get()
        {
           var secursales = await context.TblSucursalAhs.ToListAsync();

            return Mapper.Map<List<SucursalConsultaDTO>>(secursales);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SucursalConsultaDTO>> Get(int id)
        {
            var secursales = await context.TblSucursalAhs.FirstOrDefaultAsync(x => x.SucId == id);
            return Mapper.Map<SucursalConsultaDTO>(secursales);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SucursalCreacionDTO sucursalCreacion)
        {
          
            var sucursal = Mapper.Map<TblSucursalAh>(sucursalCreacion);
            context.Add(sucursal);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromBody]  SucursalActualizacionDTO actualizacionDTO, int id)
        {
            if (actualizacionDTO.SucId != id)
            {
                return BadRequest("El id de la sucursal no conincide con el id de la URl");
            }

            var ExisteSucursal = await context.TblSucursalAhs.AnyAsync(x => x.SucId == id);
            if (!ExisteSucursal)
            {
                return NotFound();
            }

            var sucrusal = Mapper.Map<TblSucursalAh>(actualizacionDTO);
            context.Update(sucrusal);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ExisteSucursal = await context.TblSucursalAhs.AnyAsync(x => x.SucId == id);
            if(!ExisteSucursal) 
            {
                return NotFound();
            }
            context.Remove(new TblSucursalAh() { SucId = id });
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<SucursalActualizacionDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var SucursalDB = await context.TblSucursalAhs.FirstOrDefaultAsync(x => x.SucId == id);

            if (SucursalDB == null)
            {
                return NotFound(id);
            }

            var SucursalDTO = Mapper.Map<SucursalActualizacionDTO>(SucursalDB);

            patchDocument.ApplyTo(SucursalDTO, ModelState); // se aplican los cambio del patch 

            var esvalido = TryValidateModel(SucursalDTO);

            if (!esvalido)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(SucursalDTO, SucursalDB);

            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
