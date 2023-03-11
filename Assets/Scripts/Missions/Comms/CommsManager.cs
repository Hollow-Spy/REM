using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommsManager : MonoBehaviour
{
    [SerializeField] GameObject[] FirstRow, SecondRow, ThirdRow;
    int FirstRowPos=0, SecondRowPos=0, ThirdRowPos=3;
    IEnumerator ComputerSwitchUpCoroutine;

    [SerializeField] GameObject LightTrigger;
    private void Start()
    {
        ComputerSwitchUpCoroutine = ComputerSwitchUpNumerator();
        StartCoroutine(ComputerSwitchUpCoroutine);
    }

    IEnumerator ComputerSwitchUpNumerator()
    {
        while(true)
        {
            ThirdRow[ThirdRowPos].SetActive(false);
            FirstRowPos = (FirstRow.Length - 1) - ThirdRowPos;
            FirstRow[FirstRowPos].SetActive(true);
            yield return new WaitForSeconds(2);
            FirstRow[FirstRowPos].SetActive(false);
            SecondRowPos = FirstRowPos - 1;
            if(SecondRowPos < 0)
            {
                SecondRowPos = SecondRow.Length-1;
            }
            SecondRow[SecondRowPos].SetActive(true);
            yield return new WaitForSeconds(2);
            SecondRow[SecondRowPos].SetActive(false);

            ThirdRowPos = SecondRowPos;
            for (int i=0;i<2;i++)
            {
                ThirdRowPos++;
                if(ThirdRowPos >= ThirdRow.Length)
                {
                    ThirdRowPos = 0;
                }
            }
            ThirdRow[ThirdRowPos].SetActive(true);
            yield return new WaitForSeconds(2);


        }
    }

    public void RecievedCall()
    {
        StopCoroutine(ComputerSwitchUpCoroutine);

    }

    public void Pinging()
    {
        LightTrigger.SetActive(true);
    }    
    
}
