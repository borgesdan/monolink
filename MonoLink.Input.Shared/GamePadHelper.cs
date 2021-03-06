using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace MonoLink.Input
{
    /// <summary>Classe que gerencia e auxilia nas entradas do usuário com o GamePad.</summary>
    public class GamePadHelper : IUpdate
    {
        private KeyButtonMap map = new KeyButtonMap();
        private Dictionary<Buttons, Keys?> keyboardMap = new Dictionary<Buttons, Keys?>();
        private KeyboardState keyboardState = new KeyboardState();
        private KeyboardState lastKeyboardState = new KeyboardState();

        /// <summary>
        /// Obtém ou define se o teclado pode ser utilizado mesmo com o GamePad conectado.
        /// </summary>
        public bool AlwaysEnabledKeyboard { get; set; } = true;
        /// <sumary>Obtém ou define se esta instância está disponível para ser atualizada.</sumary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>Obtém o estado atual do GamePad.</summary>
        public GamePadState State { get; private set; } = new GamePadState();
        /// <summary>Obtém o estado anterior do GamePad antes da atualização.</summary>
        public GamePadState OldState { get; private set; } = new GamePadState();
        /// <summary>Obtém o index do GamePad.</summary>
        public PlayerIndex Index { get; set; } = PlayerIndex.One;
        /// <summary>Obtém ou define o mapeamento de teclas para botões do GamePad.</summary>
        public KeyButtonMap Map 
        {
            get => map;
            set
            {
                map = value;
                
                if(map != null)
                    keyboardMap = map.GetKeyboardMap();
            }
        }

        //------------- CONSTRUCTOR ---------------//       

        /// <summary>Inicializa uma nova instância de GamePadHelper.</summary>
        /// <param name="playerIndex">O index do GamePad.</param>
        /// <param name="map">O mapeamento de teclas do teclado para os botões do GamePad.</param>        
        public GamePadHelper(PlayerIndex playerIndex, KeyButtonMap map = null)
        {
            Index = playerIndex;
            Map = map;
        }

        //---------------------------------------//
        //-----         FUNÇÕES             -----//
        //---------------------------------------//

        /// <summary>Atualiza os estados do GamePad.</summary>
        /// <param name="gameTime">Uma instância de GameTime.</param>
        public void Update(GameTime gameTime)
        {
            if (!IsEnabled)
                return;

            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            OldState = State;
            State = GamePad.GetState(Index);
        }

        /// <summary>
        /// <summary>Define o mapeamento de teclas para botões do GamePad.</summary>
        /// </summary>
        /// <param name="keyboardMap">o mapeamento de teclas para botões do GamePad.</param>
        public void SetMap(KeyButtonMap keyboardMap)
        {
            Map = keyboardMap;
        }

        //Método de auxiliar para checar o estado dos botões e executar um comando
        private bool CheckButton(Buttons button, Predicate<Keys> function)
        {
            Keys? k = null;
            if (keyboardMap != null && keyboardMap.ContainsKey(button))
                k = keyboardMap[button];  

            return k.HasValue && function.Invoke(k.Value);
        }

        /// <summary>Verifica se o botão selecionado está pressionado.</summary>
        /// <param name="button">O botão do GamePad a ser verificado.</param>
        public bool IsDown(Buttons button)
        {
            if(State.IsConnected)
            {
                //return State.IsButtonDown(button)
                //    || (!AlwaysEnabledKeyboard && CheckButton(button, keyboardState.IsKeyDown));

                bool state = State.IsButtonDown(button);
                bool cond = AlwaysEnabledKeyboard && CheckButton(button, keyboardState.IsKeyDown);

                return state || cond;
            }
            else
            {
                return CheckButton(button, keyboardState.IsKeyDown);
            }
        }

        /// <summary>Verifica se o botão selecionado estava liberado e foi pressionado.</summary>
        /// <param name="button">O botão do GamePad a ser verificado.</param>
        public bool IsJustPressed(Buttons button)
        {
            if (State.IsConnected)
            {
                //return OldState.IsButtonUp(button) && State.IsButtonDown(button)
                //    || (AlwaysEnabledKeyboard && (CheckButton(button, lastKeyboardState.IsKeyUp) && CheckButton(button, keyboardState.IsKeyDown)));

                bool state = OldState.IsButtonUp(button) && State.IsButtonDown(button);
                bool cond = AlwaysEnabledKeyboard && (CheckButton(button, lastKeyboardState.IsKeyUp) && CheckButton(button, keyboardState.IsKeyDown));

                return state || cond;
            }
            else
            {
                return CheckButton(button, lastKeyboardState.IsKeyUp) && CheckButton(button, keyboardState.IsKeyDown);
            }
        }

        /// <summary>Verifica se o botão estava pressionada e foi liberada.</summary>
        /// <param name="key">A tecla a ser verificada.</param>
        public bool IsJustReleased(Buttons button)
        {
            if (State.IsConnected)
            {
                //return OldState.IsButtonDown(button) && State.IsButtonUp(button)
                //    || (AlwaysEnabledKeyboard && (CheckButton(button, lastKeyboardState.IsKeyDown) && CheckButton(button, keyboardState.IsKeyUp)));

                bool state = OldState.IsButtonDown(button) && State.IsButtonUp(button);
                bool cond = AlwaysEnabledKeyboard && (CheckButton(button, lastKeyboardState.IsKeyDown) && CheckButton(button, keyboardState.IsKeyUp));

                return state || cond;
            }
            else
            {
                return CheckButton(button, lastKeyboardState.IsKeyDown) && CheckButton(button, keyboardState.IsKeyUp);
            }
        }

        /// <summary>Verifica se o botão selecionado está liberado.</summary>     
        /// <param name="button">O botão do GamePad a ser verificado.</param>
        public bool IsUp(Buttons button)
        {
            if (State.IsConnected)
            {
                //return State.IsButtonUp(button)
                //    || (AlwaysEnabledKeyboard && CheckButton(button, keyboardState.IsKeyUp));

                bool state = State.IsButtonUp(button);
                bool cond = AlwaysEnabledKeyboard && CheckButton(button, keyboardState.IsKeyUp);

                return state || cond;
            }
            else
            {
                return CheckButton(button, keyboardState.IsKeyUp);
            }
        }

        /// <summary>Verifica o estado da analógico esquerdo.</summary>        
        public Vector2 GetLeftThumbState()
        {
            Vector2 thumb;

            if (State.IsConnected)
            {
                thumb = State.ThumbSticks.Left;

                if (thumb == Vector2.Zero && AlwaysEnabledKeyboard)
                    thumb = GetLeftThumbKeyboard();
            }
            else
            {
                thumb = GetLeftThumbKeyboard();                
            }

            return thumb;
        }

        private Vector2 GetLeftThumbKeyboard()
        {
            Vector2 thumb = Vector2.Zero;

            if(CheckButton(Buttons.LeftThumbstickUp, keyboardState.IsKeyDown))
                thumb.Y = 1;
            if (CheckButton(Buttons.LeftThumbstickDown, keyboardState.IsKeyDown))
                thumb.Y = -1;
            if (CheckButton(Buttons.LeftThumbstickRight, keyboardState.IsKeyDown))
                thumb.X = 1;
            if (CheckButton(Buttons.LeftThumbstickLeft, keyboardState.IsKeyDown))
                thumb.X = -1;

            return thumb;
        }

        /// <summary>Verifica o estado da analógico direito.</summary>
        public Vector2 GetRightThumbState()
        {
            Vector2 thumb;

            if (State.IsConnected)
            {
                thumb = State.ThumbSticks.Left;
                
                if (thumb == Vector2.Zero && AlwaysEnabledKeyboard)
                    thumb = GetRightThumbKeyboard();
            }
            else
            {
                thumb = GetRightThumbKeyboard();
            }

            return thumb;
        }

        private Vector2 GetRightThumbKeyboard()
        {
            Vector2 thumb = Vector2.Zero;

            if (CheckButton(Buttons.RightThumbstickUp, keyboardState.IsKeyDown))
                thumb.Y = 1;
            if (CheckButton(Buttons.RightThumbstickDown, keyboardState.IsKeyDown))
                thumb.Y = -1;
            if (CheckButton(Buttons.RightThumbstickRight, keyboardState.IsKeyDown))
                thumb.X = 1;
            if (CheckButton(Buttons.RightThumbstickLeft, keyboardState.IsKeyDown))
                thumb.X = -1;

            return thumb;
        }
    }
}