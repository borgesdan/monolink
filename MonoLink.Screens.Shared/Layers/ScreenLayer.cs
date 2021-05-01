using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoLink.Screens
{
    /// <summary>
    /// Classe que representa uma camada de exibição de uma tela.
    /// </summary>
    public abstract class ScreenLayer
    {
        //---------------------------------------//
        //-----         PROPRIEDADES        -----//
        //---------------------------------------//        
        /// <summary>Obtém a tela em que essa camada está associada.</summary>
        public Screen Screen { get; protected set; } = null;
        /// <summary>Obtém ou define o valor do efeito parallax.</summary>
        public float Parallax { get; set; } = 1f;
        /// <summary>Define o nome da camada.</summary>
        public string Name { get; set; } = "";
        /// <summary>Obtém ou define se a camada está habilitada a sofrer atualizações.</summary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>Obtém ou define se a camada está habilitada a ser desenhada na tela.</summary>
        public bool IsVisible { get; set; } = true;

        //---------------------------------------//
        //-----         CONSTRUTOR          -----//
        //---------------------------------------//        

        /// <summary>
        /// Inicializa uma nova instância da classe ScreenLayer.
        /// </summary>
        /// <param name="screen">A tela em que a camada será associada.</param>
        /// <param name="name">O nome da camada.</param>
        protected ScreenLayer(Screen screen, string name = "")
        {
            Screen = screen;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe ScreenLayer como cópia de outro Layer.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        protected ScreenLayer(ScreenLayer source)
        {
            this.Screen = source.Screen;
            this.Parallax = source.Parallax;
        }

        //---------------------------------------//
        //-----         FUNÇÕES             -----//
        //---------------------------------------//        

        /// <summary>Atualiza a camada.</summary>
        /// <param name="gameTime">Fornece acesso aos valores de tempo do jogo.</param>
        public void Update(GameTime gameTime)
        {
            if (IsEnabled)
            {
                OnUpdate(gameTime);
            }                
        }

        /// <summary>Desenha a camada.</summary>
        /// <param name="gameTime">Fornece acesso aos valores de tempo do jogo.</param>
        /// <param name="spriteBatch">Um objeto SpriteBatch para desenho.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                OnDraw(gameTime, spriteBatch);
            }                
        }

        public virtual void OnUpdate(GameTime gameTime) { }
        public virtual void OnDraw(GameTime gameTime, SpriteBatch spriteBatch) { }        
    }
}