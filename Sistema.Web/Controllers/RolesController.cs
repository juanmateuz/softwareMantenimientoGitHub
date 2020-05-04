using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Web.Controllers.Models.Usuarios.Rol;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador")]//autorizacion segun roles
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public RolesController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Roles/listar
        //modelo me refleja la entidad solo con los datos que el rol requiera
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolViewModel>> Listar()//nombre metodo generamos una tarea asincrona y llamamos CategoriaViewModel
        {
            var rol = await _context.Roles.ToListAsync();//objeto llamado categoria ToListAsync:obtenemos la lista del registro _context de la coleccion categorias

            return rol.Select(r => new RolViewModel //retorno el objeto siguiendo la estructura CategoriaViewModel
            {
                idrol = r.idrol,//informacion a mostrar en el listado
                nombre = r.nombre,
                descripcion = r.descripcion,
                condicion = r.condicion
            });
        }

        //metodo llenar select rol
        // GET: api/Roles/Select
        //modelo me refleja la entidad solo con los datos que el rol requiera
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()//nombre metodo generamos una tarea asincrona y llamamos SelectViewModel
        {
            var rol = await _context.Roles.Where(r => r.condicion == true).ToListAsync();//objeto llamado categoria ToListAsync:obtenemos la lista del registro _context de la coleccion rol

            return rol.Select(r => new SelectViewModel //retorno el objeto siguiendo la estructura SelectViewModel
            {
                idrol = r.idrol,//informacion a mostrar en el listado
                nombre = r.nombre,

            });
        }
        private bool RolExists(int id)
        {
            return _context.Roles.Any(e => e.idrol == id);
        }
    }
}