using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MonoFantasy.Logic.Map
{
    class CollisionLayer
    {
        public static void test()
        {
            StreamWriter sw = null;
            StreamReader sr = null;
            int[,] collisions = new int[41, 23];
            try 
            {
                sw = new StreamWriter(@"C:\Users\Benjamin Piro\Desktop\writing\test.txt");
                for (int i = 0; i < 23; i++)
                {
                    for (int j = 0; j < 41; j++)
                    {
                        if ((i >= 10 && i <= 15) && (j >= 20 && j <= 25))
                        {
                            sw.Write("1");
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
