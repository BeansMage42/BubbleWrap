using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DLLPickUps : MonoBehaviour
{
    void Start()
    {
        String line;
        try
        {
            string path = Path.Combine(Application.streamingAssetsPath, "DropChance");
            StreamReader sr = new StreamReader(path);
            float[] numbers = new float[6];
            float sum = 0;

            for (int i = 0; i < 12; i++)
            {
                line = sr.ReadLine();
                if (i % 2 != 0)
                {
                    float n = float.Parse(line);
                    numbers[(int)(i* 0.5f)] = n;
                    sum += n;
                    print(n);
                }
            }

            PickUp.sum = sum;
            print(sum + "sum of values");
            float pre = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                pre += numbers[i];
                PickUp.dropChance[i] = pre;
                
                print(PickUp.dropChance[i]);
            }
            
            //line = sr.ReadLine();
            
            sr.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }
    }
}
