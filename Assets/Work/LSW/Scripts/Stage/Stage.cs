using System;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    [SerializeField] private int _sceneNum;
    public NotifyValue<bool> _isUnlock = new NotifyValue<bool>(false);
    public event Action<int> _onEnter;
    private Image _image;
    [SerializeField] private Sprite _unlockImage, _lockImage;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        _image.sprite = _isUnlock.Value ? _unlockImage : _lockImage;
        _isUnlock.OnValueChanged += HandleStageLock;
    }

    private void HandleStageLock(bool prev, bool next)
    {
        if (next)
            _image.sprite = _unlockImage;
    }

    public void OnClick()
    {
        if (_isUnlock.Value == true)
            _onEnter?.Invoke(_sceneNum);
    }

    public void ClearAllListener()
    {
        _onEnter = null;
    }
}
