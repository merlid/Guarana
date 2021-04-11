using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTimeLine : MonoBehaviour
{
    [SerializeField]
    private float positionAtCurve;

    [SerializeField]
    private Transform[] routes;

    [SerializeField]
    private int routeToGo;

    private float tParam;

    private Vector3 objectPosition;

    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        //speedModifier = 0.3f;
       // coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 p0 = routes[routeToGo].GetChild(0).position;
        Vector3 p1 = routes[routeToGo].GetChild(1).position;
        Vector3 p2 = routes[routeToGo].GetChild(2).position;
        Vector3 p3 = routes[routeToGo].GetChild(3).position;

        tParam = positionAtCurve;

        objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

        transform.position = objectPosition;

    }

  /*  void ChangeRoute(int newRoute)
    {

    } */

}
