//Danilo Borges Santos, 24/03/2021.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoLink
{
    /// <summary>
    /// Representa a base de uma animação 2D.
    /// </summary>
    public abstract class Animation2D : IUpdateDraw
    {
        protected int time = 0;

        /// <summary>Obtém ou define se a animação está apta a ser atualizada.</summary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>Obtém ou define se a animação está apta a ser desenhada.</summary>
        public bool IsVisible { get; set; } = true;
        /// <summary>Obtém ou define um nome para a animação.</summary>
        public string Name { get; set; } = "";
        /// <summary>Obtém ou define em milisegundos o tempo de exibição de cada imagem.</summary>
        public int Time { get => time; set => time = Math.Abs(value); }

        /// <summary>Obtém o tempo total da animação.</summary>
        public abstract TimeSpan TotalTime { get; }
        /// <summary>Obtém o tempo que já se passou da animação.</summary>
        public abstract TimeSpan ElapsedTime { get; }

        /// <summary>Retorna True caso o tempo passado seja maior que o tempo total da animação.</summary>
        public bool IsFinished { get; protected set; } = false;

        /// <summary>Obtém ou define a posição.</summary>
        public Vector2 Position { get; set; } = Vector2.Zero;
        /// <summary>Obtém ou define a escala.</summary>
        public Vector2 Scale { get; set; } = Vector2.One;
        /// <summary>Obtém ou define a rotação.</summary>
        public float Rotation { get; set; } = 0;
        /// <summary>Obtém ou define a cor da textura a ser desenhada.</summary>
        public Color Color { get; set; } = Color.White;
        /// <summary>Obtém ou define a origem do desenho e da rotação.</summary>
        public Vector2 Origin { get; set; } = Vector2.Zero;
        /// <summary>Obtém ou define o LayerDepth no desenho do componente, de 0f a 1f, se necessário.</summary>
        public float LayerDepth { get; set; } = 0;
        /// <summary>Obtém ou define os efeitos de espelhamento.</summary>
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;

        /// <summary>Inicializa uma nova instância da classe.</summary>                
        /// <param name="time">O tempo em milisegundos de cada quadro da animação.</param>        
        /// <param name="name">Um nome para a animação.</param>
        protected Animation2D(int time = 150, string name = "")
        {
            Time = time;
            Name = name;
        }

        /// <summary>Inicializa uma nova instância da classe com um construtor de cópia.</summary>
        /// <param name="source">A animação a ser copiada.</param>
        protected Animation2D(Animation2D source)
        {
            this.IsVisible = source.IsVisible;
            this.IsEnabled = source.IsEnabled;            
            this.Time = source.Time;
        }

        /// <summary>
        /// Atualiza a animação.
        /// </summary>
        /// <param name="gameTime">Providência o tempo de jogo.</param>
        public abstract void Update(GameTime gameTime);
        /// <summary>
        /// Desenha a animação.
        /// </summary>
        /// <param name="gameTime">Providência o tempo de jogo.</param>
        /// <param name="spriteBatch">A instância do SpriteBatch corrente.</param>
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        /// <summary>
        /// Redefine a animação.
        /// </summary>
        public abstract void Reset();
        /// <summary>
        /// Obtém os limites da animação.
        /// </summary>
        /// <returns></returns>
        public abstract Rectangle GetBounds();
    }
}