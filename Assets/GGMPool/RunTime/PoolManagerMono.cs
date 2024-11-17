using UnityEngine;

namespace GGMPool
{
    public class PoolManagerMono : MonoBehaviour
    {
        [SerializeField] private PoolManagerSO _poolManager;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _poolManager.InitializePool(transform);
        }
    }
}
