using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace TamanaEngine.Shaders
{
    public class PhongShader : Shader
    {
        public PhongShader()
        {
            InitShader();
            SetNewShader("./res/base.vs", "./res/base.fs");
        }
    }
}
