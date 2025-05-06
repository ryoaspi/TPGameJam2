using UnityEngine;

public class EnemyLent : MonoBehaviour
{
    #region Api Unity
    private void Awake()
    {
        Debug.Log("Bonjour");
    }

    void Start()
    {
        if (_waypoints.Length == 0)
        {
            Debug.LogError("Aucun waypoint assigné !");
            enabled = false;
        }

        _currentWaypointIndex = 0;
        transform.position = _waypoints[_currentWaypointIndex].position;
    }
   
    void Update()
    {
        Debug.Log("Je suis en VIE");
        Move();
        TryShoot();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<AmmoControle>())
        {
            CalculeDamage(collision.GetComponent<AmmoControle>());
        }
    }

    #endregion


    #region Utils

    private void Move()
    {
        if (_waypoints.Length == 0) return;

        Transform targetWaypoint = _waypoints[_currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, _speed * Time.deltaTime);
        transform.position =new Vector3(transform.position.x, transform.position.y, 0);

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
    private void TryShoot()
    {
        if (_fireCooldown <= 0)
        {
            Shoot();
            _fireCooldown = 1f / _fireRate;

        }
        else _fireCooldown -= Time.deltaTime;
    }

    private void Shoot()
    {
        if (_ammo)
        {
            GameObject ammo = Instantiate(_ammo, transform.position + transform.right * 0.5f, transform.rotation);

        }
    }
    private void CalculeDamage(AmmoControle ammo)
    {
        _currentLife -= ammo.GetDamage();
        if (_currentLife <= 0)
        {
            gameObject.SetActive(false);
            _currentLife = _life;
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Oh je suis mort");
    }

    #endregion


    #region Private And Protected

    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] private bool _loop = true;

    private int _currentWaypointIndex = 0;

    [Header("Tir")]
    [SerializeField] private GameObject _ammo;
    [SerializeField] private float _fireRate = 1f; // Tir par seconde
    private float _fireCooldown = 0f;
    
    [Header("vie")]
    [SerializeField] private int _life = 3;
    private int _currentLife;

    #endregion
}
