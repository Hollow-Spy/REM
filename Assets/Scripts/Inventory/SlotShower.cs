using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotShower : MonoBehaviour
{
    [SerializeField] Transform[] Slots;
    [SerializeField] Transform[] Items;
    [SerializeField] string[] ItemsStored;
    [SerializeField] bool[] Usable;
    [SerializeField] bool[] SlotTaken;
    [SerializeField] Animator AddedAnim;
    [SerializeField] GameObject AddedItemSound;
    int ItemsCollectedCout=0;
    [SerializeField] Transform selectedSlot;
    [SerializeField] FontTyper BigTextBox;
   
    void Start()
    {
       // AddItem("Security Code");
        selectedSlot.name = "0";
        
        
      


    }



    public void SetItemUsable(string name, bool state) //set a certain item from the list usable
    {
       for(int i=0;i<ItemsStored.Length;i++)
        {
            if(name == ItemsStored[i])
            {
                Usable[i] = state;
                return;
            }
        }
    }


     bool ValidSelection(int index) //  if theres an item currently selected / check if the index of the item from the list is usuable
    {
        if (index == -1)
        {
            BigTextBox.Type("No Valid Item Selected.");
            return false;
        }
     
        return true;
    }

    public void Inspect() 
    {

        int index = GetItemSelected();
        if(ValidSelection(index))
        {
            Items[index].SendMessage("Inspect");
        }
      
       
    }

    public void UseItem() 
    {
      
        int index = GetItemSelected();
        int indexpos = GetItemIndex(GetItemName(int.Parse(selectedSlot.name)));
      
        if (ValidSelection(index))
        {
            if (!Usable[indexpos])
            {
                BigTextBox.Type("There's no use for this item right now.");
                return;
            }
            Items[index].SendMessage("Use");
        }     
    }

    int GetItemSelected() //get the id of the selected item itself according to the list of items (transforms)
    {
        string _itemname = GetItemName(int.Parse(selectedSlot.name));

        for (int i = 0; i < Items.Length; i++)
        {
            if (_itemname == Items[i].name)
            {
                return i;
              
            }
        }
        return -1;
    }

    int GetItem(string name) //get the id of the item itself according to the list of items (transforms)
    {
     

        for (int i = 0; i < Items.Length; i++)
        {
            if (name == Items[i].name)
            {
                return i;

            }
        }
        return -1;
    }

    string GetItemName(int index) //get the name of current selected item
    {
        return ItemsStored[index];
    }

    int GetItemIndex(string name) // get item id according to list of enabled items
    {
        for(int i=0;i<ItemsCollectedCout;i++)
        {
            if(ItemsStored[i] == name)
            {
                return i;
            }
        }
        return -1;
    }

   public void AddItem(string name) // add item by name
    {
        for(int i=0;i<Items.Length;i++)
        {
            if(name == Items[i].name)
            {
               
                for (int a = 0; a < Slots.Length; a++)
                {
                    if(SlotTaken[a] == false)
                    {
                        Items[i].gameObject.SetActive(true);
                        // Items[i].position = Slots[a].position;
                        SlotTaken[a] = true;
                        ItemsStored[a] = Items[i].name;
                        ItemsCollectedCout++;

                         a = Slots.Length;
                        AddedAnim.Play("Updated");
                        Instantiate(AddedItemSound, transform.position, Quaternion.identity);
                    }
                }
                i = Items.Length;
                
            }
        }
    }

    public void OrganizeSlots()
    {
        for (int i = 0; i < ItemsCollectedCout; i++)
        {
            if (GetItemIndex(ItemsStored[i]) != -1)
            {
               
                Items[GetItem(ItemsStored[i])].position = Slots[i].position;
            }
        }
    }

    
}
