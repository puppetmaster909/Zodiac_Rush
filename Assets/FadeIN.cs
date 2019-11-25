using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIN : MonoBehaviour
{
    public Image whiteFade;
    
    // Start is called before the first frame update
    void Start()
    {
        whiteFade.canvasRenderer.SetAlpha(0.0f);

        fadeIn();
    }

    void fadeIn()
    {
        whiteFade.CrossFadeAlpha(1, 4, false);
    }
}
