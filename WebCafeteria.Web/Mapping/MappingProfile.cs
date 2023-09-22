using AutoMapper;
using System;
using WebCafeteria.Entidades;
using WebCafeteria.Entidades.Dtos;
using WebCafeteria.Entidades.Dtos.Cliente;
using WebCafeteria.Entidades.Dtos.TamañoProducto;
using WebCafeteria.Entidades.Dtos.Venta;
using WebCafeteria.Entidades.Entidades;
using WebCafeteria.Web.Models.Carrito;
using WebCafeteria.Web.ViewModels;
using WebCafeteria.Web.ViewModels.Carrito;
using WebCafeteria.Web.ViewModels.Categoria;
using WebCafeteria.Web.ViewModels.Ciudad;
using WebCafeteria.Web.ViewModels.Cliente;
using WebCafeteria.Web.ViewModels.DetalleVenta;
using WebCafeteria.Web.ViewModels.Pais;
using WebCafeteria.Web.ViewModels.Productos;
using WebCafeteria.Web.ViewModels.Proveedor;
using WebCafeteria.Web.ViewModels.Tamaño;
using WebCafeteria.Web.ViewModels.TamañoProducto;
using WebCafeteria.Web.ViewModels.Venta;

namespace WebCafeteria.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            LoadPaisesMapping();
            LoadCategoriasMapping();
            LoadTamañosMapping();
            LoadCiudadesMapping();
            LoadProveedoresMapping();
            LoadProductosMapping();
            LoadCarritoMapping();
            LoadVentasMapping();
            LoadTamañosProductosMapping();
            LoadDetalleVentaMapping();
            LoadClientesMapping();
        }

        private void LoadClientesMapping()
        {
            CreateMap<ClienteListDto, ClienteListVm>();
            CreateMap<ClienteEditVm, Cliente>();
            CreateMap<Cliente, ClienteEditVm>()
    .ForMember(dest => dest.EmailAnterior, opt => opt.MapFrom(src => src.Email));

            CreateMap<Cliente, ClienteListVm>()
    .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.Id))
    .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.Nombre))
    .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais.NombrePais))
    .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.Ciudad.NombreCiudad));
        }

        private void LoadCarritoMapping()
        {
            CreateMap<ItemCarrito, ItemCarritoVm>();
        }


        private void LoadDetalleVentaMapping()
        {
            CreateMap<DetalleVentaListDto, DetalleVentaListVm>();
        }

        private void LoadTamañosProductosMapping()
        {
            CreateMap<TamañoProductoListDto, TamañoProductoListVm>();
                //.ForMember(dest => dest.Imagen,
                //opt => opt.MapFrom(src => src.Imagen));
            CreateMap<TamañoProductoEditVm, TamañoProducto>().ReverseMap();
            CreateMap<TamañoProducto, TamañoProductoListVm>()
                .ForMember(dest => dest.NombreProducto,
                opt => opt.MapFrom(src => src.producto.NombreProducto))
                .ForMember(dest => dest.NombreTamaño, opt => opt.MapFrom(src => src.tamaño.NombreTamaño))
                                .ForMember(dest => dest.Imagen,
                opt => opt.MapFrom(src => src.producto.Imagen));


        }

        private void LoadVentasMapping()
        {
            CreateMap<VentaListDto, VentaListVm>();
        }

        private void LoadProductosMapping()
        {
            CreateMap<ProductoListDto, ProductoListVm>();
            CreateMap<ProductoEditVm, Producto>().ReverseMap();
            CreateMap<Producto, ProductoListVm>()
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.NombreCategoria));
        }

        private void LoadProveedoresMapping()
        {
            CreateMap<ProveedorListDto, ProveedorListVm>();
            CreateMap<ProveedorEditVm, Proveedor>().ReverseMap();
            CreateMap<Proveedor, ProveedorListVm>()
                .ForMember(dest => dest.ProveedorId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NombreProveedor, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais.NombrePais))
                .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.Ciudad.NombreCiudad));

        }

        private void LoadTamañosMapping()
        {
            CreateMap<Tamaño, TamañoListVm>();
            CreateMap<Tamaño, TamañoEditVm>().ReverseMap();
        }

        private void LoadCategoriasMapping()
        {
            CreateMap<Categoria, CategoriaListVm>();
            CreateMap<Categoria, CategoriaEditVm>().ReverseMap();
        }

        private void LoadCiudadesMapping()
        {
            CreateMap<CiudadListDto, CiudadListVm>();
            CreateMap<CiudadEditVm, Ciudad>().ReverseMap();
            CreateMap<Ciudad, CiudadListVm>()
                .ForMember(dest => dest.NombrePais,
                opt => opt.MapFrom(src => src.Pais.NombrePais));
        }

        private void LoadPaisesMapping()
        {
            CreateMap<Pais, PaisListVm>();
            CreateMap<Pais, PaisEditVm>().ReverseMap();

        }

    }
}