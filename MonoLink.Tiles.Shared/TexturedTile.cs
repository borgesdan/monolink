using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoLink.Tiles
{
    public class TexturedTile : Tile
    {
        public Texture2D Texture { get; } = null;
        public Rectangle? Frame { get; set; } = null;

        public TexturedTile(Game game, Texture2D texture) : base(game)
        {
            Texture = texture;
        }

        public TexturedTile(Game game, Texture2D texture, Rectangle? frame) : base(game)
        {
            Texture = texture;
            Frame = frame;
        }

        public TexturedTile(TexturedTile source) : base(source)
        {
            this.Texture = source.Texture;
            this.Frame = source.Frame;
        }

        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: Texture,
                position: Position,
                sourceRectangle: Frame,
                color: Color,
                rotation: Rotation,
                origin: Origin,
                scale: Scale,
                effects: Effects,
                layerDepth: LayerDepth
                );
        }

        public override Rectangle GetBounds()
        {
            return GameHelper.GetBounds(new Transform(Position, Vector2.Zero, Scale, Rotation),
                Frame.HasValue ? Frame.Value.Width : Texture.Width,
                Frame.HasValue ? Frame.Value.Height : Texture.Height,
                Origin);
        }
    }
}
