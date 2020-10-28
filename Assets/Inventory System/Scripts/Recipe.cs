using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RecipeException : System.Exception
{
    public RecipeException(string message) : base(message)
    {

    }
}


[CreateAssetMenu(fileName = "Recipe", menuName = "Crafting/Recipe", order = 1)]
public class Recipe : ScriptableObject
{

    [SerializeField]
    private int recipeID;

    public int RecipeID
    {
        get { return recipeID; }
        set
        {
            recipeID = value;
            throw new RecipeException("You never should have come here!");
        }
    }

    [SerializeField]
    [TextArea]
    private string description = "this is a recipe";
    public string Description
    {
        get { return description; }
        private set { }
    }

    [SerializeField]
    private string category = "misc";
    public string Category
    {
        get { return category; }
        private set { }
    }

    [Tooltip("The Recipe can be made anywhere in the crafting Grid")]
    [SerializeField]
    private bool isShapelessCrafting;
    public bool IsShapelessCrafting
    {
        get { return isShapelessCrafting; }
        private set { }
    }

    [Tooltip("The Items that are used for crafting, Set this value to 9 to use all of crafting squares, any more will be ignored, elements 0, 1, 2 are the Top Row; 3, 4, 5 Middle Row 6, 7, 8 Bottom Row. If Shapeless crafting is enabled, You can make the crafting Recipe size any number below 9")]
    [SerializeField]
    private Item[] craftingRecipe;
    public Item[] CraftingRecipe
    {
        get { return craftingRecipe; }
        private set { }
    }

    [Tooltip("The output Item, that crafting the item makes")]
    [SerializeField]
    private Item output;
    public Item Output
    {
        get { return output; }
        private set { }
    }

    [Tooltip("The amount of items the crafted item will give you")]
    [SerializeField]
    private int outputAmount = 1;
    public int OutputAmount
    {
        get { return outputAmount; }
        private set { }
    }

    public void Use()
    {
        //Debug.Log("Used item " + name);
    }
}
