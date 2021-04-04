//Danilo Borges Santos, 03/04/2021

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoLink.Screen
{
    //TODO: Não implementado
    public sealed class InfinityLayer<T> : ScreenLayer where T : Animation2D
    {
        Viewport viewport;
        GraphicsDevice device;

        private float infPositionStartX = 0;
        private float infPositionStartY = 0;

        public T Animation { get; }
        /// <summary>Obtém ou define se a animação será repetida ao infinito no eixo X.</summary>
        public bool InfinityX { get; set; } = true;
        /// <summary>Obtém ou define se a animação será repetida ao infinito no eixo Y.</summary>
        public bool InfinityY { get; set; } = true;

        public InfinityLayer(Screen screen, T animation, string name = "") : base(screen, name)
        {
            Animation = animation;
            device = screen.Game.GraphicsDevice;
            viewport = device.Viewport;
        }

        public InfinityLayer(InfinityLayer<T> source) : base(source)
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
            viewport = device.Viewport;

            Animation.Update(gameTime);

            Rectangle bounds = Animation.GetBounds();

            //Cálculo do eixo X
            Point x1 = new Point(-int.MaxValue, 0);
            Point x2 = new Point(viewport.X, viewport.Y);
            int aw = bounds.Width;
            //Distância entre a o eixo X da view e o eixo X da câmera, R = (v1.X - v2.X)
            int rx = x1.X - x2.X;
            //Quantidade de animações dentro dessa distância, Q = |R| / aw
            int qx = Math.Abs(rx) / aw;
            //Posição para início do desenho, Ps = (aw * q) - aw
            int psx = Math.Abs(x1.X) - (aw * qx);

            //Cálculo do eixo Y
            Point y1 = new Point(0, -int.MaxValue);
            Point y2 = new Point(viewport.X, viewport.Y);
            int h = bounds.Height;
            //Distância entre a o eixo Y do limite e o eixo Y da view, R = (v1.Y - v2.Y)
            int ry = y1.Y - y2.Y;
            //Quantidade de animações dentro dessa distância, Q = |R| / (altura da animação)
            int qy = Math.Abs(ry) / h;
            //Posição para início do desenho Ps = (aw * q) - aw
            int psy = Math.Abs(y1.Y) - (h* qy);
            
            infPositionStartX = InfinityX ? -psx : Animation.Position.X;
            infPositionStartY = InfinityY ? -psy : Animation.Position.Y;
        }
    }
}