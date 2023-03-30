using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityManager : MonoBehaviour
{
    [SerializeField] GameObject Prompt,OkConfirmation ,LockdownText,UnlockedText,SearchTriggers,Disengage,Room8Lights,RoomDoorInteract;
    
    CanvasScreenSplit Splitter;
    [SerializeField] DoorOpener[] LockedDoors;
    [SerializeField] GameObject DoorOpenSound;

    [SerializeField] Transform CheckPointPos;
    bool once = true;
    private void Start()
    {
        Splitter = FindObjectOfType<CanvasScreenSplit>();
        for(int i=0;i<LockedDoors.Length;i++)
        {
            LockedDoors[i].ForceCloseNBlock();
        }


    }
    public void EnablePrompt()
    {
        Prompt.SetActive(true);
        SearchTriggers.SetActive(true);
        Disengage.gameObject.layer = 0;
        //enable triggers for search
    }
    public void EnableOkayButton()
    {
        Prompt.gameObject.layer = 0;
        OkConfirmation.SetActive(true);
    }

  
    public void OkayPressed()
    {
        UnlockedText.SetActive(true);
        LockdownText.SetActive(false);
        Prompt.SetActive(false);
        OkConfirmation.SetActive(false);
        Disengage.SetActive(false);

        StartCoroutine(ShowResults());
    }
    IEnumerator ShowResults()
    {
        yield return new WaitForSeconds(2f);
        Splitter.PopUp(44);

        RoomDoorInteract.SetActive(false);
        Room8Lights.SetActive(true);
        while(Splitter.GetisCensored(44))
        {
            yield return null;
        }
        yield return new WaitForSeconds(3.5f);
        for (int i = 0; i < LockedDoors.Length; i++)
        {
            LockedDoors[i].Unlock();
        }
        Instantiate(DoorOpenSound, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        Splitter.PopUpText("Doors are unlocked",44);


        yield return new WaitForSeconds(2f);
        Splitter.PopDown(44);
        Room8Lights.SetActive(false);
        RoomDoorInteract.SetActive(true);

        FindObjectOfType<GoalManager>().UpdateGoal(1);

       
        if(once)
        {
            CheckpointManager checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointManager>();
            checkpoint.CheckPointPos = CheckPointPos.position;
            checkpoint.CurrentMission = 1;
            once = false;
        }
       
    }



}
