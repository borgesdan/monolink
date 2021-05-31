//Danilo Borges Santos, 24/03/2021.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MonoLink
{
    /// <summary>
    /// Representa uma animação com texturas carregadas de arquivos separados.
    /// </summary>
    public sealed class TextureAnimation : Animation2D
    {
        private int elapsedAnimationTime = 0;
        private int elapsedGameTime = 0;
        
        /// <summary>
        /// Obtém as texturas da animação.
        /// </summary>
        public List<Texture2D> Textures { get; private set; } = new List<Texture2D>();
        /// <summary>
        /// Obtém o index da textura atual.
        /// </summary>
        public int CurrentIndex { get; private set; } = 0;
        /// <summary>
        /// Obtém a textura atual.
        /// </summary>
        public Texture2D CurrentTexture => Textures.Count > 0 ? Textures[CurrentIndex] : null;

        public override TimeSpan TotalTime => new TimeSpan(0, 0, 0, 0, Time * Textures.Count);
        public override TimeSpan ElapsedTime => new TimeSpan(0, 0, 0, 0, elapsedAnimationTime);

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public TextureAnimation(TextureAnimation source) : base(source)
        {
            this.elapsedAnimationTime = source.elapsedAnimationTime;
            this.elapsedGameTime = source.elapsedGameTime;
            this.CurrentIndex = source.CurrentIndex;

            Textures = new List<Texture2D>();

            for(int i = 0; i < source.Textures.Count; i++)
            {
                Textures.Add(source.Textures[i]);
            }
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="time">O tempo de exibição de cada textura.</param>
        /// <param name="name">O nome da animação.</param>
        /// <param name="textures">A lista de texturas a ser usada na animação.</param>
        public TextureAnimation(int time, string name, Texture2D[] textures) : base(time, name)
        {
            this.Textures = new List<Texture2D>(textures);
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="time">O tempo de exibição de cada textura.</param>
        /// <param name="name">O nome da animação.</param>
        /// <param name="textures">A lista de texturas a ser usada na animação.</param>
        public TextureAnimation(int time, string name, List<Texture2D> textures) : base(time, name)
        {
            this.Textures = textures;
        }

        public override void OnUpdate(GameTime gameTime)
        {
            IsFinished = false;

            //Verifica se existem texturas.
            if (Textures.Count > 0 && Time > 0)
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
                    if (CurrentIndex > Textures.Count - 1)
                    {
                        elapsedAnimationTime = 0;
                        CurrentIndex = 0;
                        IsFinished = true;
                    }
                }
            }
        }

        public override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (CurrentTexture != null)
            {
                spriteBatch.Draw(
                   texture: CurrentTexture,
                   position: Position,
                   sourceRectangle: null,
                   color: Color,
                   rotation: Rotation,
                   origin: Origin,
                   scale: Scale,
                   effects: Effects,
                   layerDepth: LayerDepth
                   );
            }
        }

        public override void Reset()
        {
            CurrentIndex = 0;
            elapsedAnimationTime = 0;
            elapsedGameTime = 0;
        }

        public override Rectangle GetBounds()
        {
            Transform transform = new Transform(Position, Vector2.Zero, Scale, Rotation);
            return GameHelper.GetBounds(transform, CurrentTexture.Width, CurrentTexture.Height, Origin);
        }
    }
}