using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Xna.Framework.Common
{
    /// <summary>
    /// Representa um retângulo que informa uma parte específica (ou região)
    /// de uma textura (ou imagem) que pode representar uma ação, tile ou outro tipo de informação.
    /// </summary>
    public struct TextureFrame : IEquatable<TextureFrame>
    {
        //------------- VARIABLES ----------------// 

        /// <summary>A posição no eixo X na Textura.</v>
        public int X;
        /// <summary>A posição no eixo Y na Textura.</summary>
        public int Y;
        /// <summary>A largura do frame.</summary>
        public int Width;
        /// <summary>A altura do frame.</summary>
        public int Height;
        /// <summary>Valor de correção para alinhamento deste recorte em uma animação, no eixo X, caso necessário.</summary>
        public float AlignX;
        /// <summary>Valor de correção para alinhamento deste deste recorte em uma animação, no eixo Y, caso necessário.</summary>
        public float AlignY;

        /// <summary>Obtém um retângulo com a posição e tamanho do frame dentro do SpriteSheet.</summary>
        public Rectangle Bounds { get => new Rectangle(X, Y, Width, Height); }
        /// <summary>Obtém um vetor com os valores do alinhamento.</summary>
        public Vector2 Align { get => new Vector2(AlignX, AlignY); }
        /// <summary>Obtém um SpriteFrame vazio.</summary>
        public static TextureFrame Empty { get => new TextureFrame(Rectangle.Empty, Vector2.Zero); }

        //------------- CONSTRUCTOR --------------//

        /// <summary>
        /// Cria um novo objeto SpriteFrame.
        /// </summary>
        /// <param name="x">A posição no eixo X do recorte na Textura.</param>
        /// <param name="y">A posição no eixo Y do recorte na Textura.</param>
        /// <param name="width">A largura do recorte.</param>
        /// <param name="height">A altura do recorte.</param>
        /// <param name="alignX">Valor de correção para alinhamento deste recorte em uma série de recortes de uma animação no eixo X, caso necessário.</param>
        /// <param name="alignY">Valor de correção para alinhamento deste recorte em uma série de recortes de uma animação no eixo Y, caso necessário.</param>
        public TextureFrame(int x, int y, int width, int height, float alignX = 0, float alignY = 0)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            AlignX = alignX;
            AlignY = alignY;
        }

        /// <summary>
        /// Cria um novo objeto SpriteFrame;
        /// </summary>
        /// <param name="rectangle">O objeto Rectangle a ser utilizado.</param>
        /// <param name="align">Um vetor com o alinhamento para correção em animações, caso necessário.</param>
        public TextureFrame(Rectangle rectangle, Vector2 align)
        {
            X = rectangle.X;
            Y = rectangle.Y;
            Width = rectangle.Width;
            Height = rectangle.Height;
            AlignX = align.X;
            AlignY = align.Y;
        }

        //--------------- METHODS ----------------//

        public override bool Equals(object obj)
        {
            return obj is TextureFrame frame && Equals(frame);
        }
        
        public bool Equals(TextureFrame other)
        {
            return X == other.X &&
                   Y == other.Y &&
                   Width == other.Width &&
                   Height == other.Height &&
                   AlignX == other.AlignX &&
                   AlignY == other.AlignY;
        }

        public override int GetHashCode()
        {
            int hashCode = -542504893;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            hashCode = hashCode * -1521134295 + AlignX.GetHashCode();
            hashCode = hashCode * -1521134295 + AlignY.GetHashCode();
            return hashCode;
        }
        
        public static bool operator ==(TextureFrame left, TextureFrame right)
        {
            return left.Equals(right);
        }
        
        public static bool operator !=(TextureFrame left, TextureFrame right)
        {
            return !(left == right);
        }
    }
}
