﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebCafeteria.Entidades;

namespace WebCafeteria.Datos.Interfaces
{
    public interface IRepositorioCategorias
    {
        List<Categoria> GetCategorias();
        void Agregar(Categoria categoria);
        void Editar(Categoria categoria);
        void Borrar(int id);
        bool Existe(Categoria categoria);
        Categoria GetCategoriaPorId(int categoriaId);
        bool EstaRelacionado(Categoria categoria);
        List<Categoria> GetCategoriasPorPagina(int cantidad, int pagina);
        int GetCantidad();
        List<SelectListItem> GetCategoriasDropDownList();
    }
}
