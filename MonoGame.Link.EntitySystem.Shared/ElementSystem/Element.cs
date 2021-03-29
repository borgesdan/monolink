using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Common;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.EntitySystem
{
    /// <summary>
    /// Representa um elemento, que representa uma parte de uma entidade.
    /// </summary>
    public class Element : IDisposable, IUpdate, INameable, IDraw, IOwner<ElementaryEntity>
    {
        //-------------     VARS    ---------------//
        bool disposed = false;
        Game game = null;

        //------------- PROPERTIES ---------------//

        /// <summary>Obtém ou define se o componente está habilitado a sofrer atualizações.</summary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>Obtém ou define se o componente está habilitado a ser desenhado na tela.</summary>
        public bool IsVisible { get; set; } = true;
        /// <summary>Obtém ou define um nome específico para o componente.</summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>Obtém a entidade associada a esse componente.</summary>
        public ElementaryEntity Owner { get; set; } = null;
        /// <summary>Obtém a instância corrente da classe Game.</summary>
        public Game Game { get => game ?? Owner?.Game; set => game = value; }
        /// <summary>Obtém acesso aos jobs deste elemento.</summary>
        public ElementCollection<Job, Element> Jobs { get; internal set; }

        /// <summary>Evento chamado quando a atualização do componente chega no fim.</summary>
        public event Action<Element, GameTime> OnUpdateEvent;
        /// <summary>Evento chamado quando o desenho do componente chega no fim.</summary>
        public event Action<Element, GameTime, SpriteBatch> OnDrawEvent;

        //------------- CONSTRUCTOR --------------//

        /// <summary>
        /// Inicializa uma nova instância do componente.
        /// </summary>
        internal Element() 
        {            
            this.Jobs = new ElementCollection<Job, Element>(this);
        }

        /// <summary>
        /// Inicializa uma nova instância do componente como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada</param>
        internal Element(Element source)
        {
            this.IsEnabled = source.IsEnabled;
            this.IsVisible = source.IsVisible;
            this.Name = source.Name;
            this.Owner = null;
            this.Jobs = new ElementCollection<Job, Element>(this, source.Jobs);
        }

        //--------------- METHODS ----------------//        

        /// <summary>
        /// Atualiza o componente.
        /// </summary>
        /// <param name="gameTime">Obtém o acesso aos tempos de jogo.</param>
        public void Update(GameTime gameTime)
        {
            if (IsEnabled)
            {
                OnUpdate(gameTime);

                for (int i = 0; i < Jobs.Count; i++)
                {
                    Jobs[i].Update(gameTime);
                }

                OnUpdateEvent?.Invoke(this, gameTime);
            }
        }

        /// <summary>
        /// Desenha o componente.
        /// </summary>
        /// <param name="gameTime">Obtém o acesso aos tempos de jogo.</param>
        /// <param name="spriteBatch">Uma instância de SpriteBatch para desenhar na tela.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                OnDraw(gameTime, spriteBatch);

                for (int i = 0; i < Jobs.Count; i++)
                {
                    Jobs[i].Draw(gameTime, spriteBatch);
                }

                OnDrawEvent?.Invoke(this, gameTime, spriteBatch);
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
            if(!disposed)
            {
                if(disposing)
                {
                    Owner = null;                    
                }

                disposed = true;
            }
        }
    }
}