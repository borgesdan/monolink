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
        /// <summary>Obtém o valor do tile em uma linha e coluna no mapa de inteiros.</summary>
        public int Value;
        /// <summary>Obtém a cor a ser utilizada no Tile.s</summary>
        public Color Color;
    }
}
