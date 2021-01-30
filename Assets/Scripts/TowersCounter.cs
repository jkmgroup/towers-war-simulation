using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersCounter : MonoBehaviour
{
  public static event Action<int> towerNumChange;

  [Tooltip("Curent towers number")]
  [SerializeField]
  private int numTowers = 1;

  public void AddTower()
  {
    numTowers++;
    towerNumChange?.Invoke(numTowers);
  }

  public void RemoveTower()
  {
    numTowers--;
    towerNumChange?.Invoke(numTowers);
  }

}
