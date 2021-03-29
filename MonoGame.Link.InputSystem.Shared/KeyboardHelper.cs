using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Common;

namespace Microsoft.Xna.Framework.InputSystem
{
    /// <summary>Classe que gerencia e auxilia nas entradas do jogador com um teclado.</summary>
    public class KeyboardHelper : IUpdate
    {
        /// <sumary>Obtém ou define se esta instância está disponível para ser atualizada.</sumary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>Obtém o estado atual do teclado.</summary>
        public KeyboardState State { get; private set; }
        /// <summary>Obtém o estado anterior do teclado antes da atualização.</summary>
        public KeyboardState OldState { get; private set; }

        /// <summary>
        /// Inicializa uma nova instância de KeyboardHelper.
        /// </summary>
        public KeyboardHelper() { }

        /// <summary>Atualiza os estados do teclado.</summary>
        /// <param name="gameTime">Obtém os tempos de jogo.</param>
        public void Update(GameTime gameTime)
        {
            if (IsEnabled)
            {
                OldState = State;
                State = Keyboard.GetState();
            }            
        }

        /// <summary>Verifica se a tecla selecionada está pressionada.</summary>
        /// <param name="key">A tecla a ser verificada.</param>
        public bool Down(Keys key)
        {
            return State.IsKeyDown(key);
        }

        /// <summary>Verifica se a tecla selecionada foi pressionada.</summary>
        /// <param name="key">A tecla a ser verificada.</param>
        public bool Pressed(Keys key)
        {
            return OldState.IsKeyUp(key) && State.IsKeyDown(key);
        }

        /// <summary>Verifica se a tecla foi liberada.</summary>
        /// <param name="key">A tecla a ser verificada.</param>
        public bool Released(Keys key)
        {
            return OldState.IsKeyDown(key) && State.IsKeyUp(key);
        }

        /// <summary>Verifica se a tecla selecionada está liberada.</summary>   
        /// <param name="key">A tecla a ser verificada.</param>
        public bool Up(Keys key)
        {
            return State.IsKeyUp(key);
        }
    }
}