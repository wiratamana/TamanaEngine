using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using TamanaEngine.Components;

namespace TamanaEngine
{
    public class Camera : Component
    {
        public static Camera mainCamera;
        public Matrix4 perspectiveProjectionMatrix { get; }

        public Camera(float fov, float aspect, float zNear, float zFar)
        {
            transform = new Transform();
            perspectiveProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(fov, aspect, zNear, zFar);
        }

        public Matrix4 GetViewProjection { get { return Matrix4.LookAt(transform.position, transform.position + transform.localForward, transform.localUp); } }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            Game.view = mainCamera.GetViewProjection;
        }

        public override void Render()
        {
            
        }
    }
}
