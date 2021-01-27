using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ObjectPool))]
public class Scene : MonoBehaviour
{
  [Tooltip("Max life time")]
  [SerializeField]
  private Text counterLable;

  public enum State { addTowers, maxTowers };
  private State state = State.addTowers;

  private ObjectPool objectPool;

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
    objectPool = GetComponent<ObjectPool>();
    TowerCounte.Instance().CounterLable = counterLable;
  }

  // Update is called once per frame
  void Update()
  {
    if (state == State.addTowers && !TowerCounte.Instance().CanAddNext)
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
