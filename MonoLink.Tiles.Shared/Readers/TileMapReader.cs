using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoLink.Tiles
{
    /// <summary>
    /// Representa uma classe que desenha os tiles na tela através da leitura de um dicionário int-tile.
    /// </summary>    
    public class TileMapReader : TileReader
    {
        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="map">Array de inteiros que representa o mapa de tiles.</param>
        /// <param name="table">A tabela número-tile.</param>
        /// <param name="type">O estilo do tile a ser desenhado na tela.</param>
        /// <param name="tileWidth">A largura dos tiles.</param>
        /// <param name="tileHeight">A altura dos tiles.</param>
        public TileMapReader(int[,] map, Dictionary<int, Tile> table, TileStyle type, int tileWidth, int tileHeight) : base(table, tileWidth, tileHeight)
        {
            finalMap = map;
            tileType = type;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public TileMapReader(TileMapReader source) : base(source)
        {
            foreach (TileInfo i in source.infoList)
                this.infoList.Add(i);
        }        

        /// <summary>
        /// Lê o array contido no mapa e ordena as posições dos tiles.
        /// </summary>
        public override void Read()
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

                    if(Table.ContainsKey(index))
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
            for(int i = 0; i < infoList.Count; i++)
            {
                var tile = Table[infoList[i].Value];
                tile.Position = infoList[i].Position;
                tile.Color = infoList[i].Color;
                tile.Draw(gameTime, spriteBatch);
            }
        }        
    }
}