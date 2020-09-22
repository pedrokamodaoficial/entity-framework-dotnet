using dotnet_ef.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_ef.Interface
{
    interface IProduto
    {

        List<Produto> Listar();
        List<Produto> BuscarPorNome(string nome);
        Produto BuscarPorID(Guid id);
        void Adicionar(Produto produto);
        void Editar(Produto produto);
        void Excluir(Guid id);

    }
}
