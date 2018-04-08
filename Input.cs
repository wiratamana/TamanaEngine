using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;

namespace TamanaEngine
{
    class Input
    {
        private static bool W;
        private static bool S;
        private static bool A;
        private static bool D;
        private static bool Escape;
        private static Vector2 mousePosition { get { return new Vector2(Mouse.GetState().X, Mouse.GetState().Y); } }
        public static Vector2 lastMousePosition;
        public static Vector2 deltaMousePosition
        {
            get
            {

                return Vector2.Clamp(lastMousePosition - mousePosition, new Vector2(-10,-10), new Vector2(10,10));
            }
        }

        public static bool GetKey(Key key)
        {
            var keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(key))
            {
                if (key == Key.W)
                {
                    W = true;
                    return W;
                }
                if (key == Key.S)
                {
                    S = true;
                    return S;
                }
                if (key == Key.D)
                {
                    D = true;
                    return D;
                }
                if (key == Key.A)
                {
                    A = true;
                    return A;
                }
                if (key == Key.Escape)
                {
                    Escape = true;
                    return Escape;
                }
            }

            if(keyState.IsKeyUp(key))
            {
                if (key == Key.W)
                {
                    W = false;
                    return W;
                }
                if (key == Key.S)
                {
                    S = false;
                    return S;
                }
                if (key == Key.D)
                {
                    D = false;
                    return D;
                }
                if (key == Key.A)
                {
                    A = false;
                    return A;
                }
                if (key == Key.Escape)
                {
                    Escape = false;
                    return Escape;
                }
            }

            return false;
        }


    }
}

public enum KeyCode
{
    Q,W,S,D
}
