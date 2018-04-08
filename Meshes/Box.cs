using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace TamanaEngine.Meshes
{
    public class Box : Mesh
    {
        public Box() 
        {
            InitMesh("./res/BOX.obj");
        }

        public override void Render()
        {
            base.Render();
        }
    }
}
