using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TowerUIData", menuName = "TowerUIData")]
public class TowerUiData : ScriptableObject
{
  public Image icon;
  public Text nameText;
  public Text descriptionText;
}
