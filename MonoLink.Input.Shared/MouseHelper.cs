using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoLink.Input
{
    /// <summary>Classe que auxilia no gerenciamento de entradas do mouse.</summary>
    public class MouseHelper : IUpdate
    {
        //---------------------------------------//
        //-----         VARIAVEIS           -----//
        //---------------------------------------//   

        private int clickTime = 0;
        private byte clicks = 0;
        private bool hasDoubleClick = false;

        //---------------------------------------//
        //-----         PROPRIEDADES        -----//
        //---------------------------------------//

        /// <sumary>Obtém ou define se esta instância está disponível para ser atualizada.</sumary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>Obtém ou define o tempo para reconhecimento de um duplo clique em milisegundos.</summary>
        public int DoubleClickDelay { get; set; } = 300;
        /// <summary>Obtém ou define o botão a ser verificado o duplo clique, por padrão MouseButtons.Left.</summary>
        public MouseButtons DoubleClickButton { get; set; } = MouseButtons.Left;
        /// <summary>Obtém o estado atual do mouse.</summary>
        public MouseState State { get; private set; }
        /// <summary>Obtém estado anterior do mouse.</summary>
        public MouseState OldState { get; private set; }
        /// <summary>Obtém os eventos do mouse.</summary>
        public MouseEvents MouseEvents { get; private set; } = new MouseEvents();

        //---------------------------------------//
        //-----         CONSTRUTOR          -----//
        //---------------------------------------//

        /// <summary>Inicializa uma nova instância da classe MouseHelper.</summary>
        public MouseHelper()
        {
            State = Mouse.GetState();
        }

        /// <summary>Inicializa uma nova instância da classe MouseHelper.</summary>
        /// <param name="state">O estado definido do mouse.</param>
        public MouseHelper(MouseState state)
        {
            State = state;
        }

        //---------------------------------------//
        //-----         METÓDOS             -----//
        //---------------------------------------//

        /// <summary>Atualiza o estado do mouse.</summary>
        /// <param name="gameTime">Fornece acesso aos valores de tempo do jogo.</param>
        public void Update(GameTime gameTime)
        {
            if (IsEnabled)
            {
                MouseEvents.Update(gameTime);

                OldState = State;
                State = Mouse.GetState();

                //Verifica o duplo clique
                CheckDoubleClick(gameTime);
            }            
        }

        //Verifica o duplo clique do botão definido
        private void CheckDoubleClick(GameTime gameTime)
        {
            hasDoubleClick = false;
            clickTime += gameTime.ElapsedGameTime.Milliseconds;
            bool pressed = IsJustPressed(DoubleClickButton);     
            
            if(clickTime <= DoubleClickDelay)
            {
                if (pressed)
                {
                    clicks++;

                    if (clicks >= 2)
                    {
                        clicks = 0;
                        clickTime = 0;
                        hasDoubleClick = true;
                    }
                }
            }
            else
            {
                clickTime = 0;
                clicks = 0;
            }            
        }        

        /// <summary>Verifica se houve duplo clique.</summary>
        public bool CheckDoubleClick() => hasDoubleClick;

        /// <summary>Checa se o botão do mouse está pressionado.</summary>
        /// <param name="button">O botão do mouse a ser checado.</param>
        public bool IsDown(MouseButtons button)
        {            
             return Check(button, false) == ButtonState.Pressed;
        }

        /// <summary>Checa se o botão do mouse está liberado.</summary>
        /// <param name="button">O botão do mouse a ser checado.</param>
        public bool IsUp(MouseButtons button)
        {            
            return Check(button, false) == ButtonState.Released;
        }

        /// <summary>Checa se o botão do mouse foi pressionado.</summary>
        /// <param name="button">O botão do mouse a ser checado.</param>
        public bool IsJustPressed(MouseButtons button)
        {            
            return Check(button, true) == ButtonState.Released 
                && Check(button, false) == ButtonState.Pressed;
        }

        /// <summary>
        /// Checa se o botão do mouse foi liberado.
        /// </summary>
        /// <param name="button">O botão do mouse a ser checado.</param>
        public bool IsJustReleased(MouseButtons button)
        {
            return Check(button, true) == ButtonState.Pressed
                && Check(button, false) == ButtonState.Released;
        }

        //Checa os estados dos botões
        private ButtonState Check(MouseButtons button, bool checkLastState)
        {
            MouseState mState = State;

            if (checkLastState)
                mState = OldState;

            if (button == MouseButtons.Left)
                return mState.LeftButton;
            else if (button == MouseButtons.Right)
                return mState.RightButton;
            else if (button == MouseButtons.Middle)
                return mState.MiddleButton;
            else if (button == MouseButtons.X1)
                return mState.XButton1;
            else
                return mState.XButton2;
        }
    }
}
