using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Common;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.EntitySystem
{
    /// <summary>
    /// Classe que realiza ações em um Element.
    /// </summary>
    public class Job : IDisposable, IUpdate, IDraw, INameable, IOwner<Element>
    {
        bool disposed = false;

        /// <summary>Obtém ou define se o Job está habilitado a sofrer atualizações.</summary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>Obtém ou define se o Job está habilitado a ser desenhado na tela.</summary>
        public bool IsVisible { get; set; } = true;
        /// <summary>Obtém ou define um nome específico para o componente.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Obtém o elemento associado a esse widget.</summary>
        public Element Owner { get; set; } = null;
        /// <summary>Obtém a instância corrente da classe Game.</summary>
        public Game Game { get => Owner?.Game; }

        //------------- CONSTRUCTOR --------------//

        /// <summary>
        /// Inicializa uma nova instância de Job.
        /// </summary>
        internal Job() { }

        /// <summary>
        /// Inicializa uma nova instância do Job como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada</param>
        internal Job(Job source)
        {
            this.IsEnabled = source.IsEnabled;
            this.IsVisible = source.IsVisible;
            this.Name = source.Name;
            this.Owner = null;
        }

        //------------- METHODS --------------//

        /// <summary>
        /// Atualiza o Job.
        /// </summary>
        /// <param name="gameTime">Obtém o acesso aos tempos de jogo.</param>
        public void Update(GameTime gameTime)
        {
            if (IsEnabled)
            {
                OnUpdate(gameTime);
            }
        }

        /// <summary>
        /// Desenha o Job.
        /// </summary>
        /// <param name="gameTime">Obtém o acesso aos tempos de jogo.</param>
        /// <param name="spriteBatch">Uma instância de SpriteBatch para desenhar na tela.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                OnDraw(gameTime, spriteBatch);
            }
        }

        /// <summary>
        /// Sobrecarregue este método para definir as atualizações do componente a ser chamado no método Update.
        /// </summary>
        /// <param name="gameTime">Obtém o acesso aos tempos de jogo.</param>
        protected virtual void OnUpdate(GameTime gameTime) { }

        /// <summary>
        /// Sobrecarregue este método para definir o desenho do componente a ser chamado no método Draw.
        /// </summary>
        /// <param name="gameTime">Obtém o acesso aos tempos de jogo.</param>
        protected virtual void OnDraw(GameTime gameTime, SpriteBatch spriteBatch) { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Owner = null;
                }

                disposed = true;
            }
        }
    }
}