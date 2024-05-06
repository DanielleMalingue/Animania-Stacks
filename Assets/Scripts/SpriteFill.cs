using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpriteFill : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    public Color fillColor;

    void Start()
    {
        FillCircleColors();
    }

    void FillCircleColors()
    {
        // Iterate through each child of the GridLayoutGroup
        for (int i = 0; i < gridLayoutGroup.transform.childCount; i++)
        {
            // Get the child GameObject
            GameObject circleObject = gridLayoutGroup.transform.GetChild(i).gameObject;

            // Get the Image component of the child
            Image circleImage = circleObject.GetComponent<Image>();

            // Check if the child has an Image component
            if (circleImage != null)
            {
                // Set the color of the Image component
                circleImage.color = fillColor;
            }
        }
    }
}