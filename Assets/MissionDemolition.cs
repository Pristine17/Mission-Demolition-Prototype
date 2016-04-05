using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionDemolition : MonoBehaviour {

    public enum GameMode
    {
        idle,playing,levelEnd
    }

    static public MissionDemolition S;

    public GameObject[] castles;
    public Text gtLevel;
    public Text gtScore;
    public Vector3 castlePos;

    public bool __________________________________________;

    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Slingshot";

    void Start()
    {
        S = this;
        level = 0;
        levelMax = castles.Length;
        StartLevel();    
    }

    void StartLevel()
    {
        if(castle!=null)
        {
            Destroy(castle);
        }

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach(GameObject obj in gos)
        {
            Destroy(obj);
        }

        castle=Instantiate(castles[level]) as GameObject;
        castle.transform.position=castlePos;
        shotsTaken=0;
        SwitchView("Both");
        ProjectileLine.S.Clear();

        Goal.goalMet = false;

        ShowGT();

        mode = GameMode.playing;
    }

    void Update()
    {
        ShowGT();

        if(mode==GameMode.playing&&Goal.goalMet==true)
        {
            mode = GameMode.levelEnd;
            SwitchView("Both");
            Invoke("NextLevel", 2f);
        }
    }

    static public void SwitchView(string eView)
    {
        S.showing = eView;
        switch(S.showing)
        {
            case "Slingshot": FollowCam.S.poi = null;
                              break;
            
            case "Castle": FollowCam.S.poi = S.castle;
                              break;

            case "Both": FollowCam.S.poi = GameObject.Find("ViewBoth"); 
                            break;
        }
    }

    void ShowGT()
    {
        gtLevel.text = "Level : " + (level + 1) + " of " + levelMax;
        gtScore.text = "Shots taken: " + shotsTaken;
    }

    void NextLevel()
    {
        level++;
        if(level==levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    void OnGUI()
    {
        Rect buttonRect = new Rect(Screen.width/2-50,10,100,24);
        
        switch(showing)
        {
            case "Both": if (GUI.Button(buttonRect, "Show Slingshot"))
                                {
                                    SwitchView("Slingshot");
                                }
                                break;
            
            case "Slingshot" : if(GUI.Button(buttonRect, "Show Castle"))
                                {
                                    SwitchView("Castle");
                                }
                                break;
            case "Castle":    if (GUI.Button(buttonRect, "Show Both"))
                                {
                                    SwitchView("Both");
                                }
                                break;
        }
    }

    static public void ShotsFired()
    {
        S.shotsTaken++;
    }



}
