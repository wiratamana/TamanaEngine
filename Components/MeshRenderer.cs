using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamanaEngine.Shaders;
using TamanaEngine.Meshes;
using OpenTK;

namespace TamanaEngine.Components
{
    public class MeshRenderer : Component
    {
        public Shader shader { get; set; }
        public Mesh mesh { get; set; }
        public Material material { get; set; }
        public bool isLight;

        public MeshRenderer()
        {
            mesh = new Mesh();
            shader = new Shader();
            material = Materials.red;
            material.SetMeshRenderer(this);

            SetShader(ShaderList.Phong);
            SetMesh(MeshList.Box);
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            
        }

        public override void Render()
        {            
            shader.Bind();
            if (!isLight)
                material.Update();

            transform.model = Matrix4.CreateFromQuaternion(transform.rotation);
            transform.model *= Matrix4.CreateTranslation(transform.position);
            transform.model *= Matrix4.CreateScale(transform.scale);

            shader.SetUniformMatrix4f(shader.GetUniformLocation("model"), ref transform.model);
            shader.SetUniformMatrix4f(shader.GetUniformLocation("view"), ref Game.view);
            shader.SetUniformMatrix4f(shader.GetUniformLocation("projection"), ref Game.projection);

            mesh.Render();
            shader.Unbind();
        }

        public void SetShader(ShaderList shaderList)
        {
            if (shaderList == ShaderList.Lamp)
            {
                shader = new LampShader();
                isLight = true;
            }

            if (shaderList == ShaderList.Phong)
            {
                shader = new PhongShader();
                isLight = false;
            }
        }

        public void SetMesh(MeshList meshList)
        {
            if (meshList == MeshList.Box)
            {
                mesh.InitMesh("./res/BOX.obj");
            }

            if (meshList == MeshList.Sphere)
            {
                mesh.InitMesh("./res/SPHERE.obj");
                Console.WriteLine("./res/SPHERE.obj " + mesh.PrintVerticesLength());
            }

            if (meshList == MeshList.Monkey)
            {
                mesh.InitMesh("./res/MONKEY.obj");
                Console.WriteLine("./res/MONKEY.obj " + mesh.PrintVerticesLength());
            }

            if (meshList == MeshList.Rock)
            {
                mesh.InitMesh("./res/ROCK.obj");
                Console.WriteLine("./res/ROCK.obj " + mesh.PrintVerticesLength());
            }

            if (meshList == MeshList.Godzilla)
            {
                mesh.InitMesh("./res/GODZILLA.obj");
                Console.WriteLine("./res/GODZILLA.obj " + mesh.PrintVerticesLength());
            }
        }
    }
}
