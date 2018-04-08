using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using TamanaEngine.Components;
using TamanaEngine.Shaders;

namespace TamanaEngine
{
    public class Material
    {
        private MeshRenderer meshRenderer;
        private Shader shader;
        private Mesh mesh;

        public Vector3 lightColor;
        public Vector3 lightDiffuse;
        public Vector3 lightAmbient;
        public Vector3 lightSpecular;
        public Vector3 materialAmbient;
        public Vector3 materialDiffuse;
        public Vector3 materialSpecular;
        public Vector3 materialColor;
        public float materialShininess;

        public Material(MeshRenderer meshRenderer)
        {
            this.meshRenderer = meshRenderer;
            shader = meshRenderer.shader;
            mesh = meshRenderer.mesh;

            lightColor = new Vector3(.2f, 1f, .2f);
            lightDiffuse = lightColor * new Vector3(.5f, .5f, .5f);
            lightAmbient = lightDiffuse * new Vector3(.2f, .2f, .2f);
            lightSpecular = new Vector3(1f, 1f, 1f);

            materialAmbient = new Vector3(1f, .5f, .3f);
            materialDiffuse = new Vector3(1f, .5f, .3f);
            materialSpecular = new Vector3(.5f, .5f, .5f);
            materialShininess = 32f;

            shader.ReCreateProgram();
            shader.AddShader("./res/base.vs", ShaderType.VertexShader);
            shader.AddShader("./res/base.fs", ShaderType.FragmentShader);
            shader.CompileShader();

            shader.SetUniform3f(shader.GetUniformLocation("light.position"), Game.gameObjects[1].transform.position);
            shader.SetUniform3f(shader.GetUniformLocation("view.pos"), Game.camera.transform.position);
        }

        public Material(Vector3 color)
        {
            lightColor = color;
            lightDiffuse = lightColor * new Vector3(.5f, .5f, .5f);
            lightAmbient = lightDiffuse * new Vector3(.2f, .2f, .2f);
            lightSpecular = new Vector3(1f, 1f, 1f);

            materialAmbient = new Vector3(1f, .5f, .3f);
            materialDiffuse = new Vector3(1f, .5f, .3f);
            materialSpecular = new Vector3(.5f, .5f, .5f);
            materialShininess = 32f;
        }

        public void SetMeshRenderer(MeshRenderer meshRenderer)
        {
            this.meshRenderer = meshRenderer;
            shader = meshRenderer.shader;
            mesh = meshRenderer.mesh;

            shader.ReCreateProgram();
            shader.AddShader("./res/base.vs", ShaderType.VertexShader);
            shader.AddShader("./res/base.fs", ShaderType.FragmentShader);
            shader.CompileShader();
        }

        public void Update()
        {
            shader.SetUniform3f(shader.GetUniformLocation("light.ambient"), lightAmbient);
            shader.SetUniform3f(shader.GetUniformLocation("light.diffuse"), lightDiffuse);
            shader.SetUniform3f(shader.GetUniformLocation("light.specular"), lightSpecular);

            shader.SetUniform3f(shader.GetUniformLocation("material.ambient"), materialAmbient);
            shader.SetUniform3f(shader.GetUniformLocation("material.diffuse"), materialDiffuse);
            shader.SetUniform3f(shader.GetUniformLocation("material.specular"), materialSpecular);
            shader.SetUniform1f(shader.GetUniformLocation("material.shininess"), materialShininess);
        }

        public void ChangeColor(Vector3 color)
        {
            lightColor = color;
            shader.ReCreateProgram();
            shader.AddShader("./res/base.vs", ShaderType.VertexShader);
            shader.AddShader("./res/base.fs", ShaderType.FragmentShader);
            shader.CompileShader();
        }
    }
}
