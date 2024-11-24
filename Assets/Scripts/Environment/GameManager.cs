using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    public enum GameState
    {
        Title, LevelSelection, InGame, Pause, GameOver, GameWin    
    }
    
    [Header("Game Info")]
    public GameState state;
    public GameObject playerGO;
    public GameObject placeholderCamGO;
    public GameObject gizmo;
    
    [Header("Level")]
    public int curColorIdx;
    public bool isSliding = false;

    public int curLevelMaxSlide;
    public int curSlideIdx;
    
    private List<Grid> allGrids = new List<Grid>();
    private int filledGrids = 0;
    private int wallCount = 0;

    [Header("Game Over")] 
    [SerializeField] private Vector3 respawnPosition;
    [SerializeField] private Vector3 respawnRotation;
    
    
    private void Awake()
    {
        S = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        curColorIdx = 0;
        state = GameState.Title;
    }

    // Update is called once per frame
    void Update()
    {
        if (curSlideIdx <= 0)
        {
            isSliding = false;
            curSlideIdx = 0;
        }

        if (state == GameState.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetAllGrids();
                ResetPlayer();
                state = GameState.InGame;
                GUIManager.S.UpdateState();
            }
        }
    }

    public void RestoreSlideTime()
    {
        curSlideIdx = curLevelMaxSlide;
    }

    public void AddGrid(Grid grid)
    {
        allGrids.Add(grid);        
    }

    public void ResetAllGrids()
    {
        foreach (var grid in allGrids)
        {
            grid.ResetColor();
        }
        filledGrids = 0;
    }

    public void StartWallSliding(Transform wallTransform, Vector3 sideOffset)
    {
        RestoreSlideTime();
        isSliding = true;
        wallCount = 0;

        Player.S.ClimbWall(wallTransform, sideOffset);
    }

    public void IncrementWallCount()
    {
        wallCount++;
        if (wallCount >= 3)
        {
            EndWallSliding();
        }
    }

    public void EndWallSliding()
    {
        isSliding = false;
        wallCount = 0;

        Player.S.DropFromWall();
    }

    public void IncrementGrids()
    {
        filledGrids++;
        if (filledGrids == allGrids.Count)
        {
            state = GameState.GameWin;
            GUIManager.S.UpdateState();
        }
    }

    public void ResetPlayer()
    {
        // TODO: other reset logic 
        Player.S.transform.position = respawnPosition;
        Player.S.transform.localEulerAngles = respawnRotation;
    }

    public void GameOver()
    {
        // TODO: other game over logic
        state = GameState.GameOver;
        GUIManager.S.UpdateState();
        gizmo.SetActive(false);
    }

    public void ActivatePlayer()
    {
        playerGO.SetActive(true);
        placeholderCamGO.SetActive(false);
        gizmo.SetActive(true);
        GUIManager.S.UpdateTotalGrids(allGrids.Count);
    }

    public int Filled()
    {
        return filledGrids;
    }
}
