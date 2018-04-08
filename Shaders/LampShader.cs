using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamanaEngine.Shaders
{
    public class LampShader : Shader
    {
        public LampShader()
        {
            InitShader();
            SetNewShader("./res/light.vs", "./res/light.fs");
        }
    }
}
