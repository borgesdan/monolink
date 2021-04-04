using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoLink.Screen
{    
    /// <summary>
    /// Representa um gerenciador de telas do jogo.
    /// </summary>
    public sealed class ScreenManager : IDisposable
    {
        //------------- VARS ---------------//

        private bool disposed = false;
        private bool changed = false;
        private Screen standbyScreen = null;
        private List<Screen> screens  = new List<Screen>();

        /// <summary>Obtém a instância corrente da classe Game.</summary>
        public Game Game { get; private set; } = null;
        /// <summary>Obtém a tela ativa.</summary>
        public Screen Active { get; private set; } = null;
        /// <summary>Obtém ou define se o gerenciador está habilitado a sofrer atualizações.</summary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>Obtém ou define se o gerenciador está habilitado a desenhar as telas.</summary>
        public bool IsVisible { get; set; } = true;        

        /// <summary>
        /// Atualiza o gerenciador de tela.
        /// </summary>
        /// <param name="gameTime">Obtém acesso aos tempos de jogo.</param>
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

        /// <summary>
        /// Desenha a tela de jogo.
        /// </summary>
        /// <param name="gameTime">Obtém acesso aos tempos de jogo.</param>
        /// <param name="spriteBatch">O objeto SpriteBatch para desenho.</param>
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
        /// Define uma tela a ser carregada ao seu estado inicial.
        /// </summary>
        /// <param name="name">O nome da tela a ser redefinida.</param>
        public void Reset(string name)
        {
            this[name].Reset();            
        }

        /// <summary>Adiciona telas para esse gerenciador.</summary>
        /// <param name="screens">A quantidade desejada de telas a serem adicionadas.</param>
        public void Add(params Screen[] screens)
        {
            foreach (var s in screens)
            {
                this.screens.Add(s);
                s.Manager = this;

                if (Active == null)
                {
                    Active = s;
                }
            }
        }

        /// <summary>Remove uma tela do gerenciador. Retorna true caso sucesso.</summary>
        /// <param name="name">Nome da tela.</param>
        public bool Remove(string name)
        {
            Screen finder = this[name];
            return screens.Remove(finder);
        }

        /// <summary>
        /// Troca a tela ativa para a tela selecionada.
        /// </summary>
        /// <param name="index">O index da tela pela ordem de adição.</param>
        /// <param name="reset">
        /// Defina true caso deseje que o gerenciador chame o método Reset()
        /// da tela ativa após a troca de tela.
        /// </param>
        public void Change(int index, bool reset)
        {
            Screen old = Active;
            Screen finder = screens[index];

            changed = true;
            standbyScreen = finder;

            if (reset)
                old.Reset();
        }

        /// <summary>Troca a tela tiva para a tela selecionada.</summary>
        /// <param name="name">O nome da tela que será ativada.</param>
        /// <param name="reset">True se deseja que o gerenciador chame o método Reset() da tela atual.</param>
        public void Change(string name, bool reset)
        {
            int index = screens.FindIndex(s => s.Name.Equals(name));

            if (index != -1)
                Change(index, reset);
            else
                throw new NullReferenceException($"Não foi possível encontrar uma tela com este nome: {name}");
        }

        /// <summary>
        /// Troca para próxima tela da lista.
        /// </summary>
        /// <param name="reset">True se deseja que o gerenciador chame o método Reset() da tela atual.</param>
        public void Next(bool reset)
        {
            int index = screens.FindIndex(x => x.Equals(Active));

            if (index >= screens.Count - 1)
                index = 0;
            else
                index++;

            Change(screens[index].Name, reset);
        }

        /// <summary>
        /// Troca para a tela anterior da lista.
        /// </summary>
        /// <param name="reset">True se deseja que o gerenciador chame o método Reset() da tela atual.</param>
        public void Back(bool reset)
        {
            //Voltar a tela.
            int index = screens.FindIndex(x => x.Equals(Active));

            if (index <= 0)
                index = screens.Count - 1;
            else
                index--;

            Change(screens[index].Name, reset);
        }

        /// <summary>Busca e retorna uma tela definida pelo nome.</summary>
        /// <param name="name">O nome da tela.</param>
        public Screen Find(string name)
        {
            var s = screens.Find(x => x.Name.Equals(name));
            if (s != null)
                return s;
            else
                throw new NullReferenceException($"A tela com o nome: {name} retornou null.");
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
                Game = null;
                screens.Clear();
                screens = null;
                Active = null;
                standbyScreen = null;
            }                

            disposed = true;
        }
    }
}
