using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CraftingItem : MonoBehaviour
{
    public CursorItem cursorItem;

    public Crafting craftingTable;

    // Declared with auto-property
    public Item ItemInSlot { get; private set; }
    public int ItemCount { get; private set; }

    // scene references
    [SerializeField]
    private TMPro.TextMeshProUGUI itemCountText;

    [SerializeField]
    private Image itemIcon;

    // Update is called once per frame
    void Update()
    {
        UpdateRecipeItem();
    }

    public bool CheckRecipe(out Item iteminslot, out int itemcount)
    {
        for(int i = 0; i < craftingTable.GetRecipeTable().GetRecipesSize(); i++)
        {
            // Check for Shapeless Recipes
            if(craftingTable.GetRecipeTable().GetRecipe(i).IsShapelessCrafting)
            {
                int[] shapelessCountCheck = new int[craftingTable.GetRecipeTable().GetRecipe(i).CraftingRecipe.Length];
                bool shapelessCheck = false;
                for (int k = 0; k < craftingTable.GetRecipeTable().GetRecipe(i).CraftingRecipe.Length; k++)
                {
                    for (int j = 0; j < craftingTable.GetCraftingTable().Count; j++)
                    {
                        if (craftingTable.GetRecipeTable().GetRecipe(i).CraftingRecipe[k] == craftingTable.GetCraftingTable()[j].ItemInSlot)
                        {
                            shapelessCountCheck[k]++;
                        }
                    }
                }
                for(int k = 0; k < shapelessCountCheck.Length; k++)
                {
                    if(shapelessCountCheck[k] > 1 || shapelessCountCheck[k] <= 0)
                    {
                        shapelessCheck = false;
                        break;
                    }
                    else
                    {
                        shapelessCheck = true;
                    }
                }
                if(shapelessCheck)
                {
                    iteminslot = craftingTable.GetRecipeTable().GetRecipe(i).Output;
                    itemcount = craftingTable.GetRecipeTable().GetRecipe(i).OutputAmount;
                    return true;
                }
            }

            // Check for Non Shapeless Recipes
            else
            {
                bool check = false;
                for (int j = 0; j < craftingTable.GetCraftingTable().Count; j++)
                {
                    if(craftingTable.GetRecipeTable().GetRecipe(i).CraftingRecipe[j] == craftingTable.GetCraftingTable()[j].ItemInSlot)
                    {
                        check = true;
                    }
                    else
                    {
                        check = false;
                        break;
                    }
                }

                if(check)
                {
                    iteminslot = craftingTable.GetRecipeTable().GetRecipe(i).Output;
                    itemcount = craftingTable.GetRecipeTable().GetRecipe(i).OutputAmount;
                    return check;
                }
            }
        }
        iteminslot = null;
        itemcount = 0;
        return false;
    }

    private void UpdateRecipeItem()
    {
        Item item;
        int itemCount;
        if (CheckRecipe(out item, out itemCount))
        {
            Debug.Log("Crafting");
            ItemInSlot = item;

            ItemCount = itemCount;
            itemCountText.text = ItemCount.ToString();
            itemCountText.enabled = true;

            itemIcon.sprite = ItemInSlot.Icon;
            itemIcon.enabled = true;
        }
        else
        {
            ClearSlot();
        }

        //createdRecipe = true;
    }

    /// <summary>
    /// Sets what is contained in this slot
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public void SetContents(Item item, int count)
    {
        ItemInSlot = item;
        ItemCount = count;
    }

    /// <summary>
    /// Activate the item currently held in the slot
    /// </summary>
    public void UseItem()
    {
        if (ItemInSlot != null)
        {
            //if (ItemCount >= 1)
            //{
            //ItemInSlot.Use();
            //onItemUse.Invoke(ItemInSlot);
            //ItemCount--;
            //b_needsUpdate = true;
            //cursorItem.ItemInSlot = ItemInSlot;
            cursorItem.PickupItem(this);
            ItemPickedup();
            //}
        }
    }

    public void ItemPickedup()
    {
        Debug.Log("Item Crafted!");
        craftingTable.CraftItem();

        ItemCount = 0;
        itemCountText.text = ItemCount.ToString();

        if(ItemCount <= 0)
        {
            itemIcon.enabled = false;
            itemCountText.enabled = false;
            ItemInSlot = null;
            itemIcon.sprite = null;
        }

    }

    /// <summary>
    /// Removes everything in the item slot
    /// </summary>
    /// <returns></returns>
    public void ClearSlot()
    {
        ItemInSlot = null;
        ItemCount = 0;
        itemIcon.enabled = false;
        itemCountText.enabled = false;
        itemCountText.text = ItemCount.ToString();
    }
    /// <summary>
    /// Update visuals of slot to match items contained
    /// </summary>
    private void UpdateSlot()
    {
        //if (ItemCount == 0)
        //{
        //    ItemInSlot = null;
        //}
        //
        //if (ItemInSlot != null)
        //{
        //
        //}
        //
        //else
        //{
        //    itemIcon.gameObject.SetActive(false);
        //}
    }
}

