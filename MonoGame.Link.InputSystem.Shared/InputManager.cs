using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Common;

namespace Microsoft.Xna.Framework.InputSystem
{
    /// <summary>
    /// Classe de gerenciamento de entradas do usuário.
    /// </summary>
    public class InputManager : IUpdate
    {
        public bool IsEnabled { get; set; }

        /// <summary>Obtém ou define o GamePad do Player 1.</summary>
        public GamePadHelper One { get; set; }
        /// <summary>Obtém ou define o GamePad do Player 2.</summary>
        public GamePadHelper Two { get; set; }
        /// <summary>Obtém ou define o GamePad do Player 3.</summary>
        public GamePadHelper Three { get; set; }
        /// <summary>Obtém ou define o GamePad do Player 4.</summary>
        public GamePadHelper Four { get; set; }
        /// <summary>Obtém ou define o gerenciamento do teclado.</summary>
        public KeyboardHelper Keyboard { get; set; }
        /// <summary>Obtém ou define o gerenciamento do Mouse.</summary>
        public MouseHelper Mouse { get; set; }

        /// <summary>
        /// Inicializa uma nova instância da classe InputManager.
        /// </summary>
        public InputManager()
        {
            One = new GamePadHelper(PlayerIndex.One);
            Two = new GamePadHelper(PlayerIndex.Two);
            Three = new GamePadHelper(PlayerIndex.Three);
            Four = new GamePadHelper(PlayerIndex.Four);
            Keyboard = new KeyboardHelper();
            Mouse = new MouseHelper();
        }

        /// <summary>
        /// Obtém o GamePadHelper pelo seu index.
        /// </summary>
        /// <param name="index">O index do jogador.</param>
        public GamePadHelper this[PlayerIndex index]
        {
            get
            {
                switch (index)
                {
                    case PlayerIndex.One:
                        return One;
                    case PlayerIndex.Two:
                        return Two;
                    case PlayerIndex.Three:
                        return Three;
                    case PlayerIndex.Four:
                        return Four;
                    default:
                        return One;
                }
            }
        }

        /// <summary>Atualiza os estados das entradas do usuário.</summary>
        /// <param name="gameTime">Uma instância de GameTime.</param>
        public void Update(GameTime gameTime)
        {
            if(IsEnabled)
            {
                One.Update(gameTime);
                Two.Update(gameTime);
                Three.Update(gameTime);
                Four.Update(gameTime);
                Keyboard.Update(gameTime);
                Mouse.Update(gameTime);
            }            
        }
    }
}
