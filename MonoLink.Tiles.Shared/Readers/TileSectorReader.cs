using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoLink;

namespace MonoLink.Tiles
{
    public class TileSectorReader<T> : MapReader<T> where T : Tile
    {
        TileType tileType = TileType.Rectangle;
        List<int[]> total = new List<int[]>();
        TileSector[,] sectorsList = null;
        Dictionary<Point, TileSector> point_sector = new Dictionary<Point, TileSector>();
        List<TileInfo> infoList = new List<TileInfo>();

        /// <summary>Obtém ou define a posição inicial para o cálculo de ordenação dos tiles.</summary>
        public Vector2 StartPosition { get; set; } = Vector2.Zero;

        public int Rows { get; set; } = 10;
        public int Columns { get; set; } = 10;

        /// <summary>
        /// Inicializa uma nova instância de SectorReader.
        /// </summary>        
        /// <param name="sectors">Os setores a serem lidos.</param>
        /// <param name="table">A tabela número-tile.</param>        
        /// <param name="type">O estilo do tile a ser desenhado na tela.</param>
        /// <param name="tileWidth">A largura dos tiles.</param>
        /// <param name="tileHeight">A altura dos tiles.</param>
        /// <param name="rows">A quantidade de linhas de cada array que representa os setores de um mapa.</param>
        /// <param name="columns">A quantidade de colunas de cada array que representa os setores de um mapa.</param>        
        public TileSectorReader(TileSector[,] sectors, Dictionary<int, T> table, TileType type, int tileWidth, int tileHeight, int rows, int columns) : base(table, tileWidth, tileHeight)
        {
            sectorsList = sectors;
            //Length = length;
            tileType = type;

            Rows = rows;
            Columns = columns;
        }

        /// <summary>
        /// Lê o array contido nos setores e ordena as posições dos tiles.
        /// </summary>
        public override void Read()
        {
            //dimensões do array
            int d0 = sectorsList.GetLength(0);
            int d1 = sectorsList.GetLength(1);

            //total = new List<int[]>(d0 * Length);
            total = new List<int[]>(d0 * Rows);

            for (int t = 0; t < total.Capacity; t++)
                total.Add(null);

            //Confere a linha
            for (int row = 0; row < d0; row++)
            {
                //Confiro a coluna
                for (int col = 0; col < d1; col++)
                {
                    //busco o setor na linha e coluna selecionada
                    TileSector s = sectorsList[row, col];
                    //recebo o mapa do setor
                    int[,] _map = s.GetSector();

                    int lr = _map.GetLength(0);
                    int lc = _map.GetLength(1);

                    //confiro a linha e a coluna do mapa
                    for (int sr = 0; sr < lr; sr++)
                    {
                        //int[] numbers = new int[Length];
                        int[] numbers = new int[Columns];

                        //faço a busca pelos números
                        for (int sc = 0; sc < lc; sc++)
                        {
                            numbers[sc] = _map[sr, sc];

                            point_sector.Add(new Point(sr + (row * lr), sc + (col * lc)), s);
                        }

                        //insiro a linha no array total
                        int ins = sr + (row * lr);

                        int[] index = total[ins];

                        if (index == null)
                            total[ins] = numbers;
                        else
                        {
                            List<int> n = new List<int>();
                            n.AddRange(index);

                            foreach (var j in numbers)
                                n.Add(j);

                            total[ins] = n.ToArray();
                        }
                    }
                }
            }

            TotalMap = new int[total.Count, total[01].GetLength(0)];

            for (int i = 0; i < total.Count; i++)
            {
                int[] row = total[i];

                for (int j = 0; j < row.GetLength(0); j++)
                {
                    TotalMap[i, j] = row[j];
                }
            }

            ReadFinalMap();
        }

        private void ReadFinalMap()
        {
            IsRead = false;

            //dimensões do array
            int d0 = TotalMap.GetLength(0);
            int d1 = TotalMap.GetLength(1);

            for (int row = 0; row < d0; row++)
            {
                for (int col = 0; col < d1; col++)
                {
                    //O valor da posição no array
                    int index = TotalMap[row, col];
                    //Recebe o Tile da tabela
                    //Dictionary<int, Tile> table = point_sector[new Point(row, col)].Table;

                    if (Table.ContainsKey(index))
                    {
                        int w = TileWidth;
                        int h = TileHeight;
                        float sx = StartPosition.X;
                        float sy = StartPosition.Y;

                        float x, y;

                        if (tileType == TileType.Rectangle)
                        {
                            x = (w * col) + sx;
                            y = (h * row) + sy;
                        }
                        else
                        {
                            x = ((w / 2) * -row) + ((w / 2) * col) + sx;
                            y = ((h / 2) * col) - ((h / 2) * -row) + sy;
                        }

                        TileInfo info;
                        info.Position = new Vector2(x, y);
                        info.Index = index;

                        infoList.Add(info);
                    }
                }
            }

            IsRead = true;
        }

        /// <summary>
        /// Obtém a linha e a coluna no mapa geral informando o setor.
        /// </summary>
        /// <param name="sector">O setor informado.</param>
        /// <param name="row">A linha desejada.</param>
        /// <param name="column">A coluna desejada.</param>
        public Point GetPoint(Point sector, int row, int column)
        {
            //int r = (Length * sector.X) + row;
            //int c = (Length * sector.Y) + column;

            int r = (Rows * sector.X) + row;
            int c = (Columns * sector.Y) + column;

            return new Point(r, c);
        }        

        /// <summary>
        /// Obtém o valor da posição informada.
        /// </summary>
        /// <param name="sector">O setor a ser buscado.</param>
        /// <param name="row">A linha desejada do setor.</param>
        /// <param name="column">A coluna desejada do setor.</param>
        public int GetValue(Point sector, int row, int column)
        {
            int[,] m = sectorsList[sector.X, sector.Y].GetSector();
            return m[row, column];
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var t in Table.Values)
                t.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < infoList.Count; i++)
            {
                var tile = Table[infoList[i].Index];
                tile.Position = infoList[i].Position;
                tile.Draw(gameTime, spriteBatch);
            }
        }
    }
}
