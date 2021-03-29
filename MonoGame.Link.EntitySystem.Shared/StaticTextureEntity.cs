﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Common;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.EntitySystem
{
    /// <summary>
    /// Representa uma entidade que fornece acesso a atualização e desenho de um Texture2D.
    /// </summary>
    public sealed class StaticTextureEntity : Entity
    {
        /// <summary>
        /// Obtém ou define a textura com seus frames.
        /// </summary>
        public TextureItem Texture { get; set; } = null;

        /// <summary>
        /// Obtém ou define o index do frame a ser utilizado.
        /// </summary>
        public int FrameIndex { get; set; } = 0;

        public override Rectangle Bounds2D
        {
            get
            {
                if(Texture != null && Texture.Texture != null && Texture.ContainsFrames)
                {
                    TextureFrame frame = Texture.Frames[FrameIndex].Frame;
                    Util.GetBounds(Transform, frame.Width, frame.Height, Origin, frame.AlignX, frame.AlignY);
                }

                return Rectangle.Empty;
            }
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public StaticTextureEntity(StaticTextureEntity source) : base(source)
        {
            this.Texture = new TextureItem(source.Texture);
            this.FrameIndex = source.FrameIndex;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="game">A instância da classe Game.</param>
        /// <param name="texture">Uma instância da classe TextureItem.</param>
        /// <param name="name">O nome da entidade.</param>
        public StaticTextureEntity(Game game, TextureItem texture, string name = "") : base(game, name)
        {
            this.Texture = texture;
        }

        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(Texture != null && Texture.Texture != null)
            {
                Texture2D texture = Texture.Texture;

                if (Texture.ContainsFrames)
                {                    
                    Rectangle source = Texture.Frames[FrameIndex].Frame.Bounds;

                    spriteBatch.Draw(
                        texture: texture,
                        position: Transform.Position2,
                        sourceRectangle: source,
                        color: this.Color,
                        rotation: Transform.Rotation2,
                        origin: this.Origin,
                        scale: Transform.Scale2,
                        effects: this.SpriteEffects,
                        layerDepth: this.LayerDepth
                        );
                }
                else
                {
                    spriteBatch.Draw(
                        texture: texture,
                        position: Transform.Position2,
                        sourceRectangle: null,
                        color: this.Color,
                        rotation: Transform.Rotation2,
                        origin: this.Origin,
                        scale: Transform.Scale2,
                        effects: this.SpriteEffects,
                        layerDepth: this.LayerDepth
                        );
                }
            }
        }
    }
}