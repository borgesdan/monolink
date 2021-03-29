using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Common;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.EntitySystem
{
    public sealed class TextElement : Element2D, IBuildable<TextElement>
    {
        Rectangle bounds;

        //------------- PROPERTIES ---------------//

        /// <summary>Obtém ou define a instância de SpriteFont a ser utilizada.</summary>
        public SpriteFont Font { get; set; } = null;

        /// <summary>Obtém ou define o texto a ser exibido atráves de uma instância da classe StringBuilder.</summary>
        public StringBuilder Text { get; set; }        

        public override Rectangle Bounds
        {
            get
            {
                Vector2 measure = Vector2.Zero;

                if (Text != null && Text.Length > 0)
                    measure = Font.MeasureString(Text);

                bounds = this.GetBounds((int)measure.X, (int)measure.Y, Origin);
                return bounds;
            }
        }

        //------------- CONSTRUCTOR ---------------//

        /// <summary>
        /// Inicializa uma nova instância do componente.
        /// </summary>
        public TextElement() : base()
        {
        }        

        /// <summary>
        /// Inicializa uma nova instância do componente como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public TextElement(TextElement source) : base(source)
        {
            this.Font = source.Font;
            this.Text = new StringBuilder(source.Text.ToString());            
        }

        //------------- METHODS ---------------//

        /// <summary>
        /// Define os atributos do componente.
        /// </summary>
        /// <param name="setFunction">Define os atributos do componente através de uma função.</param>    
        public TextElement Build(Action<TextElement> setFuncion)
        {
            setFuncion(this);

            return this;
        }

        /// <summary>
        /// Obtém uma nova instância de um SpriteFont através de um cópia profunda.
        /// </summary>
        /// <param name="source">A origem para cópia.</param>
        public SpriteFont GetDeepCopy(SpriteFont source)
        {
            var glyphs = source.Glyphs;
            List<Rectangle> glyphBounds = new List<Rectangle>();
            List<Rectangle> cropping = new List<Rectangle>();
            List<Vector3> kerning = new List<Vector3>();
            List<char> chars = new List<char>();

            foreach (var g in glyphs)
            {
                chars.Add(g.Character);
                glyphBounds.Add(g.BoundsInTexture);
                cropping.Add(g.Cropping);
                kerning.Add(new Vector3(g.LeftSideBearing, g.Width, g.RightSideBearing));
            }

            SpriteFont font = new SpriteFont(source.Texture, glyphBounds, cropping, chars, source.LineSpacing, source.Spacing, kerning, source.DefaultCharacter);
            return font;
        }

        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Text != null)
            {
                var transform = Owner.Transform.D2;

                spriteBatch.DrawString(Font, Text, transform.Position, Color, transform.Rotation, Origin, transform.Scale, Effects, LayerDepth);
            }
        }
    }
}