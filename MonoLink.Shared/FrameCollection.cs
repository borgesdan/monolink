using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoLink
{
    /// <summary>
    /// Representa o encapsulamento de objetos HitFrames em relação a um SpriteFrame.
    /// </summary>
    public class FrameCollection
    {
        /// <summary>
        /// Obtém ou define o frame de referência.
        /// </summary>
        public SpriteFrame Frame { get; set; } = SpriteFrame.Empty;
        /// <summary>
        /// Obtém ou define os HitFrames que pertencem ao SpriteFrame.
        /// </summary>
        public List<HitFrame> HitFrames { get; set; } = new List<HitFrame>();

        //------------- CONSTRUCTOR ---------------//        

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="frame">Define o SpriteFrame de referência.</param>        
        /// <param name="name">Define o nome da coleção.</param>        
        public FrameCollection(SpriteFrame frame, params HitFrame[] hitFrames)
        {
            Frame = frame;            

            if(hitFrames != null)
            {
                HitFrames.AddRange(hitFrames);
            }
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public FrameCollection(FrameCollection source)
        {
            this.Frame = source.Frame;
            this.HitFrames.AddRange(source.HitFrames);
        }

        //------------- METHODS ---------------//        
        
        /// <summary>
        /// Adiciona um novo HitFrame.
        /// </summary>
        public FrameCollection Add(HitFrame hitFrame)
        {
            HitFrames.Add(hitFrame);
            return this;
        }

        /// <summary>
        /// Adiciona uma coleção de HitFrames.
        /// </summary>
        public FrameCollection AddRange(params HitFrame[] hitframes)
        {
            if(hitframes != null)
                HitFrames.AddRange(hitframes);
            
            return this;
        }

        /// <summary>
        /// Obtém um HitFrame relativo a um retângulo.
        /// </summary>
        /// <param name="index">O index do HitFrame na propriedade HitFrames.</param>
        /// <param name="target">O tamanho final do SpriteFrame.</param>
        /// <param name="scale">O valor da escala do SpriteFrame.</param>
        /// <param name="flip">Informa se o SpriteFrame sofre um efeito de espelhamento.</param>
        public HitFrame GetRelativeHitFrame(int index, Rectangle target, Vector2 scale, SpriteEffects flip)
        {
            HitFrame hf = HitFrames[index];

            int x = Frame.X - hf.X;
            int y = Frame.Y - hf.Y;
            int w = Frame.Width - hf.Width;
            int h = Frame.Height - hf.Height;

            Rectangle rectangle = new Rectangle(target.X - (int)(x * scale.X), target.Top - (int)(y * scale.Y), target.Width - (int)(w * scale.X), target.Height - (int)(h * scale.Y));
            Point rotated = RotationHelper.Get(rectangle.Location, target.Center.ToVector2(), MathHelper.ToRadians(180));

            if (flip == SpriteEffects.FlipHorizontally)
            {   
                rectangle.X = rotated.X - rectangle.Width;
            }
            if (flip == SpriteEffects.FlipVertically)
            {                
                rectangle.Y = rotated.Y - rectangle.Height;
            }

            HitFrame hit = new HitFrame(rectangle, hf.CanCollide, hf.CanTakeDamage, hf.DamagePercentage, hf.CanInflictDamage, hf.Power);
            hit.T01 = hf.T01;
            hit.T02 = hf.T02;
            hit.T03 = hf.T03;
            hit.T04 = hf.T04;
            hit.T05 = hf.T05;

            return hit;
        }        
    }
}
