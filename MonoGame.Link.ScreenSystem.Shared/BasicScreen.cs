using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Common;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.ScreenSystem
{
    public class BasicScreen : BasicScreen<IUpdateDraw>
    {
        public BasicScreen(string name = "") : base(name)
        {
        }

        public BasicScreen(BasicScreen source) : base(source)
        {
        }
    }

    public class BasicScreen<T> : Screen where T : IUpdateDraw
    {
        public List<T> Items { get; set; } = new List<T>();
        
        /// <summary>
        /// Inicializa umva nova instância da classe.
        /// </summary>
        /// <param name="name">Define o nome da tela.</param>
        public BasicScreen(string name = "") : base(name)
        {
        }

        public BasicScreen(BasicScreen<T> source) : base(source)
        {
            for (int i = 0; i < source.Items.Count; i++)
            {
                Items.Add(source.Items[i]);
            }
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            for(int i = 0; i < Items.Count; i++)
            {
                Items[i].Update(gameTime);
            }
        }

        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Draw(gameTime, spriteBatch);
            }
        }
    }
}
