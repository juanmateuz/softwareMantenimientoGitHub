using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Ventas;
using Sistema.Web.Controllers.Models.Ventas.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public PersonasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Personas/listarClientes
        //modelo me refleja la entidad solo con los datos que el persona requiera
        [Authorize(Roles = "Mecanico, Administrador")]//autorizacion segin roles
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaViewModel>> ListarCliente()//nombre metodo generamos una tarea asincrona y llamamos CategoriaViewModel
        {
            var persona = await _context.Personas.Where(p => p.tipo_persona=="Cliente").ToListAsync();//objeto llamado categoria ToListAsync:obtenemos la lista del registro _context de la coleccion categorias
            //include porque esta relacionado con la tabla categoria
            return persona.Select(p => new PersonaViewModel //retorno el objeto siguiendo la estructura CategoriaViewModel
            {
                idpersona = p.idpersona,
                tipo_persona = p.tipo_persona,
                nombre = p.nombre,
                tipo_documento = p.tipo_documento,
                num_documento = p.num_documento,
                direccion = p.direccion,
                telefono = p.telefono,
                email = p.email
            });
        }

        // GET: api/Personas/listarProovedores
        //modelo me refleja la entidad solo con los datos que el usuario requiera
        [Authorize(Roles = "Almacenero, Administrador")]//autorizacion segin roles
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaViewModel>> ListarProveedores()//nombre metodo generamos una tarea asincrona y llamamos CategoriaViewModel
        {
            var persona = await _context.Personas.Where(p => p.tipo_persona == "Proveedor").ToListAsync();//objeto llamado categoria ToListAsync:obtenemos la lista del registro _context de la coleccion categorias
            //include porque esta relacionado con la tabla categoria
            return persona.Select(p => new PersonaViewModel //retorno el objeto siguiendo la estructura CategoriaViewModel
            {
                idpersona = p.idpersona,
                tipo_persona = p.tipo_persona,
                nombre = p.nombre,
                tipo_documento = p.tipo_documento,
                num_documento = p.num_documento,
                direccion = p.direccion,
                telefono = p.telefono,
                email = p.email
            });
        }


        //metodo llenar select proveedores
        // GET: api/Personas/SelectProveedores
        //modelo me refleja la entidad solo con los datos que el usuario requiera
        [Authorize(Roles = "Almacenero, Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> SelectProveedores()//nombre metodo generamos una tarea asincrona y llamamos SelectViewModel
        {
            var persona = await _context.Personas.Where(p => p.tipo_persona =="Proveedor").ToListAsync();//objeto llamado categoria ToListAsync:obtenemos la lista del registro _context de la coleccion categorias

            return persona.Select(p => new SelectViewModel //retorno el objeto siguiendo la estructura SelectViewModel
            {
                idpersona = p.idpersona,//informacion a mostrar en el listado
                nombre = p.nombre,

            });
        }

        // POST: api/Personas/Crear
        [Authorize(Roles = "Mecanico,Vendedor, Administrador")]//autorizacion segin roles
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //permite realizar validaciones segun los data annotation
            }

            //Validar email no repetido
            var email = model.email.ToLower();

            if (await _context.Personas.AnyAsync(p => p.email == email))//u.email obtengo el email del context
            {
                return BadRequest("El email ya existe");
            }

            //llamo a metodo para crear las claves


            Persona persona = new Persona //entidad como tal Repuesto
            {
                tipo_persona = model.tipo_persona,
                nombre = model.nombre,
                tipo_documento = model.tipo_documento,
                num_documento = model.num_documento,
                direccion = model.direccion,
                telefono = model.telefono,
                email = model.email.ToLower(),
              
            };
            _context.Personas.Add(persona);// me agregue esa categoria
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

        // PUT: api/Persona/Actualizar
        [Authorize(Roles = "Mecanico,Vendedor, Administrador")]//autorizacion segin roles
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idpersona<= 0)
            {
                return BadRequest();
            }

            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.idpersona == model.idpersona);

            if (persona== null) //si usuario no existe
            {
                return NotFound();
            }

            persona.tipo_persona = model.tipo_persona;
            persona.nombre = model.nombre;
            persona.tipo_documento = model.tipo_documento;
            persona.num_documento = model.num_documento;
            persona.direccion = model.direccion;
            persona.telefono = model.telefono;
            persona.email = model.email.ToLower();


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }
        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.idpersona == id);
        }
    }
}