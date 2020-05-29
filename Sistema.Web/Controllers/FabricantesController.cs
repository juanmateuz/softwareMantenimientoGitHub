using Microsoft.AspNetCore.Authorization;  //autorizacion token
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Almacen;
using Sistema.Web.Controllers.Models.Ventas.Fabricante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricantesController : ControllerBase
    {
        private readonly DbContextSistema baseDatos;

        public FabricantesController(DbContextSistema context)
        {
            baseDatos = context;
        }

        // GET: api/Fabricantes/Listar
        //modelo me refleja la entidad solo con los datos que el usuario requiera
        [Authorize(Roles = "Mecanico, Administrador,Ingeniero")]//autorizacion segun roles
        [HttpGet("[action]")]
        public async Task<IEnumerable<FabricanteViewModel>> Listar()//nombre metodo generamos una tarea asincrona y llamamos CategoriaViewModel
        {
            // var articulo = await baseDatos.Distribuidor.Include(a => a.categoria).ToListAsync();//objeto llamado categoria ToListAsync:obtenemos la lista del registro baseDatos de la coleccion categorias
            var fabricante = await baseDatos.Fabricantes.ToListAsync();

            //include porque esta relacionado con la tabla categoria
            return fabricante.Select(f => new FabricanteViewModel //retorno el objeto siguiendo la estructura CategoriaViewModel
            {                
                idfabricante = f.idfabricante,                
                nombre = f.nombre,
                pais = f.pais,
                telefono = f.telefono,
                email=f.email            
            });
        }

        // GET: api/Fabricantes/Mostrar/1
        [Authorize(Roles = "Mecanico, Administrador,Ingeniero")]//autorizacion segin roles
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)//espera como parametro un id debemos enviarle la url
        {
            var fabricante = await baseDatos.Fabricantes.FindAsync(id);//FindAsync(id):busca por id 

            //var articulo = await baseDatos.Distribuidor.Include(a=> a.categoria).
            //    SingleOrDefaultAsync(a=>a.idarticulo==id);//FindAsync(id):busca por id 

            if (fabricante == null)
            {
                return NotFound();// si registro no existe
            }

            return Ok(new FabricanteViewModel
            {//propiedades objeto categoria view model
                idfabricante = fabricante.idfabricante,
                nombre = fabricante.nombre,
                pais = fabricante.pais,
                telefono = fabricante.telefono,
                email=fabricante.email                    
            }); // existe registro
        }

        //metodo llenar select fabricantes
        // GET: api/Personas/SelectProveedores
        //modelo me refleja la entidad solo con los datos que el usuario requiera
        [Authorize(Roles = "Mecanico, Administrador,Ingeniero")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<FabricanteViewModel>> SelectProveedores()//nombre metodo generamos una tarea asincrona y llamamos SelectViewModel
        {
            var fa = await baseDatos.Fabricantes.ToListAsync();//objeto llamado categoria ToListAsync:obtenemos la lista del registro baseDatos de la coleccion categorias

            return fa.Select(p => new FabricanteViewModel //retorno el objeto siguiendo la estructura SelectViewModel
            {
                idfabricante = p.idfabricante,    //informacion a mostrar en el listado
                nombre = p.nombre,
            });
        }
        // PUT: api/Fabricantes/Actualizar
        [Authorize(Roles = "Mecanico, Administrador")]//autorizacion segin roles
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)///enviamos todo el objeto ActualizarViewModel
        {
            if (!ModelState.IsValid)//valido modelo de data anotation
            {
                return BadRequest(ModelState);
            }

            if (model.idfabricante <= 0)// categoria existe
            {
                return BadRequest();
            }

            var fabricante = await baseDatos.Fabricantes.FirstOrDefaultAsync(f => f.idfabricante == model.idfabricante);
            // baseDatos.Repuestos.FirstOrDefaultAsync: devuelve primer registro que encuentre

            if (fabricante == null)
            {//si no encuntra nada
                return NotFound();
            }

            fabricante.idfabricante = model.idfabricante;
            fabricante.nombre = model.nombre;
            fabricante.pais = model.pais; //indico a mi objeto categoria que el nombre va a ser igual al del modelo
            fabricante.telefono = model.telefono;
            fabricante.email = model.email;

            try //captura excepcions
            {
                await baseDatos.SaveChangesAsync();//guardamos los cambios
            }
            catch (DbUpdateConcurrencyException)
            {
                // guardar excepcion
                return BadRequest();
            }
            return Ok();
        }

        // POST: api/Fbricantes/Crear
        [Authorize(Roles = "Mecanico, Administrador")]//autorizacion segin roles
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //permite realizar validaciones segun los data annotation
            }

            Fabricante fabricantes = new Fabricante //entidad como tal Repuesto
            {
                //Fabricante
                nombre = model.nombre,
                pais= model.pais,
                telefono = model.telefono,
                email=model.email
            };
            baseDatos.Fabricantes.Add(fabricantes);// me agregue esa categoria

            try
            {
                await baseDatos.SaveChangesAsync();//guarda los cambios
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok();
        }
        private bool FabricanteExists(int id)
        {
            return baseDatos.Fabricantes.Any(e => e.idfabricante == id);
        }
    }
}