using UnityEngine;

public class EnemyTurrel : MonoBehaviour
{
    #region Api Unity
    
    void Start()
    {
        
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
    [SerializeField] private float _angleOffset = -45f;

    #endregion
}
