using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Mantenimiento;
using Sistema.Web.Controllers.Models.solicitudMantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public MantenimientosController(DbContextSistema context)
        {
            _context = context;
        }
        // repuestos
        // GET: api/Mantenimientos/Listar
        //modelo me refleja la entidad solo con los datos que el mantenimiento requiera
        [Authorize(Roles = "Administrador,Mecanico,Ingeniero")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SolicitudViewModel>> Listar()//nombre metodo generamos una tarea asincrona y llamamos CategoriaViewModel
        {
            var solicitud = await _context.Mantenimientos.ToListAsync();//objeto llamado model ToListAsync:obtenemos la lista del registro _context de la coleccion Mantenimiento
            Console.WriteLine($"La solicitud is {solicitud} solicitud.");
            return solicitud.Select(s => new SolicitudViewModel //retorno el objeto siguiendo la estructura CategoriaViewModel
            {
                idsolicitud = s.idsolicitud,//informacion a mostrar en el listar
                idequipos = s.idequipos,
                idusuario = s.idusuario,
                fechasolicitud = s.fechasolicitud,
                equipo = s.equipo,
                tipomante = s.tipomante,
                solicitado_por = s.solicitado_por,
                recibido_por = s.recibido_por,
                prioridad = s.prioridad,
                descripcion_ingeniero = s.descripcion_ingeniero,
                descripcion_mecanico = s.descripcion_mecanico,
                estado2 = s.estado2,
                estado = s.estado
            });
        }

        //metodo llenar select model
        //GET: api/Mantenimientos/listar
        //modelo me refleja la entidad solo con los datos que el mantenimiento requiera
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()//nombre metodo generamos una tarea asincrona y llamamos SelectViewModel
        {
            var solicitud = await _context.Mantenimientos.Where(c => c.equipo == "juan").ToListAsync();//objeto llamado model ToListAsync:obtenemos la lista del registro _context de la coleccion Mantenimiento

            return solicitud.Select(c => new SelectViewModel //retorno el objeto siguiendo la estructura SelectViewModel
            {
                idsolicitud = c.idsolicitud,//informacion a mostrar en el listado
                idequipos = c.idequipos,
                equipo = c.equipo
            });
        }

        // GET: api/Repuestos/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<SolicitudViewModel>> Mostrar([FromRoute] string[] id)//espera como parametro un id debemos enviarle la url
        {
            var a = "";
            Console.WriteLine("--------------------------------------------------------------------");
            foreach (string i in id)
            {
                a = (i);
            }
            String[] parts = a.Split(",");
            String id2 = parts[0];
            String rol2 = parts[1];

            Console.WriteLine(id2);
            Console.WriteLine(rol2);

            if (rol2 == "Mecanico")
            {
                //FirstOrDefaultAsync(s => s.idsolicitud == id)
                var solicitud = await _context.Mantenimientos.Where(i => i.ejecuta == id2)
                                              .ToListAsync();//FindAsync(id):busca por id 
                return solicitud.Select(i => new SolicitudViewModel
                {//propiedades objeto model view model
                    idsolicitud = i.idsolicitud,//informacion a mostrar en el listad
                    idequipos = i.idequipos,
                    idusuario = i.idusuario,
                    fechasolicitud = i.fechasolicitud,
                    equipo = i.equipo,
                    tipomante = i.tipomante,
                    solicitado_por = i.solicitado_por,
                    recibido_por = i.recibido_por,
                    prioridad = i.prioridad,
                    descripcion_ingeniero = i.descripcion_ingeniero,
                    descripcion_mecanico = i.descripcion_mecanico,
                    estado2 = i.estado2,
                    estado = i.estado,
                    ejecuta = i.ejecuta
                }); // existe registro
            }else
            {
                //FirstOrDefaultAsync(s => s.idsolicitud == id)
                var solicitud = await _context.Mantenimientos.Where(i => i.recibido_por == id2 || i.solicitado_por == id2)
                      .ToListAsync();//FindAsync(id):busca por id 
                var solicitu = _context.Mantenimientos.Where(i => i.recibido_por == id2 && i.estado2 == "Creado").Count();
                Console.WriteLine("--------------------------------------------------------------------");
                Console.WriteLine($"conteo {solicitu} filas.");
                return solicitud.Select(i => new SolicitudViewModel
                {//propiedades objeto model view model
                    idsolicitud = i.idsolicitud,//informacion a mostrar en el listad
                    idequipos = i.idequipos,
                    idusuario = i.idusuario,
                    fechasolicitud = i.fechasolicitud,
                    equipo = i.equipo,
                    tipomante = i.tipomante,
                    solicitado_por = i.solicitado_por,
                    recibido_por = i.recibido_por,
                    prioridad = i.prioridad,
                    descripcion_ingeniero = i.descripcion_ingeniero,
                    descripcion_mecanico = i.descripcion_mecanico,
                    estado2 = i.estado2,
                    estado = i.estado,
                    conteo = solicitu,
                    ejecuta = i.ejecuta

                }); // existe registro
            }
        }

        // PUT: api/Repuestos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)///enviamos todo el objeto ActualizarViewModel
        {
            if (!ModelState.IsValid)//valido modelo de data anotation
            {
                return BadRequest(ModelState);
            }

            if (model.idsolicitud <= 0)// model existe
            {
                return base.BadRequest();
            }

            var solicitud = await _context.Mantenimientos.FirstOrDefaultAsync(c => c.idsolicitud == model.idsolicitud);// _context.Repuestos.FirstOrDefaultAsync: devuelve primer registro que encuentre

            if (solicitud == null)
            {//si no encuntra nada
                return NotFound();
            }
            // solicitud.idsolicitud = model.idsolicitud;//informacion a mostrar en el listad
            solicitud.idequipos = model.idequipos;
            solicitud.idusuario = model.idusuario;
            solicitud.fechasolicitud = model.fechasolicitud;
            solicitud.equipo = model.equipo;
            solicitud.tipomante = model.tipomante;
            solicitud.solicitado_por = model.solicitado_por;
            solicitud.recibido_por = model.recibido_por;
            solicitud.prioridad = model.prioridad;
            solicitud.descripcion_ingeniero = model.descripcion_ingeniero;
            solicitud.descripcion_mecanico = model.descripcion_mecanico;
            solicitud.estado2 = model.estado2;
            solicitud.estado = model.estado;
            solicitud.ejecuta = model.ejecuta;

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

        // POST: api/Repuestos/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] crearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //permite realizar validaciones segun los data annotation
            }

            Mantenimiento mantenimiento = new Mantenimiento  //entidad como tal Repuesto
            {
                //informacion a mostrar en el listad
                idequipos = model.idequipos,
                idusuario = model.idusuario,
                fechasolicitud = model.fechasolicitud,
                equipo = model.equipo,
                tipomante = model.tipomante,
                solicitado_por = model.solicitado_por,
                recibido_por = model.recibido_por,
                prioridad = model.prioridad,
                descripcion_ingeniero = model.descripcion_ingeniero,
                descripcion_mecanico = model.descripcion_mecanico,
                estado2 = model.estado2,
                estado = model.estado
            };
            _context.Mantenimientos.Add(mantenimiento);// me agregue esa model

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

        // DELETE: api/Repuestos/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var solicitud = await _context.Mantenimientos.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }
            _context.Mantenimientos.Remove(solicitud);
            try
            {
                await _context.SaveChangesAsync();//guarda datos
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok(solicitud);
        }

        //Desactivar PUT: api/Mantenimientos/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var model = await _context.Mantenimientos.FirstOrDefaultAsync(s => s.idsolicitud == id);// _context.Repuestos.FirstOrDefaultAsync: devuelve primer registro que encuentre

            if (model == null)
            {//si no encuntra nada
                return NotFound();
            }

            model.estado = false;

            try
            {
                await _context.SaveChangesAsync();//guardamos los cambios
            }
            catch (DbUpdateConcurrencyException)
            {                
                return BadRequest();// guardar excepcion
            }

            return Ok();
        }

        //activar PUT: api/Mantenimientos/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var model = await _context.Mantenimientos.FirstOrDefaultAsync(s => s.idsolicitud == id);// _context.Repuestos.FirstOrDefaultAsync: devuelve primer registro que encuentre

            if (model == null)
            {
                return NotFound();//si no encuntra nada
            }

            model.estado = true;
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
        private bool MantenimientoExists(int id)
        {
            return _context.Mantenimientos.Any(e => e.idsolicitud == id);
        }
    }
}