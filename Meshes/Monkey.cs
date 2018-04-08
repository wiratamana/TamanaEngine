using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamanaEngine.Meshes
{
    class Monkey : Mesh
    {
        public Monkey()
        {
            InitMesh("./res/Monkey.obj");
        }

        public override void Render()
        {
            base.Render();
        }
    }
}
