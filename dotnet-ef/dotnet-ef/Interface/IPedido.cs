using dotnet_ef.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_ef.Interface
{
    interface IPedido
    {
        List<Pedido> Listar();
        Pedido BuscarPorID(Guid id);
        /// <summary>
        /// Adiciona um novo pedido.
        /// </summary>
        /// <param name="pedidosItens"></param>
        /// <returns>Pedido</returns>
        Pedido Adicionar(List<PedidoItem> pedidosItens);
    }
}
