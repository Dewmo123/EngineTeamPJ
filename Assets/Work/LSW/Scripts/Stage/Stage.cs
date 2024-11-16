using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage : MonoBehaviour
{
    [SerializeField] private int _sceneNum;
    public bool _isUnlock;
    public event Action<int> _onEnter;
    [SerializeField] private Sprite _unlockImage, _lockImage;

    private void Start()
    {
        gameObject.GetComponent<Image>().sprite = _isUnlock ? _unlockImage : _lockImage;
    }

    public void OnClick()
    {
        if (_isUnlock == true)
            _onEnter?.Invoke(_sceneNum);
    }

    public void ClearAllListener()
    {
        _onEnter = null;
    }
}
