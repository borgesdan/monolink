using System;

namespace MonoLink
{
    /// <summary>Classe de auxílio para clonagem de um objeto</summary>
    public static class Clone
    {
        /// <summary>
        /// Cria um cópia de um objeto quando pertence a uma classe herdada e não é possível utilizar seu construtor de cópia.
        /// </summary>
        /// <typeparam name="A">O tipo da classe do objeto.</typeparam>
        /// <param name="args">Os argumentos do construtor de cópia.</param>
        public static A Get<A>(params object[] args)
        {
            return (A)Activator.CreateInstance(typeof(A), args);
            //return (A)Activator.CreateInstance(obj.GetType(), args);
        }
    }
}