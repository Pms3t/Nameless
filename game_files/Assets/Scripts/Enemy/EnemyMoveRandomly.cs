using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveRandomly : MonoBehaviour
{
    [SerializeField]
    private float newPathRangeZMin = -20f;
    [SerializeField]
    private float newPathRangeZMax = 20f;
    [SerializeField]
    private float newPathRangeXMin = -20f;
    [SerializeField]
    private float newPathRangeXMax = 20f;

    private NavMeshAgent navMeshAgent;
    private NavMeshPath path;
    [SerializeField]
    private float timer;
    [SerializeField]
    private bool onRoute;

    private bool validPath;

    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        // If AI doesn't have route to take
        if (!onRoute)
        {
            // Start Initiate as coroutine
            StartCoroutine(Initiate());
        }
    }

    // Creates new position where AI should move between X and Z axis.
    private Vector3 NewRoamingPosition()
    {
        float x = Random.Range(newPathRangeXMin, newPathRangeXMax);
        float z = Random.Range(newPathRangeZMin, newPathRangeZMax);

        Vector3 roamPosition = new Vector3(x, 0, z);

        return roamPosition;
    }

    // IEnumerator method will continue looping until onRoute is turned to false;
    private IEnumerator Initiate()
    {
        onRoute = true;

        // WaitForSeconds can be only used with a yield statement in coroutines!
        yield return new WaitForSeconds(timer);
        NewPath();
        
        // Check if path is manageable. Returns boolean.
        validPath = navMeshAgent.CalculatePath(target, path);
        //if (!validPath) { }

        // If invalid path is entered as a destination CHANGE it.
        while (!validPath)
        {
            // add delay
            yield return new WaitForSeconds(0.01f);
            NewPath();
            validPath = navMeshAgent.CalculatePath(target, path);
        }

        onRoute = false;
    }

    // Telling AI's navMeshAgent where it should move to.
    void NewPath()
    {
        target = NewRoamingPosition();
        navMeshAgent.SetDestination(target);
    }
}

// ENEMY ERRORS

    // ERROR 1 (01.02.2020):
    // FOR SOME REASON IF PLAYER COLLIDES WITH ENEMY ENEMY'S MESH STARTS GOING BANANAS WITH HIGHLY INCREASED SPEED.
    // IGNORING WHERE THE REAL ENEMY COLLIDER IS.
    // LOOKS DOPE THOUGH
    // MOST REASONABLY REASON WHY THIS HAPPENS:
    // AT THE MOMENT ENEMY DOESN'T KNOW WHAT TO DO IF PLAYER COLLIDES WITH IT WHICH INTERUPS THE PATH ASSIGNED TO IT.