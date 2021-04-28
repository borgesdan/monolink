using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoLink.Tiles
{
    public abstract class TileBuilder<T> : ITileBuilder where T : Tile
    {
        protected int[,] TotalMap = null;

        /// <summary>Obtém se o método Read() leu todo seu conteúdo e chegou ao fim.</summary>
        public bool IsBuilded { get; protected set; } = false;
        /// <summary>Obtém ou define a largura dos tiles para cálculos posteriores.</summary>
        public int TileWidth { get; protected set; } = 0;
        /// <summary>Obtém ou define a altura dos tiles para cálculos posteriores.</summary>
        public int TileHeight { get; protected set; } = 0;
        /// <summary>Obtém ou define a tabela de índices com seus respectivos Tiles.</summary>
        public Dictionary<int, T> Table { get; protected set; } = new Dictionary<int, T>();
        /// <summary>Obtém a lista de tiles ordenados pelo método Read(). Point representa a linha e a coluna onde se encontra o Tile.</summary>
        public Dictionary<Point, T> Tiles { get; protected set; } = new Dictionary<Point, T>();

        protected TileBuilder(Dictionary<int, T> table, int tileWidth, int tileHeight)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Table = table;
        }

        public abstract void Build();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        /// <summary>
        /// Obtém o valor da posição informada no mapa.
        /// </summary>
        /// <param name="row">A linha desejada.</param>
        /// <param name="column">A coluna desejada.</param>
        public int GetValue(int row, int column)
        {
            return TotalMap[row, column];
        }

        /// <summary>
        /// Obtém o mapa.
        /// </summary>
        public int[,] GetMap()
        {
            return (int[,])TotalMap.Clone();
        }

        /// <summary>
        /// Obtém o número de elementos do mapa total informando a dimensão.
        /// </summary>
        /// <param name="dimension">Linha = 0 ou coluna = 1</param>        
        public int GetLength(int dimension)
        {
            return TotalMap.GetLength(dimension);
        }
    }
}
