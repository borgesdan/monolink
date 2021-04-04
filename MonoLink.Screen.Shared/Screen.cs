using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoLink.Screen
{
    /// <summary>
    /// Representa uma tela base de jogo.
    /// </summary>
    public abstract class Screen : IDisposable
    {
        /// <summary>
        /// Obtém ou define o gerenciador de telas.
        /// </summary>
        internal ScreenManager Manager { get; set; }

        /// <summary>Obtém a classe Game do jogo.</summary>
        public Game Game { get; }
        /// <summary>Obtém ou define o nome da tela.</summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>Obtém ou define se a tela está habilitada a sofrer atualizações.</summary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>Obtém ou define se a tela está habilitada a ser desenhada na tela.</summary>
        public bool IsVisible { get; set; } = true;     

        /// <summary>Inicializa uma nova instância da classe Screen.</summary> 
        /// <param name="game">A classe Game do jogo.</param>
        /// <param name="name">Define o nome da tela.</param>
        public Screen(Game game, string name = "")
        {
            Game = game;
            Name = name;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A tela a ser copiada.</param>
        public Screen(Screen source)
        {
            Name = source.Name;
            IsEnabled = source.IsEnabled;
            IsVisible = source.IsVisible;
        }

        /// <summary>
        /// Atualiza a tela.
        /// </summary>
        /// <param name="gameTime">Obtém acesso aos tempos de jogo.</param>
        public void Update(GameTime gameTime) 
        {
            if (IsEnabled)
            {
                OnUpdate(gameTime);                
            }
        }

        /// <summary>
        /// Desenha a tela.
        /// </summary>
        /// <param name="gameTime">Obtém acesso aos tempos de jogo.</param>
        /// <param name="spriteBatch">O objeto SpriteBatch para desenho.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            if (IsVisible)
            {
                OnUpdate(gameTime);
            }
        }

        /// <summary>
        /// Sobrecarregue este método para definir as atualizações da tela a ser chamado no método Update.
        /// </summary>
        /// <param name="gameTime">Obtém o acesso aos tempos de jogo.</param>
        protected virtual void OnUpdate(GameTime gameTime) { }

        /// <summary>
        /// Sobrecarregue este método para definir o desenho da tela a ser chamado no método Draw.
        /// </summary>
        /// <param name="gameTime">Obtém o acesso aos tempos de jogo.</param>
        protected virtual void OnDraw(GameTime gameTime, SpriteBatch spriteBatch) { }
        
        /// <summary>
        /// Carrega as propriedades da tela definidas neste método.
        /// </summary>
        public void Load() 
        {
            OnLoad();
        }

        /// <summary>
        /// Sobrecarregue esse método caso deseje carregar sua tela a ser chamada no método Load().
        /// </summary>
        public virtual void OnLoad() { }

        /// <summary>Redefine a tela ao estado padrão.</summary>
        public virtual void Reset() 
        {
            IsEnabled = true;
            IsVisible = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposed)
        {
        }
    }
}