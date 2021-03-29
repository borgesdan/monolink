using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Common;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.EntitySystem
{
    /// <summary>
    /// Widget que efetua a troca de cor progressiva de um componente associado.
    /// </summary>
    public sealed class ColorModificationJob : Job, IBuildable<ColorModificationJob>
    {
        float elapsedTime = 0;
        IColorable colorableComponent = null;

        /// <summary>Obtém ou define a cor final a ser alcançada. </summary>
        public Color Final { get; set; } = Color.White;
        /// <summary>Obtém ou define o tempo em milisegundos a ser atrasada para cada mudança de cor (default = 0).</summary>
        public float Delay { get; set; } = 0;
        /// <summary>Encapsula um método que será chamado quando a cor final for alcançada.</summary>        
        public event Action OnFinish;

        /// <summary>
        /// Inicializa uma nova instância do widget.
        /// </summary>
        public ColorModificationJob() : base()
        {
        }               

        /// <summary>
        /// Inicializa uma nova instância do componente como cópia de outra instância.
        /// </summary>
        /// <param name="source">O componente a ser copiado.</param>
        public ColorModificationJob(ColorModificationJob source) : base(source)
        {
            elapsedTime = source.elapsedTime;
            colorableComponent = null;
            Final = source.Final;
            Delay = source.Delay;
            OnFinish = source.OnFinish;
        }     
        
        /// <summary>
        /// Define os atributos deste widget.
        /// </summary>
        /// <param name="setFuncion">Define as propriedades do widget através de uma função.</param>
        public ColorModificationJob Build(Action<ColorModificationJob> setFuncion)
        {
            setFuncion(this);
            return this;
        }        

        protected override void OnUpdate(GameTime gameTime)
        {
            if (Owner is IColorable colorable)
                colorableComponent = colorable;

            if (colorableComponent != null)
            {
                elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

                if (elapsedTime > Delay)
                {
                    Color active = colorableComponent.Color;
                    Color final = Final;

                    if (!final.Equals(active))
                    {
                        if (active.R > final.R)
                            active.R--;
                        else if (active.R < final.R)
                            active.R++;

                        if (active.G > final.G)
                            active.G--;
                        else if (active.G < final.G)
                            active.G++;

                        if (active.B > final.B)
                            active.B--;
                        else if (active.B < final.B)
                            active.B++;

                        if (active.A > final.A)
                            active.A--;
                        else if (active.A < final.A)
                            active.A++;

                        colorableComponent.Color = active;
                    }

                    if (active == final)
                    {
                        OnFinish?.Invoke();
                    }

                    elapsedTime = 0;
                }
            }
        }
    }
}
