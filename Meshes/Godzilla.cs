using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamanaEngine.Meshes
{
    public class Godzilla : Mesh
    {
        public Godzilla()
        {
            InitMesh("./res/GODZILLA.obj");
        }

        public override void Render()
        {
            base.Render();
        }
    }
}
