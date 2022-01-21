﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RezepteSammelung;

public static class ColorCollection
{
    public static readonly Brush IsAvaiable = new SolidColorBrush(Colors.DarkGreen);
    public static readonly Brush IsNotAvaiable = new SolidColorBrush(Colors.Red);
}
