using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.IO;

using TamanaEngine.Components;

namespace TamanaEngine
{
    class Game : GameWindow
    {
        public static GameObject camera;
        public static Matrix4 projection;
        public static Matrix4 view;

        public static List<GameObject> gameObjects;

        public Game() : base(1280, // initial width
        720, // initial height
        GraphicsMode.Default,
        "Tamana Engine",  // initial title
        GameWindowFlags.Default,
        DisplayDevice.Default,
        4, // OpenGL major version
        0, // OpenGL minor version
        GraphicsContextFlags.ForwardCompatible)
        {

        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.FrontFace(FrontFaceDirection.Cw);
            GL.CullFace(CullFaceMode.Front);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.FramebufferSrgb);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            Materials.Start();

            CursorVisible = false;
            gameObjects = new List<GameObject>();

            camera = new GameObject("Camera");
            Camera.mainCamera = (camera.AddComponent<Camera>(new Camera(1, 16f / 9f, 0.1f, 1000f)) as Camera);
            projection = Camera.mainCamera.perspectiveProjectionMatrix;
            view = Camera.mainCamera.GetViewProjection;

            gameObjects.Add(new GameObject());
            gameObjects.Add(new GameObject());
            gameObjects.Add(new GameObject());
            gameObjects.Add(new GameObject());
            gameObjects.Add(new GameObject());
            gameObjects[0].AddComponent<MeshRenderer>(new MeshRenderer());
            gameObjects[1].AddComponent<MeshRenderer>(new MeshRenderer());
            gameObjects[2].AddComponent<MeshRenderer>(new MeshRenderer());
            gameObjects[3].AddComponent<MeshRenderer>(new MeshRenderer());
            gameObjects[4].AddComponent<MeshRenderer>(new MeshRenderer());

            gameObjects[4].GetComponent<MeshRenderer>().material = Materials.green;
            gameObjects[4].GetComponent<MeshRenderer>().material.SetMeshRenderer(gameObjects[4].GetComponent<MeshRenderer>());
            gameObjects[3].GetComponent<MeshRenderer>().material = Materials.blue;
            gameObjects[3].GetComponent<MeshRenderer>().material.SetMeshRenderer(gameObjects[3].GetComponent<MeshRenderer>());

            gameObjects[0].transform.position = new Vector3(0, -10, 0);
            gameObjects[1].transform.position = new Vector3(-5, -1, -1);
            gameObjects[2].transform.position = new Vector3(0, 10, 0);
            gameObjects[3].transform.position = new Vector3(0, 0, 10);
            gameObjects[4].transform.position = new Vector3(0, 0, -10);

            gameObjects[0].GetComponent<MeshRenderer>().SetMesh(MeshList.Sphere);
            gameObjects[0].GetComponent<MeshRenderer>().SetShader(ShaderList.Phong);
                                                       
            gameObjects[1].GetComponent<MeshRenderer>().SetMesh(MeshList.Monkey);
            gameObjects[1].GetComponent<MeshRenderer>().SetShader(ShaderList.Lamp);
                                                       
            gameObjects[2].GetComponent<MeshRenderer>().SetMesh(MeshList.Rock);
            gameObjects[2].GetComponent<MeshRenderer>().SetShader(ShaderList.Phong);
                                                       
            gameObjects[3].GetComponent<MeshRenderer>().SetMesh(MeshList.Monkey);
            gameObjects[3].GetComponent<MeshRenderer>().SetShader(ShaderList.Phong);
                                                       
            gameObjects[4].GetComponent<MeshRenderer>().SetMesh(MeshList.Monkey);
            gameObjects[4].GetComponent<MeshRenderer>().SetShader(ShaderList.Phong);

            for (int x = 0; x < gameObjects.Count; x++)
            {
                gameObjects[x].Start();
            }
        }

        float a;
        //Material red = new Material();
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Input.GetKey(OpenTK.Input.Key.W))
            {
                camera.transform.position += camera.transform.localForward * 3 * (float)UpdatePeriod;
            }
            
            if (Input.GetKey(OpenTK.Input.Key.S))
            {
                camera.transform.position += -camera.transform.localForward * 3 * (float)UpdatePeriod;
            }

            if (Input.GetKey(OpenTK.Input.Key.D))
            {
                camera.transform.position += camera.transform.localRight * 3 * (float)UpdatePeriod;
            }

            if (Input.GetKey(OpenTK.Input.Key.A))
            {
                camera.transform.position += -camera.transform.localRight * 3 * (float)UpdatePeriod;
            }

            if (Input.deltaMousePosition.X != 0 || Input.deltaMousePosition.Y != 0)
            {
                Vector3 eulerAngle = camera.transform.eulerAngle;
                eulerAngle.Y += Input.deltaMousePosition.X * (float)UpdatePeriod;
                eulerAngle.Z += Input.deltaMousePosition.Y * (float)UpdatePeriod;
                camera.transform.eulerAngle = Vector3.Lerp(camera.transform.eulerAngle, eulerAngle, (float)UpdatePeriod * 35);
                camera.transform.rotation = Quaternion.FromEulerAngles(camera.transform.eulerAngle);
            }

            if (Input.GetKey(OpenTK.Input.Key.Escape))
                Exit();

            if (Focused)
            {
                ResetMouse();
            }

            gameObjects[3].GetComponent<MeshRenderer>().material.lightColor = new Vector3(1f, .2f, 1f);
            Console.WriteLine(gameObjects[3].GetComponent<MeshRenderer>().material.lightColor);

            camera.Update();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"(Vsync: {VSync}) FPS: {1f / e.Time:0}";

            Color4 backColor;
            backColor.A = 1.0f;
            backColor.R = 0.01f;
            backColor.G = 0.01f;
            backColor.B = 0.01f;
            GL.ClearColor(backColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            a += (float)RenderTime;

            for (int x = gameObjects.Count - 1; x >= 0; x--)
            {
                gameObjects[x].Update();
            }

            SwapBuffers();
        }

        private void ResetMouse()
        {
            OpenTK.Input.Mouse.SetPosition(Bounds.Left + Bounds.Width / 2, Bounds.Top + Bounds.Height / 2);
            Input.lastMousePosition = new Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y);
        }
    }
}
