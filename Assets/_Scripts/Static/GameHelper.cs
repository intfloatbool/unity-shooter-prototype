using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Static
{
    public static class GameHelper
    {
        public static class InputConstants
        {
            public const int LEFT_MOUSE = 0;
            public const int RIGHT_MOUSE = 1;
            public const int MIDDLE_MOUSE = 2;
        }

        public static class NullObjects
        {
            private static GameObject nullGameObject;

            public static GameObject NullGameObject
            {
                get
                {
                    if (nullGameObject == null)
                    {
                        nullGameObject = new GameObject("NULL GAMEOBJ");
                    }

                    return nullGameObject;
                }
            }
        }

        // *** EXTENSIONS ***
        private static System.Random rng = new System.Random();  
        public static void Shuffle<T>(this IList<T> list)  
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = rng.Next(n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }
    }
}
