using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;

namespace PaintProject
{
    class Extension
    {
        public static string fileName = "source.drw";

  
        public static void SaveFile(String source)
        {
            try
            {
                
                StreamWriter sw = new StreamWriter(fileName);
               
                sw.WriteLine(source);
                
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        public static String ReadFile(String path)
        {
            String line = "";
            String source = "";
            try
            {
                
                StreamReader sr = new StreamReader(path);
                
                line = sr.ReadLine();
                Debug.WriteLine(line);
                while (line != null)
                {
                    
                    source += line;
                    Console.WriteLine(source);
                    source += Environment.NewLine;
                    line = sr.ReadLine();
                }

                
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
            return source;
        }
    }
}
