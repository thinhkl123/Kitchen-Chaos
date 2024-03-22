using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveWarningBarUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";

    private Animator animator;

    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        animator = GetComponent<Animator>();
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        animator.SetBool(IS_FLASHING, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool show = stoveCounter.isFried() && e.progressNormalized >= burnShowProgressAmount;

        animator.SetBool(IS_FLASHING, show);
    }
}
