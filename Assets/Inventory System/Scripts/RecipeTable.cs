using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe Table", menuName = "Crafting/RecipeTable", order = 2)]
public class RecipeTable : ScriptableObject
{
    [SerializeField]
    private Recipe[] recipes;

    public Recipe GetRecipe(int id)
    {
        return recipes[id];
    }

    public int GetRecipesSize()
    {
        return recipes.Length;
    }

    public void AssignRecipeIDs()
    {
        for (int i = 0; i < recipes.Length; i++)
        {
            try
            {
                recipes[i].RecipeID = i;
            }
            catch (ItemException)
            {
                // this is fine
            }
        }
    }

}
