using System.Collections.Generic;
using _Scripts.Enums;
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

        private static TeamType[] _playableTeams = 
        {
            TeamType.TEAM_1,
            TeamType.TEAM_2,
            TeamType.TEAM_3,
            TeamType.TEAM_4,
            TeamType.TEAM_5,
            TeamType.TEAM_6,
            TeamType.TEAM_7,
            TeamType.TEAM_8,
            TeamType.TEAM_9,
            TeamType.TEAM_10,
            TeamType.TEAM_11,
            TeamType.TEAM_12
        };

        public static IReadOnlyCollection<TeamType> PlayableTeamTypes => _playableTeams;


        public static void SetActiveCursor(bool isActive, bool? isLockCursor = null)
        {
            Cursor.visible = isActive;

            if (isLockCursor.HasValue)
            {
                Cursor.lockState = isLockCursor.Value ? CursorLockMode.Locked : CursorLockMode.None;
            }
        }

        public static void CheckForNull<T>(T target, Object source = null) where T : class
        {
            if (target == null)
            {
                string msg = $"{typeof(T).FullName} is null!";
                if (source != null)
                {
                    msg += $" From source component: {source.name}!";
                }
                Debug.LogError(msg);
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
