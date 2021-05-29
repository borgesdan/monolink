using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoLink;

namespace MonoLink.Tiles
{
    /// <summary>
    /// Representa um tile a ser desenhado na tela.
    /// </summary>
    public abstract class Tile
    {
        public Game Game { get; }
        public bool IsEnabled { get; set; } = true;
        public bool IsVisible { get; set; } = true;

        /// <summary>Obtém ou define um valor opcional para o Tile.</summary>
        public int Value { get; set; } = 0;
        /// <summary>Obtém ou define a posição.</summary>
        internal Vector2 Position { get; set; } = Vector2.Zero;
        /// <summary>Obtém ou define a escala.</summary>
        internal Vector2 Scale { get; set; } = Vector2.One;
        /// <summary>Obtém ou define a origem do desenho e da rotação.</summary>
        internal Vector2 Origin { get; set; } = Vector2.Zero;
        /// <summary>Obtém ou define a cor da textura a ser desenhada.</summary>
        internal Color Color { get; set; } = Color.White;        
        /// <summary>Obtém ou define os efeitos de espelhamento.</summary>
        internal SpriteEffects Effects { get; set; } = SpriteEffects.None;

        protected Tile(Game game)
        {
            Game = game;
        }

        protected Tile(Tile source)
        {
            this.Game = source.Game;
            //this.Position = source.Position;
            //this.Color = source.Color;
            //this.Effects = source.Effects;        
            //this.Origin = source.Origin;
            //this.Scale = source.Scale;
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
       
        /// <summary>
        /// Obtém os limites do tile.
        /// </summary>
        public abstract Rectangle GetBounds();
    }
}