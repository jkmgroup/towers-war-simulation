using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
  private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

  private GameObject CreateGameObject(GameObject prefabGameObject)
  {
    var obj = Instantiate(prefabGameObject);
    obj.name = prefabGameObject.name;
    obj.transform.parent = transform;
    var objectEmitter = obj.GetComponent<ObjectEmitter>();
    if (objectEmitter)
    {
      objectEmitter.objectPool = this;
    }
    return obj;
  }
  public GameObject GetGameObject(GameObject prefabGameObject, int numObjectInPool)
  {
    if (objectPool.TryGetValue(prefabGameObject.name, out Queue<GameObject> objects))
    {
      if (objects.Count > 0)
      {
        var obj = objects.Dequeue();
        obj.SetActive(true);
        return obj;
      }
    }
    else
    {
      if (numObjectInPool > 0)
      {
        StartCoroutine(CreatePool(prefabGameObject, numObjectInPool));
      }
    }
    return CreateGameObject(prefabGameObject);
  }

  private IEnumerator CreatePool(GameObject prefabGameObject, int numObjectInPool)
  {
    var objects = new Queue<GameObject>();
    objectPool.Add(prefabGameObject.name, objects);
    int i = 0; 
    while (i < numObjectInPool)
    {
      var obj = CreateGameObject(prefabGameObject);
      obj.SetActive(false);
      objects.Enqueue(obj);
      i++;
      yield return new WaitForSeconds(0.5f);
    }
  }

  public void ReturnGameObject(GameObject gameObject)
  {
    Queue<GameObject> objects;

    if (!objectPool.TryGetValue(gameObject.name, out objects))
    {
      objects = new Queue<GameObject>();
      objectPool.Add(gameObject.name, objects);
    }
    objects.Enqueue(gameObject);
    gameObject.SetActive(false);
  }
}
