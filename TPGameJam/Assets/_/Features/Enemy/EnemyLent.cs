using UnityEngine;

public class EnemyLent : MonoBehaviour
{
    #region Api Unity
    void Start()
    {
        
    }

    
    void Update()
    {
        Move();
    }

    #endregion


    #region Utils

    private void Move()
    {
        if (_waypoints.Length == 0) return;

        Transform targetWaypoint = _waypoints[_currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            _currentWaypointIndex++;

            if (_currentWaypointIndex >= _waypoints.Length)
            {
                if (_loop) _currentWaypointIndex = 0;

                else enabled = false;
            }
        }
    }

    #endregion


    #region Private And Protected

    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] private bool _loop = true;

    private int _currentWaypointIndex = 0;

    #endregion
}
