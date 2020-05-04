using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Almacen;
using Sistema.Web.Controllers.Models.Almacen.Equipo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public EquiposController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Distribuidor/listar
        //modelo me refleja la entidad solo con los datos que el usuario requiera
        // [Authorize(Roles = "Mecanico, Administrador")]//autorizacion segin roles
        [HttpGet("[action]")]
        public async Task<IEnumerable<EquipoViewModel>> Listar()//nombre metodo generamos una tarea asincrona y llamamos CategoriaViewModel
        {
            // var articulo = await _context.Distribuidor.Include(a => a.categoria).ToListAsync();//objeto llamado categoria ToListAsync:obtenemos la lista del registro _context de la coleccion categorias
            var equipos = await _context.equipo.ToListAsync();

            //include porque esta relacionado con la tabla categoria
            return equipos.Select(a => new EquipoViewModel //retorno el objeto siguiendo la estructura CategoriaViewModel
            {

                idequipos = a.idequipos,
                idfabricante = a.idfabricante,
                serie=a.serie,
                nombre = a.nombre,
                marca = a.marca,
                voltaje = a.voltaje,
                costo = a.costo,
                year_adquisicion = a.year_adquisicion,
                descripcion = a.descripcion,
                estado = a.estado
             
            });
        }

        //metodo llenar select Equipos
        // GET: api/Personas/SelectProveedores
        //modelo me refleja la entidad solo con los datos que el usuario requiera
        [Authorize(Roles = "Mecanico, Administrador,Ingeniero")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> SelectEquipos()//nombre metodo generamos una tarea asincrona y llamamos SelectViewModel
        {
            var fa = await _context.equipo.ToListAsync();//objeto llamado categoria ToListAsync:obtenemos la lista del registro _context de la coleccion categorias

            return fa.Select(p => new SelectViewModel //retorno el objeto siguiendo la estructura SelectViewModel
            {
                idequipos = p.idequipos,    //informacion a mostrar en el listado
                nombre = p.nombre,

            });
        }

        // GET: api/Distribuidor/Mostrar/1
        // [Authorize(Roles = "Mecanico, Administrador")]//autorizacion segin roles
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)//espera como parametro un id debemos enviarle la url
        {
            var equipo = await _context.equipo.FindAsync(id);//FindAsync(id):busca por id 

            //var articulo = await _context.Distribuidor.Include(a=> a.categoria).
            //    SingleOrDefaultAsync(a=>a.idarticulo==id);//FindAsync(id):busca por id 

            if (equipo == null)
            {
                return NotFound();// si registro no existe
            }

            return Ok(new EquipoViewModel
            {//propiedades objeto categoria view model

                idfabricante = equipo.idfabricante,
                idequipos=equipo.idequipos,
                serie = equipo.serie,
                nombre = equipo.nombre,
                marca = equipo.marca,
                voltaje = equipo.voltaje,
                costo = equipo.costo,
                year_adquisicion = equipo.year_adquisicion,
                descripcion = equipo.descripcion,
                estado = equipo.estado
                   
            }); // existe registro
        }


        // PUT: api/Distribuidor/Actualizar
       // [Authorize(Roles = " Administrador")]//autorizacion segin roles
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)///enviamos todo el objeto ActualizarViewModel
        {
            if (!ModelState.IsValid)//valido modelo de data anotation
            {
                return BadRequest(ModelState);
            }

            if (model.idequipos<= 0)// categoria existe
            {
                return BadRequest();
            }

            var equipo = await _context.equipo.FirstOrDefaultAsync(a => a.idequipos == model.idequipos);// _context.Repuestos.FirstOrDefaultAsync: devuelve primer registro que encuentre

            if (equipo == null)
            {//si no encuntra nada
                return NotFound();
                
            }
            equipo.idequipos = model.idequipos;
            equipo.idfabricante = model.idfabricante;
            equipo.serie = model.serie;
            equipo.nombre = model.nombre;//indico a mi objeto categoria que el nombre va a ser igual al del modelo
            equipo.marca = model.marca;
            equipo.voltaje = model.voltaje;
            equipo.costo = model.costo;
            equipo.year_adquisicion = model.year_adquisicion;
            equipo.descripcion = model.descripcion;
            equipo.estado = model.estado;
          

            try //captura excepcions
            {
                await _context.SaveChangesAsync();//guardamos los cambios
            }
            catch (DbUpdateConcurrencyException)
            {
                // guardar excepcion
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/Distribuidor/Crear
       // [Authorize(Roles = "Mecanico, Administrador")]//autorizacion segin roles
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //permite realizar validaciones segun los data annotation
            }

            Equipos equipo = new Equipos //entidad como tal Repuesto
            {

                //equipos
                idfabricante=model.idfabricante,//indico a mi objeto categoria que el nombre va a ser igual al del modelo
                serie =model.serie,
                nombre = model.nombre,
                marca= model.marca,
                voltaje = model.voltaje,
                costo=model.costo,
                year_adquisicion=model.year_adquisicion,
                descripcion=model.descripcion,
                estado=model.estado             

            };
            _context.equipo.Add(equipo);// me agregue esa categoria

            try
            {
                await _context.SaveChangesAsync();//guarda los cambios
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }
        
      //Desactivar PUT: api/Repuestos/Desactivar/1
      [HttpPut("[action]/{id}")]
      public async Task<IActionResult> Desactivar([FromRoute] int id)
      {
          if (id <=0)
          {
              return BadRequest();
          }

          var repuesto = await _context.equipo.FirstOrDefaultAsync(c => c.idequipos == id);// _context.Repuestos.FirstOrDefaultAsync: devuelve primer registro que encuentre

          if (repuesto == null)
          {//si no encuntra nada
              return NotFound();
          }
          repuesto.estado = false;
          try
          {
              await _context.SaveChangesAsync();//guardamos los cambios
          }
          catch (DbUpdateConcurrencyException)
          {
              // guardar excepcion
              return BadRequest();
          }

          return Ok();
      }

      //activar PUT: api/Repuestos/Activar/1
      [HttpPut("[action]/{id}")]
      public async Task<IActionResult> Activar([FromRoute] int id)
      {

          if (id <= 0)
          {
              return BadRequest();
          }

          var repuesto = await _context.equipo.FirstOrDefaultAsync(c => c.idequipos == id);// _context.Repuestos.FirstOrDefaultAsync: devuelve primer registro que encuentre

          if (repuesto == null)
          {//si no encuntra nada
              return NotFound();
          }

          repuesto.estado = true;

          try
          {
              await _context.SaveChangesAsync();//guardamos los cambios
          }
          catch (DbUpdateConcurrencyException)
          {
              // guardar excepcion
              return BadRequest();
          }

          return Ok();
      }  

        private bool EquiposExists(int id)
        {
            return _context.equipo.Any(e => e.idequipos == id);
        }
    }
}