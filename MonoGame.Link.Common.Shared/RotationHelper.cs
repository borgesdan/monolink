using System;

namespace Microsoft.Xna.Framework.Common
{
    /// <summary>
    /// Classe de auxílio para cálculos de rotação.
    /// </summary>    
    public static class RotationHelper
    {
        /// <summary>
        /// Obtém a posição de um ponto ao informar a origem e o grau de rotação.        
        /// </summary>
        /// <param name="point">A posição do ponto na tela.</param>
        /// <param name="origin">A coordenada na tela que será o pivô para rotação, a origem.</param>
        /// <param name="radians">O grau da rotação em radianos.</param>
        public static Point Get(Point point, Vector2 origin, float radians)
        {
            return Get(point.ToVector2(), origin, radians).ToPoint();
        }

        /// <summary>
        /// Obtém a posição de um vetor ao informar a origem e o grau de rotação.        
        /// </summary>
        /// <param name="point">A posição do vetor na tela.</param>
        /// <param name="origin">A coordenada na tela que será o pivô para rotação, a origem.</param>
        /// <param name="radians">O grau da rotação em radianos.</param>
        public static Vector2 Get(Vector2 point, Vector2 origin, float radians)
        {
            // Cálculo retirado de: http://www.inf.pucrs.br/~pinho/CG/Aulas/Vis2d/Instanciamento/Instanciamento.htm
            //
            // Fórmula:
            //
            // xf = (xo - xr) * cos(@) - (yo - yr) * sin(@) + xr
            // yf = (yo - yr) * cos(@) + (xo - xr) * sin(@) + yr
            //
            // (xo, yo) = Ponto que você deseja rotacionar
            // (xr, yr) = Ponto em que você vai rotacionar o ponto acima(no seu caso o centro do retangulo)
            // (xf, yf) = O novo local do ponto rotacionado
            // @ = Angulo de rotação

            var resultX = (point.X - origin.X) * Math.Cos(radians) - (point.Y - origin.Y) * Math.Sin(radians) + origin.X;
            var resultY = (point.Y - origin.Y) * Math.Cos(radians) + (point.X - origin.X) * Math.Sin(radians) + origin.Y;            

            return new Vector2((float)resultX, (float)resultY);
        }
    }
}
