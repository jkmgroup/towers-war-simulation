using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjectEmitter))]
public class Bullet : MonoBehaviour
{
  [Tooltip("Flying speed")]
  [SerializeField]
  private float flyingSpeed = 4.0f;

  [Tooltip("Mini life time")]
  [SerializeField]
  private float miniLifeTime = 1.0f;

  [Tooltip("Max life time")]
  [SerializeField]
  private float maxiLifeTime = 1.0f;

  private float timeToDestroy = 0.0f;
  private ObjectEmitter objectEmitter;
    // Start is called before the first frame update
  void Start()
  {
    objectEmitter = GetComponent<ObjectEmitter>();      
  }

  // Update is called once per frame
  void Update()
  {
    timeToDestroy -= Time.deltaTime;
    if (timeToDestroy<0)
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
    timeToDestroy = Random.Range(miniLifeTime, maxiLifeTime);
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
