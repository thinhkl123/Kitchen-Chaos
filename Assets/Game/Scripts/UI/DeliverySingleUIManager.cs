using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliverySingleUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform ingredientImage;

    private void Start()
    {
        ingredientImage.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
            if (child == ingredientImage) continue;
            Destroy(child.gameObject);
        }


        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform ingredientImageTransform = Instantiate(ingredientImage, iconContainer);
            ingredientImageTransform.gameObject.SetActive(true);
            ingredientImageTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
