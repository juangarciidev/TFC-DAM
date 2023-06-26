using MaricastanaClothingStore.MODELO;
using MaricastanaClothingStore.UTILIDADES;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace MaricastanaClothingStore.DAL
{
    public class MaricastanaContext : DbContext
    {
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<LineaVenta> LineaVentas { get; set; }
        public DbSet<Privilegio> Privilegios { get; set; }
        public DbSet<Talla> Tallas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venta> Ventas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Maricastana;User Id=sa;password=abc123.");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API

            //CLAVE FORÁNEA VENTA-USUARIO
            modelBuilder.Entity<Venta>()
            .HasOne(s => s.Usuario)
            .WithMany(b => b.Ventas)
            .HasForeignKey(s => s.UsuarioId).OnDelete(DeleteBehavior.Cascade);

            //CLAVE FORÁNEA USUARIO-PRIVILEGIO
            modelBuilder.Entity<Usuario>()
            .HasOne(s => s.Privilegio)
            .WithMany(b => b.Usuarios)
            .HasForeignKey(s => s.PrivilegioId).OnDelete(DeleteBehavior.Cascade);

            //CLAVE FORÁNEA LINEAVENTA-VENTA
            modelBuilder.Entity<LineaVenta>()
            .HasOne(s => s.Venta)
            .WithMany(b => b.LineaVentas)
            .HasForeignKey(s => s.VentaId).OnDelete(DeleteBehavior.Cascade);
            //CLAVE FORÁNEA LINEAVENTA-TALLAS
            modelBuilder.Entity<LineaVenta>()
            .HasOne(s => s.Talla)
            .WithMany(b => b.LineasVentas)
            .HasForeignKey(s => s.TallaId).OnDelete(DeleteBehavior.NoAction);

            //CLAVE FORÁNEA TALLA-ARTICULO
            modelBuilder.Entity<Talla>()
            .HasOne(s => s.Articulo)
            .WithMany(b => b.Tallas)
            .HasForeignKey(s => s.ArticuloId).OnDelete(DeleteBehavior.Cascade);
            //CLAVE FORÁNEA ARTICULO-CATEGORIA
            modelBuilder.Entity<Articulo>()
            .HasOne(s => s.Categoria)
            .WithMany(b => b.Articulos)
            .HasForeignKey(s => s.CategoriaId).OnDelete(DeleteBehavior.Cascade);

            //CLAVES ALTERNATIVAS 

            modelBuilder.Entity<Categoria>().HasAlternateKey(s => s.NombreCategoria);

            modelBuilder.Entity<Privilegio>().HasAlternateKey(s => s.NombrePrivilegio);

            modelBuilder.Entity<Talla>().HasAlternateKey(s => new { s.TallaId, s.ArticuloId });

            modelBuilder.Entity<Usuario>().HasAlternateKey(s => s.DNI);
            modelBuilder.Entity<Usuario>().HasAlternateKey(s => s.Correo);
            modelBuilder.Entity<Usuario>().HasAlternateKey(s => s.LoginUsuario);

            modelBuilder.Entity<Venta>().HasAlternateKey(s => s.NumFactura);

            #region INSERTS POR DEFAULT
            #region PRVILEGIOS DE USUARIOS
            modelBuilder.Entity<Privilegio>().HasData(new Privilegio { PrivilegioId = 1, NombrePrivilegio = "Administrador" });
            modelBuilder.Entity<Privilegio>().HasData(new Privilegio { PrivilegioId = 2, NombrePrivilegio = "Dependiente" });
            //modelBuilder.Entity<Privilegio>().HasData(new Privilegio { PrivilegioId = 3, NombrePrivilegio = "Cliente" });
            #endregion
            #region CATEGORÍAS
            modelBuilder.Entity<Categoria>().HasData(new Categoria { CategoriaId = 1, NombreCategoria = "Abrigos" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { CategoriaId = 2, NombreCategoria = "Bisutería" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { CategoriaId = 3, NombreCategoria = "Bolsos" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { CategoriaId = 4, NombreCategoria = "Botas" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { CategoriaId = 5, NombreCategoria = "Camisas" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { CategoriaId = 6, NombreCategoria = "Camisetas" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { CategoriaId = 7, NombreCategoria = "Complementos" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { CategoriaId = 8, NombreCategoria = "Jerseys" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { CategoriaId = 9, NombreCategoria = "Pantalones" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { CategoriaId = 10, NombreCategoria = "Vestidos" });
            #endregion
            #region ARTÍCULOS Y SUS TALLAS
            string ruta = Directory.GetCurrentDirectory();


            modelBuilder.Entity<Articulo>().HasData(new Articulo
            {
                ArticuloId = 1,
                Codigo = "ABC1",
                Nombre = "Bolso",
                Marca = "IMZ",
                Color = "Arena",
                PrecioUnidad = 49.00m,
                CategoriaId = 3,
                Imagen = ConversorImagen.bitmapImageToBytes(new BitmapImage(new Uri(ruta + @"\\IMAGENES\\Bolso.jpg", UriKind.Relative)))
            });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 1, NombreTalla = "ÚNICA", Cantidad = 5, ArticuloId = 1, });


            modelBuilder.Entity<Articulo>().HasData(new Articulo
            {
                ArticuloId = 2,
                Codigo = "BCW2",
                Nombre = "Camiseta de manga larga",
                Marca = "PAN",
                Color = "Verde agua",
                PrecioUnidad = 25.00m,
                CategoriaId = 6,
                Imagen = ConversorImagen.bitmapImageToBytes(new BitmapImage(new Uri(ruta + @"\\IMAGENES\\CamisetaMangaLarga.jpg", UriKind.Relative)))
            });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 2, NombreTalla = "XS", Cantidad = 2, ArticuloId = 2, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 3, NombreTalla = "S", Cantidad = 3, ArticuloId = 2, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 4, NombreTalla = "M", Cantidad = 2, ArticuloId = 2, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 5, NombreTalla = "L", Cantidad = 2, ArticuloId = 2, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 6, NombreTalla = "XL", Cantidad = 2, ArticuloId = 2, });


            modelBuilder.Entity<Articulo>().HasData(new Articulo
            {
                ArticuloId = 3,
                Codigo = "DWG5",
                Nombre = "Camisa de manga larga",
                Marca = "PAISIE",
                Color = "Blanco",
                PrecioUnidad = 30.00m,
                CategoriaId = 5,
                Imagen = ConversorImagen.bitmapImageToBytes(new BitmapImage(new Uri(ruta + @"\\IMAGENES\\CamisaMangaLarga.jpg", UriKind.Relative)))
            });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 7, NombreTalla = "XS", Cantidad = 1, ArticuloId = 3, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 8, NombreTalla = "S", Cantidad = 3, ArticuloId = 3, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 9, NombreTalla = "M", Cantidad = 1, ArticuloId = 3, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 10, NombreTalla = "L", Cantidad = 2, ArticuloId = 3, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 11, NombreTalla = "XL", Cantidad = 1, ArticuloId = 3, });


            modelBuilder.Entity<Articulo>().HasData(new Articulo
            {
                ArticuloId = 4,
                Codigo = "CYU3",
                Nombre = "Abrigo",
                Marca = "PAN",
                Color = "Beige",
                PrecioUnidad = 149.95m,
                CategoriaId = 1,
                Imagen = ConversorImagen.bitmapImageToBytes(new BitmapImage(new Uri(ruta + @"\\IMAGENES\\Abrigo.jpg", UriKind.Relative)))
            });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 12, NombreTalla = "XS", Cantidad = 1, ArticuloId = 4, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 13, NombreTalla = "S", Cantidad = 1, ArticuloId = 4, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 14, NombreTalla = "M", Cantidad = 1, ArticuloId = 4, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 15, NombreTalla = "L", Cantidad = 2, ArticuloId = 4, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 16, NombreTalla = "XL", Cantidad = 1, ArticuloId = 4, });


            modelBuilder.Entity<Articulo>().HasData(new Articulo
            {
                ArticuloId = 5,
                Codigo = "SDC8",
                Nombre = "Jersey",
                Marca = "PAISIE",
                Color = "Azul",
                PrecioUnidad = 84.95m,
                CategoriaId = 8,
                Imagen = ConversorImagen.bitmapImageToBytes(new BitmapImage(new Uri(ruta + @"\\IMAGENES\\Jersey.jpg", UriKind.Relative)))
            });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 17, NombreTalla = "XS", Cantidad = 2, ArticuloId = 5, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 18, NombreTalla = "S", Cantidad = 2, ArticuloId = 5, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 19, NombreTalla = "M", Cantidad = 2, ArticuloId = 5, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 20, NombreTalla = "L", Cantidad = 2, ArticuloId = 5, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 21, NombreTalla = "XL", Cantidad = 2, ArticuloId = 5, });


            modelBuilder.Entity<Articulo>().HasData(new Articulo
            {
                ArticuloId = 6,
                Codigo = "WDF9",
                Nombre = "Pantalón ancho",
                Marca = "CRISTINA BEAUTIFUL",
                Color = "Verde agua",
                PrecioUnidad = 49.95m,
                CategoriaId = 9,
                Imagen = ConversorImagen.bitmapImageToBytes(new BitmapImage(new Uri(ruta + @"\\IMAGENES\\PantalonAncho.jpg", UriKind.Relative)))
            });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 22, NombreTalla = "XS", Cantidad = 1, ArticuloId = 6, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 23, NombreTalla = "S", Cantidad = 2, ArticuloId = 6, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 24, NombreTalla = "M", Cantidad = 3, ArticuloId = 6, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 25, NombreTalla = "L", Cantidad = 2, ArticuloId = 6, });
            modelBuilder.Entity<Talla>().HasData(new Talla { TallaId = 26, NombreTalla = "XL", Cantidad = 1, ArticuloId = 6, });
            #endregion
            #region USUARIOS
            modelBuilder.Entity<Usuario>().HasData
                (new Usuario
                {
                    UsuarioId = 1,
                    Nombre = "Susana",
                    Apellidos = "Ocampo Rodríguez",
                    DNI = "34986966X",
                    Correo = "susanaocamporguez@gmail.com",
                    Telefono = "662 247 761",
                    FechaNacimiento = new DateTime(1971, 10, 11),
                    Direccion = "Benito Vicetto, 13, 3ºA, Ourense",
                    FotoPerfil = ConversorImagen.bitmapImageToBytes(new BitmapImage(new Uri(ruta + @"\\IMAGENES\\administradora.jpg", UriKind.Relative))),
                    LoginUsuario = "admin",
                    LoginContraseña = "abc123.",
                    PrivilegioId = 1,
                });

            modelBuilder.Entity<Usuario>().HasData
                (new Usuario
                {
                    UsuarioId = 2,
                    Nombre = "María",
                    Apellidos = "Fernández Pérez",
                    DNI = "35732938P",
                    Correo = "mariafdezl@gmail.com",
                    Telefono = "654 876 129",
                    FechaNacimiento = new DateTime(1968, 6, 23),
                    Direccion = "Vista Hermosa, 21, 1ºB, Ourense",
                    FotoPerfil = ConversorImagen.bitmapImageToBytes(new BitmapImage(new Uri(ruta + @"\\IMAGENES\\dependienta.jpg", UriKind.Relative))),
                    LoginUsuario = "dependienta",
                    LoginContraseña = "abc123.",
                    PrivilegioId = 2,
                });

            //modelBuilder.Entity<Usuario>().HasData
            //    (new Usuario
            //    {
            //        UsuarioId = 3,
            //        Nombre = "Público en General",
            //        Apellidos = "Público en General",
            //        DNI = "00000000X",
            //        Correo = "publicoengeneral@gmail.com",
            //        Telefono = "666 666 666",
            //        FechaNacimiento = new DateTime(1999, 1, 31),
            //        Direccion = "Anónima",
            //        FotoPerfil = ConversorImagen.bitmapImageToBytes(new BitmapImage(new Uri(ruta + @"\\IMAGENES\\publicoGeneral.jpg", UriKind.Relative))),
            //        LoginUsuario = "clientegeneral",
            //        LoginContraseña = "abc123.",
            //        PrivilegioId = 3,
            //    });
            #endregion
            #region VENTAS
            modelBuilder.Entity<Venta>().HasData
                (new Venta
                {
                    VentaId = 1,
                    NumFactura = "20230415182057",    
                    FechaVenta = new DateTime(2023, 4, 15),
                    PrecioTotal = 399.95m,
                    UsuarioId = 1                                
                });
            modelBuilder.Entity<Venta>().HasData
                (new Venta
                {
                    VentaId = 2,
                    NumFactura = "20230521192037",
                    FechaVenta = new DateTime(2023, 5, 21),
                    PrecioTotal = 50.00m,
                    UsuarioId = 1
                });
            modelBuilder.Entity<Venta>().HasData
                (new Venta
                {
                    VentaId = 3,
                    NumFactura = "20230105182055",
                    FechaVenta = new DateTime(2023, 1, 4),
                    PrecioTotal = 310.00m,
                    UsuarioId = 2
                });
            modelBuilder.Entity<Venta>().HasData
                (new Venta
                {
                    VentaId = 4,
                    NumFactura = "20221222134023",
                    FechaVenta = new DateTime(2022, 12, 22),
                    PrecioTotal = 259.99m,
                    UsuarioId = 1
                });
            modelBuilder.Entity<Venta>().HasData
                (new Venta
                {
                    VentaId = 5,
                    NumFactura = "20230213202054",
                    FechaVenta = new DateTime(2023, 2, 13),
                    PrecioTotal = 230.00m,
                    UsuarioId = 1
                });
            modelBuilder.Entity<Venta>().HasData
                (new Venta
                {
                    VentaId = 6,
                    NumFactura = "20230610182057",
                    FechaVenta = new DateTime(2023, 6, 10),
                    PrecioTotal = 103.99m,
                    UsuarioId = 2
                });
            modelBuilder.Entity<Venta>().HasData
                (new Venta
                {
                    VentaId = 7,
                    NumFactura = "20230505182057",
                    FechaVenta = new DateTime(2023, 5, 5),
                    PrecioTotal = 87.49m,
                    UsuarioId = 1
                });
            modelBuilder.Entity<Venta>().HasData
                (new Venta
                {
                    VentaId = 8,
                    NumFactura = "20230430114032",
                    FechaVenta = new DateTime(2023, 4, 30),
                    PrecioTotal = 149.95m,
                    UsuarioId = 2
                });
            modelBuilder.Entity<Venta>().HasData
                (new Venta
                {
                    VentaId = 9,
                    NumFactura = "20230328173957",
                    FechaVenta = new DateTime(2023, 3, 28),
                    PrecioTotal = 24.95m,
                    UsuarioId = 1
                });
            modelBuilder.Entity<Venta>().HasData
                (new Venta
                {
                    VentaId = 10,
                    NumFactura = "20230215191134",
                    FechaVenta = new DateTime(2023, 2, 5),
                    PrecioTotal = 79.95m,
                    UsuarioId = 2
                });
            #endregion
            #endregion
        }
    }
}