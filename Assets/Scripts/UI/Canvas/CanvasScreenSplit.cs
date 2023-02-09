using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScreenSplit : MonoBehaviour
{
    [SerializeField] RawImage MainCamImg;
    // [SerializeField] [] PopUpAnimators;
    [SerializeField] RenderTexture[] MainRenderTextures, PopRenderTextures,PopRenderTexturesStretched;

    IEnumerator RighStretcher, RightDeStretcher,BottomStretcher,BottomDeStretcher;

    [SerializeField] GameObject RightCensor, BottomCensor;
    [SerializeField] RawImage[] PopUpImgs;
    public List<int> PossiblePops = new List<int> {0,1,2,3 };
   
    [SerializeField] Camera[] RoomCameras; //main ones
    int CurrentEnabled;

    [SerializeField] FontTyper[] TextTypers;


    bool RightStretch,CurrentlyStretchingR,CurrentlyDeStretchingR; //og 0 //stretch -351
    bool BottomStretch, CurrentlyStretchingB, CurrentlyDeStretchingB; //og 0 // stretch 193
    
    public void SwitchMainCam(int num)
    {
        RoomCameras[CurrentEnabled].gameObject.SetActive(false);
        RoomCameras[num].gameObject.SetActive(true);
        MainCamImg.texture = MainRenderTextures[num];
        CurrentEnabled = num;
    }
    private void Update()
    {
        //Debug.Log(MainCamImg.rectTransform.rect.xMax);
    }
    private void Start()
    {
       // StartCoroutine(RightStretchNumerator());

    }
    public void PopUpText(string msg,int num)
    {
        if (PossiblePops.Count < 4)
        {
            for (int i = 0; i < 4; i++)
            {
                if (PopUpImgs[i].texture == PopRenderTextures[num] || PopUpImgs[i].texture == PopRenderTexturesStretched[num])
                {
                    TextTypers[i].Type(msg);
                    break;
                }
            }
        }
    }

    public void PopUp(int num)
    {
        if(PossiblePops.Count > 0)
        {
            int pos = Random.Range(0, PossiblePops.Count);
           // pos = 0;
            int pick = PossiblePops[pos];
            //pick = 2;
            PossiblePops.Remove(pick);



            if(pick < 2 )
            {
                PopUpImgs[pick].texture = PopRenderTextures[num];
                PopUpImgs[pick].color = Color.white;

                RighStretcher = RightStretchNumerator();
                StartCoroutine(RighStretcher);
            }
            else
            {
                PopUpImgs[pick].texture = PopRenderTexturesStretched[num];
                PopUpImgs[pick].color = Color.white;

                BottomStretcher = BottomStretchNumerator();
                StartCoroutine(BottomStretcher);
            }
            PopUpImgs[pick].gameObject.SetActive(true);

        }
    }

    public bool GetisCensored(int num)
    { 
        if (PossiblePops.Count < 4)
        {
            for (int i = 0; i < 4; i++)
            {
                if (PopUpImgs[i].texture == PopRenderTextures[num] || PopUpImgs[i].texture == PopRenderTexturesStretched[num])
                {
                   if(i < 2)
                    {
                        if(RightCensor.activeSelf)
                        {    
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                   else
                    {
                        if(BottomCensor.activeSelf)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return false;
    }


    

    IEnumerator BottomStretchNumerator()
    {

        if (!BottomStretch)
        {
            BottomStretch = true;
            CurrentlyStretchingB = true;
            if (CurrentlyDeStretchingB)
            {
                CurrentlyDeStretchingB = false;
                StopCoroutine(BottomDeStretcher);
            }



            while (MainCamImg.rectTransform.offsetMin.y < 193)
            {
                yield return null;
                float change = MainCamImg.rectTransform.offsetMin.y + 450 * Time.deltaTime;
                MainCamImg.rectTransform.offsetMin = new Vector2(MainCamImg.rectTransform.offsetMin.x, change);
            }


            MainCamImg.rectTransform.offsetMin = new Vector2(MainCamImg.rectTransform.offsetMin.x, 193);

            yield return new WaitForSeconds(.2f);
            BottomCensor.SetActive(false);
            CurrentlyDeStretchingB = false;
        }
        yield return null;
    }

    IEnumerator BottomDeStretchNumerator()
    {

        if (BottomStretch)
        {
            BottomStretch = false;
            CurrentlyDeStretchingB = true;
            if (CurrentlyStretchingB)
            {
                CurrentlyStretchingB = false;
                StopCoroutine(BottomStretcher);
            }

            BottomCensor.SetActive(true);

            while (MainCamImg.rectTransform.offsetMin.y > 0)
            {
              

                yield return null;
                float change = MainCamImg.rectTransform.offsetMin.y - 450 * Time.deltaTime;
                MainCamImg.rectTransform.offsetMin = new Vector2(MainCamImg.rectTransform.offsetMin.x,change );
            }


            MainCamImg.rectTransform.offsetMin = new Vector2(MainCamImg.rectTransform.offsetMin.x,0 );

            yield return new WaitForSeconds(.2f);
        }
        yield return null;
    }

    IEnumerator RightStretchNumerator()
    {
        
        if (!RightStretch)
        {
            RightStretch = true;
            CurrentlyStretchingR = true;
            if(CurrentlyDeStretchingR)
            {
                CurrentlyDeStretchingR = false ;
                StopCoroutine(RightDeStretcher);
            }
           


            while (MainCamImg.rectTransform.offsetMax.x > -351)
            {
                yield return null;
                float change = MainCamImg.rectTransform.offsetMax.x - 450 *Time.deltaTime;
                MainCamImg.rectTransform.offsetMax = new Vector2(change , MainCamImg.rectTransform.offsetMax.y);
            }
          

            MainCamImg.rectTransform.offsetMax = new Vector2(-351, MainCamImg.rectTransform.offsetMax.y);
           
            yield return new WaitForSeconds(.2f);
            RightCensor.SetActive(false);
            CurrentlyStretchingR = false;
        }
        yield return null;
    }

    IEnumerator RightDeStretchNumerator()
    {

        if (RightStretch)
        {          
            RightStretch = false;
            CurrentlyDeStretchingR = true;
            if(CurrentlyStretchingR)
            {
                CurrentlyStretchingR = false;
                StopCoroutine(RighStretcher);
            }

            RightCensor.SetActive(true);

            while (MainCamImg.rectTransform.offsetMax.x < 0)
            {

                yield return null;
                float change = MainCamImg.rectTransform.offsetMax.x + 450 * Time.deltaTime;
                MainCamImg.rectTransform.offsetMax = new Vector2(change, MainCamImg.rectTransform.offsetMax.y);
            }


            MainCamImg.rectTransform.offsetMax = new Vector2(0, MainCamImg.rectTransform.offsetMax.y);

            yield return new WaitForSeconds(.2f);
        }
        yield return null;
    }

    public void PopDown(int num)
    {
        if (PossiblePops.Count < 4)
        {
            for(int i=0;i < 4;i++)
            {
                if(PopUpImgs[i].texture == PopRenderTextures[num] || PopUpImgs[i].texture == PopRenderTexturesStretched[num])
                {
                    PopUpImgs[i].texture = null;
                    TextTypers[i].ClearText();
                    PopUpImgs[i].color = Color.black;
                    PossiblePops.Add(i);
                    if (i < 2)
                    {  
                        if(PossiblePops.Contains(0) && PossiblePops.Contains(1))
                        {
                            RightDeStretcher = RightDeStretchNumerator();
                            StartCoroutine(RightDeStretcher);
                        }
                      
                    }
                    else
                    {
                        if (PossiblePops.Contains(2) && PossiblePops.Contains(3))
                        {
                            BottomDeStretcher = BottomDeStretchNumerator();
                            StartCoroutine(BottomDeStretcher);
                        }
                    }
                    break;

                }
            }
        }
     }

}
