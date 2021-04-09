//Danilo Borges Santos, 30/03/2021.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MonoLink
{
    /// <summary>
    /// Representa uma animação com uma textura carregada e as informações dos frames.
    /// </summary>
    public sealed class FrameAnimation : Animation2D
    {
        private int elapsedAnimationTime = 0;
        private int elapsedGameTime = 0;

        /// <summary>
        /// Obtém a textura carregada.
        /// </summary>
        public Texture2D Texture { get; private set; }
        /// <summary>
        /// Obtém a lista de frames da textura.
        /// </summary>
        public List<SpriteFrame> Frames { get; private set; }
        /// <summary>
        /// Obtém o index do frame atual.
        /// </summary>
        public int CurrentIndex { get; private set; } = 0;
        /// <summary>
        /// Obtém o frame atual.
        /// </summary>
        public SpriteFrame CurrentFrame => Frames.Count > 0 ? Frames[CurrentIndex] : SpriteFrame.Empty;
        
        public override TimeSpan TotalTime => new TimeSpan(0, 0, 0, 0, Time * Frames.Count);
        public override TimeSpan ElapsedTime => new TimeSpan(0, 0, 0, 0, elapsedAnimationTime);

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public FrameAnimation(FrameAnimation source) : base(source)
        {
            this.elapsedAnimationTime = source.elapsedAnimationTime;
            this.elapsedGameTime = source.elapsedGameTime;
            this.CurrentIndex = source.CurrentIndex;
            this.Texture = source.Texture;

            for(int i = 0; i < source.Frames.Count; i++)
            {
                this.Frames.Add(source.Frames[i]);
            }
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="time">O tempo de exibição de cada textura.</param>
        /// <param name="name">O nome da animação.</param>
        /// <param name="texture">A textura a ser carregada</param>
        /// <param name="frames">A lista de frames da textura.</param>
        public FrameAnimation(int time, string name, Texture2D texture, SpriteFrame[] frames) : base(time, name)
        {
            Texture = texture;
            Frames = new List<SpriteFrame>(frames);
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="time">O tempo de exibição de cada textura.</param>
        /// <param name="name">O nome da animação.</param>
        /// <param name="texture">A textura a ser carregada</param>
        /// <param name="frames">A lista de frames da textura.</param>
        public FrameAnimation(int time, string name, Texture2D texture, List<SpriteFrame> frames) : base(time, name)
        {
            Texture = texture;
            Frames = frames;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsEnabled)
            {
                IsFinished = false;

                //Verifica se existem texturas.
                if (Texture != null && Frames.Count > 0 && Time > 0)
                {                    
                    //Tempo total da animação
                    elapsedAnimationTime += gameTime.ElapsedGameTime.Milliseconds;
                    //Tempo para calcular as trocas da textura.
                    elapsedGameTime += gameTime.ElapsedGameTime.Milliseconds;

                    //Se o tempo de jogo é maior que o tempo de animação de cada textura
                    if (elapsedGameTime > Time)
                    {
                        //Incrementa o index
                        CurrentIndex++;
                        //Reseta o tempo que se passou da última textura.
                        elapsedGameTime = 0;

                        //Reseta tudo caso o index seja maior do que a quantidade de texturas
                        //E seta a propriedade IsFinished como true.
                        if (CurrentIndex > Frames.Count - 1)
                        {
                            elapsedAnimationTime = 0;
                            CurrentIndex = 0;
                            IsFinished = true;
                        }
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                if (Texture != null)
                {
                    Rectangle source = CurrentFrame.Bounds;

                    spriteBatch.Draw(
                       texture: Texture,
                       position: Position,
                       sourceRectangle: source,
                       color: Color,
                       rotation: Rotation,
                       origin: Origin + CurrentFrame.Align,
                       scale: Scale,
                       effects: Effects,
                       layerDepth: LayerDepth
                       );
                }
            }
        }

        public override Rectangle GetBounds()
        {
            Transform transform = new Transform(Position, Vector2.Zero, Scale, Rotation);
            return GameHelper.GetBounds(transform, CurrentFrame.Width, CurrentFrame.Height, Origin + CurrentFrame.Align);
        }

        public override void Reset()
        {
            CurrentIndex = 0;
            elapsedAnimationTime = 0;
            elapsedGameTime = 0;
        }        
    }
}
