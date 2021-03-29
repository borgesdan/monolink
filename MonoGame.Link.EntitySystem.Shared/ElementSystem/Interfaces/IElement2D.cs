using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Common;

namespace Microsoft.Xna.Framework.EntitySystem
{
    /// <summary>
    /// Implementa funcionalidades para um componente 2D.
    /// </summary>
    public interface IElement2D : IColorable, IBoundsable
    {
        /// <summary>Obtém ou define a origem do desenho e da rotação.</summary>
        Vector2 Origin { get; set; }
        /// <summary>Obtém ou define o LayerDepth no desenho do componente, de 0f a 1f, se necessário.</summary>
        float LayerDepth { get; set; }
        /// <summary>Obtém ou define os efeitos de espelhamento.</summary>
        SpriteEffects Effects { get; set; }
    }
}