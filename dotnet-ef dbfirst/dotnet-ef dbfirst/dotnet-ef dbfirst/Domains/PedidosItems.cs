using System;
using System.Collections.Generic;

namespace dotnet_ef_dbfirst.Domains
{
    public partial class PedidosItems
    {
        public Guid Id { get; set; }
        public Guid IdPedido { get; set; }
        public Guid IdProduto { get; set; }

        public virtual Pedidos IdPedidoNavigation { get; set; }
        public virtual Produtos IdProdutoNavigation { get; set; }
    }
}
