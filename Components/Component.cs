using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamanaEngine.Components
{
    public abstract class Component
    {
        public GameObject gameObject { get; set; }
        public Transform transform { get; set; }
        public abstract void Start();
        public abstract void Update();
        public abstract void Render();

    }
}
