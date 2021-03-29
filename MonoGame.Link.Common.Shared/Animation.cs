using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Common
{
    /// <summary>
    /// Classe que implementa a função de uma animação 2D.
    /// </summary>
    public class Animation : IUpdate, IDraw
    {
        //------------- VARIABLES ----------------//

        int time = 0;
        int textureIndex = 0;
        int frameIndex = 0;
        int elapsedGameTime = 0;

        //------------- PROPERTIES ---------------//        
        
        /// <summary>Obtém ou define se a animação está apta a ser atualizada.</summary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>Obtém ou define se a animação está apta a ser desenhada.</summary>
        public bool IsVisible { get; set; } = true;
        /// <summary>Obtém ou define um nome para a animação.</summary>
        public string Name { get; set; } = "";

        /// <summary>Obtém ou define a lista de items.</summary>
        public List<TextureItem> Textures { get; set; } = new List<TextureItem>();

        /// <summary>Obtém ou define em milisegundos o tempo de exibição de cada imagem.</summary>
        public int Time { get => time; set => time = Math.Abs(value); }

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
        public TextureFrame CurrentFrame
        {
            get
            {
                if (CurrentTexture != null)
                {
                    List<TextureFrame> frames = Textures[textureIndex].GetTextureFrames();
                    return frames[frameIndex];
                }
                else
                    return TextureFrame.Empty;
            }
        }

        /// <summary>Obtém a textura ativa.</summary>
        public Texture2D CurrentTexture { get; private set; } = null;

        /// <summary>Obtém os hitframes ativos da textura.</summary>
        public List<HitFrame> CurrentHitFrames 
        {
            get
            {
                if(CurrentTexture != null && Textures[CurrentTextureIndex].ContainsFrames)
                {
                    return Textures[CurrentTextureIndex].Frames[CurrentFrameIndex].HitFrames;
                }

                return new List<HitFrame>();
            }
        }

        /// <summary>
        /// Obtém os hitframes ativos da textura relativos ao tamanho da propriedade Bounds.
        /// </summary>
        public List<HitFrame> CurrentRelativeHitFrames
        {
            get
            {
                List<HitFrame> hFrames = new List<HitFrame>();

                if (CurrentTexture != null && CurrentHitFrames.Count != 0)
                {
                    FrameCollection collection = Textures[CurrentTextureIndex].Frames[CurrentFrameIndex];                    

                    for(int i = 0; i < CurrentHitFrames.Count; i++)
                    {
                        HitFrame hRelative = collection.GetRelativeHitFrame(i, GetBounds(), Scale, Effects);                        
                        hFrames.Add(hRelative);
                    }                    
                }

                return hFrames;
            }
        }

        /// <summary>Obtém o tempo total da animação.</summary>
        public TimeSpan TotalTime
        {
            get
            {
                int frames = 0;

                foreach (TextureItem it in Textures)
                {
                    frames += it.GetTextureFrames().Count;
                }

                int m = Time * frames;
                return new TimeSpan(0, 0, 0, 0, m);
            }
        }

        /// <summary>Obtém o tempo que já se passou da animação.</summary>
        public TimeSpan ElapsedTime
        {
            get
            {
                int count = 0;

                for (int i = 0; i <= CurrentTextureIndex; i++)
                {
                    var frames = Textures[i].GetTextureFrames();
                    for (int f = 0; f <= frames.Count - 1; f++)
                    {
                        if (i == CurrentTextureIndex && f > CurrentFrameIndex)
                            break;

                        count++;
                    }
                }

                var m = Time * count;
                return new TimeSpan(0, 0, 0, 0, m);
            }
        }
        /// <summary>Retorna True se a animação chegou ao fim.</summary>
        public bool IsFinished
        {
            get
            {
                if (ElapsedTime == TotalTime)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>Obtém ou define a posição.</summary>
        public Vector2 Position { get; set; } = Vector2.Zero;
        /// <summary>Obtém ou define a escala.</summary>
        public Vector2 Scale { get; set; } = Vector2.One;
        /// <summary>Obtém ou define a rotação.</summary>
        public float Rotation { get; set; } = 0;
        /// <summary>Obtém ou define a cor da textura a ser desenhada.</summary>
        public Color Color { get; set; } = Color.White;
        /// <summary>Obtém ou define a origem do desenho e da rotação.</summary>
        public Vector2 Origin { get; set; } = Vector2.Zero;
        /// <summary>Obtém ou define o LayerDepth no desenho do componente, de 0f a 1f, se necessário.</summary>
        public float LayerDepth { get; set; } = 0;
        /// <summary>Obtém ou define os efeitos de espelhamento.</summary>
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;

        //------------- EVENTS ---------------//

        /// <summary>Evento chamado quando a animação chega ao fim.</summary>
        public event Action<Animation> OnEndAnimation;
        /// <summary>Evento chamado quando o valor do Index é mudado.</summary>
        public event Action<Animation> OnChangeIndex;
        /// <summary>Evento chamado quando o valor do FrameIndex é mudado.</summary>
        public event Action<Animation> OnChangeFrameIndex;

        //------------- CONSTRUCTOR ---------------//        

        /// <summary>Inicializa uma nova instância da classe.</summary>                
        /// <param name="time">O tempo em milisegundos de cada quadro da animação.</param>
        /// <param name="items">Define os items da animação.</param>
        /// <param name="name">Um nome para a animação.</param>
        public Animation(int time = 150, string name = "", params TextureItem[] items)
        {
            Time = time;
            Name = name;

            if (items != null)
            {
                Textures.AddRange(items);
            }                
        }

        /// <summary>Inicializa uma nova instância da classe com um construtor de cópia.</summary>
        /// <param name="source">A animação a ser copiada.</param>
        public Animation(Animation source)
        {
            this.IsVisible = source.IsVisible;
            this.IsEnabled = source.IsEnabled;
            this.elapsedGameTime = source.elapsedGameTime;
            this.CurrentTextureIndex = source.CurrentTextureIndex;
            this.CurrentFrameIndex = source.CurrentFrameIndex;

            for (int i = 0; i < source.Textures.Count; i++)
            {
                this.Textures.Add(new TextureItem(source.Textures[i]));
            }

            this.CurrentTexture = this.Textures[CurrentTextureIndex].Texture;
            this.Time = source.Time;
        }

        //------------- METHODS ---------------//

        public void Update(GameTime gameTime)
        {
            if (IsEnabled)
            {
                //Atualiza a animação.
                Animate(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                if (CurrentTexture != null)
                {
                    Vector2 finalOrigin = Origin + Textures[CurrentTextureIndex].GetTextureFrames()[CurrentFrameIndex].Align;

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

        private void Animate(GameTime gameTime)
        {
            //Verifica se existem texturas.
            if (Textures.Count > 0)
            {
                if (CurrentTexture == null)
                    CurrentTexture = Textures[0].Texture;

                int framesCount = Textures[CurrentTextureIndex].GetTextureFrames().Count;

                //Não continua se o tempo for igual a zero.
                //Não continua caso não tenha frames a serem processados.
                if (Time == 0 || framesCount <= 0)
                    return;

                //Tempo que já se passou desde a última troca de textura.
                elapsedGameTime += gameTime.ElapsedGameTime.Milliseconds;

                if (elapsedGameTime > Time)
                {
                    //Verifica se o index do frame atual, é maior que a quantidade de frames da textura ativa.
                    if (CurrentFrameIndex >= framesCount - 1)
                    {
                        //Se sim, é hora de pular de sprite ou voltar para o primeiro frame,
                        //Caso só tenhamos uma sprite
                        if (CurrentTextureIndex >= Textures.Count - 1)
                        {
                            CurrentTextureIndex = 0;
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
                    CurrentTexture = Textures[CurrentTextureIndex].Texture;
                }
            }

            if (IsFinished)
            {
                OnEndAnimation?.Invoke(this);
            }
        }

        /// <summary>Avança um sprite da animação.</summary>
        public void ForwardIndex()
        {
            CurrentTextureIndex++;

            if (CurrentTextureIndex >= Textures.Count - 1)
                CurrentTextureIndex = 0;

            CurrentTexture = Textures[CurrentTextureIndex].Texture;
        }

        /// <summary>Avança um Frame do atual sprite da animação.</summary>
        public void ForwardFrameIndex()
        {
            CurrentFrameIndex++;

            if (CurrentFrameIndex >= Textures[CurrentTextureIndex].GetTextureFrames().Count - 1)
                CurrentFrameIndex = 0;
        }

        /// <summary>Define as propriedades CurrentTextureIndex e CurrentFrameIndex com o valor 0.</summary>
        public void Reset()
        {
            CurrentTextureIndex = 0;
            CurrentFrameIndex = 0;
            elapsedGameTime = 0;
        }

        /// <summary>
        /// Adiciona um item a animação.
        /// </summary>
        /// <param name="item">O item a ser adicionado.</param>
        public Animation AddItem(TextureItem item)
        {
            Textures.Add(item);

            if (CurrentTexture == null)
            {
                CurrentTexture = Textures[0].Texture;
            }

            return this;
        }

        /// <summary>Adiciona itens a animação.</summary>
        /// <param name="sprites">Os sprites a serem adicionados.</param>
        public Animation AddItems(params TextureItem[] items)
        {
            foreach (var i in items)
                AddItem(i);

            return this;
        }
        
        /// <summary>
        /// Obtém os limites da animação.
        /// </summary>
        public Rectangle GetBounds()
        {
            if (CurrentTexture != null)
            {
                // Posição
                int x = (int)Position.X;
                int y = (int)Position.Y;
                //Escala
                float sx = Scale.X;
                float sy = Scale.Y;
                //Origem
                float ox = Origin.X;
                float oy = Origin.Y;

                Vector2 frameOrigin = Textures[CurrentTextureIndex].GetTextureFrames()[CurrentFrameIndex].Align;

                //Obtém uma matrix: -origin * escala * posição (excluíndo a rotação)
                Matrix m = Matrix.CreateTranslation(-ox + -frameOrigin.X, -oy + -frameOrigin.Y, 0)
                    * Matrix.CreateScale(sx, sy, 1)
                    * Matrix.CreateTranslation(x, y, 0);

                //Os limites finais
                Rectangle rectangle = new Rectangle((int)m.Translation.X, (int)m.Translation.Y, (int)(CurrentFrame.Width * sx), (int)(CurrentFrame.Height * sy));
                return rectangle;
            }
            else
                return Rectangle.Empty;
        }
    }
}