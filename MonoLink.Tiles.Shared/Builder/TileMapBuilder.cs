using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoLink.Tiles.Builder
{
    public class TileMapBuilder<T> : TileBuilder<T> where T : Tile
    {
        TileStyle tileType = TileStyle.Rectangle;        

        /// <summary>Obtém ou define a posição inicial para o cálculo de ordenação dos tiles.</summary>
        public Vector2 StartPosition { get; set; } = Vector2.Zero;

        /// <summary>
        /// Inicializa uma nova instância de MapReader.
        /// </summary>
        /// <param name="map">Array de inteiros que representa o mapa de tiles.</param>
        /// <param name="table">A tabela número-tile.</param>
        /// <param name="type">O estilo do tile a ser desenhado na tela.</param>
        /// <param name="tileWidth">A largura dos tiles.</param>
        /// <param name="tileHeight">A altura dos tiles.</param>
        public TileMapBuilder(int[,] map, Dictionary<int, T> table, TileStyle type, int tileWidth, int tileHeight) : base(table, tileWidth, tileHeight)
        {
            TotalMap = map;
            tileType = type;
        }

        public override void Build()
        {
            IsBuilded = false;

            Tiles.Clear();

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
                    if (Table.ContainsKey(index))
                    {
                        T tile = Clone.Get<T>(TotalMap[row, col]);

                        //Usa as configurações de tamanho geral
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

                        tile.Position = new Vector2(x, y);
                        Tiles.Add(new Point(row, col), tile);
                    }
                }
            }

            IsBuilded = true;
        }

        /// <summary>
        /// Atualiza os tiles.
        /// </summary>
        /// <param name="gameTime">Fornece acesso aos valores de tempo do jogo.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (var t in Tiles.Values)
                t.Update(gameTime);
        }

        /// <summary>Desenha os tiles.</summary>
        /// <param name="gameTime">Fornece acesso aos valores de tempo do jogo.</param>
        /// <param name="spriteBatch">Um objeto SpriteBatch para desenho.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var t in Tiles)
                t.Value.Draw(gameTime, spriteBatch);
        }
    }
}
