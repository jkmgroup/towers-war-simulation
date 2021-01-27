using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjectEmitter))]
public class Tower : MonoBehaviour
{
  [SerializeField]
  public enum State { wait, active, noBullet };

  [Tooltip("Actual state")]
  [SerializeField]
  private State state = State.wait;

  [Tooltip("Number of bullet")]
  [SerializeField]
  private int numBuletInMagazine = 12;
  private int numBullet;

  [Tooltip("Delay to next rotate step")]
  [SerializeField]
  private float timeToRotate = 0.5f;

  [Tooltip("Stay in wait state length")]
  [SerializeField]
  private float waitMaxTime = 6.0f;

  [Tooltip("Rotation mini angle")]
  [SerializeField]
  private float miniAngle = 15.0f;

  [Tooltip("Rotation max angle")]
  [SerializeField]
  private float maxAngle = 45.0f;

  [Tooltip("color When tower is Active")]
  [SerializeField]
  private Material colorWhenActive = null;

  [Tooltip("color When tower is not Active")]
  [SerializeField]
  private Material colorWhenNotActive = null;

  private float curTime = 0;
  private ObjectEmitter objectEmitter;
  private void Start()
  {
    objectEmitter = GetComponent<ObjectEmitter>();
    numBullet = numBuletInMagazine;
  }
  // Update is called once per frame
  void Update()
  {
    switch (state)
    {
      case State.wait:
        UpdateWaitState();
        break;
      case State.active:
        UpdateActiveState();
        break;
    }
  }

  public void ChangeState(State newState)
  {
    state = newState;
    curTime = 0.0f;
    if (state==State.active)
    {
      numBullet = numBuletInMagazine;
      gameObject.GetComponent<Renderer>().material = colorWhenActive;
    }
    else
      gameObject.GetComponent<Renderer>().material = colorWhenNotActive;
  }

  private void UpdateWaitState()
  {
    curTime += Time.deltaTime;
    if (curTime >= waitMaxTime)
      ChangeState(State.active);
  }

  private void UpdateActiveState()
  {
    if (numBullet <= 0)
    {
      ChangeState(State.noBullet);
      return;
    }
    
    curTime += Time.deltaTime;
    if (curTime >= timeToRotate)
    {
      transform.Rotate(new Vector3(0, Random.Range(miniAngle, maxAngle)));
      curTime = 0.0f;
      SendBullet();
    }
  }

  private void SendBullet()
  {
    var obj = objectEmitter.CreateNewGameObject();
    obj.transform.position = transform.position + transform.forward;
    obj.transform.rotation = transform.rotation;
    numBullet--;
  }
  private void OnDisable()
  {
    ChangeState(State.wait);
    transform.rotation = Quaternion.identity;
  }
}
