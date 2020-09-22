using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using dotnet_ef.Domains;
using dotnet_ef.Interface;
using dotnet_ef.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnet_ef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private readonly IProduto _produtoRepository;

        public ProdutosController()
        {
            _produtoRepository = new ProdutoRepository();
        }
        
        /// <summary>
        /// Ler todos os produtos cadastrados
        /// </summary>
        /// <returns>Produtos</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var produtos = _produtoRepository.Listar();

                if (produtos.Count == 0)
                    return NoContent();

                return Ok(new
                {
                    totalCount = produtos.Count,
                    data = produtos
                });
            }
            catch (Exception)
            {

                return BadRequest(new
                {
                    statusCode = 400,
                    error = "Ocorreu um erro no endpoint Get/produtos. Envie um e-mail para email@email.com informando."
                });
            }
        }

        /// <summary>
        /// Ler um produto a partir do seu Id
        /// </summary>
        /// <param name="id">Id do Produto</param>
        /// <returns>Produto pelo seu Id</returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                Produto produto = _produtoRepository.BuscarPorID(id);

                if (produto == null)
                    return NotFound();

                return Ok(produto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //FromForm - Recebe os dados do produto via Form-Data.
        /// <summary>
        /// Cadastra um produto no banco de dados
        /// </summary>
        /// <param name="produto"></param>
        /// <returns>Produto já cadastrado</returns>
        [HttpPost]
        public IActionResult Post([FromForm]Produto produto)
        {
            try
            {
                //Verifico se foi enviado um arquivo com a imagem
                if(produto.Imagem != null)
                {
                    //Gera o nome do arquivo único 
                    //Pego a extensão do arquivo 
                    //Concateno o nome do arquivo com sua extensão
                    var nomeArquivo = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(produto.Imagem.FileName);

                    //GetCurrentDirectory - Pega o caminho do diretório atual, aplicação esta 
                    var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), @"wwwRoot\upload\imagens", nomeArquivo);

                    //Crio um objeto do tipo FileStream passanda o caminho do arquivo
                    //Passa para criar este arquivo     
                    using var streamImagem = new FileStream(caminhoArquivo, FileMode.Create);

                    //Execute o comando da criação do arquivo no local informado
                    produto.Imagem.CopyTo(streamImagem);

                    produto.UrlImagem = "http://localhost:52090/upload/imagens" + nomeArquivo;
                }

                //Adiciona um produto
                _produtoRepository.Adicionar(produto);
                //Retorna ok com os dados do produto.
                return Ok(produto);
            }
            catch (Exception ex)
            {
                //Caso ocorra um erro retorna BadRequest com a mensagem.
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Altera um produto já cadastrado no banco de dados.
        /// </summary>
        /// <param name="id">Id do Produto</param>
        /// <param name="produto">Produto alterado</param>
        /// <returns>Produto já alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Produto produto)
        {
            try
            {
                //Busca o produto pelo seu id.
                var produtoTemp = _produtoRepository.BuscarPorID(id);
                //Caso o produto seja nulo, retorna NotFound (Não Encontrado)
                if (produtoTemp == null)
                    return NotFound();

                //Edita o produto.
                _produtoRepository.Editar(produto);

                //Retorna ok com os dados alterados de um produto que exista.
                return Ok(produto);
            }
            catch (Exception ex)
            {
                //Caso ocorra um erro retorna BadRequest com a mensagem.
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete um produto a partir do seu Id.
        /// </summary>
        /// <param name="id">Id do Produto</param>
        /// <returns>Ok</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                //Excluir um produto a partir do seu id.
                _produtoRepository.Excluir(id);

                //Retorna ok caso o produto seja escluído
                return Ok(id);
            }
            catch (Exception ex)
            {
                //Caso ocorra um erro retorna BadRequest com a mensagem.
                return BadRequest(ex.Message);
            }
        }
    }
}
