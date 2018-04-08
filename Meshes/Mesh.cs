using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using TamanaEngine.Components;

public enum MeshList { Box, Sphere, Monkey, Rock, Godzilla }

namespace TamanaEngine
{
    public class Mesh
    {
        protected float[] vertices;

        protected uint[] indices;

        protected int VBO;
        protected int VAO;
        protected int EBO;

        public virtual void InitMesh(string fileOBJ)
        {
            indices = new uint[1];
            vertices = new float[1];
            Benchmarking.Start();
            Resources.LoadOBJ(fileOBJ, out vertices);
            Benchmarking.Stop();

            GL.DeleteVertexArrays(1, ref VAO);
            GL.DeleteBuffers(1, ref VBO);
            GL.DeleteBuffers(1, ref EBO);

            Console.WriteLine(vertices.Length);

            GL.GenVertexArrays(1, out VAO);
            GL.GenBuffers(1, out VBO);
            GL.GenBuffers(1, out EBO);

            GL.BindVertexArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * vertices.Length), vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(uint) * indices.Length), indices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            GL.BindVertexArray(0);
        }

        public virtual void Render()
        {
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 6, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, sizeof(float) * 6, 3 * sizeof(float));

            GL.BindVertexArray(VAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Length);

            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.DeleteVertexArray(VAO);
        }

        public string PrintVerticesLength()
        {
            return "LENGTH : " + vertices.Length;
        }
    }
}
