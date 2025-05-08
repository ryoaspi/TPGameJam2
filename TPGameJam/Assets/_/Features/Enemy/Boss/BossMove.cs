using UnityEngine;

public class BossMove : MonoBehaviour
{
    #region Api Unity
    void Start()
    {
        if (_waypoints.Length == 0)
        {
            Debug.LogError("Aucun waypoint assigné");
            enabled = false;
        }

        _currentWaypointIndex = 0;
        transform.position = _waypoints[_currentWaypointIndex].position;
    }

    
    void Update()
    {
        if (!_player) return;
        
        float distance = Vector3.Distance(transform.position, _player.position);

        if (distance <= _detectionRange)
        {
            Move();
        }            
        
    }

    #endregion


    #region Utils

    private void Move()
    {
        if (_waypoints.Length == 0) return;

        Transform targetWaypoint = _waypoints[_currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, _speed);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _waypoints.Length)
            {
                if (_loop)_currentWaypointIndex = 0;
                else enabled = false;
            }
        }
    }

    #endregion


    #region Private And Protected

    [Header("Deplacement")]
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private bool _loop = true;

    [Header("Detection")]
    [SerializeField] private Transform _player;
    [SerializeField] private float _detectionRange = 15f;
    [SerializeField] private float _rotationSpeed = 5f;

    private int _currentWaypointIndex;


    #endregion
}
