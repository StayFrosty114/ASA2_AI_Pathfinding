using UnityEngine;
using UnityEngine.AI;

public class PathDisplayTool : MonoBehaviour 
{
    [SerializeField]
    Transform target;

    private NavMeshPath navMeshPath;

    private void Awake()
    {
        navMeshPath = new NavMeshPath();
    }

    private void Update()
    {
        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, navMeshPath);

        for (int i = 0; i < navMeshPath.corners.Length - 1; i++)
        {
            Debug.DrawLine(navMeshPath.corners[i], navMeshPath.corners[i + 1], Color.green, 0, false);
        }
    }
}
