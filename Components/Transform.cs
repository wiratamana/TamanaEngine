using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using TamanaEngine.Components;

namespace TamanaEngine
{
    public class Transform : Component
    {
        public Vector3 position { get; set; }
        public Quaternion rotation { get; set; }
        public Vector3 eulerAngle
        { get; set; }
        public Vector3 scale;
        public Vector3 localForward
        {
            get
            {
                Vector4 a = rotation * new Vector4(0, 0, -1, 0);
                return a.Xyz;
            }
        }
        public Vector3 localUp
        {
            get
            {
                Vector4 a = rotation * new Vector4(0, 1, 0, 0);
                return a.Xyz;
            }
        }
        public Vector3 localRight
        {
            get
            {
                Vector4 a = rotation * new Vector4(1, 0, 0, 0);
                return a.Xyz;
            }
        }

        public Matrix4 model;

        public Transform()
        {
            position = new Vector3(0, 0, 0);
            eulerAngle = new Vector3(0, 0, 0);
            scale = new Vector3(1, 1, 1);
            rotation = Quaternion.FromEulerAngles(eulerAngle);
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            
        }

        public override void Render()
        {
            
        }
    }
}
