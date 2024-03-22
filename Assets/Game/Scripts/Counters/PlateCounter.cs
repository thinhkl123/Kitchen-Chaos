using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateDestroyed;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;

    private int spawnPlateAmount;
    private int spawnPlateAmountMax = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;

        if (GameManager.Instance.isPlaying() && spawnPlateTimer > spawnPlateTimerMax )
        {
            spawnPlateTimer = 0f;

            if (spawnPlateAmount < spawnPlateAmountMax )
            {
                spawnPlateAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty); 
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //Player doesn't bring anything
            if (spawnPlateAmount > 0)
            {
                //At least one plate
                spawnPlateAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlateDestroyed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
