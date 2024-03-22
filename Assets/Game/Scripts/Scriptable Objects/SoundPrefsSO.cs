using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoundPrefsSO : ScriptableObject
{
    public AudioClip[] chopArray;
    public AudioClip[] deliveyFailedArray;
    public AudioClip[] deliverySuccessArray;
    public AudioClip[] footStepArray;
    public AudioClip[] objectDropArray;
    public AudioClip[] objectPickUpArray;
    public AudioClip stoveCounter;
    public AudioClip[] trashCounter;
    public AudioClip[] warningArray;
    public AudioClip[] footStep;
}
