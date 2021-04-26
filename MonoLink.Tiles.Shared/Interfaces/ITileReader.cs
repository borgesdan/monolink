using System;
using System.Collections.Generic;
using System.Text;

namespace MonoLink.Tiles
{
    /// <summary>
    /// Representa um leitor de tiles.
    /// </summary>
    public interface ITileReader
    {
        /// <summary>
        /// Obtém se o método Read() leu todo seu conteúdo e chegou ao fim.
        /// </summary>
        bool IsRead { get; }

        /// <summary>
        /// Lê o array que representa um mapa e ordena as posições dos tiles.
        /// </summary>
        void Read();
    }
}
