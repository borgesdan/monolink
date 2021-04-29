using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoLink
{
    /// <summary>
    /// Descreve um retângulo rotacionado.
    /// </summary>
    public struct RotatedRectangle : IEquatable<RotatedRectangle>
    {
        //---------------------------------------//
        //-----         VARIÁVEIS           -----//
        //---------------------------------------//

        /// <summary>Obtém o valor da coordenada Top-Left do retângulo rotacionado.</summary>
        public readonly Point P1;
        /// <summary>Obtém o valor da coordenada Top-right do retângulo rotacionado.</summary>
        public readonly Point P2;
        /// <summary>Obtém o valor da coordenada Bottom-Right do retângulo rotacionado.</summary>
        public readonly Point P3;
        /// <summary>Obtém o valor da coordenada Bottom-Left do retângulo rotacionado.</summary>
        public readonly Point P4;
        /// <summary>Obtém o valor do centro do retângulo do retângulo rotacionado.</summary>
        public readonly Point Center;

        //---------------------------------------//
        //-----         CONSTRUTOR          -----//
        //---------------------------------------//

        /// <summary>
        /// Cria uma novo objeto de RotatedRectangle.
        /// </summary>
        /// <param name="p1">O valor da coordenada Top-Left.</param>
        /// <param name="p2">O valor da coordenada Top-right.</param>
        /// <param name="p3">O valor da coordenada Bottom-Right.</param>
        /// <param name="p4">O valor da coordenada Bottom-Left.</param>
        /// <param name="center">O valor do centro do retângulo.</param>
        public RotatedRectangle(Point p1, Point p2, Point p3, Point p4, Point center)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
            P4 = p4;
            Center = center;
        }

        /// <summary>
        /// Cria um novo objeto de RotatedRectangle informando os argumentos para uma rotação de um retângulo.
        /// </summary>
        /// <param name="rectangle">O retângulo a ser rotacionado.</param>
        /// <param name="origin">A coordenada na tela que será o pivô para a rotação, a origem.</param>
        /// <param name="radians">O grau da rotação em radianos.</param>
        public RotatedRectangle(Rectangle rectangle, Vector2 origin, float radians)
        {
            RotatedRectangle r = RotationHelper.GetRectangle(rectangle, origin, radians);

            P1 = r.P1;
            P2 = r.P2;
            P3 = r.P3;
            P4 = r.P4;
            Center = r.Center;
        }

        //---------------------------------------//
        //-----         FUNÇÕES             -----//
        //---------------------------------------//

        public override bool Equals(object obj)
        {
            return obj is RotatedRectangle rectangle && Equals(rectangle);
        }

        public bool Equals(RotatedRectangle other)
        {
            return P1.Equals(other.P1) &&
                   P2.Equals(other.P2) &&
                   P3.Equals(other.P3) &&
                   P4.Equals(other.P4) &&
                   Center.Equals(other.Center);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(P1, P2, P3, P4, Center);
        }

        public static bool operator ==(RotatedRectangle left, RotatedRectangle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RotatedRectangle left, RotatedRectangle right)
        {
            return !(left == right);
        }
    }
}
