using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoLink.Entities
{
    /// <summary>
    /// Representa um objeto de jogo com propriedades 2D.
    /// </summary>
    public abstract class Entity2D : Entity
    {
        /// <summary>Obtém ou define a origem de uma entidade 2D.</summary>
        public Vector2 Origin = Vector2.Zero;
        /// <summary>Obtém ou define os efeitos de flip de uma entidade 2D.</summary>
        public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
        /// <summary>Obtém ou define a cor de desenho de uma entidade 2D.</summary>
        public Color Color { get; set; } = Color.White;
        /// <summary>Obtém ou define a profundidade de desenho de uma entidade 2D.</summary>
        public float LayerDepth { get; set; } = 0;

        /// <summary>
        /// Obtém os limites da entidade na tela.
        /// </summary>
        public Rectangle Bounds { get; protected set; }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="game">A instância corrente da classe Game.</param>
        /// <param name="name">O nome da entidade.</param>
        public Entity2D(Game game, string name = "") : base(game, name)
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser coipada.</param>
        public Entity2D(Entity2D source) : base(source)
        {
            this.Origin = source.Origin;
            this.Color = source.Color;
            this.SpriteEffects = source.SpriteEffects;
            this.LayerDepth = source.LayerDepth;
        }
    }
}
