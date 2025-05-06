using UnityEngine;

public class EnemyMissile : MonoBehaviour
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
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

        transform.rotation = Quaternion.Euler(0f, 0f, newZ);
        transform.position = new Vector3(direction.x * _speed * Time.deltaTime, direction.y * _speed * Time.deltaTime, 0f);
    }

    #endregion


    #region Private And Protected

    [Header("Détection")]
    [SerializeField] private Transform _player;
    [SerializeField] private float _detectionRange = 15f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _angleOffset = -90f;

    [Header("vie")]
    [SerializeField] private int _life = 3;
    private int _currentLife;

    [Header("Vitesse")]
    [SerializeField] private float _speed = 2f;

    #endregion
}
