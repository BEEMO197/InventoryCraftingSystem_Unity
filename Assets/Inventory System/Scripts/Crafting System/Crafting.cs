using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Assertions;
using System.Linq; // For finding all gameObjects with name
using UnityEditorInternal.Profiling.Memory.Experimental;
using System.Runtime.InteropServices.WindowsRuntime;

public class Crafting : MonoBehaviour, ISaveHandler
{
    [Tooltip("Reference to the master recipe table")]
    [SerializeField]
    private RecipeTable masterRecipeTable;

    [Tooltip("The object which will hold Item Slots as its direct children")]
    [SerializeField]
    private GameObject craftingPanel;

    [Tooltip("List size determines how many slots there will be. Contents will replaced by copies of the first element")]
    [SerializeField]
    private List<ItemSlot> craftingTable;

    /// <summary>
    /// Private key used for saving with playerprefs
    /// </summary>
    private string saveKey = "";

    // Start is called before the first frame update
    void Start()
    {
        InitItemSlots();
        InitSaveInfo();
    }

    public List<ItemSlot> GetCraftingTable()
    {
        return craftingTable;
    }

    public RecipeTable GetRecipeTable()
    {
        return masterRecipeTable;
    }
    public void CraftItem()
    {
        foreach(ItemSlot slot in craftingTable)
        {
            slot.craftingItem();
        }
    }

    private void InitItemSlots()
    {
        Assert.IsTrue(craftingTable.Count > 0, "itemSlots was empty");
        Assert.IsNotNull(craftingTable[0], "Inventory is missing a prefab for itemSlots. Add it as the first element of its itemSlot list");

        // init item slots
        for (int i = 1; i < craftingTable.Count; i++)
        {
            GameObject newObject = Instantiate(craftingTable[0].gameObject, craftingPanel.transform);
            ItemSlot newSlot = newObject.GetComponent<ItemSlot>();
            craftingTable[i] = newSlot;
        }

        foreach (ItemSlot item in craftingTable)
        {
            item.onItemUse.AddListener(OnItemUsed);
        }
    }
    private void InitSaveInfo()
    {
        // init save info
        //assert only one object with the same name, or else we can have key collisions on PlayerPrefs
        Assert.AreEqual(
            Resources.FindObjectsOfTypeAll(typeof(GameObject)).Where(gameObArg => gameObArg.name == gameObject.name).Count(),
            1,
            "More than one gameObject have the same name, therefore there may be save key collisions in PlayerPrefs"
            );

        // set a key to use for saving/loading
        saveKey = gameObject.name + this.GetType().Name;

        //Subscribe to save events on start so we are listening
        GameSaver.OnLoad.AddListener(OnLoad);
        GameSaver.OnSave.AddListener(OnSave);
    }

    private void OnDestroy()
    {
        // Remove listeners on destroy
        GameSaver.OnLoad.RemoveListener(OnLoad);
        GameSaver.OnSave.RemoveListener(OnSave);

        foreach (ItemSlot item in craftingTable)
        {
            item.onItemUse.RemoveListener(OnItemUsed);
        }
    }

    //////// Event callbacks ////////

    void OnItemUsed(Item itemUsed)
    {
        // Debug.Log("Inventory: item used of category " + itemUsed.category);
    }

    public void OnSave()
    {
        //Make empty string
        //For each item slot
        //Get its current item
        //If there is an item, write its id, and its count to the end of the string
        //If there is not an item, write -1 and 0 

        //File format:
        //ID,Count,ID,Count,ID,Count

        string saveStr = "";

        foreach (ItemSlot itemSlot in craftingTable)
        {
            int id = -1;
            int count = 0;

            if (itemSlot.HasItem())
            {
                id = itemSlot.ItemInSlot.ItemID;
                count = itemSlot.ItemCount;
            }

            saveStr += id.ToString() + ',' + count.ToString() + ',';
        }

        PlayerPrefs.SetString(saveKey, saveStr);
    }

    public void OnLoad()
    {
        //Get save string
        //Split save string
        //For each itemSlot, grab a pair of entried (ID, count) and parse them to int
        //If ID is -1, replace itemSlot's item with null
        //Otherwise, replace itemSlot with the corresponding item from the itemTable, and set its count to the parsed count

        string loadedData = PlayerPrefs.GetString(saveKey, "");

        Debug.Log(loadedData);

        char[] delimiters = new char[] { ',' };
        string[] splitData = loadedData.Split(delimiters);

        for (int i = 0; i < craftingTable.Count; i++)
        {
            int dataIdx = i * 2;

            int id = int.Parse(splitData[dataIdx]);
            int count = int.Parse(splitData[dataIdx + 1]);

            if (id < 0)
            {
                craftingTable[i].ClearSlot();
            }
            else
            {
                craftingTable[i].SetContents(masterRecipeTable.GetRecipe(id).Output, count);
            }
        }
    }
}
