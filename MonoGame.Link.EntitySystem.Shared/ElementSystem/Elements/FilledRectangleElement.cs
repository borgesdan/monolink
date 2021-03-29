using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Common;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.EntitySystem
{
    /// <summary>
    /// Componente 2D que desenha um retângulo preenchido com uma cor na tela.
    /// </summary>
    public sealed class FilledRectangleElement : Element2D, IBuildable<FilledRectangleElement>
    {
        Rectangle bounds = Rectangle.Empty;

        /// <summary>Obtém a textura.</summary>
        public Texture2D Texture { get; private set; } = null;
        /// <summary>Obtém a largura do retângulo.</summary>
        public int Width { get => Texture != null ? Texture.Width : 0; }
        /// <summary>Obtém a altura do retângulo.</summary>
        public int Height { get => Texture != null ? Texture.Height : 0; }
        
        public override Rectangle Bounds
        {
            get
            {
                if(Texture != null)
                {
                    bounds = this.GetBounds(Texture.Width, Texture.Height, Origin);
                }                
                return bounds;
            }
        }

        //------------- CONSTRUCTOR --------------//        

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        public FilledRectangleElement() : base()
        { }

        /// <summary>
        /// Inicializa uma nova instância do componente como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada</param>
        public FilledRectangleElement(FilledRectangleElement source) : base(source)
        {
            this.Texture = source.Texture;
        }

        //------------- METHODS --------------//

        /// <summary>
        /// Define os atributos do componente.
        /// </summary>
        /// <param name="setFunction">Define os atributos do componente através de uma função.</param>    
        public FilledRectangleElement Build(Action<FilledRectangleElement> setFuncion)
        {
            setFuncion(this);

            return this;
        }        

        /// <summary>
        /// Cria a textura do retângulo.
        /// </summary>
        /// <param name="width">A largura do retângulo.</param>
        /// <param name="height">A altura do retângulo.</param>
        /// <param name="color">A cor do retângulo.</param>        
        public FilledRectangleElement Create(int width,int height, Color color)
        {
            Texture = Util.GetRectangle(Game, width, height, color);
            return this;
        }        

        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                var transform = Owner.Transform.D2;

                spriteBatch.Draw
                    (
                    texture: Texture,
                    position: transform.Position,
                    sourceRectangle: null,
                    color: Color,
                    rotation: transform.Rotation,
                    origin: Origin,
                    scale: transform.Scale,
                    effects: Effects,
                    layerDepth: LayerDepth
                    );
            }
        }
    }
}
