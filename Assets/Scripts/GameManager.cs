using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Transform> InstantiatePoints;
    public GameObject maincharackterprefab;
    public GameObject AI_Prefab;
    public GameObject bodypartprefab;
    public bool ISStartGame = false;
    public Color32[] AllColors;
    public List<Charackter_Bse> AllCharackter;
    public Charackter_Bse imposter;
    public List<Charackter_Bse> NotImposter;
    public List<Transform> AllMissionPoints;
 
    public void Awake()
    {
        instance = this;
    }
   
    public void Start()
    {
        StartGame();
    }
    
    public void StartGame()
    {
        SpwanCharacters();
        ChooseImposter();
    }
    public void ChooseImposter()
    {
        imposter = AllCharackter[Random.Range(0, AllCharackter.Count)];
        NotImposter = AllCharackter.FindAll(x => x != imposter);

        AllCharackter.FindAll(x => x.GetComponent<AI>()).ForEach(x => ((AI)x).SetMission());

    }
    private void SpwanCharacters()
    {
        List<Transform> newallpoints = InstantiatePoints.ToList();
        List<Color32> newcolors = AllColors.ToList();
        int rand = Random.Range(0, newallpoints.Count);
        GameObject createdmainch = Instantiate(maincharackterprefab, newallpoints[rand].position, Quaternion.identity);
        AllCharackter.Add(createdmainch.GetComponent<Charackter_Bse>());
        int randcolor = Random.Range(0, newcolors.Count);
        createdmainch.GetComponent<MainCharackter>().CurrentColor = newcolors[randcolor];
        newcolors.RemoveAt(randcolor);
        CameraController.instance.target = createdmainch.transform;



        newallpoints.RemoveAt(rand);
        for (int i = 0; i < 9; i++)
        {
            int rand_AI = Random.Range(0, newallpoints.Count);
            GameObject created_AI = Instantiate(AI_Prefab, newallpoints[rand_AI].position, Quaternion.identity);
            AllCharackter.Add(created_AI.GetComponent<Charackter_Bse>());
            newallpoints.RemoveAt(rand_AI);
            int rand_Aı_Color = Random.Range(0, newcolors.Count);
            created_AI.GetComponent<AI>().CurrentColor = newcolors[rand_Aı_Color];
            newcolors.RemoveAt(rand_Aı_Color);
        }
    }
}
