using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoLink
{
    /// <summary>
    /// Classe de auxílios matemáticos.
    /// </summary>
    public static class LMAth
    {
        /// <summary>
        /// Retorna um vetor normalizado que representa a direção de uma posição para outra.
        /// </summary>
        /// <param name="position">A posição atual.</param>
        /// <param name="destination">A posição a ser alcançada</param>
        public static Vector2 Direction(Vector2 position, Vector2 destination)
        {
            Vector2 direction = destination - position;
            direction.Normalize();
            return direction;
        }
    }
}
