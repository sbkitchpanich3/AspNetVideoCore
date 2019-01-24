using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreVideo.Models
{
    public enum Genres
    {
        None,
        Animated,
        Horror,
        Comedy,
        Romance,
        Action
    }
}

// This is not the same as an "entity."  The model is responsible for letting the genres be a set of choices in this case, rather than having the user explicitly define every genre.
