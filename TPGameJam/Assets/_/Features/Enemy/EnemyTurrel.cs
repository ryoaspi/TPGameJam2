using UnityEngine;

public class EnemyTurrel : MonoBehaviour
{
    #region Api Unity

    private void Awake()
    {
        _currentLife = _life;
    }
    void Update()
    {
        if (!_player) return;

        float distance = Vector3.Distance(transform.position, _player.position);

        if (distance <= _detectionRange)
        {
            RotateTowardPlayer();
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

       transform.rotation = Quaternion.Euler(0f,0f, newZ);       
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


    #region Private And Protectd

    [Header("Détection")]
    [SerializeField] private Transform _player;
    [SerializeField] private float _detectionRange = 15f;
    [SerializeField] private float _rotationSpeed = 5f;

    [Header("Tir")]
    [SerializeField] private GameObject _ammo;
    [SerializeField] private float _fireRate = 1f; // Tir par seconde
    private float _fireCooldown = 0f;
    [SerializeField] private float _angleOffset = -90;

    [Header("vie")]
    [SerializeField] private int _life = 3;
    private int _currentLife;

    #endregion
}
