using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
  [Tooltip("Flying speed")]
  [SerializeField]
  private float flyingSpeed = 4.0f;

  [Tooltip("distance range")]
  [SerializeField]
  private MinMax<float> distanceRange = new MinMax<float>(1.0f, 4.0f);

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
      var scene = GlobalClassManager.Instance().scene;
      if (scene.SceneState == Scene.State.addTowers)
      {
        scene.AddTower(transform.position);
      }
      GlobalClassManager.Instance().ObjectsPool.ReturnGameObject(gameObject);
    }else
      transform.position += transform.forward * flyingSpeed * Time.deltaTime;
  }

  private void OnEnable()
  {
    flyRange = Random.Range(distanceRange.min, distanceRange.max);
  }

  private void OnCollisionEnter(Collision collision)
  {
    var tower = collision.collider.GetComponent<Tower>();
    if (tower)
    {
      var globalClassManager = GlobalClassManager.Instance();
      globalClassManager.ObjectsPool.ReturnGameObject(gameObject);
      globalClassManager.scene.RemoveTower(tower.gameObject);
    }
  }
}
