// Danilo Borges Santos, 2020.

// PolygonCollisionResult by
//
// Copyright (c) 2006 Laurent Cozic
// https://www.codeproject.com/Articles/15573/2D-Polygon-Collision-Detection
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoLink.Collisions
{
    /// <summary>
    /// Estrutura que guarda os resultados de uma colisão de polígonos.
    /// </summary>
    public struct PolygonCollisionResult : IEquatable<PolygonCollisionResult>
    {
        /// <summary>Obtém o valor True caso o polígono esteja previsto a colidir baseado em sua velocidade.</summary>
        public bool WillIntersect;
        /// <summary>Obtém o valor True caso o polígono esteja intersectando outro polígono (Colidiu).</summary>
        public bool Intersect;
        /// <summary>Obtém o quanto é necessário para voltar a posição antes da colisão.</summary>
        public Vector2 Subtract; //MinimumTranslationVector

        /// <summary>
        /// Cria um objeto de PolygonCollisionResult.
        /// </summary>
        /// <param name="willIntersect">Define se o polígono está previsto a colidir baseado em sua velocidade.</param>
        /// <param name="intersect">Define se o polígono estpa intersectando outro polígono (Colidiu).</param>
        /// <param name="distance">Define o quanto o polígono adentrou na colisão no eixo X e Y.</param>
        public PolygonCollisionResult(bool willIntersect, bool intersect, Vector2 distance)
        {
            WillIntersect = willIntersect;
            Intersect = intersect;
            Subtract = distance;
        }

        public override bool Equals(object obj)
        {
            return obj is PolygonCollisionResult result && Equals(result);
        }

        public bool Equals(PolygonCollisionResult other)
        {
            return WillIntersect == other.WillIntersect &&
                   Intersect == other.Intersect &&
                   Subtract.Equals(other.Subtract);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(WillIntersect, Intersect, Subtract);
        }

        public static bool operator ==(PolygonCollisionResult left, PolygonCollisionResult right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PolygonCollisionResult left, PolygonCollisionResult right)
        {
            return !(left == right);
        }
    }    
}