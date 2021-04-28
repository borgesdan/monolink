using System;
using System.Collections.Generic;
using System.Text;

namespace MonoLink.Tiles
{
    /// <summary>
    /// Representa um leitor de tiles.
    /// </summary>
    public interface ITileBuilder
    {
        /// <summary>
        /// Obtém se o método Build() leu todo seu conteúdo e chegou ao fim.
        /// </summary>
        bool IsBuilded { get; }

        /// <summary>
        /// Lê o array que representa um mapa e cria os tiles ordenados em suas posições..
        /// </summary>
        void Build();
    }
}
