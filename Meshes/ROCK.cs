using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamanaEngine.Meshes
{
    class ROCK: Mesh
    {
        public ROCK()
        {
            InitMesh("./res/ROCK.obj");
        }

        public override void Render()
        {
            base.Render();
        }
    }
}
