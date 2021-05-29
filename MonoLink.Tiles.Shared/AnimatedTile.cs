using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoLink.Tiles
{
    /// <summary>
    /// Representa um tile com uma animação.
    /// </summary>
    public class AnimatedTile : Tile
    {
        /// <summary>
        /// Obtém a animação do tile.
        /// </summary>
        public Animation2D Animation { get; } = null;

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="game">A instância da classe Game.</param>
        /// <param name="animation">Define a animação do tile.</param>
        public AnimatedTile(Game game, Animation2D animation) : base(game)
        {
            Animation = animation;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia da outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public AnimatedTile(AnimatedTile source) : base(source)
        {
            this.Animation = source.Animation;
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            Animation.Position = this.Position;
            Animation.Color = this.Color;
            Animation.Effects = this.Effects;
            Animation.Origin = this.Origin;
            //Animation.Rotation = this.Rotation;
            Animation.Scale = this.Scale;
            
            Animation.Update(gameTime);
        }

        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Animation.Draw(gameTime, spriteBatch);
        }

        public override Rectangle GetBounds()
        {
            return Animation.GetBounds();
        }
    }
}
