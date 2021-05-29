using System;
using Microsoft.Xna.Framework;

namespace MonoLink
{
    /// <summary>
    /// Representa um retângulo que informa uma parte específica (ou região) de um Texture2D.
    /// </summary>
    public struct SpriteFrame : IEquatable<SpriteFrame>
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
        /// <summary>Valor da origem deste recorte no eixo X, caso necessário.</summary>
        public float OriginX;
        /// <summary>Valor da origem deste deste recorte no eixo Y, caso necessário.</summary>
        public float OriginY;

        /// <summary>Obtém os limites do frame.</summary>
        public Rectangle Bounds { get => new Rectangle(X, Y, Width, Height); }
        /// <summary>Obtém a origem do frame.</summary>
        public Vector2 Origin { get => new Vector2(OriginX, OriginY); }
        /// <summary>Obtém um SpriteFrame vazio.</summary>
        public static SpriteFrame Empty { get => new SpriteFrame(Rectangle.Empty, Vector2.Zero); }

        //------------- CONSTRUCTOR --------------//

        /// <summary>
        /// Cria um novo objeto SpriteFrame.
        /// </summary>
        /// <param name="x">A posição no eixo X do recorte na Textura.</param>
        /// <param name="y">A posição no eixo Y do recorte na Textura.</param>
        /// <param name="width">A largura do recorte.</param>
        /// <param name="height">A altura do recorte.</param>
        /// <param name="originX">Valor da origem deste recorte no eixo X, caso necessário.</param>
        /// <param name="originY">Valor da origem deste deste recorte no eixo Y, caso necessário.</param>
        public SpriteFrame(int x, int y, int width, int height, float originX = 0, float originY = 0) 
            : this(new Rectangle(x, y, width, height), new Vector2(originX, originY)) { }

        /// <summary>
        /// Cria um novo objeto SpriteFrame.
        /// </summary>
        /// <param name="rectangle">O objeto Rectangle a ser utilizado.</param>
        /// <param name="align">Define a origem do frame, se necessário.</param>
        public SpriteFrame(Rectangle rectangle, Vector2 align)
        {
            X = rectangle.X;
            Y = rectangle.Y;
            Width = rectangle.Width;
            Height = rectangle.Height;
            OriginX = align.X;
            OriginY = align.Y;
        }

        //--------------- METHODS ----------------//

        public override bool Equals(object obj)
        {
            return obj is SpriteFrame frame && Equals(frame);
        }
        
        public bool Equals(SpriteFrame other)
        {
            return X == other.X &&
                   Y == other.Y &&
                   Width == other.Width &&
                   Height == other.Height &&
                   OriginX == other.OriginX &&
                   OriginY == other.OriginY;
        }

        public override int GetHashCode()
        {
            int hashCode = -542504893;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            hashCode = hashCode * -1521134295 + OriginX.GetHashCode();
            hashCode = hashCode * -1521134295 + OriginY.GetHashCode();
            return hashCode;
        }
        
        public static bool operator ==(SpriteFrame left, SpriteFrame right)
        {
            return left.Equals(right);
        }
        
        public static bool operator !=(SpriteFrame left, SpriteFrame right)
        {
            return !(left == right);
        }
    }
}
