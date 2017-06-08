using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    private GridSpace button;
    public Sprite RedMarker;
    public Sprite BlueMarker;
    public bool condition;
    public string playerSide;
    private object image;

    public bool interactable { get; private set; }

    void Start()
    {
        button = GetComponent<GridSpace>();   
    }

    void Update()
    {

        if (condition)
        {
            button.image.overrideSprite = RedMarker;
        }
    }

}
