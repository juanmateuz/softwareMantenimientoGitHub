using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Almacen;
using Sistema.Web.Controllers.Models.Almacen.Repuesto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepuestoesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public RepuestoesController(DbContextSistema context)
        {
            _context = context;
        }

        // Repuestos ---- //modelo me refleja la entidad solo con los datos que el repuesto requiera
        // GET: api/Repuestoes/listar
        [Authorize(Roles = "Administrador,Ingeniero,Mecanico")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<RepuestoViewModel>> Listar()//nombre metodo generamos una tarea asincrona y llamamos repuestoViewModel
        {
            var repuesto = await _context.Repuestos.Include(d => d.distribuidor).Include(e => e.equipo).ToListAsync();//objeto llamado repuesto ToListAsync:obtenemos la lista del registro _context de la coleccion repuestos

            return repuesto.Select(r => new RepuestoViewModel //retorno el objeto siguiendo la estructura repuestoViewModel
            {
                idrepuestos = r.idrepuestos,//informacion a mostrar en el listar
                iddistribuidor = r.iddistribuidor,
                idequipo = r.idequipos,
                tipo = r.tipo,
                nombre = r.nombre,
                nombreDistribuidor = r.distribuidor.nombre,
                nombreEquipo = r.equipo.nombre,
                referencia = r.referencia,
                cantidad = r.cantidad,
                stockminimo = r.stockminimo
            });
        }

        //metodo llenar select repuesto
        // GET: api/Repuestoes/Select        
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()//nombre metodo generamos una tarea asincrona y llamamos SelectViewModel
        {
            var repuesto = await _context.Repuestos.Where(c => c.nombre == "juan").ToListAsync();//objeto llamado repuesto ToListAsync:obtenemos la lista del registro _context de la coleccion repuestos

            return repuesto.Select(c => new SelectViewModel //retorno el objeto siguiendo la estructura SelectViewModel
            {
                idrepuestos = c.idrepuestos,//informacion a mostrar en el listado
                nombre = c.nombre
            });
        }

        // GET: api/Repuestoes/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)//espera como parametro un id debemos enviarle la url
        {

            var repuesto = await _context.Repuestos.Include(d => d.distribuidor).
                SingleOrDefaultAsync(d=> d.idrepuestos==id);//busca por id 
            if (repuesto == null)
            {
                return NotFound();// si registro no existe
            }

            return Ok(new RepuestoViewModel            {
                //propiedades objeto repuesto view model
                idrepuestos = repuesto.idrepuestos,//informacion a mostrar en el listado
                tipo = repuesto.tipo,
                nombre = repuesto.nombre,
                referencia = repuesto.referencia,
                cantidad = repuesto.cantidad,
                stockminimo = repuesto.stockminimo
            }); //Existe registro
        }

        // PUT: api/Repuestoes/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)///enviamos todo el objeto ActualizarViewModel
        {
            if (!ModelState.IsValid)//valido modelo de data anotation
            {
                return BadRequest(ModelState);
            }

            if (model.idrepuestos <= 0)//Repuesto existe
            {
                return BadRequest();
            }

            var repuesto = await _context.Repuestos.FirstOrDefaultAsync(c => c.idrepuestos == model.idrepuestos);// _context.Repuestos.FirstOrDefaultAsync: devuelve primer registro que encuentre

            if (repuesto == null)
            {
                return NotFound();//si no encuntra nada
            }

            if (repuesto.stockminimo>=repuesto.cantidad && repuesto.correoEnviado)
            {
                repuesto.correoEnviado = false;
            }

            repuesto.idequipos = model.idequipos;
            repuesto.tipo = model.tipo;
            repuesto.nombre = model.nombre; //indico a mi objeto repuesto que el nombre va a ser igual al del modelo
            repuesto.referencia = model.referencia;
            repuesto.cantidad = model.cantidad;
            repuesto.stockminimo = model.stockminimo;
            try //captura excepcion
            {
                await _context.SaveChangesAsync();//guardamos los cambios
            }
            catch (DbUpdateConcurrencyException)
            {               
                return BadRequest(); // guardar excepcion
            }
            return Ok();
        }

        // POST:api/Repuestoes/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //permite realizar validaciones segun los data annotation
            }

            Repuesto repuesto = new Repuesto  //entidad como tal Repuesto
            {
                iddistribuidor = model.iddistribuidor,
                idequipos = model.idequipos,
                tipo = model.tipo,
                nombre = model.nombre, //le envio lo que tengo en el objeto model que esta en json 
                referencia = model.referencia,
                cantidad = model.cantidad,
                stockminimo = model.stockminimo
            };
            _context.Repuestos.Add(repuesto);// me agregue ese repuesto

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

        // DELETE: api/Repuestoes/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var repuesto = await _context.Repuestos.FindAsync(id);
            if (repuesto == null)
            {
                return NotFound();
            }

            _context.Repuestos.Remove(repuesto);

            try
            {
                await _context.SaveChangesAsync();//Guarda datos
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(repuesto);
        }
            int cantidadFaltante;
            string nombreRepuesto = "";
            string referencia = "";
        //Post: api/Repuestoes/sinStock/id 
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> sinstock([FromRoute] int id) {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var repuesto = await _context.Repuestos.FindAsync(id);
            if (repuesto.cantidad < repuesto.stockminimo && !repuesto.correoEnviado)
            {
                Console.WriteLine("-----------------Repuesto sin stock-----------------------");
                nombreRepuesto = repuesto.nombre;
                referencia = repuesto.referencia;
                cantidadFaltante = repuesto.stockminimo - repuesto.cantidad;
                Email();

                if (repuesto == null)
                {//si no encuntra nada
                    return NotFound();
                }

                repuesto.correoEnviado = true;
                try
                {
                    await _context.SaveChangesAsync();//guardamos los cambios
                }
                catch (DbUpdateConcurrencyException)
                {                   
                    return BadRequest(); // guardar excepcion
                }

            }else
            {
                Console.WriteLine("Correo ya ha sido enviado");
            }           
            return Ok(repuesto);
        }
        
        public ActionResult Email()
        {            
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("juanmateuz@gmail.com", "juanmateuz21*");
                // Parte 2
                MailMessage mm = new MailMessage();
                mm.IsBodyHtml = true;
                mm.Priority = MailPriority.Normal;
                mm.From = new MailAddress("mantenimiento@panaderialavictoria.com.co");
                mm.Subject = "Alerta Stock Mantenimiento";
                mm.Body = "<h1>Cordial saludo</h1>";
                mm.Body += "<p> <h3>El repuesto " + nombreRepuesto + " referencia "+ referencia+ " esta agotado </h3></p>";
                mm.Body += "<p> <h3>Cantidad faltante para stock minimo: " + cantidadFaltante+" </h3></p>";                
                mm.To.Add(new MailAddress("juan.380172809@ucaldas.edu.co"));
                try
                {
                    smtp.Send(mm); // Enviar el mensaje
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }            
                // Parte 1
                Console.WriteLine("juan");
                return RedirectToAction("Index");
        }

        private bool RepuestoExists(int id)
        {
            return _context.Repuestos.Any(e => e.idrepuestos == id);
        }
    }
}