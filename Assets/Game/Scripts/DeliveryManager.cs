using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeSuccess;
    public static DeliveryManager instance
    {
        get;
        private set;
    }


    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTime;
    private float spawnRecipeTimeMax = 4f;
    private int waitingRecipeMax = 4;
    private int recipesDeliveredAmount;

    private void Awake()
    {
        instance = this;
        waitingRecipeSOList = new List<RecipeSO> ();
    }

    private void Start()
    {
        spawnRecipeTime = spawnRecipeTimeMax;
        recipesDeliveredAmount = 0;
    }

    private void Update()
    {
        spawnRecipeTime -= Time.deltaTime;
        if (spawnRecipeTime <= 0f )
        {
            spawnRecipeTime = spawnRecipeTimeMax;

            if (GameManager.Instance.isPlaying() && waitingRecipeSOList.Count < waitingRecipeMax )
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                //Debug.Log(waitingRecipeSO.recipeName);
            }  
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i=0; i<waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //Has same amount of ingerdient
                bool plateMatchRecipe = true;
                bool matchIngredient = false;
                foreach (KitchenObjectSO waitingKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (waitingKitchenObjectSO == plateKitchenObjectSO)
                        {
                            //Match Ingredient;
                            matchIngredient = true;
                            break;
                        }
                    }

                    if (!matchIngredient)
                    {
                        plateMatchRecipe = false;
                    }
                }

                if (plateMatchRecipe)
                {
                    //Debug.Log("Correct Recipe");
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    waitingRecipeSOList.RemoveAt(i);
                    recipesDeliveredAmount++;
                    return;
                }
            }
        }

        //Debug.Log("Wrong Recipe");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetRecipesDeliveredAmount()
    {
        return recipesDeliveredAmount;
    }
}
