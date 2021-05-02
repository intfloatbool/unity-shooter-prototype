using System.Collections.Generic;
using _Scripts.Battle;
using _Scripts.Static;
using UnityEngine;

namespace _Scripts.UI
{
    public class KillEventsUI : MonoBehaviour
    {
        [SerializeField] private Transform _infoKillRoot;
        [SerializeField] private InfoKillUI _infoKillPrefab;
        [SerializeField] private UnitSpawner _unitSpawner;

        private LinkedList<InfoKillUI> _infoKillUiPool = new LinkedList<InfoKillUI>();
        
        private void OnValidate()
        {
            if (!_unitSpawner)
            {
                _unitSpawner = FindObjectOfType<UnitSpawner>();
            }
        }

        private void Awake()
        {
            GameHelper.CheckForNull(_infoKillRoot, this);
            GameHelper.CheckForNull(_infoKillPrefab, this);
            GameHelper.CheckForNull(_unitSpawner, this);

            if (_unitSpawner != null)
            {
                _unitSpawner.OnSpawned += UnitSpawnerOnSpawned;
            }
        }

        private void OnDestroy()
        {
            if (_unitSpawner != null)
            {
                _unitSpawner.OnSpawned -= UnitSpawnerOnSpawned;
            }
        }

        private void UnitSpawnerOnSpawned(BattleUnit spawnedUnit)
        {
            GameHelper.CheckForNull(spawnedUnit, this);
            var hittable = spawnedUnit.HittableObject;
            GameHelper.CheckForNull(hittable, this);

            hittable.OnDied += () =>
            {
                OnUnitDied(spawnedUnit);
            };
        }

        private void OnUnitDied(BattleUnit battleUnit)
        {
            var lastHitData = battleUnit.HittableObject.LastHitData;
            if (lastHitData.unitSource != null)
            {
                ShowEventUI(lastHitData.unitSource, battleUnit);
            }
        }

        private void ShowEventUI(BattleUnit killer, BattleUnit killedVictim)
        {
            var infoKillInstance = GetInfoKillInstance();
            infoKillInstance.Init(killer, killedVictim);
            infoKillInstance.transform.SetSiblingIndex(0);
        }

        private InfoKillUI GetInfoKillInstance()
        {
            InfoKillUI infoKillInstance = default;
            foreach (var pooled in _infoKillUiPool)
            {
                if (pooled != null && !pooled.gameObject.activeInHierarchy)
                {
                    infoKillInstance = pooled;
                    infoKillInstance.gameObject.SetActive(true);
                    break;
                }
            }

            if (infoKillInstance == null)
            {
                infoKillInstance = Instantiate(_infoKillPrefab, _infoKillRoot);
                if(!infoKillInstance.gameObject.activeInHierarchy) 
                    infoKillInstance.gameObject.SetActive(true);
            }

            return infoKillInstance;
        }
    }
}
