using System;
using System.Collections.Generic;
using System.Text;

namespace MonoLink.Tiles
{
    public class TileSector
    {
        int[,] array = null;        

        /// <summary>
        /// Inicializa uma nova instância de Setor.
        /// </summary>
        /// <param name="sector">Um array com a mesma quantidade de linhas e colunas.</param>
        /// <param name="table">Define a tabela de índices com seus respectivos Tiles.</param>
        public TileSector(int[,] sector)
        {
            array = sector;
        }

        /// <summary>
        /// Obtém o mapa com a numeração dos tiles.
        /// </summary>
        public int[,] GetSector()
        {
            return (int[,])array.Clone();
        }
    }
}
