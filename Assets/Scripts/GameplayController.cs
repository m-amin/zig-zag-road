
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [HideInInspector] public bool gamePlaying;
    [SerializeField] private GameObject tile;
    [SerializeField] private Material tileMat;
    [SerializeField] private Light dayLight;
    
    private Camera mainCamera;
    private bool camColorLerp;
    private Color cameraColor;
    private Color[] tileColor_Day;
    private Color tileColor_Night;
    private int tileColor_Index;
    private Color tileTrueColor;
    private float timer;
    private float timeInterval = 5f;
    private float camLerpTime;
    private float camLerpInterval = 1f;
    private int direction = 1;

    public AudioSource audioSource;
    private Vector3 currentTilePosition; 
    void Awake()
    {
        MakeSingleton();
        currentTilePosition = new Vector3(-2, 0, 2);
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
        cameraColor = Camera.main.backgroundColor;
        tileTrueColor = tileMat.color;
        tileColor_Index = 0;
        tileColor_Day = new Color[3];
        tileColor_Day[0] = new Color(10 / 256f, 139 / 256f, 203 / 256f);
        tileColor_Day[1] = new Color(10 / 256f, 200 / 256f, 20 / 256f);
        tileColor_Day[2] = new Color(220 / 256f, 170 / 256f, 45 / 256f);
        tileColor_Night = new Color(0, 8 / 256f, 11 / 256f);
        tileMat.color = tileColor_Day[0];
    }

    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            CreateTiles();
        }
    }

    void Update()
    {
        CheckLerpTimer();
    }

    void OnDisable()
    {
        instance = null;
        tileMat.color = tileTrueColor;
    }
    
    void MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void CheckLerpTimer()
    {
        timer += Time.deltaTime;
        if (timer > timeInterval)
        {
            timer -= timeInterval;
            camColorLerp = true;
            camLerpTime = 0f;
        }

        if (camColorLerp)
        { 
            camLerpTime += Time.deltaTime;
            float perc = camLerpTime / camLerpInterval;

            if (direction == 1)
            {
                mainCamera.backgroundColor = Color.Lerp(cameraColor, Color.black, perc);
                tileMat.color = Color.Lerp(tileColor_Day[tileColor_Index], tileColor_Night, perc);
                dayLight.intensity = 1f - perc;
            }else
            {
                mainCamera.backgroundColor = Color.Lerp(Color.black, cameraColor, perc);
                tileMat.color = Color.Lerp(tileColor_Night, tileColor_Day[tileColor_Index], perc);
                dayLight.intensity = perc;
            }

            if (perc > 0.98f)
            {
                camLerpTime = 1f;
                direction *= -1;
                camColorLerp = false;

                if (direction == -1)
                {
                    tileColor_Index = Random.Range(0, tileColor_Day.Length);
                }
            }
        }
    }

    void CreateTiles()
    {
        Vector3 newTilePosition = currentTilePosition;
        int rand = Random.Range(0, 100);
        if (rand < 50)
        {
            newTilePosition.x -= 1f;
        }
        else
        {
            newTilePosition.z += 1f;
        }
        currentTilePosition = newTilePosition;
        Instantiate(tile, currentTilePosition, Quaternion.identity);
    }

    public void ActivateTileSpawner()
    {
        StartCoroutine(SpawnNewTiles());
    }

    IEnumerator SpawnNewTiles()
    {
        yield return new WaitForSeconds(0.3f);
        CreateTiles();

        if (gamePlaying)
        {
            StartCoroutine(SpawnNewTiles());
        }
    }

    public void PlayCollectAudio()
    {
        audioSource.Play();
    }
}
