using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Xna.Framework.EntitySystem
{
    public interface IBuildable<T>
    {
        T Build(Action<T> func);
    }
}
