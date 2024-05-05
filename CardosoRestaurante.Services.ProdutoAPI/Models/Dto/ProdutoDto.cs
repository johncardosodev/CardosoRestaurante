﻿namespace CardosoRestaurante.Services.ProdutoAPI.Models.Dto
{
    /// <summary>
    /// Representa um objeto de transferência de dados (DTO) para um produto.
    /// </summary>
    public class ProdutoDto
    {
        /// <summary>
        /// Obtém ou define o ID do produto.
        /// </summary>
        public int ProdutoId { get; set; }

        /// <summary>
        /// Obtém ou define o nome do produto.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Obtém ou define o preço do produto.
        /// </summary>
        public double Preco { get; set; }

        /// <summary>
        /// Obtém ou define a descrição do produto.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Obtém ou define a categoria do produto.
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Obtém ou define a URL da imagem do produto.
        /// </summary>
        public string ImagemUrl { get; set; }

        public int Porcao { get; set; }
    }
}