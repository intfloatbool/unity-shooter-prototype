namespace _Scripts.AI
{
    public enum AIAction : byte
    {
        NONE = 0,
        LOOKING_FOR_ENEMY = 10,
        WALK_TO_ENEMY = 15,
        ATTACK_ENEMY = 20,
        STOP = 30
    }
}
