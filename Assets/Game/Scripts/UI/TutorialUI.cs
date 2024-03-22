using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveUpKey;
    [SerializeField] private TextMeshProUGUI moveDownKey;
    [SerializeField] private TextMeshProUGUI moveRightKey;
    [SerializeField] private TextMeshProUGUI moveLeftKey;
    [SerializeField] private TextMeshProUGUI interactKey;
    [SerializeField] private TextMeshProUGUI altpKey;
    [SerializeField] private TextMeshProUGUI pauseKey;

    private void Start()
    {
        GameInput.instance.OnRebinding += GameInput_OnRebinding;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        UpdateVisual();
        Show();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.isCountingDownToStart())
        {
            Hide();
        }
    }

    private void GameInput_OnRebinding(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        moveUpKey.text = GameInput.instance.GetKeyBinding(GameInput.Binding.MoveUp);
        moveDownKey.text = GameInput.instance.GetKeyBinding(GameInput.Binding.MoveDown);
        moveRightKey.text = GameInput.instance.GetKeyBinding(GameInput.Binding.MoveRight);
        moveLeftKey.text = GameInput.instance.GetKeyBinding(GameInput.Binding.MoveLeft);
        interactKey.text = GameInput.instance.GetKeyBinding(GameInput.Binding.Interact);
        altpKey.text = GameInput.instance.GetKeyBinding(GameInput.Binding.InteractAlternate);
        pauseKey.text = GameInput.instance.GetKeyBinding(GameInput.Binding.Pause);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
