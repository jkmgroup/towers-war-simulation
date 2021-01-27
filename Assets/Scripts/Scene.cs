using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ObjectPool))]
[RequireComponent(typeof(ObjectEmitter))]
public class Scene : MonoBehaviour
{
  [Tooltip("Towers lable counter")]
  [SerializeField]
  private Text counterLable = null;

  public enum State { addTowers, maxTowers };
  private State state = State.addTowers;

  [ExecuteInEditMode]
  private void OnValidate()
  {
    if (!counterLable)
    {
      Debug.LogError("counterLable can be null!");
      Debug.Break();
    }
  }

  void Start()
  {
    GlobalClassManager.Instance().TowersCounte.CounterLable = counterLable;
    GlobalClassManager.Instance().ObjectsPool = GetComponent<ObjectPool>();
    GlobalClassManager.Instance().TowersEmitter = GetComponent<ObjectEmitter>();
  }

  // Update is called once per frame
  void Update()
  {
    if (state == State.addTowers && !GlobalClassManager.Instance().TowersCounte.CanAddNext)
    {
      SetStateMaxTowers();
    }
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
}
