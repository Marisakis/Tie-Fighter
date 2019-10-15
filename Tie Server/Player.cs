﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tie_Server.GameObjects;

namespace Tie_Server
{
    public class Player
    {
        int id { get; set; }
        string name { get; set; }
        int score { get; set; }

        Crosshair crosshair { get; set; }

    }
}
