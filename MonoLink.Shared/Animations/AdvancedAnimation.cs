using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoLink
{
    /// <summary>
    /// Classe que implementa a função de uma animação 2D avançada.
    /// </summary>
    public class AdvancedAnimation : Animation2D
    {
        //------------- VARIABLES ----------------//

        int textureIndex = 0;
        int frameIndex = 0;
        int elapsedGameTime = 0;
        int elapsedAnimationTime = 0;

        //------------- PROPERTIES ---------------//

        /// <summary>Obtém ou define a lista de sprites.</summary>
        public List<SpriteSheet> Sprites { get; set; } = new List<SpriteSheet>();

        /// <summary>Obtém o index na lista Textures da textura ativa.</summary>
        public int CurrentTextureIndex
        {
            get => textureIndex;
            set
            {
                textureIndex = value;
                OnChangeIndex?.Invoke(this);
            }
        }

        /// <summary>Obtém o index do frame ativo da textura.</summary>
        public int CurrentFrameIndex
        {
            get => frameIndex;
            set
            {
                frameIndex = value;
                OnChangeFrameIndex?.Invoke(this);
            }
        }

        /// <summary>Obtém o frame ativo da textura.</summary>
        public SpriteFrame CurrentFrame
        {
            get
            {
                if (CurrentTexture != null)
                {
                    List<SpriteFrame> frames = Sprites[textureIndex].Frames;
                    return frames[frameIndex];
                }
                else
                    return SpriteFrame.Empty;
            }
        }

        /// <summary>Obtém a textura ativa.</summary>
        public Texture2D CurrentTexture { get; private set; } = null;

        /// <summary>Obtém o tempo total da animação.</summary>
        public override TimeSpan TotalTime
        {
            get
            {
                int frames = 0;

                foreach (SpriteSheet it in Sprites)
                {
                    frames += it.Frames.Count;
                }

                int m = Time * frames;
                return new TimeSpan(0, 0, 0, 0, m);
            }
        }

        public override TimeSpan ElapsedTime => new TimeSpan(0, 0, 0, 0, elapsedAnimationTime);        

        //------------- EVENTS ---------------//

        /// <summary>Evento chamado quando a animação chega ao fim.</summary>
        public event Action<AdvancedAnimation> OnEndAnimation;
        /// <summary>Evento chamado quando o valor do Index é mudado.</summary>
        public event Action<AdvancedAnimation> OnChangeIndex;
        /// <summary>Evento chamado quando o valor do FrameIndex é mudado.</summary>
        public event Action<AdvancedAnimation> OnChangeFrameIndex;

        //------------- CONSTRUCTOR ---------------//        

        /// <summary>Inicializa uma nova instância da classe.</summary>                
        /// <param name="time">O tempo em milisegundos de cada quadro da animação.</param>
        /// <param name="items">Define os items da animação.</param>
        /// <param name="name">Um nome para a animação.</param>
        public AdvancedAnimation(int time = 150, string name = "", params SpriteSheet[] items) : base(time, name)
        {
            if (items != null)
            {
                Sprites.AddRange(items);
            }                
        }

        /// <summary>Inicializa uma nova instância da classe com um construtor de cópia.</summary>
        /// <param name="source">A animação a ser copiada.</param>
        public AdvancedAnimation(AdvancedAnimation source) : base(source)
        {
            this.elapsedGameTime = source.elapsedGameTime;
            this.CurrentTextureIndex = source.CurrentTextureIndex;
            this.CurrentFrameIndex = source.CurrentFrameIndex;

            for (int i = 0; i < source.Sprites.Count; i++)
            {
                this.Sprites.Add(new SpriteSheet(source.Sprites[i]));
            }

            this.CurrentTexture = this.Sprites[CurrentTextureIndex].Texture;
        }

        //------------- METHODS ---------------//

        public override void Update(GameTime gameTime)
        {
            if (IsEnabled)
            {
                IsFinished = false;

                //Verifica se existem texturas.
                if (Sprites.Count > 0 && Time > 0)
                {
                    if (CurrentTexture == null)
                        CurrentTexture = Sprites[0].Texture;                    
                    
                    //Não continua caso não tenha frames a serem processados.
                    if (!Sprites[CurrentTextureIndex].HasFrames)
                        return;

                    //Tempo total da animação
                    elapsedAnimationTime += gameTime.ElapsedGameTime.Milliseconds;
                    //Tempo que já se passou desde a última troca de textura.
                    elapsedGameTime += gameTime.ElapsedGameTime.Milliseconds;

                    if (elapsedGameTime > Time)
                    {
                        //Verifica se o index do frame atual, é maior que a quantidade de frames da textura ativa.
                        if (CurrentFrameIndex >= Sprites[CurrentTextureIndex].Frames.Count - 1)
                        {
                            //Se sim, é hora de pular de sprite ou voltar para o primeiro frame,
                            //Caso só tenhamos uma sprite
                            if (CurrentTextureIndex >= Sprites.Count - 1)
                            {
                                CurrentTextureIndex = 0;
                                IsFinished = true;
                                elapsedAnimationTime = 0;
                            }
                            else
                            {
                                CurrentTextureIndex++;
                            }

                            CurrentFrameIndex = 0;
                        }
                        else
                        {
                            CurrentFrameIndex++;
                        }

                        //Reseta o tempo.
                        elapsedGameTime = 0;
                        //Atualiza o sprite atual.
                        CurrentTexture = Sprites[CurrentTextureIndex].Texture;
                    }
                }

                if (IsFinished)
                {
                    OnEndAnimation?.Invoke(this);
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                if (CurrentTexture != null)
                {
                    Vector2 finalOrigin = Origin + Sprites[CurrentTextureIndex].Frames[CurrentFrameIndex].Origin;

                    spriteBatch.Draw(
                       texture: CurrentTexture,
                       position: Position,
                       sourceRectangle: CurrentFrame.Bounds,
                       color: Color,
                       rotation: Rotation,
                       //origin: Origin,
                       origin: finalOrigin,
                       scale: Scale,
                       effects: Effects,
                       layerDepth: LayerDepth
                       );
                }
            }
        }

        /// <summary>Avança um sprite da animação.</summary>
        public void ForwardIndex()
        {
            CurrentTextureIndex++;

            if (CurrentTextureIndex >= Sprites.Count - 1)
                CurrentTextureIndex = 0;

            CurrentTexture = Sprites[CurrentTextureIndex].Texture;
        }

        /// <summary>Avança um Frame do atual sprite da animação.</summary>
        public void ForwardFrameIndex()
        {
            CurrentFrameIndex++;

            if (CurrentFrameIndex >= Sprites[CurrentTextureIndex].Frames.Count - 1)
                CurrentFrameIndex = 0;
        }

        /// <summary>Define as propriedades CurrentTextureIndex e CurrentFrameIndex com o valor 0.</summary>
        public override void Reset()
        {
            CurrentTextureIndex = 0;
            CurrentFrameIndex = 0;
            elapsedAnimationTime = 0;
            elapsedGameTime = 0;
        }

        /// <summary>
        /// Adiciona um item a animação.
        /// </summary>
        /// <param name="item">O item a ser adicionado.</param>
        public AdvancedAnimation Add(SpriteSheet item)
        {
            Sprites.Add(item);

            if (CurrentTexture == null)
            {
                CurrentTexture = Sprites[0].Texture;
            }

            return this;
        }

        /// <summary>Adiciona itens a animação.</summary>
        /// <param name="sprites">Os sprites a serem adicionados.</param>
        public AdvancedAnimation AddRange(params SpriteSheet[] items)
        {
            foreach (var i in items)
                Add(i);

            return this;
        }
        
        /// <summary>
        /// Obtém os limites da animação.
        /// </summary>
        public override  Rectangle GetBounds()
        {
            if (CurrentTexture != null)
            {
                Vector2 frameOrigin = Sprites[CurrentTextureIndex].Frames[CurrentFrameIndex].Origin;

                Transform transform = new Transform(Position, Vector2.Zero, Scale, Rotation);
                return GameHelper.GetBounds(transform, CurrentFrame.Width, CurrentFrame.Height, Origin + frameOrigin);
            }
            else
                return Rectangle.Empty;
        }
    }
}