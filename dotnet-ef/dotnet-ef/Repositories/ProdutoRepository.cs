using dotnet_ef.Context;
using dotnet_ef.Domains;
using dotnet_ef.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace dotnet_ef.Repositories
{
    public class ProdutoRepository : IProduto
    {
        private readonly PedidoContext _ctx;

        public ProdutoRepository()
        {
            _ctx = new PedidoContext();
        }

        /// <summary>
        /// Adiciona um produto
        /// </summary>
        /// <param name="produto"></param>
        public void Adicionar(Produto produto)
        {
            try
            {
                _ctx.Produtos.Add(produto);
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Busca um produto por seu Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna o produto buscado pelo Id.</returns>
        public Produto BuscarPorID(Guid id)
        {
            try
            {
                return _ctx.Produtos.Find(id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Busca pelo nome do produto.
        /// </summary>
        /// <param name="nome"></param>
        /// <returns>Retorna o produto buscado pelo nome.</returns>
        public List<Produto> BuscarPorNome(string nome)
        {
            try
            {
                List<Produto> produtos = _ctx.Produtos.Where(c => c.NomeProduto.Contains(nome)).ToList();
                return produtos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Edita um produto já cadastrado.
        /// </summary>
        /// <param name="produto"></param>
        public void Editar(Produto produto)
        {
            try
            {
                //Busco um produto pelo seu Id
                Produto produtoTemp = BuscarPorID(produto.Id);

                //Altera as propriedades do produto
                produtoTemp.NomeProduto = produto.NomeProduto;
                produtoTemp.Preco = produto.Preco;

                //Altera o produto no contexto
                _ctx.Produtos.Update(produtoTemp);
                //Salva o produto
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deleta um produto já cadastrado.
        /// </summary>
        /// <param name="id"></param>
        public void Excluir(Guid id)
        {
            try
            {
                Produto produto = BuscarPorID(id);

                if (produto == null)
                    throw new Exception("Produto não encontrado.");

                _ctx.Produtos.Remove(produto);
                _ctx.SaveChanges();
            
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista todos os produtos cadastrados.
        /// </summary>
        /// <returns>Retorna todos os produtos e seus dados, respectivamente.</returns>
        public List<Produto> Listar()
        {
            try
            {
                return _ctx.Produtos.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
