using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Common
{
    public interface IDraw
    {
        /// <sumary>Obtém ou define se esta instância está disponível para ser desenhada.</sumary>
        bool IsVisible { get; set; }
        /// <summary>
        /// Desenha a entidade.
        /// </summary>
        /// <param name="gameTime">Obtém os tempos de jogo.</param>
        /// <param name="spriteBatch">A instância de um SpriteBatch para desenho 2D.</param>
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
