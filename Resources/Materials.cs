using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using TamanaEngine;

namespace TamanaEngine
{
    public class Materials
    {
        public static Material red;
        public static Material green;
        public static Material blue;

        public static void Start()
        {
            red = new Material(new Vector3(1, 0, 0));
            green = new Material(new Vector3(0, 1, 0));
            blue = new Material(new Vector3(0, 0, 1));

            red.lightColor   = new Vector3(1, 0, 0);
            green.lightColor = new Vector3(0, 1, 0);
            blue.lightColor  = new Vector3(0, 0, 1);
        }
    }
}
