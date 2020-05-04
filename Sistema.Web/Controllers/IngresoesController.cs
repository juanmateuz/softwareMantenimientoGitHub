using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Almacen;
using Sistema.Web.Controllers.Models.Almacen.Ingreso;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresoesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public IngresoesController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Ingresoes/listar
        //modelo me refleja la entidad solo con los datos que el usuario requiera
        [Authorize(Roles = "Mecanico, Administrador")]//autorizacion segin roles
        [HttpGet("[action]")]
        public async Task<IEnumerable<IngresoViewModel>> Listar()//nombre metodo generamos una tarea asincrona y llamamos IngresoViewModel
        {
            //(i => i.usuario).Include(i => i.persona) obtienen nombre usuario y nombre persona
            var ingreso = await _context.Ingresos.Include(i => i.usuario).Include(i => i.persona)
                .OrderByDescending(i=> i.idingreso)//objeto llamado categoria ToListAsync:obtenemos la lista del registro _context de la coleccion 
                .Take(100).ToListAsync();// toma los primeros 100
            //include porque esta relacionado con la tabla categoria
            return ingreso.Select(i => new IngresoViewModel //retorno el objeto siguiendo la estructura IngresoViewModel
            {
                idingreso = i.idingreso,
                idproveedor = i.idproveedor,
                proveedor = i.persona.nombre,
                idusuario = i.idusuario,
                usuario = i.usuario.nombre,
                tipo_comprobante = i.tipo_comprobante,
                serie_comprobante = i.serie_comprobante,
                num_comprobante = i.num_comprobante,
                fecha_hora = i.fecha_hora,
                impuesto = i.impuesto,
                total = i.total,
                estado = i.estado
            });
        }

        // POST: api/Ingresoes/Crear
        [Authorize(Roles = "Mecanico, Administrador")]//autorizacion segin roles
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //permite realizar validaciones segun los data annotation
            }

            var fechaHora = DateTime.Now;

            Ingreso ingreso = new Ingreso
            {
                idproveedor = model.idproveedor,
                idusuario = model.idusuario,
                tipo_comprobante = model.tipo_comprobante,
                serie_comprobante = model.serie_comprobante,
                num_comprobante = model.num_comprobante,
                fecha_hora = fechaHora,
                impuesto = model.impuesto,
                total = model.total,
                estado = "Aceptado"

            };
            
            try
            {
                _context.Ingresos.Add(ingreso);// me agregue el ingreso

                await _context.SaveChangesAsync();//guarda los cambios
                // almacenar los detalles
                var id = ingreso.idingreso; //valor llave primaria del ingraso
                foreach (var det in model.detalles)//recorro detalles del objeto model instancia de la clase crear viewmodel 
                {//detalles los recibo de la vista
                    // 
                    DetalleIngreso detalle = new DetalleIngreso
                    {
                        //por cada detalle agrego un objeto
                        idingreso = id,
                        idarticulo = det.idarticulo,
                        cantidad = det.cantidad,
                        precio = det.precio
                    };
                    _context.DetalleIngresos.Add(detalle);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        private bool IngresoExists(int id)
        {
            return _context.Ingresos.Any(e => e.idingreso == id);
        }
    }
}