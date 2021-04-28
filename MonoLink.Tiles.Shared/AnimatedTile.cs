using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoLink.Tiles
{
    public class AnimatedTile : Tile
    {
        public Animation2D Animation { get; set; } = null;

        public AnimatedTile(Game game, Animation2D animation) : base(game)
        {
            Animation = animation;
        }

        public AnimatedTile(AnimatedTile source) : base(source)
        {
            this.Animation = source.Animation;
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            Animation.Update(gameTime);
        }

        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Animation.Draw(gameTime, spriteBatch);
        }
    }
}
