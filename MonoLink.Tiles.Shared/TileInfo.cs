using Microsoft.Xna.Framework;

namespace MonoLink.Tiles
{
    /// <summary>
    /// Armazena informações para o desenho dos tiles em uma classe MapReader.
    /// </summary>
    public struct TileInfo
    {
        /// <summary>Obtém a posiçao do tile na tela.</summary>
        public Vector2 Position;
        /// <summary>Obtém o valor da coordenada no mapa em que se encontra o TileInfo.</summary>
        public int Value;
        /// <summary>Obtém a cor a ser utilizada no Tile</summary>
        public Color Color;
    }
}
