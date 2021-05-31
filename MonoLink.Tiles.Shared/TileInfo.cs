using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoLink.Tiles
{
    /// <summary>
    /// Armazena informações para o desenho dos tiles em uma classe MapReader.
    /// </summary>
    public struct TileInfo
    {
        /// <summary>Obtém a posição do tile.</summary>
        public Vector2 Position;
        /// <summary>Obtém a sua escala.</summary>
        public Vector2 Scale;
        /// <summary>Obtém a sua cor.</summary>
        public Color Color;
        /// <summary>Obtém os efeitos de espelhamento.</summary>
        public SpriteEffects Effects;
        /// <summary>Obtém o valor da rotação.</summary>
        public float Rotation;
        /// <summary>Obtém o valor da coordenada no mapa em que se encontra o TileInfo.</summary>
        public int CoordValue;
        /// <summary>Obtém o valor opcional do Tile.</summary>
        public int TileValue;

        /// <summary>
        /// Cria um novo objeto da estrutura.
        /// </summary>
        /// <param name="cordValue">O valor da coordenada no mapa em que se encontra o TileInfo.</param>
        /// <param name="tileValue">O valor opcional do Tile.</param>
        /// <param name="position">A posição do tile.</param>
        /// <param name="scale">A sua escala.</param>
        /// <param name="color">A sua cor.</param>
        /// <param name="effects">Os efeitos de espelhamento.</param>
        /// <param name="rotation">Define a rotação.</param>
        internal TileInfo(int cordValue, int tileValue, Vector2 position, Vector2 scale, Color color, SpriteEffects effects, float rotation)
        {
            Position = position;            
            Scale = scale;
            Color = color;
            Effects = effects;
            Rotation = rotation;
            CoordValue = cordValue;
            TileValue = tileValue;
        }       
    }
}
