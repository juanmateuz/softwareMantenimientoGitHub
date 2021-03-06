﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Web.Controllers.Models.Usuarios.Rol;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador")]//Autorizacion segun roles
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DbContextSistema baseDatos;

        public RolesController(DbContextSistema context)
        {
            baseDatos = context;
        }

        // GET: api/Roles/listar
        //modelo me refleja la entidad solo con los datos que el Usuario requiera
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolViewModel>> Listar()//nombre metodo generamos una tarea asincrona y llamamos CategoriaViewModel
        {
            var rol = await baseDatos.Roles.ToListAsync();//objeto llamado Roles ToListAsync:obtenemos la lista del registro baseDatos de la coleccion Roles

            return rol.Select(r => new RolViewModel //retorno el objeto siguiendo la estructura CategoriaViewModel
            {
                idrol = r.idrol,//informacion a mostrar en el listado
                nombre = r.nombre,
                descripcion = r.descripcion,
                condicion = r.condicion
            });
        }

        //modelo me refleja la entidad solo con los datos que el rol requiera --- metodo llenar select rol
        // GET: api/Roles/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()//nombre metodo generamos una tarea asincrona y llamamos SelectViewModel
        {
            var rol = await baseDatos.Roles.Where(r => r.condicion == true).ToListAsync();//objeto llamado Roles ToListAsync:obtenemos la lista del registro baseDatos de la coleccion rol

            return rol.Select(r => new SelectViewModel //retorno el objeto siguiendo la estructura SelectViewModel
            {
                idrol = r.idrol,//informacion a mostrar en el listado
                nombre = r.nombre,

            });
        }
        private bool RolExists(int id)
        {
            return baseDatos.Roles.Any(e => e.idrol == id);
        }
    }
}