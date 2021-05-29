using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace MonoLink
{
    /// <summary>
    /// Representa um objeto de jogo que contém uma textura e seus frames.
    /// </summary>
    public sealed class SpriteSheet : IDisposable
    {
        bool disposed = false;

        /// <summary>
        /// A textura que representa o sprite sheet.
        /// </summary>
        public Texture2D Texture { get; private set; } = null;
        /// <summary>
        /// Obtém ou define os frames da textura. Frames são retângulos que informam partes específicas (ou regiões)
        /// da textura que representam ações, tiles ou outro tipo de informação.
        /// </summary>
        public List<SpriteFrame> Frames { get; set; } = new List<SpriteFrame>();          
        /// <summary>
        /// Obtém true caso a lista de Frames não esteja vazia.
        /// </summary>
        public bool HasFrames { get => Frames.Count > 0; }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="texture">A textura que representa uma imagem.</param>
        /// <param name="frames">Define os frames da textura.</param>
        public SpriteSheet(Texture2D texture, List<SpriteFrame> frames)
        {
            Texture = texture;
            Frames = frames;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>        
        public SpriteSheet(SpriteSheet source)
        {
            Texture = source.Texture;

            for (int i = 0; i < source.Frames.Count; i++)
            {
                this.Frames.Add(source.Frames[i]);
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if(disposing)
            {
                Texture.Dispose();                
            }

            disposed = true;
        }
    }
}