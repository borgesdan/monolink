using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoLink
{
    /// <summary>
    /// Oferece ajuda de funcionalidades ao MonoGame.
    /// </summary>
    public class GameHelper
    {
        /// <summary>
        /// Obtém um objeto Texture2D preechido com uma cor definida.
        /// </summary>
        /// <param name="game">A instância da classe Game.</param>
        /// <param name="width">Define a largura do retângulo.</param>
        /// <param name="height">Define a altura do retângulo.</param>
        /// <param name="color">Define a cor do retângulo.</param>
        public static Texture2D GetFilledTexture(Game game, int width, int height, Color color)
        {
            Color[] data;
            Texture2D texture;

            //Inicializa a textura com o tamanho pré-definido
            texture = new Texture2D(game.GraphicsDevice, width, height);
            //Inicializa o array de cores, sendo a quantidade a multiplicação da altura e largura do retângulo.
            data = new Color[texture.Width * texture.Height];

            //Cada cor do array é setada com a cor definida do argumento.
            for (int i = 0; i < data.Length; ++i)
                data[i] = color;

            //Seta o array de cores a textura
            texture.SetData(data);

            return texture;
        }

        /// <summary>
        /// Calcula e define os limites de um objeto 2D ao informar sua posição, escala e origem.
        /// </summary>
        /// <param name="transform">A transformação do objeto.</param>
        /// <param name="width">Informa o valor da largura do objeto.</param>
        /// <param name="height">Informa o valor da altura do objeto.</param>
        /// <param name="origin">Informa a origem para o cálculo.</param>        
        public static Rectangle GetBounds(Transform transform, int width, int height, Vector2 origin)
        {
            //Posição
            int x = (int)transform.X;
            int y = (int)transform.Y;
            //Escala
            float sx = transform.Sx;
            float sy = transform.Sy;
            //Origem
            float ox = origin.X;
            float oy = origin.Y;

            //Obtém uma matrix: -origin * escala * posição (excluíndo a rotação)
            Matrix m = Matrix.CreateTranslation(-ox, -oy, 0)
                * Matrix.CreateScale(sx, sy, 1)
                * Matrix.CreateTranslation(x, y, 0);

            //Os limites finais
            Rectangle rectangle = new Rectangle((int)m.Translation.X, (int)m.Translation.Y, (int)(width * transform.Sx), (int)(height * transform.Sy));
            return rectangle;
        }
    }
}