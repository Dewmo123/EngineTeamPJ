using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscButtons : MoveUIManager
{
    private Button[] _buttons;
    [SerializeField] private AudioMixer mixer;
    private Dictionary<string, Slider> _sliders = new Dictionary<string, Slider>();
    public UnityEvent onSceneLoad;
    private void Awake()
    {
        Slider[] sliders = GetComponentsInChildren<Slider>();
        sliders.ToList().ForEach(item =>
        {
            _sliders.Add(item.gameObject.name, item);
            float val;
            mixer.GetFloat(item.gameObject.name, out val);
            item.value = val;
        });
        _buttons = GetComponentsInChildren<Button>();
        DisableButtons();
    }
    #region Set Button ans Move
    private void DisableButtons()
    {
        _buttons.ToList().ForEach(item => item.interactable = false);
        _sliders.ToList().ForEach(item => item.Value.interactable = false);
    }
    private void EnableButtons()
    {
        _buttons.ToList().ForEach(item => item.interactable = true);
        _sliders.ToList().ForEach(item => item.Value.interactable = true);
    }
    public override void MoveOriginPos(bool noTime = false)
    {
        base.MoveOriginPos(noTime);
        DisableButtons();
    }
    public override void MoveTargetPos(bool noTime = false)
    {
        base.MoveTargetPos(noTime);
        EnableButtons();
    }
    #endregion
    #region Button function
    public void Lobby()
    {
        Time.timeScale = 1;
        onSceneLoad?.Invoke();
        SceneManager.LoadScene(0);
    }
    public void ResetButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        Application.Quit();
    }
    #endregion
    public void HandleSliderValueChanged(string name)
    {
        mixer.SetFloat(name, _sliders[name].value);
    }
}
