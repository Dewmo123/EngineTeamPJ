using GGMPool;
using UnityEngine;

public class AgentVFX : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private bool _canGenerateAfterImage;
    [SerializeField] private float _generateTerm;
    [SerializeField] private PoolManagerSO _poolManager;
    [SerializeField] private PoolTypeSO _afterImage;

    private float _currentTime;
    private Player _player;

    public void Initialize(Player player)
    {
        _player = player;
    }

    public void ToggleAfterImage(bool value)
    {
        _canGenerateAfterImage = value;
    }
    private void Update()
    {
        if (!_canGenerateAfterImage) return;
        _currentTime += Time.deltaTime;
        if (_currentTime > _generateTerm)
        {
            _currentTime = 0;
            AfterImage img = _poolManager.Pop(_afterImage) as AfterImage;
            Sprite sprite = _player.rendererCompo.sprite;
            bool isFlip = !_player.IsFacingRight();
            img.SetAfterImage(sprite, isFlip, transform.position, 0.2f);
        }
    }
}
