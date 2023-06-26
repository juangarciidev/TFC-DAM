using ExamenVentas.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaricastanaClothingStore.DAL
{
    public class UnitOfWork : IDisposable
    {
        public UnitOfWork()
        {
            context.Database.EnsureCreated();
        }

        private MaricastanaContext context = new MaricastanaContext();
        private bool disposed = false;


        private ArticulosRepository articulosRepository;
        private CategoriasRepository categoriasRepository;
        private LineasVentasRepository lineasVentasRepository;
        private PrivilegiosRepository privilegiosRepository;
        private TallasRepository tallasRepository;
        private UsuariosRepository usuariosRepository;
        private VentasRepository ventasRepository;

        public ArticulosRepository ArticulosRepository
        {
            get
            {
                if (articulosRepository == null)
                {
                    articulosRepository = new ArticulosRepository(context);
                }

                return articulosRepository;
            }
        }

        public CategoriasRepository CategoriasRepository
        {
            get
            {
                if (categoriasRepository == null)
                {
                    categoriasRepository = new CategoriasRepository(context);
                }

                return categoriasRepository;
            }
        }

        public LineasVentasRepository DetalleVentasRepository
        {
            get
            {
                if (lineasVentasRepository == null)
                {
                    lineasVentasRepository = new LineasVentasRepository(context);
                }

                return lineasVentasRepository;
            }
        }

        public PrivilegiosRepository PrivilegiosRepository
        {
            get
            {
                if (privilegiosRepository == null)
                {
                    privilegiosRepository = new PrivilegiosRepository(context);
                }

                return privilegiosRepository;
            }
        }

        public TallasRepository TallasRepository
        {
            get
            {
                if (tallasRepository == null)
                {
                    tallasRepository = new TallasRepository(context);
                }

                return tallasRepository;
            }
        }

        public UsuariosRepository UsuariosRepository
        {
            get
            {
                if (usuariosRepository == null)
                {
                    usuariosRepository = new UsuariosRepository(context);
                }

                return usuariosRepository;
            }
        }

        public VentasRepository VentasRepository
        {
            get
            {
                if (ventasRepository == null)
                {
                    ventasRepository = new VentasRepository(context);
                }

                return ventasRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
