using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoLink.Tiles
{
    public class TileSectorReader : TileReader
    {
        //Lista dos setores que irão compor o mapa final
        readonly int[,][,] sectors = null;
        //Lista para ser manejada no método Read().
        List<int[]> total = new List<int[]>();

        /// <summary>Obtém a quantidade de linhas de cada setor do mapa.</summary>
        public int Rows { get; private set; } = 10;
        /// <summary>Obtém a quantidade de colunas de cada setor do mapa.</summary>
        public int Columns { get; private set; } = 10;

        /// <summary>
        /// Inicializa uma nova instância de SectorReader.
        /// </summary>        
        /// <param name="sectors">Um array de arrays que representa um mapa com seus setores</param>
        /// <param name="table">A tabela número-tile.</param>        
        /// <param name="type">O estilo do tile a ser desenhado na tela.</param>
        /// <param name="tileWidth">A largura dos tiles.</param>
        /// <param name="tileHeight">A altura dos tiles.</param>
        /// <param name="rows">A quantidade de linhas de cada array que representa os setores de um mapa.</param>
        /// <param name="columns">A quantidade de colunas de cada array que representa os setores de um mapa.</param>        
        public TileSectorReader(int[,][,] sectors, Dictionary<int, Tile> table, TileStyle type, int tileWidth, int tileHeight, int rows, int columns) : base(table, tileWidth, tileHeight)
        {
            this.sectors = sectors;

            tileType = type;

            Rows = rows;
            Columns = columns;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public TileSectorReader(TileSectorReader source) : base(source)
        {
            foreach (int[] t in source.total)
                this.total.Add(t);
            
            source.sectors.CopyTo(this.sectors, 0);

            foreach (TileInfo i in source.infoList)
                this.infoList.Add(i);

            this.Rows = source.Rows;
            this.Columns = source.Columns;
        }

        /// <summary>
        /// Lê o array contido nos setores e ordena as posições dos tiles.
        /// </summary>
        public override void Read()
        {
            //dimensões do array
            int d0 = sectors.GetLength(0);
            int d1 = sectors.GetLength(1);

            total = new List<int[]>(d0 * Rows);

            for (int t = 0; t < total.Capacity; t++)
                total.Add(null);

            //Confere a linha
            for (int row = 0; row < d0; row++)
            {
                //Confiro a coluna
                for (int col = 0; col < d1; col++)
                {  
                    //recebo o mapa do setor
                    int[,] _map = sectors[row, col];

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

            finalMap = new int[total.Count, total[01].GetLength(0)];

            for (int i = 0; i < total.Count; i++)
            {
                int[] row = total[i];

                for (int j = 0; j < row.GetLength(0); j++)
                {
                    finalMap[i, j] = row[j];
                }
            }

            ReadFinalMap();
        }

        private void ReadFinalMap()
        {
            IsRead = false;

            //dimensões do array
            int d0 = finalMap.GetLength(0);
            int d1 = finalMap.GetLength(1);

            for (int row = 0; row < d0; row++)
            {
                for (int col = 0; col < d1; col++)
                {
                    //O valor da posição no array
                    int index = finalMap[row, col];
                    //Recebe o Tile da tabela
                    //Dictionary<int, Tile> table = point_sector[new Point(row, col)].Table;

                    if (Table.ContainsKey(index))
                    {
                        int w = TileWidth;
                        int h = TileHeight;
                        float sx = StartPosition.X;
                        float sy = StartPosition.Y;

                        float x, y;

                        if (tileType == TileStyle.Rectangle)
                        {
                            x = (w * col) + sx;
                            y = (h * row) + sy;
                        }
                        else
                        {
                            x = ((w / 2) * -row) + ((w / 2) * col) + sx;
                            y = ((h / 2) * col) - ((h / 2) * -row) + sy;
                        }

                        //Conserto da posição com relação a origem.
                        x += Table[index].Origin.X;
                        y += Table[index].Origin.Y;

                        x *= Table[index].Scale.X;
                        y *= Table[index].Scale.Y;

                        TileInfo info;
                        info.Position = new Vector2(x, y);
                        info.Value = index;
                        info.Color = Color.White;

                        infoList.Add(info);

                        infoIndexList.Add(new Point(row, col), infoList.Count - 1);
                    }
                }
            }

            IsRead = true;
        }

        /// <summary>
        /// Obtém a linha e a coluna do mapa final informando a linha e a coluna do setor.
        /// </summary>
        /// <param name="sector">O setor informado.</param>
        /// <param name="row">A linha do setor.</param>
        /// <param name="column">A coluna do setor.</param>
        public Point GetPoint(Point sector, int row, int column)
        {
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
            int[,] m = sectors[sector.X, sector.Y];
            return m[row, column];
        }        

        /// <summary>
        /// Obtém os limites do tile informando a linha e a coluna de um setor.
        /// </summary>
        /// <param name="sector">O setor a ser buscado.</param>
        /// <param name="row">A linha desejada.</param>
        /// <param name="column">A coluna desejada.</param>
        public Rectangle GetTileBounds(Point sector, int row, int column)
        {
            Point p = GetPoint(sector, row, column);
            return GetTileBounds(p.X, p.Y);
        }

        /// <summary>
        /// Movimenta o tile com um determinado valor de acrescimo.
        /// </summary>
        /// <param name="sector">O setor a ser buscado.</param>
        /// <param name="row">A linha no mapa.</param>
        /// <param name="column">A coluna no mapa.</param>
        /// <param name="amount">O valor da movimentação a ser acrescido.</param>
        public void Move(Point sector, int row, int column, Vector2 amount)
        {
            Point p = GetPoint(sector, row, column);
            Move(p.X, p.Y, amount);
        }

        /// <summary>
        /// Substitui um determinado TileInfo ao informar a linha e coluna no mapa.
        /// </summary>
        /// <param name="sector">O setor a ser buscado.</param>
        /// <param name="row">A linha no mapa.</param>
        /// <param name="column">A coluna no mapa.</param>
        /// <param name="tileInfo">O objeto TileInfo para substituição.</param>
        public void Replace(Point sector, int row, int column, TileInfo tileInfo)
        {
            Point p = GetPoint(sector, row, column);
            Replace(p.X, p.Y, tileInfo);
        }

        /// <summary>
        /// Obtém um objeto TileInfo ao informar a linha e a coluna no mapa.
        /// </summary>
        /// <param name="sector">O setor a ser buscado.</param>
        /// <param name="row">A linha no mapa.</param>
        /// <param name="column">A coluna no mapa.</param>
        public TileInfo GetTileInfo(Point sector, int row, int column)
        {
            Point p = GetPoint(sector, row, column);
            return GetTileInfo(p.X, p.Y);
        }

        /// <summary>
        /// Procura e retorna a posição de um TileInfo na propriedade TileInfos ao informar a linha e a coluna no mapa.
        /// </summary>
        /// <param name="sector">O setor a ser buscado.</param>
        /// <param name="row">A linha no mapa.</param>
        /// <param name="column">A coluna no mapa.</param>
        public int GetTileInfoIndex(Point sector, int row, int column)
        {
            Point p = GetPoint(sector, row, column);
            return GetTileInfoIndex(p.X, p.Y);
        }

        /// <summary>
        /// Atualiza todos os tiles contidos na propriedade Table.
        /// </summary>
        /// <param name="gameTime">Obtém acesso ao tempo de jogo.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (var t in Table.Values)
                t.Update(gameTime);
        }

        /// <summary>
        /// Desenha os tiles na tela.
        /// </summary>
        /// <param name="gameTime">Obtém acesso ao tempo de jogo.</param>
        /// <param name="spriteBatch">Obtém acesso ao SpriteBatch.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < infoList.Count; i++)
            {
                var tile = Table[infoList[i].Value];
                tile.Position = infoList[i].Position;
                tile.Color = infoList[i].Color;
                tile.Draw(gameTime, spriteBatch);
            }
        }
    }
}
