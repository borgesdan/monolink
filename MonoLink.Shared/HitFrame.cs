using System;
using Microsoft.Xna.Framework;

namespace MonoLink
{
    /// <summary>
    /// Representa um frame que específica uma caixa de colisão.
    /// </summary>
    public struct HitFrame : IEquatable<HitFrame>
    {
        /// <summary>Define se o frame pode colidir com outro HitFrame.</summary>
        public bool CanCollide;
        /// <summary>Define se o frame pode receber dano de outro HitFrame.</summary>
        public bool CanTakeDamage;
        /// <summary>Define se o frame pode infligir dano em outro HitFrame.</summary>
        public bool CanInflictDamage;
        /// <summary>Define a porcentagem do dano sofrido, de 0F a 1F.</summary>
        public float DamagePercentage;
        /// <summary>Define a potência do dano caso CanInflitDamage esteja definido como True.</summary>
        public int Power;

        /// <summary>A posição relativa no eixo X.</v>
        public int X;
        /// <summary>A posição relativa no eixo Y.</summary>
        public int Y;
        /// <summary>A largura do frame.</summary>
        public int Width;
        /// <summary>A altura do frame.</summary>
        public int Height;

        /// <summary>Define uma valor que pode ser recuperado futuramente na tag 01.</summary>
        public byte T01;
        /// <summary>Define uma valor que pode ser recuperado futuramente na tag 02.</summary>
        public byte T02;
        /// <summary>Define uma valor que pode ser recuperado futuramente na tag 03.</summary>
        public byte T03;
        /// <summary>Define uma valor que pode ser recuperado futuramente na tag 04.</summary>
        public byte T04;
        /// <summary>Define uma valor que pode ser recuperado futuramente na tag 05.</summary>
        public byte T05;

        /// <summary>Obtém os limites do HitFrame.</summary>
        public Rectangle Bounds
        {
            get => new Rectangle(X, Y, Width, Height);
        }        

        /// <summary>
        /// Cria um novo objeto HitFrame.
        /// </summary>
        /// <param name="x">A posição relativa a um TextureFrame no eixo X.</param>
        /// <param name="y">A posição relativa a um TextureFrame no eixo Y.</param>
        /// <param name="width">A largura do recorte.</param>
        /// <param name="height">A altura do recorte.</param>
        /// <param name="canCollide">Define se o frame pode colidir com outro HitFrame.</param>
        /// <param name="canTakeDamage">Define se o frame pode receber dano de outro HitFrame.</param>
        /// <param name="damagePercentage">Define a porcentagem do dano sofrido, de 0F a 1F.</param>
        /// <param name="canInflictDamage">>Define se o frame pode infligir dano em outro HitFrame.</param>
        /// <param name="power">Define a potência do dano caso CanInflitDamage esteja definido como True.</param>
        /// <param name="t1">Obtém ou define a Tag 01.</param>
        /// <param name="t2">Obtém ou define a Tag 02.</param>
        /// <param name="t3">Obtém ou define a Tag 03.</param>
        /// <param name="t4">Obtém ou define a Tag 04<./param>
        /// <param name="t5">Obtém ou define a Tag 05.</param>
        public HitFrame(int x, int y, int width, int height, bool canCollide = false, bool canTakeDamage = false, float damagePercentage = 0, bool canInflictDamage = false, int power = 0,
            byte t1 = 0, byte t2 = 0, byte t3 = 0, byte t4 = 0, byte t5 = 0) : this(new Rectangle(x, y, width, height), canCollide, canTakeDamage, damagePercentage, canInflictDamage, power, t1, t2, t3, t4, t5) { }

        /// <summary>
        /// Cria um novo objeto HitFrame.
        /// </summary>
        /// <param name="rectangle">O retângulo que representa a posição relativa e o tamanho do box.</param>
        /// <param name="canCollide">Define se o frame pode colidir com outro HitFrame.</param>
        /// <param name="canTakeDamage">Define se o frame pode receber dano de outro HitFrame.</param>
        /// <param name="damagePercentage">Define a porcentagem do dano sofrido, de 0F a 1F.</param>
        /// <param name="canInflictDamage">>Define se o frame pode infligir dano em outro HitFrame.</param>
        /// <param name="power">Define a potência do dano caso CanInflitDamage esteja definido como True.</param>
        /// <param name="t1">Obtém ou define a Tag 01.</param>
        /// <param name="t2">Obtém ou define a Tag 02.</param>
        /// <param name="t3">Obtém ou define a Tag 03.</param>
        /// <param name="t4">Obtém ou define a Tag 04<./param>
        /// <param name="t5">Obtém ou define a Tag 05.</param>
        public HitFrame(Rectangle rectangle, bool canCollide = false, bool canTakeDamage = false, float damagePercentage = 0, bool canInflictDamage = false, int power = 0,
            byte t1 = 0, byte t2 = 0, byte t3 = 0, byte t4 = 0, byte t5 = 0)
        {
            X = rectangle.X;
            Y = rectangle.Y;
            Width = rectangle.Width;
            Height = rectangle.Height;
            CanCollide = canCollide;
            CanTakeDamage = canTakeDamage;
            DamagePercentage = damagePercentage;
            CanInflictDamage = canInflictDamage;
            Power = power;

            T01 = t1;
            T02 = t2;
            T03 = t3;
            T04 = t4;
            T05 = t5;
        }

        public bool Equals(HitFrame other)
        {
            return CanCollide == other.CanCollide &&
                   CanTakeDamage == other.CanTakeDamage &&
                   DamagePercentage == other.DamagePercentage &&
                   CanInflictDamage == other.CanInflictDamage &&
                   Power == other.Power &&
                   X == other.X &&
                   Y == other.Y &&
                   Width == other.Width &&
                   Height == other.Height &&
                   T01 == other.T01 &&
                   T02 == other.T02 &&
                   T03 == other.T03 &&
                   T04 == other.T04 &&
                   T05 == other.T05;
        }

        public override int GetHashCode()
        {
            int hashCode = -856949754;
            hashCode = hashCode * -1521134295 + CanCollide.GetHashCode();
            hashCode = hashCode * -1521134295 + CanTakeDamage.GetHashCode();
            hashCode = hashCode * -1521134295 + CanInflictDamage.GetHashCode();
            hashCode = hashCode * -1521134295 + DamagePercentage.GetHashCode();
            hashCode = hashCode * -1521134295 + Power.GetHashCode();
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            hashCode = hashCode * -1521134295 + T01.GetHashCode();
            hashCode = hashCode * -1521134295 + T02.GetHashCode();
            hashCode = hashCode * -1521134295 + T03.GetHashCode();
            hashCode = hashCode * -1521134295 + T04.GetHashCode();
            hashCode = hashCode * -1521134295 + T05.GetHashCode();
            return hashCode;
        }
    }
}