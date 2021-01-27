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

  [ExecuteInEditMode]
  private void OnValidate()
  {
    if (!prefab)
    {
      Debug.LogError("prafab can be null!");
      Debug.Break();
    }
  }

  public GameObject CreateNewGameObject()
  {
    return GlobalClassManager.Instance().ObjectsPool.GetGameObject(prefab, numObjectInPool);
  }
}
