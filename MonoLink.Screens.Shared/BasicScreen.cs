using System;
using System.Collections.Generic;
using System.Text;
using MonoLink;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoLink.Screens
{
    /// <summary>
    /// Representa uma tela básica de jogo.
    /// </summary>
    public class BasicScreen : BasicScreen<IUpdateDraw>
    {
        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="game">A classe Game do jogo.</param>
        /// <param name="name">Define o nome da tela.</param>
        public BasicScreen(Game game, string name = "") : base(game, name) { }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public BasicScreen(BasicScreen source) : base(source) { }
    }

    /// <summary>
    /// Representa uma tela básica de jogo.
    /// </summary>
    /// <typeparam name="T">
    /// T é um tipo que implementa a interface IUpdateDraw.
    /// Normalmente utiliza-se um tipo da classe Entity do namespace
    /// MonoLink.Entity.
    /// </typeparam>
    public class BasicScreen<T> : Screen where T : IUpdateDraw
    {
        public List<T> Items { get; set; } = new List<T>();
        
        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="game">A classe Game do jogo.</param>
        /// <param name="name">Define o nome da tela.</param>
        public BasicScreen(Game game, string name = "") : base(game, name)
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
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
