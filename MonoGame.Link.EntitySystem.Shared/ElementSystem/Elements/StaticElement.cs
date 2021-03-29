using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Common;
using System;

namespace Microsoft.Xna.Framework.EntitySystem
{
    /// <summary>
    /// Componente 2D que armazena um Texture2D e o desenha na tela.
    /// </summary>
    public sealed class StaticElement : Element2D, IBuildable<StaticElement>
    {
        Rectangle bounds = Rectangle.Empty;
        Texture2D texture = null;

        //------------- PROPERTIES ---------------//

        /// <summary>Obtém ou define a textura a ser carregada pelo componente. A propriedade Frame será redefinida.</summary>
        public Texture2D Texture 
        {
            get => texture;
            set
            {
                texture = value;
                Frame = new FrameCollection(new TextureFrame(Frame.Frame.Bounds, Vector2.Zero));
            }
        }        

        /// <summary>Obtém ou define uma região da textura a ser desenhada.</summary>
        public FrameCollection Frame { get; set; } = new FrameCollection();
        
        public override Rectangle Bounds 
        { 
            get
            {
                Rectangle b = Frame.Frame.Bounds;
                bounds = this.GetBounds(b.Width, b.Height, Origin);
                return bounds;
            }
        }

        //------------- CONSTRUCTOR --------------//

        /// <summary>
        /// Inicializa uma nova instância do componente.
        /// </summary>
        public StaticElement() : base()
        {
        }

        /// <summary>
        /// Inicializa uma nova instância do componente como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada</param>
        public StaticElement(StaticElement source) : base(source)
        {
            this.Texture = source.Texture;            
            this.Frame = source.Frame;
        }

        //--------------- METHODS ----------------//

        /// <summary>
        /// Define os atributos do componente.
        /// </summary>
        /// <param name="setFunction">Define os atributos do componente através de uma função.</param>    
        public StaticElement Build(Action<StaticElement> setFuncion)
        {
            setFuncion(this);

            return this;
        }

        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(Texture != null)
            {
                var transform = Owner.Transform.D2;

                spriteBatch.Draw
                (
                    texture: Texture,
                    position: transform.Position,
                    sourceRectangle: Frame.Frame.Bounds,
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