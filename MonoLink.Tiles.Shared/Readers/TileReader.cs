using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoLink.Tiles
{
    /// <summary>
    /// Classe abstrada que desenha tiles na tela através de um array bidimensional de inteiros.
    /// </summary>
    /// <typeparam name="T">T é uma classe que herda de Tile.</typeparam>
    public abstract class TileReader<T> : ITileReader where T : Tile
    {
        protected int[,] finalMap = null;
        //O estilo do tyle, se retângular ou isometrico
        protected TileStyle tileType = TileStyle.Rectangle;
        //Obtém as informações necessárias para o desenho de cada tile
        protected readonly List<TileInfo> infoList = new List<TileInfo>();
        //Obtém o index do tileinfo na var infoList informando a linha e a coluna do mapa
        //Útil para descobrir rapidamente qual tile o tileinfo referencia
        protected readonly Dictionary<Point, int> infoIndexList = new Dictionary<Point, int>();

        /// <summary>Obtém as informações de desenho de cada tile na tela.</summary>
        public List<TileInfo> TileInfos { get => infoList; }
        /// <summary>Obtém ou define a posição inicial para o cálculo de ordenação dos tiles.</summary>
        public Vector2 StartPosition { get; set; } = Vector2.Zero;

        /// <summary>Obtém se o método Read() leu todo seu conteúdo e chegou ao fim.</summary>
        public bool IsRead { get; protected set; } = false;
        /// <summary>Obtém ou define a largura dos tiles para cálculos posteriores.</summary>
        public int TileWidth { get; protected set; } = 0;
        /// <summary>Obtém ou define a altura dos tiles para cálculos posteriores.</summary>
        public int TileHeight { get; protected set; } = 0;
        /// <summary>Obtém ou define a tabela de índices com seus respectivos Tiles.</summary>
        public Dictionary<int, T> Table { get; protected set; } = new Dictionary<int, T>();

        protected TileReader(Dictionary<int, T> table, int tileWidth, int tileHeight)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Table = table;
        }

        protected TileReader(TileReader<T> source)
        {
            this.finalMap = (int[,])source.finalMap.Clone();
            this.IsRead = source.IsRead;
            this.TileWidth = source.TileWidth;
            this.TileHeight = source.TileHeight;
            this.Table = source.Table;
        }

        public abstract void Read();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        /// <summary>
        /// Obtém o valor da posição informada no mapa.
        /// </summary>
        /// <param name="row">A linha desejada.</param>
        /// <param name="column">A coluna desejada.</param>
        public int GetValue(int row, int column)
        {
            return finalMap[row, column];
        }             

        /// <summary>
        /// Obtém o mapa.
        /// </summary>
        public int[,] GetMap()
        {
            return (int[,])finalMap.Clone();
        }

        /// <summary>
        /// Obtém o número de elementos do mapa total informando a dimensão.
        /// </summary>
        /// <param name="dimension">Linha = 0 ou coluna = 1</param>        
        public int GetLength(int dimension)
        {
            return finalMap.GetLength(dimension);
        }

        /// <summary>
        /// Procura e retorna a posição de um TileInfo na propriedade TileInfos ao informar a linha e a coluna no mapa.
        /// </summary>
        /// <param name="row">A linha no mapa.</param>
        /// <param name="column">A coluna no mapa.</param>
        public int GetTileInfoIndex(int row, int column)
        {
            return infoIndexList[new Point(row, column)];
        }

        /// <summary>
        /// Obtém um objeto TileInfo ao informar a linha e a coluna no mapa.
        /// </summary>
        /// <param name="row">A linha no mapa.</param>
        /// <param name="column">A coluna no mapa.</param>
        public TileInfo GetTileInfo(int row, int column)
        {
            int infoIndex = infoIndexList[new Point(row, column)];
            return infoList[infoIndex];
        }

        /// <summary>
        /// Substitui um determinado TileInfo ao informar a linha e coluna no mapa.
        /// </summary>
        /// <param name="row">A linha no mapa.</param>
        /// <param name="column">A coluna no mapa.</param>
        /// <param name="tileInfo">O objeto TileInfo para substituição.</param>
        public void Replace(int row, int column, TileInfo tileInfo)
        {
            int infoIndex = infoIndexList[new Point(row, column)];
            infoList[infoIndex] = tileInfo;
        }

        /// <summary>
        /// Movimenta o tile com um determinado valor de acrescimo.
        /// </summary>
        /// <param name="row">A linha no mapa.</param>
        /// <param name="column">A coluna no mapa.</param>
        /// <param name="amount">O valor da movimentação a ser acrescido.</param>
        public void Move(int row, int column, Vector2 amount)
        {
            int infoIndex = infoIndexList[new Point(row, column)];
            TileInfo info = infoList[infoIndex];
            info.Position += amount;
            infoList[infoIndex] = info;
        }

        public void MoveRange(int startRow, int endRow, int startColumn, int endColumn, Vector2 amount)
        {
            for (int r = startRow; r < endRow + 1; r++)
            {
                for (int c = startColumn; c < endColumn + 1; c++)
                {
                    Move(r, c, amount);
                }
            }
        }

        public abstract Rectangle GetTileBounds(int row, int column);
    }
}
