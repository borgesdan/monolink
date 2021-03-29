using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Xna.Framework.EntitySystem
{
    /// <summary>
    /// Representa um elemento que expõe propriedades para manipulação em um plano 2D.
    /// </summary>
    public abstract class Element2D : Element, IElement2D
    {
        /// <summary>Obtém ou define a origem do desenho e da rotação.</summary>
        public Vector2 Origin { get; set; } = Vector2.Zero;
        /// <summary>Obtém ou define o LayerDepth no desenho do componente, de 0f a 1f, se necessário.</summary>
        public float LayerDepth { get; set; } = 0f;
        /// <summary>Obtém ou define os efeitos de espelhamento.</summary>
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
        /// <summary>Obtém ou define a cor da textura a ser desenhada.</summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Obtém os limites 2D da entidade.
        /// </summary>
        public abstract Rectangle Bounds { get; }

        /// <summary>
        /// Inicializa uma nova instância do componente.
        /// </summary>
        internal Element2D() : base() { }

        /// <summary>
        /// Inicializa uma nova instância do componente como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada</param>
        internal Element2D(Element2D source) : base(source)
        {
            this.Color = source.Color;
            this.Origin = source.Origin;
            this.LayerDepth = source.LayerDepth;
            this.Effects = source.Effects;
        }

        /// <summary>
        /// Método de auxílio que calcula e define os limites do componente que trabalha em um plano 2D através de sua posição, escala e origem.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="width">Informa e define o valor da largura do componente.</param>
        /// <param name="height">Informa e define o valor da altura do componente.</param>
        /// <param name="origin">Informa a origem para o cálculo.</param>
        /// <param name="amountOriginX">Define um valor que deve ser incrementado a origem no eixo X, se necessário.</param>
        /// <param name="amountOriginY">Define um valor que deve ser incrementado a origem no eixo Y, se necessário.</param>
        protected Rectangle GetBounds(int width, int height, Vector2 origin, float amountOriginX = 0, float amountOriginY = 0)
        {
            var transform = Owner.Transform.D2;

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
