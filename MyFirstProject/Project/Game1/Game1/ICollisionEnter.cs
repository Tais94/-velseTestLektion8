﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    interface ICollisionEnter
    {
        void OnCollisionEnter(Collider other);
    }
}