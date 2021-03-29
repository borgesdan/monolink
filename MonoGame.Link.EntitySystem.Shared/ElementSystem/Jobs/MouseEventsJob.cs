using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Common;

namespace Microsoft.Xna.Framework.EntitySystem
{
    /// <summary>
    /// Widget que implementa a funcionalidade de receber eventos do mouse em um componente que implementa a interface IBoundsable.
    /// </summary>
    public sealed class MouseEventsJob : Job, IBuildable<MouseEventsJob>
    {
        private MouseState old;
        private MouseState state;
        private bool mouseOn = false;
        private IBoundsable boundsableComponent = null;

        //Double Click;
        private float doubleClickTime = 0;
        private short clicks = 0;
        
        /// <summary>Obtém ou define o tempo para reconhecimento de um duplo clique em milisegundos.</summary>
        public float DoubleClickDelay { get; set; } = 200;

        /// <summary>Ocorre quando um botão do mouse é pressionado.</summary>
        public event Action<MouseEventsJob, MouseState> Down;
        /// <summary>Ocorre quando um botão do mouse é liberado.</summary>
        public event Action<MouseEventsJob, MouseState> Up;
        /// <summary>Ocorre quando o ponteiro do mouse estava fora e entrou nos limites do componente.</summary>
        public event Action<MouseEventsJob, MouseState> Enter;
        /// <summary>Ocorre quando o ponteiro do mouse estava dentro e saiu nos limites do componente.</summary>
        public event Action<MouseEventsJob, MouseState> Leave;
        /// <summary>Ocorre quando o ponteiro do mouse se encontra dentro dos limites do componente.</summary>
        public event Action<MouseEventsJob, MouseState> On;
        /// <summary>Ocorre quando acontece um click duplo sobre o componente.</summary>
        public event Action<MouseEventsJob, MouseState> DoubleClick;

        /// <summary>
        /// Inicializa uma nova instância do widget.
        /// </summary>
        public MouseEventsJob()
        {
        }        

        /// <summary>
        /// Inicializa uma nova instância do widget como cópia de outra instância.
        /// </summary>
        /// <param name="source">O widget a ser copiado.</param>
        public MouseEventsJob(MouseEventsJob source) : base(source)
        {
            old = source.old;
            state = source.state;
            mouseOn = source.mouseOn;
            boundsableComponent = null;
            doubleClickTime = source.doubleClickTime;
            clicks = source.clicks;
            Down = source.Down;
            Up = source.Up;
            Enter = source.Enter;
            Leave = source.Leave;
            On = source.On;
        }

        /// <summary>
        /// Define os atributos deste widget.
        /// </summary>
        /// <param name="setFuncion">Define as propriedades do widget através de uma função.</param>
        public MouseEventsJob Build(Action<MouseEventsJob> setFuncion)
        {
            setFuncion(this);
            return this;
        }
        
        protected override void OnUpdate(GameTime gameTime)
        {
            if (Owner is IBoundsable boundsable)
                boundsableComponent = boundsable;

            if (boundsableComponent != null)
            {
                doubleClickTime += gameTime.ElapsedGameTime.Milliseconds;

                if (doubleClickTime > DoubleClickDelay)
                {
                    doubleClickTime = 0;
                    clicks = 0;
                }

                Rectangle bounds = boundsableComponent.Bounds;
                old = state;
                state = Mouse.GetState();

                //Se o ponteiro do mouse consta como dentro dos limites
                if (mouseOn)
                {
                    //mas verificando novamente ele consta fora
                    if (!bounds.Contains(state.Position))
                        Leave?.Invoke(this, state);
                }

                //se o ponteiro do mouse está dentro dos limites da entidade.
                if (bounds.Contains(state.Position))
                {
                    //se o ponteiro do mouse se encontrava fora
                    if (!mouseOn)
                        Enter?.Invoke(this, state);

                    mouseOn = true;
                    On?.Invoke(this, state);

                    //se um botão foi pressionado enquanto o controle está dentro
                    if (state.LeftButton == ButtonState.Pressed
                        || state.RightButton == ButtonState.Pressed
                        || state.MiddleButton == ButtonState.Pressed)
                    {
                        //old = state;
                        Down?.Invoke(this, state);
                    }

                    //se um botão foi liberado
                    if (old.LeftButton == ButtonState.Pressed && state.LeftButton == ButtonState.Released
                        || old.RightButton == ButtonState.Pressed && state.RightButton == ButtonState.Released
                        || old.MiddleButton == ButtonState.Pressed && state.MiddleButton == ButtonState.Released)
                    {
                        Up?.Invoke(this, state);
                    }

                    //Verifica se houve clique duplo
                    if (old.LeftButton == ButtonState.Released && state.LeftButton == ButtonState.Pressed)
                    {
                        clicks++;
                        doubleClickTime = 0;

                        if (clicks >= 2 && doubleClickTime < DoubleClickDelay)
                        {
                            DoubleClick?.Invoke(this, state);
                            clicks = 0;
                            doubleClickTime = 0;
                        }
                    }
                }
                else
                {
                    mouseOn = false;
                }
            }
        }
    }
}