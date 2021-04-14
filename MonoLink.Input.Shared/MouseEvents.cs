using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MonoLink.Input
{
    /// <summary>
    /// Implementa a funcionalidade de reconhecer eventos do mouse em determinado limite da tela.
    /// </summary>
    public class MouseEvents
    {
        private MouseState oldstate;
        private MouseState state;
        
        private Point oldPosition;
        private Point currentPosition;
        private int elapsedHoverTime = 0;
        private bool isHover = false;

        public int MouseHoverTime { get; set; } = 2000;

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        public MouseEvents()
        {
        }        

        /// <summary>
        /// Inicializa uma nova instância do classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public MouseEvents(MouseEvents source)
        {
            oldstate = source.oldstate;
            state = source.state;
            oldPosition = source.oldPosition;
            currentPosition = source.currentPosition;
            elapsedHoverTime = source.elapsedHoverTime;
            isHover = source.isHover;
            MouseHoverTime = source.MouseHoverTime;
        } 
        
        /// <summary>
        /// Retorna true caso o ponteiro do mouse se encontre dentro dos limites do retângulo.
        /// </summary>
        /// <param name="bounds">O retângulo a ser verificado.</param>
        public bool MouseOn(Rectangle bounds)
        {
            return bounds.Contains(state.Position);
        }

        /// <summary>
        /// Retorna true caso o ponteiro do mouse saiu dos limites do retângulo.
        /// </summary>
        /// <param name="bounds">O retângulo a ser verificado.</param>
        public bool MouseLeave(Rectangle bounds)
        {
            return bounds.Contains(oldstate.Position)
                && !bounds.Contains(state.Position);
        }

        /// <summary>
        /// Retorna true caso o ponteiro do mouse entrou nos limites do retângulo.
        /// </summary>
        /// <param name="bounds">O retângulo a ser verificado.</param>
        public bool MouseEnter(Rectangle bounds)
        {
            return !bounds.Contains(oldstate.Position)
                && bounds.Contains(state.Position);
        }

        /// <summary>
        /// Retorna true caso o ponteiro do mouse está se movendo dentro dos limites do retângulo.
        /// </summary>
        /// <param name="bounds">O retângulo a ser verificado.</param>
        public bool MouseMove(Rectangle bounds)
        {
            if(bounds.Contains(oldstate.Position) 
                && bounds.Contains(state.Position))
            {
                return oldstate.Position != state.Position;
            }

            return false;
        }

        /// <summary>
        /// Retorna true caso o ponteiro do mouse está dentro do limite do retângulo
        /// e se encontra parado com o valor de pausa sendo definido pela propriedade MouseHoverTime.
        /// </summary>
        /// <param name="bounds">O retângulo a ser verificado.</param>
        public bool MouseHover(Rectangle bounds)
        {
            return isHover && bounds.Contains(state.Position);
        }

        /// <summary>
        /// Atualiza os eventos do mouse.
        /// </summary>
        /// <param name="gameTime">Obtém acesso aos tempos de jogo.</param>
        public void Update(GameTime gameTime)
        {
            oldstate = state;
            state = Mouse.GetState();

            //Verifica se o mouse permanece no mesmo lugar
            oldPosition = currentPosition;
            currentPosition = Mouse.GetState().Position;

            if (oldPosition == currentPosition)
                elapsedHoverTime += gameTime.ElapsedGameTime.Milliseconds;
            else
                elapsedHoverTime = 0;

            isHover = elapsedHoverTime > MouseHoverTime;
        }
    }
}