using UnityEngine;
using UnityEngine.UI;

public class TowerCounte
{
  private int maxNumTowers = 100;
  private Text counterLable;

  private int numTowers = 1;
  public int NumTowers
  {
    get => numTowers;
  }

  private bool canAddNext = true;
  public bool CanAddNext
  {
    get => canAddNext;
  }

  public void AddTower()
  {
    numTowers++;
    if (numTowers >= maxNumTowers)
      canAddNext = false;
    UpdateLable();
  }

  public void RemoveTower()
  {
    numTowers--;
    UpdateLable();
  }

  public Text CounterLable
  {
    set { counterLable = value; }
  }

  private void UpdateLable()
  {
    if (!counterLable)
      return;
    counterLable.text = "Wieżyczki: " + numTowers;
  }
}
