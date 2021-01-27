using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEmitter : MonoBehaviour
{
  [Tooltip("Object prefab")]
  [SerializeField]
  private GameObject prefab = null;

  [Tooltip("Num object in pool at start")]
  [SerializeField]
  private int numObjectInPool = 0;

  private ObjectPool objectPool_;
  public ObjectPool objectPool
  {
    get { return objectPool_; }
    set { objectPool_ = value; }
  }

  [ExecuteInEditMode]
  private void OnValidate()
  {
    if (!prefab)
    {
      Debug.LogError("prafab can be null!");
      Debug.Break();
    }
  }
  private void Start()
  {
    if (!objectPool_)
    {
      objectPool_ = FindObjectOfType<ObjectPool>();
      if (!objectPool_)
      {
        Debug.LogError("Can find ObjectPool !");
        Debug.Break();
      }
    }

  }
  public GameObject CreateNewGameObject()
  {
    return objectPool_.GetGameObject(prefab, numObjectInPool);
  }
}
