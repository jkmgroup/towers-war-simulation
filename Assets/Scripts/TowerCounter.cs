using UnityEngine;
using UnityEngine.UI;

public class TowerCounte
{
  static private TowerCounte instance;
  static public TowerCounte Instance()
  {
    if (instance == null)
    {
      instance = new TowerCounte();
    }
    return instance;
  }

  private TowerCounte() { }
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
    if (numTowers >= maxNumTowers)
      canAddNext = false;
    numTowers++;
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
