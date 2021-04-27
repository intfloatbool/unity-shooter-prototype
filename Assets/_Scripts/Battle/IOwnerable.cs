namespace _Scripts.Battle
{
    public interface IOwnerable 
    {
        BattleUnit Owner { get; }

        void InitOwner(BattleUnit owner);
    }
}
