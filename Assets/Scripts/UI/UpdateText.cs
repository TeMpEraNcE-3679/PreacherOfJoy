using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UpdateText : MonoBehaviour
{
    public Text shadow, self;

    // Update is called once per frame
    void Update()
    {
        shadow.text = self.text;
    }
}
