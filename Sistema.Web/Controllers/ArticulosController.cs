using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Almacen;
using Sistema.Web.Controllers.Models.Almacen.Articulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ArticulosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Distribuidor/listar
        //modelo me refleja la entidad solo con los datos que el usuario requiera
        [Authorize(Roles = "Mecanico, Administrador, Ingeniero")]//autorizacion segin roles
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticuloViewModel>> Listar()//nombre metodo generamos una tarea asincrona y llamamos CategoriaViewModel
        {
           // var distribuidor = await _context.Distribuidor.Include(a => a.categoria).ToListAsync();//objeto llamado categoria ToListAsync:obtenemos la lista del registro _context de la coleccion categorias
            var distribuidor = await _context.Distribuidor.ToListAsync();

            //include porque esta relacionado con la tabla categoria
            return distribuidor.Select(a => new ArticuloViewModel //retorno el objeto siguiendo la estructura CategoriaViewModel
            {
                iddistribuidor = a.iddistribuidor,
                nombre=a.nombre,
                ciudad=a.ciudad,
                telefono=a.telefono ,
                email=a.email
            });
        }

        // GET: api/Distribuidor/Mostrar/1
        [Authorize(Roles = "Mecanico, Administrador,Ingeniero")]//autorizacion segin roles
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)//espera como parametro un id debemos enviarle la url
        {
            var distribuidor = await _context.Distribuidor.FindAsync(id);//FindAsync(id):busca por id
            //var distribuidor = await _context.Distribuidor.Include(a=> a.categoria).
            //    SingleOrDefaultAsync(a=>a.iddistribuidor==id);//FindAsync(id):busca por id 
            if (distribuidor == null)
            {
                return NotFound();// si registro no existe
            }

            return Ok(new ArticuloViewModel
            {//propiedades objeto categoria view model

                iddistribuidor= distribuidor.iddistribuidor,
                nombre=distribuidor.nombre,
                ciudad=distribuidor.ciudad,
                telefono=distribuidor.telefono                    
            }); // existe registro
        }

        //metodo llenar select fabricantes
        // GET: api/Personas/SelectProveedores
        //modelo me refleja la entidad solo con los datos que el fabricante requiera
        [Authorize(Roles = "Mecanico, Administrador,Ingeniero")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticuloViewModel>> SelectProveedores()//nombre metodo generamos una tarea asincrona y llamamos SelectViewModel
        {
            var fa = await _context.Distribuidor.ToListAsync();//objeto llamado categoria ToListAsync:obtenemos la lista del registro _context de la coleccion categorias

            return fa.Select(p => new ArticuloViewModel //retorno el objeto siguiendo la estructura SelectViewModel
            {
                iddistribuidor = p.iddistribuidor,    //informacion a mostrar en el listado
                nombre = p.nombre,
            });
        }

        // PUT: api/Distribuidor/Actualizar
        [Authorize(Roles = "Almacenero, Administrador,Ingeniero")]//autorizacion segin roles
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)///enviamos todo el objeto ActualizarViewModel
        {
            if (!ModelState.IsValid)
            {//valido modelo de data anotation
                return BadRequest(ModelState);
            }

            if (model.iddistribuidor <= 0)// categoria existe
            {
                return BadRequest();
            }

            var distribuidor = await _context.Distribuidor.FirstOrDefaultAsync(a => a.iddistribuidor == model.iddistribuidor);// _context.Repuestos.FirstOrDefaultAsync: devuelve primer registro que encuentre

            if (distribuidor == null)
            {//si no encuntra nada
                return NotFound();
            }
            distribuidor.iddistribuidor = model.iddistribuidor;
            distribuidor.nombre = model.nombre;
            distribuidor.ciudad = model.ciudad; //indico a mi objeto categoria que el nombre va a ser igual al del modelo
            distribuidor.telefono = model.telefono;     

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
        [Authorize(Roles = "Almacenero, Administrador,Ingeniero")]//autorizacion segin roles
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //permite realizar validaciones segun los data annotation
            }

            Distribuidor distribuidor = new Distribuidor //entidad como tal Repuesto
            {
                //distribuidor
                nombre = model.nombre,                
                ciudad = model.ciudad,
                telefono = model.telefono             

            };
            _context.Distribuidor.Add(distribuidor);// me agregue esa categoria

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
        ////Desactivar PUT: api/Distribuidor/Desactivar/1
        //[Authorize(Roles = "Almacenero, Administrador")]//autorizacion segin roles
        //[HttpPut("[action]/{id}")]
        //public async Task<IActionResult> Desactivar([FromRoute] int id)
        //{
        //    if (id <= 0)
        //    {
        //        return BadRequest();
        //    }

        //    var distribuidor = await _context.Distribuidor.FirstOrDefaultAsync(a => a.iddistribuidor == id);// _context.Repuestos.FirstOrDefaultAsync: devuelve primer registro que encuentre

        //    if (distribuidor == null)
        //    {//si no encuntra nada
        //        return NotFound();
        //    }

        //    distribuidor.condicion = false;

        //    try
        //    {
        //        await _context.SaveChangesAsync();//guardamos los cambios
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        // guardar excepcion
        //        return BadRequest();
        //    }

        //    return Ok();
        //}

        ////activar PUT: api/Distribuidor/Activar/1
        //[Authorize(Roles = "Almacenero, Administrador")]//autorizacion segin roles
        //[HttpPut("[action]/{id}")]
        //public async Task<IActionResult> Activar([FromRoute] int id)
        //{

        //    if (id <= 0)
        //    {
        //        return BadRequest();
        //    }

        //    var distribuidor = await _context.Distribuidor.FirstOrDefaultAsync(a => a.iddistribuidor == id);// _context.Repuestos.FirstOrDefaultAsync: devuelve primer registro que encuentre

        //    if (distribuidor == null)
        //    {//si no encuntra nada
        //        return NotFound();
        //    }

        //    distribuidor.condicion = true;

        //    try
        //    {
        //        await _context.SaveChangesAsync();//guardamos los cambios
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        // guardar excepcion
        //        return BadRequest();
        //    }

        //    return Ok();
        //}
        private bool ArticuloExists(int id)
        {
            return _context.Distribuidor.Any(e => e.iddistribuidor == id);
        }
    }
}