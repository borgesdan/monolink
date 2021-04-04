using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoLink
{
    /// <summary>
    /// Representa um objeto de jogo que contém uma textura e seus frames.
    /// </summary>
    public sealed class SpriteSheet
    {
        //------------- CONSTRUCTOR ---------------//

        /// <summary>
        /// A textura que representa o sprite sheet.
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
        public bool HasFrames { get => Frames.Count > 0; }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="texture">A textura que representa uma imagem.</param>
        /// <param name="frames">Define os frames da textura.</param>
        public SpriteSheet(Texture2D texture, params FrameCollection[] frames)
        {
            Texture = texture;

            if (frames != null)
                Frames.AddRange(frames);
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
                this.Frames.Add(new FrameCollection(source.Frames[i]));
            }
        }

        //------------- METHODS ---------------//        
        
        /// <summary>
        /// Adiciona uma coleção de frames
        /// </summary>        
        public SpriteSheet Add(FrameCollection collection)
        {
            Frames.Add(collection);
            return this;
        }

        /// <summary>
        /// Adiciona lista de coleção de frames.
        /// </summary>
        public SpriteSheet AddRange(params FrameCollection[] collections)
        {
            if(collections != null)
                Frames.AddRange(collections);

            return this;
        }

        /// <summary>
        /// Obtém uma lista com todos os SpriteFrames da textutra.
        /// </summary>        
        public List<SpriteFrame> GetSpriteFrames()
        {
            List<SpriteFrame> f = new List<SpriteFrame>();

            for(int i = 0; i < Frames.Count; i++)
            {
                f.Add(Frames[i].Frame);
            }

            return f;
        }        
    }
}