using UnityEngine;

public class EnemyLent : MonoBehaviour
{
    #region Api Unity

    void Start()
    {
        if (_waypoints.Length == 0)
        {            
            Debug.Log("Pas de waypoints assignés !");
            return;
        }

        _currentWaypointIndex = 0;
        _currentLife = _life;
    }
   
    void Update()
    {
        if (!_player) return;

        float distance = Vector3.Distance(transform.position, _player.position);

        if (distance <= _detectionRange)
        {
            
            RotateTowardPlayer();
            Move();
            TryShoot();

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<AmmoControle>())
        {
            CalculeDamage(collision.GetComponent<AmmoControle>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    #endregion


    #region Utils

    private void Move()
    {
        // Vérification du nombre de waypoints
        if (_waypoints.Length == 0) 
        {
            return;
        }

        Debug.Log("je bouge");
               
        // Forcer la position Z à 0 avant le déplacement
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        // Référence au waypoint actuel
        Transform targetWaypoint = _waypoints[_currentWaypointIndex];

        // Déplacer vers le waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, _speed * Time.deltaTime);

        // Si l'ennemi est proche du waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            Debug.Log("En chemin");
            // Mise à jour de l'index du waypoint
            _currentWaypointIndex += _direction;

            // Si on a atteint la fin ou le début des waypoints, on inverse la direction
            if (_currentWaypointIndex >= _waypoints.Length || _currentWaypointIndex < 0)
            {
                _direction = -_direction;
                // Réajuster l'index pour ne pas dépasser les bornes
                _currentWaypointIndex += _direction;
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
    private void RotateTowardPlayer()
    {
        Vector3 direction = (_player.position - transform.position).normalized;

        // Calcul de l'angle sur le plan Z (dans l'espace local)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Applique l'offet pour compenser l'orientation initiale
        angle += _angleOffset;

        // On garde uniquement la rotation Z (pitch), on ignore x et y
        float currentZ = transform.eulerAngles.z;
        float newZ = Mathf.LerpAngle(currentZ, angle, _rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, 0f, newZ);
    }

    #endregion


    #region Private And Protected

    [Header("Déplacement")]
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
   
    private int _currentWaypointIndex = 0;
    private int _direction = 1;

    [Header("Détection")]
    [SerializeField] private Transform _player;
    [SerializeField] private float _detectionRange = 15;
    [SerializeField] private float _rotationSpeed = 5f;


    [Header("Tir")]
    [SerializeField] private GameObject _ammo;
    [SerializeField] private float _fireRate = 1f; // Tir par seconde
    [SerializeField] private float _angleOffset = -90f;
    private float _fireCooldown = 0f;
    
    [Header("vie")]
    [SerializeField] private int _life = 3;
    private int _currentLife;

    #endregion
}
