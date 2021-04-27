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
    }
}
