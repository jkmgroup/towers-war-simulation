using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
  
  void Start()
  {
  }

  void Update()
  {
    curDistance += Time.deltaTime * flyingSpeed;
    if (curDistance > flyRange)
    {
      if (GlobalClassManager.Instance().TowersCounte.CanAddNext)
      {

        var tower = GlobalClassManager.Instance().TowersEmitter.CreateNewGameObject();
        tower.transform.position = transform.position;
        GlobalClassManager.Instance().TowersCounte.AddTower();
      }
      GlobalClassManager.Instance().ObjectsPool.ReturnGameObject(gameObject);
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
      var globalClassManager = GlobalClassManager.Instance();
      globalClassManager.ObjectsPool.ReturnGameObject(gameObject);
      globalClassManager.ObjectsPool.ReturnGameObject(tower.gameObject);
      globalClassManager.TowersCounte.RemoveTower();
    }
  }
}
