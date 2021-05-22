// Danilo Borges Santos, 2020.

using System;

namespace MonoLink
{
    /// <summary>
    /// Estrura que enumera as direções de um jogo 2D.
    /// </summary>
    [Flags]    
    public enum Direction2D : byte
    {
        Up          = 2,
        Down        = 4,
        Right       = 8,
        Left        = 16
    }
}