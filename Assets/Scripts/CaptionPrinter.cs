using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CaptionPrinter : MonoBehaviour
{
    [SerializeField] private Text printerText;
    
    [SerializeField][TextArea(1,10)] private string[] captions;

    [SerializeField] private UnityEvent OnPrinterOver;

    private int curIdx = 0;
    
    private void Start()
    {
        if(captions.Length >= 1) printerText.text = captions[0];
    }

    public void PrinterOnClick()
    {
        if (curIdx + 1 >= captions.Length)
        {
            printerText.text = "";
            OnPrinterOver?.Invoke();
        }
        else
        {
            printerText.text = captions[++curIdx];
        }
    }
}
