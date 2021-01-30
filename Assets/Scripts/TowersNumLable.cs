using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TowersNumLable : MonoBehaviour
{
  private Text lable;
  private void Start()
  {
    lable = GetComponent<Text>();
  }

  private void OnEnable()
  {
    TowersCounter.towerNumChange += TowersNumChage;
  }

  private void OnDisable()
  {    
    TowersCounter.towerNumChange -= TowersNumChage;
  }

  private void TowersNumChage(int numTowers)
  {
    lable.text = "Wieżyczki: " + numTowers;
  }

}