using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscButtons : MoveUIManager
{
    private Button[] _buttons;
    private Dictionary<string, Slider> _sliders;
    [SerializeField] private AudioMixerGroup MASTER;
    [SerializeField] private AudioMixerGroup SFX;
    [SerializeField] private AudioMixerGroup BGM;
    private void Awake()
    {
        Slider[] sliders = GetComponentsInChildren<Slider>();
        sliders.ToList().ForEach(item => _sliders.Add(item.gameObject.name, item));
        _buttons = GetComponentsInChildren<Button>();
        DisableButtons();
    }
    #region Set Button ans Move
    private void DisableButtons()
    {
        _buttons.ToList().ForEach(item => item.interactable = false);
    }
    private void EnableButtons()
    {
        _buttons.ToList().ForEach(item => item.interactable = true);
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
}
