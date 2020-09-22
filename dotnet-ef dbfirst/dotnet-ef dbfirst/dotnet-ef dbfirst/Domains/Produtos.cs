using System;
using System.Collections.Generic;

namespace dotnet_ef_dbfirst.Domains
{
    public partial class Produtos
    {
        public Produtos()
        {
            PedidosItems = new HashSet<PedidosItems>();
        }

        public Guid Id { get; set; }
        public string NomeProduto { get; set; }
        public float Preco { get; set; }

        public virtual ICollection<PedidosItems> PedidosItems { get; set; }
    }
}
