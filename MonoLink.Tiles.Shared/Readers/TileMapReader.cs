using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoLink.Tiles
{
    public class TileMapReader<T> : MapReader<T> where T : Tile
    {
        int[,] mapArray = null;
        TileType tileType = TileType.Rectangle;
        List<TileInfo> infoList = new List<TileInfo>();

        /// <summary>Obtém ou define a posição inicial para o cálculo de ordenação dos tiles.</summary>
        public Vector2 StartPosition { get; set; } = Vector2.Zero;

        /// <summary>
        /// Inicializa uma nova instância de MapReader.
        /// </summary>
        /// <param name="tileWidth">A largura dos tiles.</param>
        /// <param name="tileHeight">A altura dos tiles.</param>
        public TileMapReader(int[,] map, Dictionary<int, T> table, TileType type, int tileWidth, int tileHeight) : base(table, tileWidth, tileHeight)
        {
            this.mapArray = map;
            tileType = type;
        }

        /// <summary>
        /// Lê o array contido no mapa e ordena as posições dos tiles.
        /// </summary>
        public override void Read()
        {
            IsRead = false;

            TotalMap = mapArray;

            //dimensões do array
            int d0 = TotalMap.GetLength(0);
            int d1 = TotalMap.GetLength(1);

            for (int row = 0; row < d0; row++)
            {
                for (int col = 0; col < d1; col++)
                {
                    //O valor da posição no array
                    int index = TotalMap[row, col];                    

                    if(Table.ContainsKey(index))
                    {
                        int w = TileWidth;
                        int h = TileHeight;
                        float sx = StartPosition.X;
                        float sy = StartPosition.Y;

                        float x, y;

                        if (tileType == TileType.Rectangle)
                        {
                            x = (w * row) + sx;
                            y = (h * col) + sy;
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

        public override void Update(GameTime gameTime)
        {
            foreach (var t in Table.Values)
                t.Update(gameTime);            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for(int i = 0; i < infoList.Count; i++)
            {
                var tile = Table[infoList[i].Index];
                tile.Position = infoList[i].Position;
                tile.Draw(gameTime, spriteBatch);
            }
        }
    }
}