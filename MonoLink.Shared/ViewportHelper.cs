﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoLink
{
    /// <summary>
    /// Providência acesso a métodos de auxílio para conferência da Viewport desejada.
    /// </summary>
    public class ViewportHelper
    {
        /// <summary>
        /// Verifica se os limites de um ator de jogo se encontram dentro da Viewport.
        /// </summary>
        /// <param name="viewport">A tela de visão desejada.</param>
        /// <param name="actorBounds">Os limites do ator.</param>
        public static bool CheckFieldOfView(Viewport viewport, Rectangle actorBounds)
        {
            return actorBounds.Intersects(viewport.Bounds);
        }

        /// <summary>Obtém a posição de um ator de jogo ao definir o alinhamento relativo aos limites da viewport.</summary>   
        /// <param name="viewport">A tela de visão para cálculo.</param>
        /// <param name="actorSize">O tamanho do ator.</param>
        /// <param name="align">O tipo de alinhamento.</param>
        public static Vector2 GetAlign(Viewport viewport, Point actorSize, AlignType align)
        {
            int viewWidth = viewport.Width;
            int viewHeight = viewport.Height;
            int actorWidth = actorSize.X;
            int actorHeight = actorSize.Y;

            Vector2 tempPosition = Vector2.Zero;

            switch (align)
            {
                case AlignType.Center:
                    tempPosition = new Vector2(viewWidth / 2 - actorWidth / 2, viewHeight / 2 - actorHeight / 2);
                    break;
                case AlignType.Left:
                    tempPosition = new Vector2(0, viewHeight / 2 - actorHeight / 2);
                    break;
                case AlignType.Right:
                    tempPosition = new Vector2(viewWidth - actorWidth, viewHeight / 2 - actorHeight / 2);
                    break;
                case AlignType.Bottom:
                    tempPosition = new Vector2(viewWidth / 2 - actorWidth / 2, viewHeight - actorHeight);
                    break;
                case AlignType.Top:
                    tempPosition = new Vector2(viewWidth / 2 - actorWidth / 2, 0);
                    break;
                case AlignType.LeftBottom:
                    tempPosition = new Vector2(0, viewHeight - actorHeight);
                    break;
                case AlignType.LeftTop:
                    tempPosition = new Vector2(0, 0);
                    break;
                case AlignType.RightBottom:
                    tempPosition = new Vector2(viewWidth - actorWidth, viewHeight - actorHeight);
                    break;
                case AlignType.RightTop:
                    tempPosition = new Vector2(viewWidth - actorWidth, 0);
                    break;
            }

            return tempPosition;
        }
    }
}