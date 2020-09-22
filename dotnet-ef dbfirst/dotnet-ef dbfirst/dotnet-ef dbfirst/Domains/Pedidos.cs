using System;
using System.Collections.Generic;

namespace dotnet_ef_dbfirst.Domains
{
    public partial class Pedidos
    {
        public Pedidos()
        {
            PedidosItems = new HashSet<PedidosItems>();
        }

        public Guid Id { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual ICollection<PedidosItems> PedidosItems { get; set; }
    }
}
