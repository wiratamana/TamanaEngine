using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using TamanaEngine.Components;

namespace TamanaEngine
{
    public class GameObject
    {
        private List<Component> components;
        private string name;
        public Transform transform { get; private set; }

        public GameObject()
        {
            name = "GameObject";
            transform = new Transform();
            components = new List<Component>();
            AddComponent<Transform>(transform);

            components.Add(transform);
        }

        public GameObject(string name)
        {
            this.name = name;
            transform = new Transform();
            components = new List<Component>();
            AddComponent<Transform>(transform);

            components.Add(transform);
        }

        public void Start()
        {
            for(int x = 0; x < components.Count; x++)
            {
                components[x].gameObject = this;
                components[x].transform = transform;
                components[x].Start();
            }
        }

        public void Update()
        {
            for (int x = 0; x < components.Count; x++)
            {
                components[x].Update();
                components[x].Render();
            }
        }

        public T GetComponent<T>() where T : Component
        {
            for(int x = 0; x < components.Count; x++)
            {
                if (components[x] is T)
                {
                    return components[x] as T;
                }
            }
            return null;
        }
        //public T AddComponent<T>() where T : Component
        //{
        //    for (int x = 0; x < components.Count; x++)
        //    {
        //        if(components[x] is T)
        //        {
        //            return default(T);
        //        }
        //    }
        //
        //    components.Add(T);
        //}

        public Component AddComponent<T>(Component component)
        {
            for(int x = 0; x < components.Count; x++)
            {
                if(components[x] is T)
                {
                    components[x].gameObject = this;
                    components[x].transform = transform;
                    return components[x];
                }
            }

            components.Add(component);
            component.gameObject = this;
            component.transform = transform;
            return component;
        }
    }
}
