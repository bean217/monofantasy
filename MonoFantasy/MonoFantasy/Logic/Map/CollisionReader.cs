using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MonoFantasy.Logic.Map
{
    public static class CollisionReader
    {
        // Reads a collision file of 0s and 1s to determine collideable blocks between player and world
        public static Collision[,] read(string collisionFile)
        {
            StreamWriter sw = new StreamWriter("base.txt");
            sw.WriteLine("Test");
            sw.Close();

            Collision[,] collisions = new Collision[ConfigInfo.CHUNK_WIDTH, ConfigInfo.CHUNK_HEIGHT];
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(collisionFile);
                string line;
                int x = 0;
                int y = 0;
                while((line = sr.ReadLine()) != null)
                {
                    var strLine = line.Split(' ');
                    foreach (string box in strLine)
                    {
                        Collision collision;
                        switch (box)
                        {
                            case "0":
                                collision = Collision.NO_COLLIDE;
                                break;
                            case "1":
                                collision = Collision.COLLIDE;
                                break;
                            case "2":
                                collision = Collision.DOOR;
                                break;
                            default:
                                collision = Collision.NONE;
                                break;
                        }
                        collisions[x, y] = collision;
                        x = (x + 1) % ConfigInfo.CHUNK_WIDTH;
                    }
                    y = (y + 1) % ConfigInfo.CHUNK_HEIGHT;
                }
            } catch (Exception e)
            {
                Console.WriteLine($"ERROR: {e.Message}");
                throw e;
            } finally
            {
                if (sr != null)
                    sr.Close();
            }
            return collisions;
        }

        public static void test()
        {
            StreamWriter sw = null;
            StreamReader sr = null;
            int[,] collisions = new int[41, 23];
            try 
            {
                sw = new StreamWriter(@"D:\MonoGame\MonoFantasy\monofantasy\MonoFantasy\MonoFantasy\saves\save1\world\chunk0x0\layer0\tiles.txt");
                for (int i = 0; i < 23; i++)
                {
                    for (int j = 0; j < 41; j++)
                    {
                        if ((i >= 10 && i <= 15) && (j >= 20 && j <= 25))
                        {
                            sw.Write("0");
                        } else
                        {
                            sw.Write("0");
                        }
                        if (j != 40)
                        {
                            sw.Write(" ");
                        } else
                        {
                            sw.WriteLine("");
                        }
                    }
                }
                sw.Close();
                sr = new StreamReader(@"C:\Users\Benjamin Piro\Desktop\writing\test.txt");
                string line;
                int x = 0;
                int y = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    var strLine = line.Split(' ');
                    foreach (string _str in strLine) 
                    {
                        collisions[x, y] = Int16.Parse(_str);
                        x = (x + 1) % 41;
                    }
                    y = (y + 1) % 23;
                }
                for (int i = 0; i < 23; i++)
                {
                    Console.Write("[");
                    for (int j = 0; j < 41; j++)
                    {
                        Console.Write(collisions[j, i]);
                        if (j != 40)
                            Console.Write(", ");
                    }
                    Console.WriteLine("]");
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            } finally
            {
                //if (sw != null)
                //    sw.Close();
                if (sr != null)
                    sr.Close();
            }
        }
    }
}
