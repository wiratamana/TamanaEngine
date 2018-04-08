using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using OpenTK;

namespace TamanaEngine
{
    public class Resources
    {
        public static BitmapData LoadBitmapData(string filePath)
        {
            Bitmap data = new Bitmap(filePath);
            return data.LockBits(new Rectangle(0, 0, data.Width, data.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        }

        public static void LoadOBJ(string filePath, out float[] vertexArray)
        {
            vertexArray = new float[1];

            if (!File.Exists(filePath))
            {
                Console.WriteLine("ERROR: Target file is not exist.");
                return;
            }

            if(filePath[filePath.Length - 1] != 'j' && filePath[filePath.Length - 2] != 'b' && filePath[filePath.Length - 3] != 'o')
            {
                Console.WriteLine("ERROR: Target file is not OBJ file.");
                return;
            }

            List<string> readedText = new List<string>();
            readedText.AddRange(File.ReadAllLines(filePath));

            List<Vector3> vertices = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<Vector3> vertexIndices = new List<Vector3>();
            List<Vector3> textureIndices = new List<Vector3>();
            List<Vector3> normalIndices = new List<Vector3>();

            for (int x = 0; x < readedText.Count; x++)
            {
                string token1 = Convert.ToString(readedText[x][0]);
                string token2 = Convert.ToString(readedText[x][1]);
                string fusion = token1 + token2;
                bool ver = (fusion[0] == 'v' && fusion[1] == ' ');
                bool nor = (fusion[0] == 'v' && fusion[1] == 'n');
                bool ind = (fusion[0] == 'f' && fusion[1] == ' ');
                if (!ver && !nor && !ind)
                {
                    continue;
                }

                if(ver)
                {
                    int found = 0;
                    float X = 0, Y = 0, Z = 0;
                    string currentGet = "";
                    for(int i = 2; i < readedText[x].Length; i++)
                    {
                        if (readedText[x][i] != ' ' && i != readedText[x].Length - 1)
                        {
                            currentGet += Convert.ToString(readedText[x][i]);
                        }
                        else
                        {
                            if (found == 0)
                                X = (float)Convert.ToDouble(currentGet);
                            if (found == 1)
                                Y = (float)Convert.ToDouble(currentGet);
                            if (found == 2)
                            {
                                Z = (float)Convert.ToDouble(currentGet);
                                //Console.WriteLine("INFO: Added vertex to vertices. Added vertex value is new Vector3(" + X + ", " + Y + ", " + Z + ").");
                                vertices.Add(new Vector3(X, Y, Z));
                            }

                            currentGet = string.Empty;
                            found++;
                        }
                    }
                }

                if (nor)
                {
                    int found = 0;
                    float X = 0, Y = 0, Z = 0;
                    string currentGet = "";
                    for (int i = 3; i < readedText[x].Length; i++)
                    {
                        if (readedText[x][i] != ' ' && i != readedText[x].Length - 1)
                        {
                            currentGet += Convert.ToString(readedText[x][i]);
                        }
                        else
                        {
                            if (found == 0)
                                X = (float)Convert.ToDouble(currentGet);
                            if (found == 1)
                                Y = (float)Convert.ToDouble(currentGet);
                            if (found == 2)
                            {
                                Z = (float)Convert.ToDouble(currentGet);
                                //Console.WriteLine("INFO: Added normal to normals. Added normal value is new Vector3(" + X + ", " + Y + ", " + Z + ").");
                                normals.Add(new Vector3(X, Y, Z));
                            }

                            currentGet = string.Empty;
                            found++;
                        }
                    }
                }

                if (ind)
                {
                    int found = 0;
                    int stride = 0;
                    float VertexX = 0, VertexY = 0, VertexZ = 0;
                    float TextureX = 0, TextureY = 0, TextureZ = 0;
                    float NormalX = 0, NormalY = 0, NormalZ = 0;
                    string currentGetVertex = "";
                    string currentGetTexture = "";
                    string currentGetNormal = "";
                    for (int i = 2; i < readedText[x].Length; i++)
                    {
                        if(found == 0)
                        {
                            if(stride == 0)
                            {
                                currentGetVertex += readedText[x][i];
                                if (readedText[x][i + 1] != '/')
                                    continue;
                                VertexX = (float)Convert.ToDouble(currentGetVertex);
                                currentGetVertex = "";
                                stride = 1;
                                i += 1;
                                continue;
                            }
                            if (stride == 1)
                            {
                                currentGetTexture += readedText[x][i];
                                if (readedText[x][i + 1] != '/')
                                    continue;
                                //Console.WriteLine("CURRENT GET VERTEX = " + currentGetTexture);
                                TextureX = (float)Convert.ToDouble(currentGetTexture);
                                currentGetTexture = "";
                                stride = 2;
                                i += 1;
                                continue;
                            }
                            if (stride == 2)
                            {
                                currentGetNormal += readedText[x][i];
                                if (readedText[x][i + 1] != ' ')
                                    continue;
                                NormalX = (float)Convert.ToDouble(currentGetNormal);
                                currentGetNormal = "";
                                stride = 0;
                                found = 1;
                                i += 1;
                                continue;
                            }
                        }

                        if (found == 1)
                        {
                            if (stride == 0)
                            {
                                currentGetVertex += readedText[x][i];
                                if (readedText[x][i + 1] != '/')
                                    continue;
                                VertexY = (float)Convert.ToDouble(currentGetVertex);
                                currentGetVertex = "";
                                stride = 1;
                                i += 1;
                                continue;
                            }
                            if (stride == 1)
                            {
                                currentGetTexture += readedText[x][i];
                                if (readedText[x][i + 1] != '/')
                                    continue;
                                TextureY = (float)Convert.ToDouble(currentGetTexture);
                                currentGetTexture = "";
                                stride = 2;
                                i += 1;
                                continue;
                            }
                            if (stride == 2)
                            {
                                currentGetNormal += readedText[x][i];
                                if (readedText[x][i + 1] != ' ')
                                    continue;
                                NormalY = (float)Convert.ToDouble(currentGetNormal);
                                currentGetNormal = "";
                                stride = 0;
                                found = 2;
                                i += 1;
                                continue;
                            }
                        }

                        if (found == 2)
                        {
                            if (stride == 0)
                            {
                                currentGetVertex += readedText[x][i];
                                if (readedText[x][i + 1] != '/')
                                    continue;
                                VertexZ = (float)Convert.ToDouble(currentGetVertex);
                                currentGetVertex = "";
                                stride = 1;
                                i += 1;
                                continue;
                            }
                            if (stride == 1)
                            {
                                currentGetTexture += readedText[x][i];
                                if (readedText[x][i + 1] != '/')
                                    continue;
                                TextureZ = (float)Convert.ToDouble(currentGetTexture);
                                currentGetTexture = "";
                                stride = 2;
                                i += 1;
                                continue;
                            }
                            if (stride == 2)
                            {
                                currentGetNormal += readedText[x][i];
                                if (i + 1 != readedText[x].Length)
                                    continue;
                                NormalZ = (float)Convert.ToDouble(currentGetNormal);
                                currentGetNormal = "";
                                stride = 0;
                                found = 0;

                                vertexIndices.Add(new Vector3(VertexX, VertexY, VertexZ));
                                textureIndices.Add(new Vector3(TextureX, TextureY, TextureZ));
                                normalIndices.Add(new Vector3(NormalX, NormalY, NormalZ));
                                //Console.WriteLine(" NORMAL INDICES = " + NormalX + ", " + NormalY + ", " + NormalZ);
                                continue;
                            }
                        }
                    }
                }
            }

            List<float> FinalVertex = new List<float>();
            for(int i = 0; i < vertexIndices.Count; i++)
            {
                //if(i == 0 || i == 1)
                //{
                //    Console.WriteLine("VERTEX = " + vertices[0] + ", " + vertices[1] + ", " + vertices[2]);
                //    Console.WriteLine("NORMAL = " + vertices[6] + ", " + vertices[7] + ", " + vertices[8]);
                //}
                Vector3 vertA = vertices[(int)vertexIndices[i].X - 1];
                Vector3 vertB = vertices[(int)vertexIndices[i].Y - 1];
                Vector3 vertC = vertices[(int)vertexIndices[i].Z - 1];
                //Console.WriteLine((int)normalIndices[i].X - 1);
                Vector3 normA = normals[(int)normalIndices[i].X - 1];
                Vector3 normB = normals[(int)normalIndices[i].Y - 1];
                Vector3 normC = normals[(int)normalIndices[i].Z - 1];
                FinalVertex.Add(vertA.X);
                FinalVertex.Add(vertA.Y);
                FinalVertex.Add(vertA.Z);
                FinalVertex.Add(normA.X);
                FinalVertex.Add(normA.Y);
                FinalVertex.Add(normA.Z);

                FinalVertex.Add(vertB.X);
                FinalVertex.Add(vertB.Y);
                FinalVertex.Add(vertB.Z);
                FinalVertex.Add(normB.X);
                FinalVertex.Add(normB.Y);
                FinalVertex.Add(normB.Z);

                FinalVertex.Add(vertC.X);
                FinalVertex.Add(vertC.Y);
                FinalVertex.Add(vertC.Z);
                FinalVertex.Add(normC.X);
                FinalVertex.Add(normC.Y);
                FinalVertex.Add(normC.Z);
            }

            vertexArray = FinalVertex.ToArray();
        }
    }
}
