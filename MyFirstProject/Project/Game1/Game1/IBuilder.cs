﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    interface IBuilder
    {
        GameObject GetResult();

        void Buildpart(Vector2 position);
    }
}
