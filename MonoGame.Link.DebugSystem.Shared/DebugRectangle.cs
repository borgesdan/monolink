using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Common;

namespace Microsoft.Xna.Framework.DebugSystem
{
    /// <summary>
    /// Representa uma classe utilizada para visualizar os retângulos que não visíveis no jogo.
    /// </summary>
    public class DebugRectangle
    {
        List<Tuple<Rectangle, Color>> rectangles = new List<Tuple<Rectangle, Color>>();
        BasicEffect effect = null;
        List<VertexPositionColor[]> vertices = new List<VertexPositionColor[]>();        

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="game">A instância corrente da classe Game.</param>
        public DebugRectangle(Game game)
        {
            effect = new BasicEffect(game.GraphicsDevice);
            effect.VertexColorEnabled = true;
            effect.World = Matrix.CreateOrthographicOffCenter(0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height, 0, 0, 1);
        }

        /// <summary>
        /// Atualiza os retângulos adicionados.
        /// </summary>
        public void Update()
        {
            vertices.Clear();

            for (int i = 0; i < rectangles.Count; i++)
            {
                Rectangle r = rectangles[i].Item1;
                Color c = rectangles[i].Item2;

                Vector3 topLeft = new Vector3(r.X, r.Y, 0);
                Vector3 topRight = new Vector3(r.Right, r.Y, 0);
                Vector3 bottomRight = new Vector3(r.Right, r.Bottom, 0);
                Vector3 bottomLeft = new Vector3(r.X, r.Bottom, 0);

                VertexPositionColor[] array = new[]
                {
                    new VertexPositionColor(topLeft, c),
                    new VertexPositionColor(topRight, c),
                    new VertexPositionColor(bottomRight, c),
                    new VertexPositionColor(bottomLeft, c),
                    new VertexPositionColor(topLeft, c)
                };

                vertices.Add(array);
            }
        }

        /// <summary>
        /// Limpa todos os retângulos adicionados.
        /// </summary>
        public void Clear()
        {
            rectangles.Clear();
        }

        /// <summary>
        /// Adiciona retângulos ao debug.
        /// </summary>
        /// <param name="rectangles">Os retângulos a serem adicionados.</param>
        public void Add(Color color, params Rectangle[] rectangles)
        {
            foreach (Rectangle r in rectangles)
            {
                this.rectangles.Add(new Tuple<Rectangle, Color>(r, color));
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hitFrames"></param>
        public void Add(Color color, params HitFrame[] hitFrames)
        {
            foreach(HitFrame h in hitFrames)
            {
                this.Add(color, h.Bounds);
            }
        }

        /// <summary>
        /// Desenha todos os retângulos. Deve ser utilizado após o método spriteBatch.End().
        /// </summary>
        public void Draw()
        {
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                foreach(var v in vertices)
                {
                    effect.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, v, 0, 4);
                }
            }
        }
    }
}
