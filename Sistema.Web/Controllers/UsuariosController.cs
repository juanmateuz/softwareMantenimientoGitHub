using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;//configuracion crear token
using Microsoft.IdentityModel.Tokens;//token
using Sistema.Datos;
using Sistema.Entidades.Usuarios;
using Sistema.Web.Controllers.Models.Usuarios.Usuario;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;//token
using System.Linq;
using System.Security.Claims;
using System.Text;// utf8 token
using System.Threading.Tasks;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DbContextSistema _context;
        private readonly IConfiguration _config;//configuracion crear token

        public UsuariosController(DbContextSistema context, IConfiguration config)
        {
            _context = context;
            _config = config;//Configuracion token
        }

        // GET: api/Usuarios/Listar ----Modelo me refleja la entidad solo con los datos que el usuario requiera        
        // [Authorize(Roles = "Administrador")]//Autorizacion segun roles
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioViewModel>> Listar()//nombre metodo generamos una tarea asincrona y llamamos CategoriaViewModel
        {
            var usuario = await _context.Usuarios.Include(u => u.rol).ToListAsync();//objeto llamado categoria ToListAsync:obtenemos la lista del registro _context de la coleccion categorias
            //Include porque esta relacionado con la tabla categoria
            return usuario.Select(u => new UsuarioViewModel //retorno el objeto siguiendo la estructura CategoriaViewModel
            {
                idusuario = u.idusuario,
                idrol = u.idrol,
                rol = u.rol.nombre,//nombre del rol al cual se ha asignado el usuario
                nombre = u.nombre,
                tipo_documento = u.tipo_documento,
                num_documento = u.num_documento,
                direccion = u.direccion,
                telefono = u.telefono,
                email = u.email,
                password_hash = u.password_hash,
                condicion = u.condicion
            });
        }

        // POST: api/Usuarios/Crear
        [Authorize(Roles = "Administrador")]//Autorizacion segun roles
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)//permite realizar validaciones segun los data annotation
            {
                return BadRequest(ModelState); 
            }

            //Validar email no repetido
            var email = model.email.ToLower();

            if (await _context.Usuarios.AnyAsync(u => u.email == email))//u.email obtengo el email del context
            {
                return BadRequest("El email ya existe");
            }

            //llamo a metodo para crear las claves
            CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt); ;

            Usuario usuario = new Usuario //entidad como tal Usuarios
            {
                idrol = model.idrol,            
                nombre = model.nombre, //indico a mi objeto categoria que el nombre va a ser igual al del modelo
                tipo_documento = model.tipo_documento,
                num_documento = model.num_documento,
                direccion = model.direccion,
                telefono = model.telefono,
                email=model.email,
                password_hash= passwordHash,//password encriptado
                password_salt= passwordSalt,
                condicion=true

            };
            _context.Usuarios.Add(usuario);// me agregue esa Usuario
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

         // PUT: api/Usuarios/Actualizar
        [Authorize(Roles = "Administrador")]//autorizacion segin roles
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idusuario <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.idusuario == model.idusuario);

            if (usuario == null) //si usuario no existe
            {
                return NotFound();
            }

            usuario.idrol = model.idrol;
            usuario.nombre = model.nombre;
            usuario.tipo_documento = model.tipo_documento;
            usuario.num_documento = model.num_documento;
            usuario.direccion = model.direccion;
            usuario.telefono = model.telefono;
            usuario.email = model.email.ToLower();

            if (model.act_password == true)  //act_password es true actualizo el password
            {
                CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.password_hash = passwordHash;
                usuario.password_salt = passwordSalt;
            }

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

        //hachear password espera como primer parametro un string
        //y me retorna 2 valores de tipo array byte
        private void CrearPasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)//
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; // envio la llave libreria net core
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); //envio el passwor encriptado 
            }
        }

        //metodo llenar select model ----modelo me refleja la entidad solo con los datos que el usuario requiera
        // GET: api/Usuarios/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()//nombre metodo generamos una tarea asincrona y llamamos SelectViewModel
        {
            var solicitud = await _context.Usuarios.Where(c => c.rol.nombre == "Administrador").ToListAsync();//objeto llamado model ToListAsync:obtenemos la lista del registro _context de la coleccion categorias

            return solicitud.Select(c => new SelectViewModel //retorno el objeto siguiendo la estructura SelectViewModel
            {
                nombre = c.nombre,//informacion a mostrar en el listado 
            });
        }

        //metodo llenar select model
        // GET: api/Usuarios/SelectMecanico    
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> SelectMecanico()//nombre metodo generamos una tarea asincrona y llamamos SelectViewModel
        {
            var solicitud = await _context.Usuarios.Where(c => c.rol.nombre == "Mecanico").ToListAsync();//objeto llamado model ToListAsync:obtenemos la lista del registro _context de la coleccion categorias

            return solicitud.Select(c => new SelectViewModel //retorno el objeto siguiendo la estructura SelectViewModel
            {
                nombre = c.nombre,//informacion a mostrar en el listado
            });
        }

        // PUT: api/Usuarios/Desactivar/1
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.idusuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.condicion = false;

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

        // PUT: api/Usuarios/Activar/1
        //  [Authorize(Roles = "Administrador")]//autorizacion segin roles
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.idusuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.condicion = true;

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

        //Login
        // PUT: api/Usuarios/Login
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model) //recibe objeto model que instancia de loginviewmodel
        {
            var email = model.email.ToLower();//convierto a  Mayuscula
            var usuario = await _context.Usuarios.Where(u => u.condicion == true).Include(u => u.rol).FirstOrDefaultAsync(u => u.email == email);//verifica si existe el correo donde usuarios esten habilitados 

            if (usuario == null)
            {
                return NotFound();
            }
            //verifico password
            //model.password enviado desde el modelo
            if (!VerificarPasswordHash(model.password, usuario.password_hash, usuario.password_salt))//informacion enviada desde el objeto
            {
                return NotFound();
            }

            var claims = new List<Claim>//claim reclamaciones declaro array tipo lista
            {
                //contienen informacion del usuario lo que es
                new Claim(ClaimTypes.NameIdentifier, usuario.idusuario.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, usuario.rol.nombre ),//rol objeto usuario
                new Claim("idusuario", usuario.idusuario.ToString() ),
                new Claim("rol", usuario.rol.nombre ),
                new Claim("nombre", usuario.nombre )
            };

            return Ok(new { token = GenerarToken(claims) });//retorno token
        }
        //metodo verificar password
        //Espera parametro del password /passwordHashAlmacenado para ese usuario  /passwordSalt para ese usuario
        private bool VerificarPasswordHash(string password, byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))// encripto el password  usando el passwordSalt
            {
                var passwordHashNuevo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); //encripto password
                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo)); //comparo que password sea correcto
            }
        }

        //metodo para generar token
        //para indicar al usuario los privilegios que va a tener

        private string GenerarToken(List<Claim> claims)//espera lista de claim
        {
            //llave y credenciales
            //token lo comparto a el usuario al momento de logueo
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //documentacion asp.netcore
            var token = new JwtSecurityToken(
             _config["Jwt:Issuer"],
             _config["Jwt:Issuer"],
             expires: DateTime.Now.AddMinutes(300),
             signingCredentials: creds,
             claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.idusuario == id);
        }
    }
}