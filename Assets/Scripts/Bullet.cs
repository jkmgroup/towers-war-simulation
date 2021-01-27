using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjectEmitter))]
public class Bullet : MonoBehaviour
{
  [Tooltip("Flying speed")]
  [SerializeField]
  private float flyingSpeed = 4.0f;

  [Tooltip("Mini distance")]
  [SerializeField]
  private float miniDistance = 1.0f;

  [Tooltip("Max distance")]
  [SerializeField]
  private float maxDistance = 4.0f;

  private float curDistance = 0.0f;
  private float flyRange = 0.0f;
  private ObjectEmitter objectEmitter;

  void Start()
  {
    objectEmitter = GetComponent<ObjectEmitter>();      
  }

  void Update()
  {
    curDistance += Time.deltaTime * flyingSpeed;
    if (curDistance > flyRange)
    {
      if (TowerCounte.Instance().CanAddNext)
      {
        var tower = objectEmitter.CreateNewGameObject();
        tower.transform.position = transform.position;
        TowerCounte.Instance().AddTower();
      }
      objectEmitter.objectPool.ReturnGameObject(gameObject);
    }else
      transform.position += transform.forward * flyingSpeed * Time.deltaTime;
  }

  private void OnEnable()
  {
    flyRange = Random.Range(miniDistance, maxDistance);
  }

  private void OnCollisionEnter(Collision collision)
  {
    var tower = collision.collider.GetComponent<Tower>();
    if (tower)
    {
      objectEmitter.objectPool.ReturnGameObject(gameObject);
      objectEmitter.objectPool.ReturnGameObject(tower.gameObject);
      TowerCounte.Instance().RemoveTower();
    }
  }
}
