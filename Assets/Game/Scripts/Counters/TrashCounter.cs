using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectThorwed;
    new public static void ResetStaticData()
    {
        OnAnyObjectThorwed = null;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            OnAnyObjectThorwed?.Invoke(this, EventArgs.Empty);
            player.GetKitchenObject().DestroySelf();
        }
    }
}
