using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance
    { get; private set; }

    [SerializeField] private SoundPrefsSO soundPrefsSO;

    private float volumeAmount;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DeliveryManager.instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        DeliveryManager.instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickUpSomething += Player_OnPickUpSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectThorwed += TrashCounter_OnAnyObjectThorwed;

        volumeAmount = 1f;
    }

    private void TrashCounter_OnAnyObjectThorwed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(soundPrefsSO.trashCounter, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(soundPrefsSO.objectDropArray, baseCounter.transform.position);
    }

    private void Player_OnPickUpSomething(object sender, System.EventArgs e)
    {
        Player player = Player.Instance;
        PlaySound(soundPrefsSO.objectPickUpArray, player.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(soundPrefsSO.chopArray, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(soundPrefsSO.deliverySuccessArray, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(soundPrefsSO.deliveyFailedArray, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClip, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClip[Random.Range(0, audioClip.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume * volumeAmount);
    }

    public void PlayFootStepSound(Vector3 position,  float volume) 
    {
        PlaySound(soundPrefsSO.footStep, position, volume);
    }

    public void PlayCountDownSound()
    {
        PlaySound(soundPrefsSO.warningArray, Vector3.zero);
    }

    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(soundPrefsSO.warningArray, position);
    }

    public void IncreaseVolume()
    {
        volumeAmount += 0.1f;
    }

    public void DecreaseVolume()
    {
        volumeAmount -= 0.1f;
    }

    public float GetVolume()
    {
        return volumeAmount;
    }
}
