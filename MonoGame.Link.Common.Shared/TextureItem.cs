using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Microsoft.Xna.Framework.Common
{
    /// <summary>
    /// Representa um objeto que contém uma textura e as definções de frames.
    /// </summary>
    public sealed class TextureItem : IDisposable
    {
        bool disposed = false;

        //------------- CONSTRUCTOR ---------------//

        /// <summary>
        /// A textura que representa uma imagem.
        /// </summary>
        public Texture2D Texture { get; private set; } = null;
        /// <summary>
        /// Obtém ou define os frames da textura. Frames são retângulos que informam partes específicas (ou regiões)
        /// da textura que representam ações, tiles ou outro tipo de informação.
        /// </summary>
        public List<FrameCollection> Frames { get; set; } = new List<FrameCollection>();  
        
        /// <summary>
        /// Obtém true caso a lista de Frames não esteja vazia.
        /// </summary>
        public bool ContainsFrames { get => Frames.Count > 0; }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        public TextureItem()
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="game">A instância da classe Game.</param>
        /// <param name="texturePath">O caminho da textura na pasta Content.</param>
        /// <param name="frames">A lista de frames.</param>
        public TextureItem(Game game, string texturePath, params FrameCollection[] frames) : this(game.Content.Load<Texture2D>(texturePath), frames)
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="texture">A textura que representa uma imagem.</param>
        /// <param name="frames">Define os frames da textura.</param>
        public TextureItem(Texture2D texture, params FrameCollection[] frames)
        {
            Texture = texture;

            if (frames != null)
                Frames.AddRange(frames);
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>        
        public TextureItem(TextureItem source)
        {
            Texture = source.Texture;

            for (int i = 0; i < source.Frames.Count; i++)
            {
                this.Frames.Add(new FrameCollection(source.Frames[i]));
            }
        }

        //------------- METHODS ---------------//

        /// <summary>
        /// Define a textura a ser utilizada.
        /// </summary>        
        public TextureItem Set(Texture2D texture)
        {
            Texture = texture;
            return this;
        }
        
        public TextureItem Add(FrameCollection collection)
        {
            Frames.Add(collection);
            return this;
        }

        /// <summary>
        /// Adiciona objetos FrameCollection.
        /// </summary>
        public TextureItem AddRange(params FrameCollection[] collections)
        {
            if(collections != null)
                Frames.AddRange(collections);

            return this;
        }

        /// <summary>
        /// Obtém uma lista com todos os TexturesFrames da textutra.
        /// </summary>        
        public List<TextureFrame> GetTextureFrames()
        {
            List<TextureFrame> f = new List<TextureFrame>();

            for(int i = 0; i < Frames.Count; i++)
            {
                f.Add(Frames[i].Frame);
            }

            return f;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Texture = null;
                }

                disposed = true;
            }
        }
    }
}