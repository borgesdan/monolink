using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoLink;

namespace MonoLink.Tiles
{
    public class TextureTile : Tile
    {
        public Texture2D Texture { get; } = null;
        public Rectangle? Frame { get; set; } = null;

        public TextureTile(Game game, Texture2D texture) : base(game)
        {
            Texture = texture;
        }

        public TextureTile(Game game, Texture2D texture, Rectangle? frame) : base(game)
        {
            Texture = texture;
            Frame = frame;
        }

        public TextureTile(TextureTile source) : base(source)
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
    }
}
