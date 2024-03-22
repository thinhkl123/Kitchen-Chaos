using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    private float volumeAmount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        volumeAmount = 1.0f;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        audioSource.volume = volumeAmount;    
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
