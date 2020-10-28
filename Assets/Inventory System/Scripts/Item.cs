using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ItemException : System.Exception
{
    public ItemException(string message) : base(message)
    {

    }
}


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    [SerializeField]
    private int itemID;

    public int ItemID
    {
        get { return itemID; }
        set {
            itemID = value;
            throw new ItemException("You never should have come here!");
        }
    }

    [SerializeField]
    private new string name = "item";
    public string Name
    {
        get { return name; }
        private set { }
    }

    [SerializeField]
    [TextArea]
    private string description = "this is an item";
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


    [SerializeField]
    private Sprite icon;
    public Sprite Icon
    {
        get { return icon; }
        private set { }
    }


    public void Use()
    {
        //Debug.Log("Used item " + name);
    }
    public void Pickup()
    {
        //Debug.Log("Pickup item " + name);
    }
    
    public void Drop()
    {
        //Debug.Log("Drop item " + name);
    }
}
