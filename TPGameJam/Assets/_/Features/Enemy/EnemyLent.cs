using UnityEngine;

public class EnemyLent : MonoBehaviour
{
    #region Api Unity

    void Start()
    {
        if (_waypoints.Length == 0)
        {            
            Debug.Log("Pas de waypoints assignés !");
        }

        _currentWaypointIndex = 0;
        _currentLife = _life;
    }
   
    void Update()
    {        
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

        // Afficher le message chaque fois que le mouvement est mis à jour
        Debug.Log("Oui je bouge");

        // Forcer la position Z à 0 avant le déplacement
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        // Référence au waypoint actuel
        Transform targetWaypoint = _waypoints[_currentWaypointIndex];

        // Déplacer vers le waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, _speed * Time.deltaTime);

        // Si l'ennemi est proche du waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
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

    #endregion


    #region Private And Protected

    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] private bool _loop = true;

    private int _currentWaypointIndex = 0;
    private int _direction = 1;

    [Header("Tir")]
    [SerializeField] private GameObject _ammo;
    [SerializeField] private float _fireRate = 1f; // Tir par seconde
    private float _fireCooldown = 0f;
    
    [Header("vie")]
    [SerializeField] private int _life = 3;
    private int _currentLife;

    #endregion
}
