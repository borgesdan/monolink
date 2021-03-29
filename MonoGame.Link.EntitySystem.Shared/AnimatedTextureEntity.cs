using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Common;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.EntitySystem
{
    public class AnimatedTextureEntity : Entity
    {
        /// <summary>Obtém ou define a lista de animações</summary>
        public List<Animation> Animations { get; set; } = new List<Animation>();
        /// <summary>Obtém a animação ativa.</summary>
        public Animation CurrentAnimation { get; private set; } = null;
        /// <summary>Obtém o nome da animação ativa.</summary>
        public string CurrentName { get => CurrentAnimation.Name; }

        public AnimatedTextureEntity(AnimatedTextureEntity source) : base(source)
        {
            for(int i = 0; i < source.Animations.Count; i++)
            {
                Animations.Add(new Animation(source.Animations[i]));
            }

            if(source.CurrentAnimation != null)
            {
                int index = source.Animations.FindIndex(a => a.Equals(source.CurrentAnimation));
                CurrentAnimation = Animations[index];
            }            
        }

        public AnimatedTextureEntity(Game game, string name = "") : base(game, name)
        {
        }

        /// <summary>Obtém uma animação através do seu nome.</summary>
        /// <param name="name">O nome da animação.</param>
        public Animation this[string name]
        {
            get => GetAnimation(name);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            if(CurrentAnimation == null && Animations.Count > 0)
            {
                ChangeAnimation(0, true);
            }

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
            CurrentAnimation?.Draw(gameTime, spriteBatch);
        }

        //Define as propridades da entidade para as propriedades da animação corrente.
        private void SetCurrentProperties()
        {
            CurrentAnimation.Color = this.Color;
            CurrentAnimation.Effects = this.SpriteEffects;
            CurrentAnimation.LayerDepth = this.LayerDepth;
            CurrentAnimation.Origin = this.Origin;
            CurrentAnimation.Position = this.Transform.Position2;
            CurrentAnimation.Rotation = this.Transform.Rotation2;
            CurrentAnimation.Scale = this.Transform.Scale2;            
        }

        /// <summary>Adiciona uma nova animação ao ator.</summary>
        /// <param name="animation">Um instância da classe Animation.</param>
        public AnimatedTextureEntity AddAnimation(Animation animation)
        {
            Animations.Add(animation);

            if (Animations.Count == 1)
            {
                ChangeAnimation(Animations[0], true);
            }

            return this;
        }

        /// <summary>Adiciona uma lista de animações ao ator.</summary>
        /// <param name="animations">A lista de animações.</param>
        public AnimatedTextureEntity AddAnimations(params Animation[] animations)
        {
            foreach (var a in animations)
            {
                AddAnimation(a);
            }

            return this;
        }

        /// <summary>Troca a animação ativa.</summary>
        /// <param name="animation">A instância da animação desejada.</param>
        /// <param name="resetAnimation">Defina True se deseja que a animação corrente será resetada.</param>
        public AnimatedTextureEntity ChangeAnimation(Animation animation, bool resetAnimation)
        {
            if (Animations.Contains(animation))
                return ChangeAnimation(animation.Name, resetAnimation);

            return this;
        }

        /// <summary>Troca a animação ativa.</summary>
        /// <param name="index">O index da animação na lista de animações.</param>
        /// <param name="resetAnimation">Defina True se deseja que a animação corrente será resetada.</param>
        public AnimatedTextureEntity ChangeAnimation(int index, bool resetAnimation)
        {
            return ChangeAnimation(Animations[index].Name, resetAnimation);
        }

        /// <summary>Troca a animação ativa.</summary>
        /// <param name="name">Nome da próxima animação.</param>
        /// <param name="resetAnimation">Defina True se deseja que a animação corrente será resetada.</param>
        public AnimatedTextureEntity ChangeAnimation(string name, bool resetAnimation)
        {
            Animation tempAnimation = GetAnimation(name);

            if (resetAnimation)
                CurrentAnimation?.Reset();

            CurrentAnimation = tempAnimation ?? throw new ArgumentException("Animação não encontrada.", nameof(name));
            SetCurrentProperties();

            return this;
        }

        /// <summary>Encontra uma animação pelo seu nome.</summary>
        /// <param name="name">O nome da animação a ser encontrada.</param>
        public Animation GetAnimation(string name)
        {
            Animation a = Animations.Find(x => x.Name.Equals(name));
            return a;
        }
    }
}