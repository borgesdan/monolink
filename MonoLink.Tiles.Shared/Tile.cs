using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoLink;

namespace MonoLink.Tiles
{
    public abstract class Tile
    {
        public Game Game { get; }
        public bool IsEnabled { get; set; } = true;
        public bool IsVisible { get; set; } = true;

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

        protected Tile(Game game)
        {
            Game = game;
        }

        protected Tile(Tile source)
        {
            this.Game = source.Game;
            this.Position = source.Position;
            this.Color = source.Color;
            this.Effects = source.Effects;
            this.LayerDepth = source.LayerDepth;            
            this.Origin = source.Origin;
            this.Rotation = source.Rotation;
            this.Scale = source.Scale;
        }

        public void Update(GameTime gameTime) 
        {
            if(IsEnabled)
            {
                OnUpdate(gameTime);
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            if (IsVisible)
            {
                OnDraw(gameTime, spriteBatch);
            }
        }

        protected virtual void OnUpdate(GameTime gameTime) { }
        protected virtual void OnDraw(GameTime gameTime, SpriteBatch spriteBatch) { }
    }
}