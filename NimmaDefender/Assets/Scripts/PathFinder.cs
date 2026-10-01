using System;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    //DebugTools
    DebugTools debugTools;

    [SerializeField] private float enemySpeed = 1;
    private GameObject[] wayPoints;
    private int nextWayPoint = 1;
    private GameObject coliderAnchor;
    private bool canMove = true;

    private void Start()
    {
        debugTools = FindAnyObjectByType<DebugTools>();

        wayPoints = GameObject.FindGameObjectsWithTag("Waypoints");
        Array.Sort(wayPoints, (a, b) => a.name.CompareTo(b.name));
        coliderAnchor = gameObject.GetComponentInChildren<Transform>().gameObject;
    }

    private void Update()
    {
        if (canMove) {
            PathMovement();
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Gate"))
        {
            canMove = false;
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Gate"))
        {
            canMove = true;
        }
    }

    private void PathMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[nextWayPoint].transform.position, enemySpeed * Time.deltaTime * debugTools.TimeScale);
        coliderAnchor.transform.rotation = Quaternion.LookRotation(Vector3.forward, wayPoints[nextWayPoint].transform.position - transform.position);
        if (transform.position == wayPoints[nextWayPoint].transform.position && nextWayPoint < (wayPoints.Length - 1))
        {
            nextWayPoint++;
        }
    }
}
