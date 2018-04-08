using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;
using System.IO;
using System.Drawing.Imaging;

public enum ShaderList { Phong, Lamp }

namespace TamanaEngine
{
    public class Shader
    {
        private int program;
        protected Material material;

        private int vertexShader;
        private int fragmentShader;

        public virtual void InitShader()
        {
            program = GL.CreateProgram();
            if (program == 0)
                Console.WriteLine("ERROR: Failed to create shader!");
        }

        public void SetMaterial(Material material)
        {
            this.material = material;
            Console.WriteLine("SET MATERIAL");
        }

        public void Bind()
        {
            GL.AttachShader(program, fragmentShader);
            GL.AttachShader(program, vertexShader);
            GL.UseProgram(program);
        }

        public void Unbind()
        {
            GL.DetachShader(program, fragmentShader);
            GL.DetachShader(program, vertexShader);
        }

        public void AddShader(string filePath, ShaderType type)
        {
            if (type == ShaderType.FragmentShader)
            {
                GL.DeleteShader(fragmentShader);
            }

            if (type == ShaderType.VertexShader)
            {
                GL.DeleteShader(vertexShader);
            }

            int temp = GL.CreateShader(type);
            string shader = File.ReadAllText(filePath);

            GL.ShaderSource(temp, shader);
            GL.CompileShader(temp);

            if (!ShaderErrorCheck(temp))
                Console.WriteLine("ERROR : Shader compilation failed! " + type);

            if (type == ShaderType.FragmentShader)
            {
                fragmentShader = temp;
                GL.AttachShader(program, fragmentShader);
            }

            if (type == ShaderType.VertexShader)
            {
                vertexShader = temp;
                GL.AttachShader(program, vertexShader);
            }

        }

        public void CompileShader()
        {
            GL.LinkProgram(program);
            if (!ProgramLinkingErrorCheck(program))
                Console.WriteLine("ERROR: Program linking failed!");

            GL.ValidateProgram(program);
            if(!ProgramValidatingErrorCheck(program))
                Console.WriteLine("ERROR: Program validating failed!");
        }

        public void SetNewShader(string vertexShaderPath, string fragmentShaderPath)
        {
            ReCreateProgram();
            AddShader(vertexShaderPath, ShaderType.VertexShader);
            AddShader(fragmentShaderPath, ShaderType.FragmentShader);
            CompileShader();
        }

        public void ReCreateProgram()
        {
            GL.DeleteProgram(program);
            program = GL.CreateProgram();
            if (program == 0)
                Console.WriteLine("ERROR: Failed to create shader!");
        }

        public int GetUniformLocation(string uniformName)
        { return GL.GetUniformLocation(program, uniformName); }

        public void SetUniform4f(int uniformLocation, Color4 color)
        { GL.Uniform4(uniformLocation, color); }

        public void SetUniform3f(int uniformLocation, ref Vector3 vector3)
        { GL.Uniform3(uniformLocation, ref vector3); }

        public void SetUniform3f(int uniformLocation, Vector3 vector3)
        { GL.Uniform3(uniformLocation, vector3); }

        public void SetUniform3f(int uniformLocation, float x, float y, float z)
        { GL.Uniform3(uniformLocation, x,y,z); }

        public void SetUniformMatrix4f(int location, ref Matrix4 matrix)
        { GL.UniformMatrix4(location, false, ref matrix); }

        public void SetUniform1f(int uniformLocation, float value)
        { GL.Uniform1(uniformLocation, value); }

        private bool ShaderErrorCheck(int shader)
        {
            int errorChecking;
            GL.GetShader(shader, ShaderParameter.CompileStatus, out errorChecking);
            if (errorChecking == 0) return false;
            return true;
        }

        private bool ProgramLinkingErrorCheck(int program)
        {
            int errorChecking;
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out errorChecking);
            if (errorChecking == 0) return false;
            return true;
        }

        private bool ProgramValidatingErrorCheck(int program)
        {
            int errorChecking;
            GL.GetProgram(program, GetProgramParameterName.ValidateStatus, out errorChecking);
            if (errorChecking == 0) return false;
            return true;
        }
    }
}
