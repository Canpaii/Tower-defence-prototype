using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PortraitChange : MonoBehaviour
{
    public float buildingCost;
    public Color enoughCurrency;
    public Color insufficientCurrency;
    public Image portraitImage;

    void Update()
    {
        if (Currency.Instance.currency >= buildingCost)
        {
            portraitImage.color = enoughCurrency;
        }
        else
        {
            portraitImage.color = insufficientCurrency;
        }
    }
}
