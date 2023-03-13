using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntercomInteract : MonoBehaviour
{

    [SerializeField] Transform Pivot,EmptyTransform,ObstacleTempTransform;
    [SerializeField] GameObject Obstacle,gametab,PopTrigger,SucessSound,AlertSound,ClickSound;
    [SerializeField] SpriteRenderer PlayerRenderer,OuterCircleRenderer;
    bool ActiveGame;
    [SerializeField] float RotSpeed;
    [SerializeField] CanvasPopUpTrigger PopCanvasTrigger;
    IEnumerator SpawnCoroutine;
    [SerializeField] TransmitterManager manager;
    static float ObstacleSpeed=0.5f, ObstacleRotationSpeed= 1f;
    static float MinSpawnTime=1, MaxSpawnTime=3f;
    bool busy;
    float TimeLeft = 10;
   
    public void Interaction()
    {
        if(!ActiveGame)
        {
            Instantiate(ClickSound, transform.position, Quaternion.identity);
            ActiveGame = true;
            gameObject.layer = 0;
            StartGame();
          
        }
      

    }
    void StartGame()
    {
        TimeLeft = 10;
        if (SpawnCoroutine != null)
        {
            StopCoroutine(SpawnCoroutine);
        }
        SpawnCoroutine = SpawnNumerator();
        StartCoroutine(SpawnCoroutine);
    }

    public void Lost()
    {

     if(busy)
        {
            return;
        }
        Instantiate(AlertSound, transform.position, Quaternion.identity);
        busy = true;
        TimeLeft = 11;
        StopCoroutine(SpawnCoroutine);
        
        StartCoroutine(LostNumerator());

        
        
    }
    IEnumerator LostNumerator()
    {
        float tempPlayerSpeed = RotSpeed;
        RotSpeed = 0;
        PlayerRenderer.color = Color.red;
        OuterCircleRenderer.color = Color.red;


        TransmitterObstacle[] Sprites = FindObjectsOfType<TransmitterObstacle>();
        for (int i = 0; i < Sprites.Length; i++)
        {
            Sprites[i].GetComponent<SpriteRenderer>().color = Color.red;
            Sprites[i].Speed = 0;
            Sprites[i].RotSpeed = 0;

        }
        yield return new WaitForSeconds(.5f);

        RotSpeed = tempPlayerSpeed;
        PlayerRenderer.color = Color.white;
        OuterCircleRenderer.color = Color.white;

        for (int i = 0; i < Sprites.Length; i++)
        {
            Destroy(Sprites[i].gameObject);
        }
        busy = false;


        if (PopCanvasTrigger.canActivate)
        {
            StartGame();
        }
           
    }



    IEnumerator SpawnNumerator()
    {
        while(true)
        {
            float time = Random.Range(MinSpawnTime, MaxSpawnTime);
            yield return new WaitForSeconds(time);
            float rand = Random.Range(0,1);
            if(rand == 0)
            {
                rand = -1;
            }
            EmptyTransform.rotation = Pivot.rotation;
            EmptyTransform.Rotate(0, 0, rand * time * Random.Range(-100 * ObstacleRotationSpeed ,100 * ObstacleRotationSpeed));

            GameObject obj = Instantiate(Obstacle, ObstacleTempTransform.transform.localPosition, EmptyTransform.rotation , gametab.transform);
            obj.transform.localPosition = ObstacleTempTransform.transform.localPosition;
            //obj.transform.rotation = EmptyTransform.localRotation;
            obj.GetComponent<TransmitterObstacle>().Speed = ObstacleSpeed;
            obj.GetComponent<TransmitterObstacle>().RotSpeed = ObstacleRotationSpeed;

        }
    }

    void Victory()
    {
        StopCoroutine(SpawnCoroutine);

        TransmitterObstacle[] Sprites = FindObjectsOfType<TransmitterObstacle>();
        for (int i = 0; i < Sprites.Length; i++)
        {
            Destroy( Sprites[i].gameObject);
        }
        PlayerRenderer.color = Color.green;
        OuterCircleRenderer.color = Color.green;

        Instantiate(SucessSound, transform.position, Quaternion.identity);
        ObstacleSpeed = ObstacleSpeed * 1.12f;
        ObstacleRotationSpeed = ObstacleRotationSpeed * 1.15f;
        MinSpawnTime = MinSpawnTime * 0.87f;
        MaxSpawnTime = MaxSpawnTime * 0.87f;
        manager.AddTransmittor();

        Invoke("RemoveTrigger", 2);

    }

    void RemoveTrigger()
    {
        PopTrigger.SetActive(false);

    }
    private void Update()
    {
        if(ActiveGame)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                Pivot.Rotate(0, 0, -RotSpeed*Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.X))
            {
                Pivot.Rotate(0, 0, RotSpeed * Time.deltaTime);

            }

            TimeLeft -= Time.deltaTime;

            if(TimeLeft<=0 && !busy)
            {
                busy = true;
                Victory();
            }
        }

        if(!PopCanvasTrigger.canActivate)
        {
            ActiveGame = false;
            gameObject.layer = 8;



        }

    }



}
