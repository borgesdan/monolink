using Microsoft.Xna.Framework;

namespace MonoLink
{
    public interface IUpdate
    {
        /// <sumary>Obtém ou define se esta instância está disponível para ser atualizada.</sumary>
        bool IsEnabled { get; set; }
        /// <summary>
        /// Chama o método de atualização.
        /// </summary>
        /// <param name="gameTime">Obtém o acesso aos tempos de jogo.</param>
        void Update(GameTime gameTime);
    }
}