using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.Almacen; //donde se encuentrael archivo categoria map
using Sistema.Datos.Mapping.Usuarios;
using Sistema.Datos.Mapping.Ventas;
using Sistema.Entidades.Almacen; //exponer categoria ruta
using Sistema.Entidades.Mantenimiento;
using Sistema.Entidades.Usuarios;
using Sistema.Entidades.Ventas;
namespace Sistema.Datos
{  // exponemos la coleccion de todos los registros en un objeto llamado articulo
    public class DbContextSistema: DbContext //:para heredar de la clase DbContext y se crea la dependencia a Microsoft.EntityFrameworkCore 
    { 
        public DbSet<Repuesto> Repuestos { get; set; }//exponemos la coleccion de todas las categorias en el objeto Repuestos
        public DbSet<Distribuidor> Distribuidor { get; set; }//exponemos la coleccion de todas los articulos en el objeto Distribuidor
        public DbSet<Rol> Roles { get; set; }//exponemos la coleccion de todas los articulos en el objeto Rol
        public DbSet<Usuario> Usuarios { get; set; }//exponemos la coleccion de  en el objeto Rol
        public DbSet<Persona> Personas { get; set; }//exponemos la coleccion  en el objeto persona
        public DbSet<Ingreso> Ingresos { get; set; }//exponemos la ingreso  en el objeto persona            
        public DbSet<Equipos> equipo { get; set; }//exponemos la ingreso  en el objeto persona
        public DbSet<Fabricante> Fabricantes { get; set; }//exponemos la ingreso  en el objeto persona
        public DbSet<DetalleIngreso> DetalleIngresos { get; set; }//exponemos la coleccion  en el objeto persona
        public DbSet<Mantenimiento> Mantenimientos { get; set; }

        public DbContextSistema(DbContextOptions<DbContextSistema>options): base(options)//constructor
        {  // recibimos como parametro un objeto que instancia de la clase DbContextOptions
           //en este caso el paremetro q recibe es option de tipo DbContextSistema
           //: base(options) le indicamos parametro al padre DbContent
           
        }
        // agregamos metodo permite mapear entidades con la BD
        //le estamos enviado como parametro modelBuilder que instancia a la clase ModelBuilder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //cuando cree el modelo envie modelBuilder
            modelBuilder.ApplyConfiguration(new RepuestoMap());//aplique la configuracion de categoriamap
            modelBuilder.ApplyConfiguration(new ArticuloMap());//aplique la configuracion de articulomap
            modelBuilder.ApplyConfiguration(new RolMap());//aplique la configuracion de Rolmap
            modelBuilder.ApplyConfiguration(new UsuarioMap());//aplique la configuracion de Usuaiomap
            modelBuilder.ApplyConfiguration(new PersonaMap());//aplique la configuracion de Usuaiomap
            modelBuilder.ApplyConfiguration(new IngresoMap());//aplique la configuracion de Ingresomap
            modelBuilder.ApplyConfiguration(new DetalleIngresoMap());//aplique la configuracion de detalle_ingrso
            modelBuilder.ApplyConfiguration(new FabricanteMap());//aplique la configuracion de fabricante
            modelBuilder.ApplyConfiguration(new equipoMap());//aplique la configuracion de equipos
            modelBuilder.ApplyConfiguration(new MantenimientoMap());
            // se recomienda para cada entidad crear una configuracion
        }
    }
}
