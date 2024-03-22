using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
       if (!HasKitchenObject())
       {
            //Don't have kitchen object above
            if (player.HasKitchenObject())
            {
                //Player has kitchen object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player doesn't have object
            }
       }
       else
       {
            //Have kitchen object above
            if (!player.HasKitchenObject())
            {
                //Player doesn't have object
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                //Player bring a object
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player hold a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
       }
    }
}
