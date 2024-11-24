using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public static GUIManager S;

    [Header("HUDs")] 
    public GameObject HUD_title;
    public GameObject HUD_onBoarding;
    public GameObject HUD_inGame;
    public GameObject HUD_gameWin;
    public GameObject HUD_gameOver;

    [Header("In Game")]
    public Text txt_currentFill;
    public Text txt_totalGrids;
    public Text txt_totalGrids_shadow;

    private Color[] colors = new Color[5];
    
    private void Awake()
    {
        S = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        colors[0] = new Color(235/255.0f, 173/255.0f,213/255.0f,1.0f );
        colors[1] = new Color(135/255.0f, 204/255.0f, 113/255.0f, 1.0f );
        colors[2] = new Color(180/255.0f, 155/255.0f, 224/255.0f,1.0f);
        colors[3] = new Color(121/255.0f, 176/255.0f, 214/255.0f, 1.0f);
        colors[4] = new Color(248/255.0f, 241/255.0f, 161/255.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.S.state == GameManager.GameState.InGame)
        {
            txt_currentFill.text = GameManager.S.Filled().ToString();
            txt_currentFill.color = GameManager.S.curColorIdx == 0 ? colors[0] : colors[(GameManager.S.curColorIdx - 1) % 5];
        }
    }

    public void UpdateState()
    {
        HUD_title?.SetActive(GameManager.S.state == GameManager.GameState.Title);
        HUD_onBoarding?.SetActive(GameManager.S.state == GameManager.GameState.LevelSelection);
        HUD_inGame?.SetActive(GameManager.S.state == GameManager.GameState.InGame);
        HUD_gameOver?.SetActive(GameManager.S.state == GameManager.GameState.GameOver);
        HUD_gameWin?.SetActive(GameManager.S.state== GameManager.GameState.GameWin);
    }

    public void StartGame()
    {
        GameManager.S.state = GameManager.GameState.LevelSelection;
        UpdateState();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateTotalGrids(int total)
    {
        txt_totalGrids.text = total.ToString();
        txt_totalGrids_shadow.text = total.ToString();
    }
}
