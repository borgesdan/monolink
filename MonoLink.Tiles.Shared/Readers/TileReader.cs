using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoLink.Tiles
{
    /// <summary>
    /// Classe abstrada que desenha tiles na tela através de um array bidimensional de inteiros.
    /// </summary>
    public abstract class TileReader
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
        public Dictionary<int, Tile> Table { get; protected set; } = new Dictionary<int, Tile>();

        protected TileReader(Dictionary<int, Tile> table, int tileWidth, int tileHeight)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Table = table;
        }

        protected TileReader(TileReader source)
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
        /// Obtém o valor da coordenada no mapa.
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
        /// Obtém o número de elementos do mapa informando a sua dimensão.
        /// </summary>
        /// <param name="dimension">Somente duas dimensões, linha = 0 ou coluna = 1.</param>
        public int GetLength(int dimension)
        {
            return finalMap.GetLength(dimension);
        }

        /// <summary>
        /// Busca e retorna o index de um TileInfo dentro da lista TileInfos as coordenadas no mapa.
        /// </summary>
        /// <param name="row">A linha no mapa.</param>
        /// <param name="column">A coluna no mapa.</param>
        public int GetTileInfoIndex(int row, int column)
        {
            return infoIndexList[new Point(row, column)];
        }

        /// <summary>
        /// Obtém um objeto TileInfo ao informar as coordenadas no mapa.
        /// </summary>
        /// <param name="row">A linha no mapa.</param>
        /// <param name="column">A coluna no mapa.</param>
        public TileInfo GetTileInfo(int row, int column)
        {
            int infoIndex = infoIndexList[new Point(row, column)];
            return infoList[infoIndex];
        }

        /// <summary>
        /// Substitui um determinado TileInfo ao informar as coordenadas no mapa.
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
        /// Incrementa a posição de um TileInfo com um determinado valor de acrescimo ao informar as coordenadas no mapa.
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

        /// <summary>
        /// Incrementa a posição de vários TileInfo com um determinado valor de acrescimo ao infomar as coordenadas de início e fim no mapa.
        /// </summary>
        /// <param name="startRow">A linha no mapa a ser o ponto de partida.</param>
        /// <param name="endRow">A linha no mapa que será o ponto final.</param>
        /// <param name="startColumn">A coluna no mapa a ser o ponto de partida.</param>
        /// <param name="endColumn">A coluna no mapa que será o ponto final.</param>
        /// <param name="amount">O valor de movimentação a ser acrescido.</param>
        public void MoveRange(int startRow, int endRow, int startColumn, int endColumn, Vector2 amount)
        {
            for (int r = startRow; r < endRow + 1; r++)
            {
                for (int c = startColumn; c < endColumn + 1; c++)
                {
                    if(CheckTileInfo(r, c))
                    {
                        Move(r, c, amount);
                    }
                }
            }
        }

        /// <summary>
        /// Verifica se existe um objeto TileInfo nas coordenadas do mapa.
        /// </summary>
        /// <param name="row">A linha no mapa.</param>
        /// <param name="column">A coluna no mapa.</param>
        public bool CheckTileInfo(int row, int column)
        {
            return infoIndexList.ContainsKey(new Point(row, column));
        }

        /// <summary>
        /// Obtém os limites de um determinado Tile ao informar as coordenadas no mapa.
        /// </summary>
        /// <param name="row">A linha no mapa.</param>
        /// <param name="column">A coluna no mapa.</param>
        public virtual Rectangle GetTileBounds(int row, int column)
        {
            int index = finalMap[row, column];
            int infoIndex = infoIndexList[new Point(row, column)];

            Tile tile = Table[index];
            TileInfo info = infoList[infoIndex];

            tile.Position = info.Position;
            tile.Color = info.Color;

            return tile.GetBounds();
        }

        /// <summary>
        /// Obtém todas as coordenadas do mapa que contenham o específico valor.
        /// </summary>
        /// <param name="value">A valor a ser buscado em todo o mapa.</param>
        public List<Point> GetAllCoord(int value)
        {
            List<Point> points = new List<Point>();

            for(int i = 0; i < finalMap.GetLength(0); i++)
            {
                for(int j = 0; j < finalMap.GetLength(1); j++)
                {
                    if (finalMap[i, j] == value)
                        points.Add(new Point(i, j));
                }
            }

            return points;
        }
    }
}
