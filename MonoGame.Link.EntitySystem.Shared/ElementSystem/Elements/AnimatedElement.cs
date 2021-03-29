using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Common;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.EntitySystem
{
    /// <summary>
    /// Componente que implementa a funcionalidade de armazenar uma lista de animações.
    /// </summary>
    public sealed class AnimatedElement : Element2D, IBuildable<AnimatedElement>
    {
        Rectangle bounds = Rectangle.Empty;

        //------------- PROPERTIES ---------------//

        /// <summary>Obtém ou define a lista de animações</summary>
        public List<Animation> Animations { get; set; } = new List<Animation>();
        /// <summary>Obtém a animação ativa.</summary>
        public Animation CurrentAnimation { get; private set; } = null;
        /// <summary>Obtém o nome da animação ativa.</summary>
        public string CurrentName { get => CurrentAnimation.Name; }        
        
        public override Rectangle Bounds
        {
            get
            {
                if(CurrentAnimation != null)
                {
                    Rectangle anmBounds = CurrentAnimation.CurrentFrame.Bounds;
                    Vector2 align = CurrentAnimation.CurrentFrame.Align;
                    bounds = this.GetBounds(anmBounds.Width, anmBounds.Height, Origin, align.X, align.Y);
                }
                else
                {
                    bounds = this.GetBounds(0, 0, Origin);
                }
                
                return bounds;
            }
        }

        //------------- CONSTRUCTOR ---------------//        

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        public AnimatedElement() : base() { }

        /// <summary>
        /// Inicializa uma nova instância como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public AnimatedElement(AnimatedElement source) : base(source)
        {
            for (int i = 0; i < source.Animations.Count; i++)
            {
                this.Animations.Add(new Animation(source.Animations[i]));
            }            
        }

        //------------- METHODS ---------------//

        /// <summary>
        /// Define os atributos do componente.
        /// </summary>
        /// <param name="setFunction">Define os atributos do componente através de uma função.</param>
        public AnimatedElement Build(Action<AnimatedElement> setFunction)
        {
            setFunction.Invoke(this);
            return this;
        }        

        /// <summary>Obtém uma animação através do seu nome.</summary>
        /// <param name="name">O nome da animação.</param>
        public Animation this[string name]
        {
            get => GetAnimation(name);
        }        

        protected override void OnUpdate(GameTime gameTime)
        {
            if (CurrentAnimation == null && Animations.Count > 0)
                ChangeAnimation(0, true);

            if (CurrentAnimation != null)
            {                
                //Seta as propriedades
                SetCurrentProperties();

                //Update da animação ativa.
                CurrentAnimation.Update(gameTime);
            }
        }

        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.Draw(gameTime, spriteBatch);
            }
        }

        //Define as propridades da entidade para as propriedades da animação corrente.
        private void SetCurrentProperties()
        {
            var transform = Owner.Transform.D2;

            CurrentAnimation.Position = transform.Position;
            CurrentAnimation.Scale = transform.Scale;
            CurrentAnimation.Rotation = transform.Rotation;
            CurrentAnimation.Effects = Effects;
            CurrentAnimation.Color = Color;
            CurrentAnimation.Origin = Origin;
            CurrentAnimation.LayerDepth = LayerDepth;
        }        

        /// <summary>Encontra uma animação pelo seu nome.</summary>
        /// <param name="name">O nome da animação a ser encontrada.</param>
        public Animation GetAnimation(string name)
        {
            Animation a = Animations.Find(x => x.Name.Equals(name));
            return a;
        }

        /// <summary>Adiciona uma lista de animações ao componente.</summary>
        /// <param name="animations">A lista de animações.</param>
        public AnimatedElement AddAnimations(params Animation[] animations)
        {
            foreach (var a in animations)
            {
                AddAnimation(a);
            }

            return this;
        }

        /// <summary>Adiciona uma nova animação ao componente.</summary>
        /// <param name="animation">Um objeto da classe Animation.</param>
        public AnimatedElement AddAnimation(Animation animation)
        {
            Animations.Add(animation);

            if (Animations.Count == 1)
            {
                ChangeAnimation(Animations[0], true);
            }

            return this;
        }

        /// <summary>Troca a animação ativa.</summary>
        /// <param name="animation">A instância da animação desejada.</param>
        /// <param name="resetAnimation">Defina True se deseja que a animação corrente será resetada.</param>
        public AnimatedElement ChangeAnimation(Animation animation, bool resetAnimation)
        {
            if (Animations.Contains(animation))
                ChangeAnimation(animation.Name, resetAnimation);

            return this;
        }

        /// <summary>Troca a animação ativa.</summary>
        /// <param name="index">O index da animação na lista de animações.</param>
        /// <param name="resetAnimation">Defina True se deseja que a animação corrente será resetada.</param>
        public AnimatedElement ChangeAnimation(int index, bool resetAnimation)
        {
            ChangeAnimation(Animations[index].Name, resetAnimation);

            return this;
        }

        /// <summary>Troca a animação ativa.</summary>
        /// <param name="name">Nome da próxima animação.</param>
        /// <param name="resetAnimation">Defina True se deseja que a animação corrente será resetada.</param>
        public AnimatedElement ChangeAnimation(string name, bool resetAnimation)
        {
            Animation tempAnimation = GetAnimation(name);

            if (resetAnimation)
                CurrentAnimation?.Reset();

            CurrentAnimation = tempAnimation ?? throw new ArgumentException("Animação não encontrada com esse parâmetro", nameof(name));
            SetCurrentProperties();

            return this;
        }

        /// <summary>Encontra todas as animações que contenham o nome especificado.</summary>
        /// <param name="name">O nome a ser pesquisado.</param>
        public List<Animation> GetAllAnimations(string name)
        {
            var anms = Animations.FindAll(x => x.Name.Contains(name));
            return anms;
        }
    }
}