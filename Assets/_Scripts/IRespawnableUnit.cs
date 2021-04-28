using _Scripts.Battle;

namespace _Scripts
{
    public interface IRespawnableUnit
    {
        void InitRespawnBehaviour(UnitSpawner spawner, BattleUnit originalPrefab);
    }
}
