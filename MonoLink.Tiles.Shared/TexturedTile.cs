using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoLink.Tiles
{
    /// <summary>
    /// Representa um tile com uma textura estática.
    /// </summary>
    public class TexturedTile : Tile
    {
        /// <summary>
        /// Obtém a textura do tile.
        /// </summary>
        private Texture2D Texture { get; } = null;
        /// <summary>
        /// Obtém ou define o frame a ser utilizado da textura.
        /// </summary>
        public Rectangle? Frame { get; set; } = null;

        public override Rectangle Bounds
        {
            get
            {
                return GameHelper.GetBounds(new Transform(Position, Vector2.Zero, Scale, 0),
                Frame.HasValue ? Frame.Value.Width : Texture.Width,
                Frame.HasValue ? Frame.Value.Height : Texture.Height,
                Origin);
            }
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="game">A instância da classe Game.</param>
        /// <param name="texture">A textura a ser utilizada.</param>
        /// <param name="frame">O frame da textura.</param>
        public TexturedTile(Game game, Texture2D texture, Rectangle? frame = null) : base(game)
        {
            Texture = texture;
            Frame = frame;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="game">A instância da classe Game.</param>
        /// <param name="texturePath">O caminho da textura na pasta content.</param>
        /// <param name="frame">O frame da textura.</param>
        public TexturedTile(Game game, string texturePath, Rectangle? frame = null) : base(game)
        {
            Texture = game.Content.Load<Texture2D>(texturePath);
            Frame = frame;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia da outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public TexturedTile(TexturedTile source) : base(source)
        {
            this.Texture = source.Texture;
            this.Frame = source.Frame;
        }

        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: Texture,
                position: Position,
                sourceRectangle: Frame,
                color: Color,
                rotation: 0,
                origin: Origin,
                scale: Scale,
                effects: Effects,
                layerDepth: 0
                );
        }        
    }
}
