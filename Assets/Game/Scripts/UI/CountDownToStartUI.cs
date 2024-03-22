using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownToStartUI : MonoBehaviour
{
    private const string NUMBER_POP_UP = "NumberPopUp";

    [SerializeField] private TextMeshProUGUI countDownText;

    private Animator animator;
    private int currentCountDownNumber;
    private int previousCountDownNumber;

    private void Start()
    {
        animator = GetComponent<Animator>();
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void Update()
    {
        currentCountDownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountDownTime());
        countDownText.text = currentCountDownNumber.ToString();

        if (currentCountDownNumber !=  previousCountDownNumber)
        {
            previousCountDownNumber = currentCountDownNumber;
            animator.SetTrigger(NUMBER_POP_UP);
            SoundManager.instance.PlayCountDownSound();
        }
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.isCountingDownToStart())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
