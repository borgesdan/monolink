using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Common
{
    /// <summary>
    /// Oferece ajuda de funcionalidades ao MonoGame.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Obtém uma textura 2D no formato retângular preenchida com uma cor.
        /// </summary>
        /// <param name="game">A instância da classe Game.</param>
        /// <param name="width">A largura do retângulo.</param>
        /// <param name="height">A altura do retângulo.</param>
        /// <param name="color">A cor do retângulo.</param>
        /// <returns></returns>
        public static Texture2D GetRectangle(Game game, int width, int height, Color color)
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
        /// Método de auxílio que calcula e define os limites do componente que trabalha em um plano 2D através de sua posição, escala e origem.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="width">Informa e define o valor da largura do componente.</param>
        /// <param name="height">Informa e define o valor da altura do componente.</param>
        /// <param name="origin">Informa a origem para o cálculo.</param>
        /// <param name="amountOriginX">Define um valor extra que deve ser incrementado a origem no eixo X, se necessário.</param>
        /// <param name="amountOriginY">Define um valor extra que deve ser incrementado a origem no eixo Y, se necessário.</param>
        public static Rectangle GetBounds(Transform transform, int width, int height, Vector2 origin, float amountOriginX = 0, float amountOriginY = 0)
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
            Matrix m = Matrix.CreateTranslation(-ox + -amountOriginX, -oy + -amountOriginY, 0)
                * Matrix.CreateScale(sx, sy, 1)
                * Matrix.CreateTranslation(x, y, 0);

            //Os limites finais
            Rectangle rectangle = new Rectangle((int)m.Translation.X, (int)m.Translation.Y, (int)(width * transform.Sx), (int)(height * transform.Sy));
            return rectangle;
        }
    }
}