using System;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    #region Api Unity
    void Start()
    {
        if (_player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
                _player = playerObject.transform;
        }
    }

    
    void Update()
    {
        if (!_player || _hasLaunched) return;

        float distance = Vector3.Distance(transform.position, _player.position);
        
        if (distance <= _detectionRange)
        {
            
            SetLaunchDirection(); //Calcule direction + rotation
            _hasLaunched = true; // Marque le missile comme lancé
        }
    }

    private void FixedUpdate()
    {
        if (_hasLaunched)
        {
            
            transform.position += _moveDirection * (_speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Obstacle") )
        {
            gameObject.SetActive(false);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            gameObject.SetActive(false);
        }
    }
    #endregion


    #region Utils

    private void SetLaunchDirection()
    {
        _moveDirection = (_player.position - transform.position).normalized;
        
        float angle = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg;
        angle += _angleOffset;
        
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    

    #endregion


    #region Private And Protected

    [Header("D�tection")]
    [SerializeField] private Transform _player;
    [SerializeField] private float _detectionRange = 15f;    
    [SerializeField] private float _angleOffset = -90f;

    [Header("Vitesse")]
    [SerializeField] private float _speed = 2f;

    private bool _hasLaunched = false;
    private Vector3 _moveDirection;

    #endregion
}
