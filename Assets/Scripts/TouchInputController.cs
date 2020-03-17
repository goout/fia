using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInputController : MonoBehaviour
{
   [SerializeField] private Color actionColor = new Color(255, 0, 0, 255);

    private Color imageColor;

    private Image image = null;


    void Start()
    {
        image = GetComponent<Image>();
        imageColor = image.color;
    }

    public void OnInputDown()
    {
        image.color = actionColor;
    }

    public void OnInputExit()
    {
        image.color = imageColor;
    }
}
