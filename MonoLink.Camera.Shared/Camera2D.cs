using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoLink.Camera
{
    /// <summary>
    /// Representa uma projeção de câmera no desenho 2D.
    /// </summary>
    public class Camera2D
    {
        private readonly Game game = null;

        //---------------------------------------//
        //-----         PROPRIEDADES        -----//
        //---------------------------------------//
        
        /// <summary>Obtém ou define o zoom da câmera.</summary>
        public float Zoom { get; set; } = 1;
        /// <summary>Obtém ou define o deslocamento do zoom.</summary>
        public Vector2 ZoomOffset { get; set; }

        /// <summary>Obtém ou define a posição da câmera.</summary>
        public Vector2 Position { get; set; }
        /// <summary>Obtém ou define a posição no eixo X da câmera.</summary>
        public float X { get => Position.X; set => Position = new Vector2(value, Y); }
        /// <summary>Obtém ou define a posição no eixo Y da câmera.</summary>
        public float Y { get => Position.Y; set => Position = new Vector2(X, value); }
        /// <summary>Obtém o tamanho total do campo de exibição da câmera.</summary> 
        public Rectangle Bounds { get => new Rectangle(Position.ToPoint(), game.GraphicsDevice.Viewport.Bounds.Size); }

        //---------------------------------------//
        //-----         CONSTRUTOR          -----//
        //---------------------------------------//

        /// <summary>
        /// Inicializa uma nova instância da classe Câmera.
        /// </summary>
        /// <param name="game">A classe Game do jogo.</param>
        ///<param name="position">Define a posição inicial da câmera.</param>
        /// <param name="zoom">O zoom da câmera (por padrão 1).</param>
        public Camera2D(Game game, Vector2 position, float zoom)
        {
            Position = position;
            Zoom = zoom;
            this.game = game;

            Rectangle view = game.GraphicsDevice.Viewport.Bounds;
            ZoomOffset = new Vector2(view.Width * 0.5f, view.Height * 0.5f);
        }

        /// <summary>
        /// Inicializa uma nova instância da classe Camera.
        /// </summary>
        public Camera2D(Game game) : this(game, Vector2.Zero, 1f) { }

        /// <summary>
        /// Inicializa uma nova instância da classe Camera como cópia de outra instância.
        /// </summary>
        /// <param name="source">Instância a ser copiada.</param>
        public Camera2D(Camera2D source)
        {
            Position = source.Position;
            Zoom = source.Zoom;
            game = source.game;
        }

        //---------------------------------------//
        //-----         FUNÇÕES             -----//
        //---------------------------------------//                

        /// <summary>Movimenta a câmera no sentido especificado.</summary>
        /// <param name="amount">O valor a ser movida a câmera.</param>
        public void Move(Vector2 amount)
        {
            Position += amount;
        }

        /// <summary>
        /// Movimenta a câmera no sentido específicado.
        /// </summary>
        /// <param name="x">O valor do movimento no eixo X.</param>
        /// <param name="y">O valor do movimento no eixo Y.</param>
        public void Move(float x, float y)
        {
            Move(new Vector2(x, y));
        }

        /// <summary>
        /// Aplica zoom na câmera.
        /// </summary>
        /// <param name="zoom">O incremento do zoom.</param>
        public void ZoomIn(float zoom)
        {
            Zoom += zoom;
        }        

        /// <summary>
        /// Foca a câmera em um determinado ator de jogo.
        /// </summary>   
        /// <param name="bounds">Os limites do objeto.</param>
        public void Focus(Rectangle bounds)
        {
            X = bounds.Center.X - game.GraphicsDevice.Viewport.Width / 2;
            Y = bounds.Center.Y - game.GraphicsDevice.Viewport.Height / 2;
        }

        /// <summary>
        /// Obtém a Matrix a ser usada no método 
        /// SpriteBatch.Begin(transformMatrix).
        /// </summary>
        public Matrix GetTransform()
        {
            return Matrix.CreateTranslation(-X - ZoomOffset.X, -Y - ZoomOffset.Y, 0)
                * Matrix.CreateScale(Zoom, Zoom, 1)
                * Matrix.CreateTranslation(ZoomOffset.X, ZoomOffset.Y, 0);
        }
    }
}
