using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Common;

namespace Microsoft.Xna.Framework.EntitySystem
{
    /// <summary>
    /// Representa uma classe que expõe propriedades e métodos para facilitar a manipulação de objetos de jogo.
    /// </summary>
    public sealed class ElementaryEntity : IDisposable, IUpdateDraw, INameable
    {
        //------------- VARIABLES ---------------//        
        bool disposed = false;        

        //------------- PROPERTIES ---------------//

        /// <summary>Obtém ou define o nome da entidade que pode ser utilizado como critério de busca.</summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>Obtém a instância corrente da classe Game.</summary>
        public Game Game { get; set; } = null;
        /// <summary>Obtém ou define se a entidade está habilitada a sofrer atualizações.</summary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>Obtém ou define se a entidade está habilitada a ser desenhada na tela.</summary>
        public bool IsVisible { get; set; } = true;
        /// <summary>Obtém ou define as transformações de posição, escala e rotação da entidade.</summary>
        public Transform Transform { get; set; } = new Transform();
        /// <summary>Obtém acesso aos elementos da entidade.</summary>
        public ElementCollection<Element, ElementaryEntity> Elements { get; internal set; }
        /// <summary>Evento chamado quando a atualização do componente chega no fim.</summary>
        public event Action<ElementaryEntity, GameTime> OnUpdateEvent;
        /// <summary>Evento chamado quando o desenho do componente chega no fim.</summary>
        public event Action<ElementaryEntity, GameTime, SpriteBatch> OnDrawEvent;

        //------------- CONSTRUCTOR --------------//

        /// <summary>
        /// Inicializa uma nova instância de Entity.
        /// </summary>
        /// <param name="game">A instância corrente da classe Game.</param>
        /// <param name="name">O nome da entidade.</param>
        public ElementaryEntity(Game game, string name = "")
        {
            this.Name = name;
            this.Game = game;
            this.Elements = new ElementCollection<Element, ElementaryEntity>(this);
        }

        /// <summary>
        /// Inicializa uma nova instância de Entity como cópia de outro Entity.
        /// </summary>
        /// <param name="source">A instância a ser coipada.</param>
        public ElementaryEntity(ElementaryEntity source)
        {
            this.Name = source.Name;
            this.Game = source.Game;
            this.IsEnabled = source.IsEnabled;
            this.IsVisible = source.IsVisible;
            this.Transform = new Transform(source.Transform);
            this.Elements = new ElementCollection<Element, ElementaryEntity>(this, source.Elements);
        }

        //--------------- METHODS ----------------//

        /// <summary>
        /// Atualiza a entidade.
        /// </summary>
        /// <param name="gameTime">Obtém o acesso aos tempos de jogo.</param>
        public void Update(GameTime gameTime)
        {
            if(IsEnabled)
            {                
                for(int i = 0; i < Elements.Count; i++)
                {
                    Elements[i].Update(gameTime);
                }

                Transform.Update(gameTime);
                OnUpdateEvent?.Invoke(this, gameTime);
            }
        }

        /// <summary>
        /// Desenha a entidade.
        /// </summary>
        /// <param name="gameTime">Obtém o acesso aos tempos de jogo.</param>
        /// <param name="spriteBatch">Uma instância de SpriteBatch para desenhar na tela.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(IsVisible)
            {
                for (int i = 0; i < Elements.Count; i++)
                {
                    Elements[i].Draw(gameTime, spriteBatch);
                }

                OnDrawEvent?.Invoke(this, gameTime, spriteBatch);
            }
        }

        //--------------- DISPOSE ----------------//

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if(disposing)
                {
                    Transform = null;
                    Name = null;
                    Game = null;

                    for (int i = 0; i < Elements.Count; i++)
                        Elements[i].Dispose();

                    Elements.Clear();
                    Elements = null;
                }

                disposed = true;
            }
        }
    }
}