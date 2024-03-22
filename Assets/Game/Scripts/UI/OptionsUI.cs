using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI instance {  get; private set; }

    [SerializeField] private Button soundEffectIncreaseButton;
    [SerializeField] private Button soundEffectDecreaseButton;
    [SerializeField] private Button musicIncreaseButton;
    [SerializeField] private Button musicDecreaseButton;
    [SerializeField] private TextMeshProUGUI soundEffectVolumeText;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Transform pressToRebindingUI;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;

        soundEffectIncreaseButton.onClick.AddListener(() =>
        {
            SoundManager.instance.IncreaseVolume();
            UpdateVisual();
        });
        soundEffectDecreaseButton.onClick.AddListener(() =>
        {
            SoundManager.instance.DecreaseVolume();
            UpdateVisual();
        });
        musicIncreaseButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.IncreaseVolume();
            UpdateVisual();
        });
        musicDecreaseButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.DecreaseVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            GamePauseUI.instance.Show();
            Hide();
        });

        moveUpButton.onClick.AddListener(() =>
        {
            ReBinding(GameInput.Binding.MoveUp);
        });
        moveDownButton.onClick.AddListener(() =>
        {
            ReBinding(GameInput.Binding.MoveDown);
        });
        moveLeftButton.onClick.AddListener(() =>
        {
            ReBinding(GameInput.Binding.MoveLeft);
        });
        moveRightButton.onClick.AddListener(() =>
        {
            ReBinding(GameInput.Binding.MoveRight);
        });
        interactButton.onClick.AddListener(() =>
        {
            ReBinding(GameInput.Binding.Interact);
        });
        interactAltButton.onClick.AddListener(() =>
        {
            ReBinding(GameInput.Binding.InteractAlternate);
        });
        pauseButton.onClick.AddListener(() =>
        {
            ReBinding(GameInput.Binding.Pause);
        });

        UpdateVisual();
        HidePressToRebindingUI();
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    /*
    private void Update()
    {
        UpdateVisual();
    }
    */

    private void UpdateVisual()
    {
        soundEffectVolumeText.text = Mathf.Ceil((SoundManager.instance.GetVolume()*10f)).ToString();
        musicVolumeText.text = Mathf.Ceil((MusicManager.Instance.GetVolume() * 10f)).ToString();
        moveUpText.text = GameInput.instance.GetKeyBinding(GameInput.Binding.MoveUp);
        moveDownText.text = GameInput.instance.GetKeyBinding(GameInput.Binding.MoveDown);
        moveLeftText.text = GameInput.instance.GetKeyBinding(GameInput.Binding.MoveLeft);
        moveRightText.text = GameInput.instance.GetKeyBinding(GameInput.Binding.MoveRight);
        interactText.text = GameInput.instance.GetKeyBinding(GameInput.Binding.Interact);
        interactAltText.text = GameInput.instance.GetKeyBinding(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.instance.GetKeyBinding(GameInput.Binding.Pause);

    }    
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void HidePressToRebindingUI()
    {
        pressToRebindingUI.gameObject.SetActive(false);
    }

    private void ShowPressToRebindingUI()
    {
        pressToRebindingUI.gameObject.SetActive(true);
    }

    private void ReBinding(GameInput.Binding binding)
    {
        ShowPressToRebindingUI();
        GameInput.instance.ReBinding(binding, () =>
        {
            HidePressToRebindingUI();
            UpdateVisual();
        });
    }
}
