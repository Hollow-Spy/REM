using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScroller : MonoBehaviour
{
    public bool DialogueDone;
    [SerializeField] List<FontTyper> Prompts;
    [SerializeField] List<string> Texts;
    [SerializeField] GameObject[] Batches;
    [SerializeField] RectTransform ScrollPos;

    [SerializeField] int[] TextEnd;
    [SerializeField] DialogueLoadScene Loader;
    [SerializeField] float InitialTime;
    private void Start()
    {


        TextEnd = new int[Batches.Length];
        int EndIndex = 0;
       for(int i=0;i< Batches.Length;i++)
        {
            TextMeshProUGUI[] temp;
            
            temp = Batches[i].GetComponentsInChildren<TextMeshProUGUI>();

            int a;
            for(a=0;a<temp.Length;a++)
            {
                Texts.Add(temp[a].text);
                Prompts.Add(temp[a].gameObject.GetComponent<FontTyper>());
                temp[a].text = "";
            }
            TextEnd[EndIndex] = Texts.Count;
            EndIndex++;
            Batches[i].SetActive(false);

        }
       
        StartCoroutine(StartChat());
    }

    IEnumerator StartChat()
    {
        int CurrentBatch = 0;
        int CurrentText=0;
        yield return new WaitForSeconds(InitialTime);
        while(CurrentBatch < Batches.Length)
        {
            Batches[CurrentBatch].SetActive(true);
            for(int i=CurrentText;i<TextEnd[CurrentBatch];i++)
            {
                Prompts[CurrentText].Type(Texts[CurrentText]);

                while (Prompts[CurrentText].IsTpying())
                {
                    yield return null;
                }
                CurrentText++;

            }


            RectTransform tempform = Batches[CurrentBatch].GetComponent<RectTransform>();
            while(tempform.position != ScrollPos.position)
            {
                yield return null;
                tempform.position = Vector2.MoveTowards(tempform.position, ScrollPos.position, 220 * Time.deltaTime);
              

            }
            Batches[CurrentBatch].SetActive(false);



            CurrentBatch++;
        }

       

        if(Loader)
        {
            Loader.loadScene();
        }
    }

    
}
