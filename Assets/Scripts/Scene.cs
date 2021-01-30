using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ObjectPool))]
[RequireComponent(typeof(ObjectEmitter))]
[RequireComponent(typeof(TowersCounter))]
public class Scene : MonoBehaviour
{

  [SerializeField]
  private int maxNumTowers = 100;

  private TowersCounter towersCounter;

  public enum State { addTowers, maxTowers };
  private State state = State.addTowers;
  public State SceneState
  {
    get => state;
  }
  private ObjectEmitter towersEmitter = null;


  void Start()
  {
    towersCounter = GetComponent<TowersCounter>();
    GlobalClassManager.Instance().ObjectsPool = GetComponent<ObjectPool>();
    towersEmitter = GetComponent<ObjectEmitter>();
    GlobalClassManager.Instance().scene = this;
  }

  private void SetStateMaxTowers()
  {
    state = State.maxTowers;
    var towers = GetComponentsInChildren<Tower>();
    foreach (var tower in towers)
    {
      tower.ChangeState(Tower.State.active);
    }
  }

  public void AddTower(Vector3 pos)
  {
    var tower = towersEmitter.CreateNewGameObject();
    tower.transform.position = pos;
    towersCounter.AddTower();
  }
  public void RemoveTower(GameObject tower)
  {
    if (!tower.activeSelf)
      return;
    GlobalClassManager.Instance().ObjectsPool.ReturnGameObject(tower);
    towersCounter.RemoveTower();
  }
  private void OnEnable()
  {
    TowersCounter.towerNumChange += TowersNumChange;
  }

  private void OnDestroy()
  {
    TowersCounter.towerNumChange -= TowersNumChange;
  }

  private void TowersNumChange(int numTowers)
  {
    if (state == State.maxTowers)
      return;
    if (numTowers >= maxNumTowers)
      SetStateMaxTowers();
  }
}
