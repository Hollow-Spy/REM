using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    [SerializeField] GameObject[] Lights;
    [SerializeField] GoalManager goalmanager;
    
    [SerializeField] Renderer WhiteLight, RedLight;

    [SerializeField] GameObject LightCharmTrigger,Mission4;

    [SerializeField] SlotShower slots;

    [SerializeField] GameObject LightGuide,LightCharmObj;

    [SerializeField] GameObject Bot;

    [SerializeField] Transform CheckPointPos;

    private void Start()
    {
        CheckpointManager checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointManager>();
        checkpoint.CheckPointPos = CheckPointPos.position;
        checkpoint.CurrentMission = 3;
        if (!Bot.activeSelf)
        {
            Bot.SetActive(true);
        }
     
        goalmanager.UpdateGoal(3);

        for(int i=0;i<Lights.Length;i++)
        {
            Lights[i].SetActive(false);
        }

        WhiteLight.sharedMaterials[2].SetColor("_EmissionColor",Color.black);
        RedLight.sharedMaterials[1].SetColor("_EmissionColor", Color.black);

        if(slots.GetItemIndex("Light Charm") == -1)
        {
            LightGuide.SetActive(true);
            StartCoroutine(WaitingColection());
        }
        
    }

    IEnumerator WaitingColection()
    {
        while(LightCharmObj.activeSelf)
        {
            yield return null;
        }
        LightGuide.SetActive(false);
    }

    public void BulbClick()
    {
        goalmanager.UpdateGoal(4);
        Mission4.SetActive(true);
        for (int i = 0; i < Lights.Length; i++)
        {
            Lights[i].SetActive(true);
        }

        ResetLights();
        LightCharmTrigger.SetActive(true);
        Invoke("CloseMission",2);
    }
    void CloseMission()
    {
        gameObject.SetActive(false);

    }



    void ResetLights()
    {
        WhiteLight.sharedMaterials[2].SetColor("_EmissionColor", Color.white * 4);
        RedLight.sharedMaterials[1].SetColor("_EmissionColor", Color.red * 3);
    }

    private void OnDisable()
    {
        ResetLights();
    }

}
