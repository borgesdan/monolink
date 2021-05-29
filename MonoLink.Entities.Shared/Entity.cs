using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoLink.Entities
{
    /// <summary>
    /// Representa um objeto de jogo.
    /// </summary>
    public class Entity : IDisposable, IUpdateDraw
    {
        bool disposed = false;

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

        /// <summary>Encapsula métodos a serem chamados no fim do método Update.</summary>
        public event Action<Entity, GameTime> OnUpdateEvent;
        /// <summary>Encapsula métodos a serem chamados no fim do método Draw.</summary>
        public event Action<Entity, GameTime, SpriteBatch> OnDrawEvent;

        /// <summary>
        /// Inicializa uma nova instância de Entity.
        /// </summary>
        /// <param name="game">A instância corrente da classe Game.</param>
        /// <param name="name">O nome da entidade.</param>
        public Entity(Game game, string name = "")
        {
            this.Name = name;
            this.Game = game;
        }

        /// <summary>
        /// Inicializa uma nova instância de Entity como cópia de outro Entity.
        /// </summary>
        /// <param name="source">A instância a ser coipada.</param>
        public Entity(Entity source)
        {
            this.Name = source.Name;
            this.Game = source.Game;
            this.IsEnabled = source.IsEnabled;
            this.IsVisible = source.IsVisible;
            this.Transform = new Transform(source.Transform);  
            this.OnUpdateEvent = source.OnUpdateEvent;
            this.OnUpdateEvent = source.OnUpdateEvent;
        }

        /// <summary>
        /// Atualiza a entidade.
        /// </summary>
        /// <param name="gameTime">Obtém o acesso aos tempos de jogo.</param>
        public void Update(GameTime gameTime)
        {
            if (IsEnabled)
            {
                OnUpdate(gameTime);
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
            if (IsVisible)
            {
                OnDraw(gameTime, spriteBatch);
                OnDrawEvent?.Invoke(this, gameTime, spriteBatch);
            }
        }

        protected virtual void OnUpdate(GameTime gameTime) { }
        protected virtual void OnDraw(GameTime gameTime, SpriteBatch spriteBatch) { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (disposing)
                {
                    Transform = null;
                    Name = null;
                    Game = null;
                }                
            }

            disposed = true;
        }
    }
}