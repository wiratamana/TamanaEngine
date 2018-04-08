using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace TamanaEngine.Tools
{
    public class Tool
    {
        public static Vector3 toEulerAngle(Quaternion q)
        {
            // roll (x-axis rotation)
            double x;
            double sinr = +2.0 * (q.W * q.X + q.Y * q.Z);
            double cosr = +1.0 - 2.0 * (q.X * q.X + q.Y * q.Y);
            x = Math.Atan2(sinr, cosr);

            // pitch (y-axis rotation)
            double y;
            double sinp = +2.0 * (q.W * q.Y - q.Z * q.X);
            y = Math.Asin(sinp);

            // yaw (z-axis rotation)
            double z;
            double siny = +2.0 * (q.W * q.Z + q.X * q.Y);
            double cosy = +1.0 - 2.0 * (q.Y * q.Y + q.Z * q.Z);
            z = Math.Atan2(siny, cosy);

            return new Vector3((float)x, (float)y, (float)z);
        }
    }
}