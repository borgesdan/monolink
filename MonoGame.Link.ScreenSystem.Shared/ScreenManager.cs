using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Common;

namespace Microsoft.Xna.Framework.ScreenSystem
{    
    /// <summary>
    /// Representa um gerenciador de telas do jogo.
    /// </summary>
    public sealed class ScreenManager : IDisposable, IUpdate, IDraw
    {
        //------------- VARS ---------------//

        //A classe é um singleton, assim cada tela pode buscar a instância corrente
        //do ScreenManager através da propriedade Instance.
        private static ScreenManager instance = null;

        private bool disposed = false;
        private bool changed = false;
        private Screen standbyScreen = null;

        /// <summary>Obtém a instância corrente da classe Game.</summary>
        public Game Game { get; private set; } = null;
        /// <summary>Obtém ou define a lista de telas.</summary>
        public List<Screen> Screens { get; set; } = new List<Screen>();
        /// <summary>Obtém a tela ativa.</summary>
        public Screen Active { get; private set; } = null;
        /// <summary>Obtém ou define se o gerenciador está habilitado a sofrer atualizações.</summary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>Obtém ou define se o gerenciador está habilitado a desenhar as telas.</summary>
        public bool IsVisible { get; set; } = true;
        /// <summary>Obtém a instância da classe ScreenManager.</summary>
        public static ScreenManager Instance { get => instance; }

        private ScreenManager(Game game)
        {
            Game = game;
        }

        /// <summary>
        /// Inicializa e retorna uma nova instância da classe ScreenManager.
        /// </summary>
        /// <param name="game">A instância corrente da classe Game.</param>
        public static ScreenManager Create(Game game)
        {
            if (instance == null)
            {
                instance = new ScreenManager(game);                
            }

            return instance;
        }

        public void Update(GameTime gameTime)
        {
            if(IsEnabled)
            {
                if (changed)
                {
                    Active = standbyScreen;
                    changed = false;
                    standbyScreen = null;
                }

                Active?.Update(gameTime);
            }            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(IsVisible)
            {
                Active?.Draw(gameTime, spriteBatch);
            }
        }
            

        /// <summary>Obtém uma tela informando seu nome.</summary>  
        public Screen this[string name] => Find(name);

        /// <summary>
        /// Define uma cena carregada ao seu estado inicial.
        /// </summary>
        /// <param name="name">O nome da tela a ser redefinida.</param>
        public void Reset(string name)
        {
            var s = this[name];
            s.Reset();
        }

        /// <summary>Adiciona telas para esse gerenciador.</summary>
        /// <param name="screens">A quantidade desejada de telas a serem adicionadas.</param>
        public void Add(params Screen[] screens)
        {
            foreach (var s in screens)
            {
                Screens.Add(s);

                if (Active == null)
                {
                    Active = s;
                }
            }
        }

        /// <summary>Remove uma tela do gerenciador.</summary>
        /// <param name="name">Nome da tela.</param>
        public void Remove(string name)
        {
            Screen finder = this[name];
            Screens.Remove(finder);
        }

        /// <summary>Troca para a tela selecionada.</summary>
        /// <param name="name">O nome da tela que será ativada.</param>
        public void Change(string name) => Change(name, false);

        /// <summary>Troca para a tela selecionada.</summary>
        /// <param name="name">O nome da tela que será ativada.</param>
        /// <param name="reset">True se deseja que o gerenciador chame o método Reset() da tela atual.</param>
        public void Change(string name, bool reset)
        {
            Screen old = Active;
            Screen finder = this[name];

            if (finder != null)
            {
                //Active = finder;
                changed = true;
                standbyScreen = finder;
            }
            else
            {
                throw new ArgumentException("Não foi encontrada uma tela com esse nome", nameof(name));
            }

            if (reset)
                old.Reset();
        }

        /// <summary>
        /// Troca para próxima tela da lista.
        /// </summary>
        /// <param name="reset">True se deseja que o gerenciador chame o método Reset() da tela atual.</param>
        public void Next(bool reset)
        {
            int index = Screens.FindIndex(x => x.Equals(Active));

            if (index >= Screens.Count - 1)
                index = 0;
            else
                index++;

            Change(Screens[index].Name, reset);
        }

        /// <summary>
        /// Troca para a tela anterior da lista.
        /// </summary>
        /// <param name="reset">True se deseja que o gerenciador chame o método Reset() da tela atual.</param>
        public void Back(bool reset)
        {
            //Voltar a tela.
            int index = Screens.FindIndex(x => x.Equals(Active));

            if (index <= 0)
                index = Screens.Count - 1;
            else
                index--;

            Change(Screens[index].Name, reset);
        }

        /// <summary>Busca e retorna uma tela definida pelo nome.</summary>
        /// <param name="name">O nome da tela.</param>
        public Screen Find(string name)
        {
            var s = Screens.Find(x => x.Name.Equals(name));
            return s;
        }        

        /// <summary>Libera os recursos da classe.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                instance = null;
                Game = null;
                Screens.Clear();
                Screens = null;
                Active = null;
            }                

            disposed = true;
        }
    }
}
