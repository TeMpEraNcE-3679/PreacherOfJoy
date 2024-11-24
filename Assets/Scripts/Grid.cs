using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    public enum GridType
    {
        Base, Wall, Horizontal
    }
    

    private Color[] colors = new Color[5];
    private List<Color> initialColors = new List<Color>();
    private Material[] _materials;
    
    public GridType type;
    private int coloredTime = 0;

    [SerializeField] private Text txt_counter;
    protected bool hasBeenColored = false;

    protected void Awake()
    {
        colors[0] = new Color(235/255.0f, 173/255.0f,213/255.0f,1.0f );
        colors[1] = new Color(135/255.0f, 204/255.0f, 113/255.0f, 1.0f );
        colors[2] = new Color(180/255.0f, 155/255.0f, 224/255.0f,1.0f);
        colors[3] = new Color(121/255.0f, 176/255.0f, 214/255.0f, 1.0f);
        colors[4] = new Color(248/255.0f, 241/255.0f, 161/255.0f, 1.0f);

        _materials = GetComponent<MeshRenderer>().materials;
        foreach (var material in _materials)
        {
            initialColors.Add(material.color);
        }
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.S != null) GameManager.S.AddGrid(this);
    }

    // Update is called once per frame
    private void Update()
    {
        if (type == GridType.Base)
        {
            txt_counter.text = coloredTime.ToString();
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (type == GridType.Base)
        {
            if (!GameManager.S.isSliding)
            {
                if (other.transform.CompareTag("Player"))
                {
                    if (!hasBeenColored)
                    {
                         Debug.Log("The player has filled a base grid: " + transform.name + "(" + transform.GetInstanceID() + ")");
                        GameManager.S.IncrementGrids();
                        hasBeenColored = true;
                    }
                    
                    if (++coloredTime > 3)
                    {
                        GameManager.S.GameOver();
                    }
                    NextColor();
                }
            }
        }

        if (type == GridType.Horizontal)
        {
            if (!GameManager.S.isSliding)
            {
                if (other.transform.CompareTag("Player"))
                {
                    if (!hasBeenColored)
                    {
                        Debug.Log("The player has filled a horizontal grid: " + transform.name + "(" + transform.GetInstanceID() + ")");
                        GameManager.S.IncrementGrids();
                        hasBeenColored = true;
                    }
                    NextColor();
                }
            }
        }

        if (type == GridType.Wall)
        {
            if (GameManager.S.isSliding)
            {
                if (other.transform.CompareTag("Player"))
                {
                    if (!hasBeenColored)
                    {
                        Debug.Log("The player has filled a wall grid: " + transform.name + "(" + transform.GetInstanceID() + ")");
                        GameManager.S.IncrementGrids();
                        hasBeenColored = true;
                    }
                    NextColor();
                }
            }
        }
    }


    protected void NextColor()
    {
        foreach (Material material in _materials)
        {
            material.color = Color.black;
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", colors[GameManager.S.curColorIdx % 5] * 1.1f);
        }
        
        GameManager.S.curColorIdx++;
        AudioManager.S.PlayNextNote();
    }

    public void ResetColor()
    {
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].color = initialColors[i];
            _materials[i].DisableKeyword("_EMISSION");
        }

        coloredTime = 0;
        hasBeenColored = false;
    }
}
