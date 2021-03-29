using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Common;

namespace Microsoft.Xna.Framework.EntitySystem
{
    public class BarElement : Element2D, IBuildable<BarElement>
    {
        Rectangle bounds;
        float wPercentage = 1;
        float hPercentage = 1;

        /// <summary>Obtém ou define a animação da barra.</summary>
        public Animation Animation { get; set; } = null;
        /// <summary>Obtém ou define a porcentagem da largura a ser desenhada, de 0F a 1F.</summary>
        public float WidthPercentage { get => wPercentage; set => wPercentage = MathHelper.Clamp(value, 0, 1); }
        /// <summary>Obtém ou define a porcentagem da largura a ser desenhada, de 0F a 1F.</summary>
        public float HeightPercentage { get => hPercentage; set => hPercentage = MathHelper.Clamp(value, 0, 1); }
        /// <summary> Obtém ou define se o progresso será do início para o fim da barra.</summary>
        public bool StartToEnd = true;        

        public override Rectangle Bounds
        {
            get
            {
                return bounds;
            }
        }

        //------------- CONSTRUCTOR --------------//        

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        public BarElement() : base() { }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public BarElement(BarElement source) : base(source)
        {
        }

        //------------- METHODS --------------//

        protected override void OnUpdate(GameTime gameTime)
        {
            Animation?.Update(gameTime);
        }

        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(Animation != null)
            {
                var transform = Owner.Transform.D2;
                Rectangle currentFrame = Animation.CurrentFrame.Bounds;                

                if (WidthPercentage != 1 || HeightPercentage != 1)
                {
                    float w = currentFrame.Width * WidthPercentage;  
                    float h = currentFrame.Height * HeightPercentage;                    
                    
                    if (StartToEnd)
                    {
                        float x = currentFrame.Width - w;
                        currentFrame.X += (int)x;
                    }

                    currentFrame.Width = (int)w;
                    currentFrame.Height = (int)h;
                }              

                currentFrame.X += (int)transform.X;
                currentFrame.Y += (int)transform.Y;

                spriteBatch.Draw(texture: Animation.CurrentTexture, destinationRectangle: currentFrame, 
                    sourceRectangle: null, color: Color, rotation: transform.Rotation, 
                    origin: Origin, effects: Effects, layerDepth: LayerDepth);
            }
        }

        /// <summary>
        /// Define os atributos do elemento.
        /// </summary>
        /// <param name="setFunction">Define os atributos do elemento através de uma função.</param>
        public BarElement Build(Action<BarElement> buildFunction)
        {
            buildFunction?.Invoke(this);
            return this;
        }
    }
}