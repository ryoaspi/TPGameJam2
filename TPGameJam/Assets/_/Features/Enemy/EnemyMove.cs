using System;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    #region Api Unity
  
    void Start()
    {
        _currentLife = _life;
        _score = FindObjectOfType<EnemyScore>();
    }

    
    void Update()
    {
        if (!_activated && Vector3.Distance(transform.position, _player.position) <= _activationDistance)
        {
            _enemyToActivate.SetActive(true);
            _activated = true;
        }
        
        Move();
        CheckLife();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer== LayerMask.NameToLayer("Ammo"))
        {
            int damage = other.GetComponent<AmmoControle>().GetDamage();
            _currentLife -= damage;
            other.gameObject.SetActive(false); // désactive le projectile après collision
        }
    }
    #endregion
    
    
    #region Utils

    private void Move()
    {
        Vector3 direction = _move.normalized;
        transform.position += direction * (_speed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0, angle -90);
        }
    }

    
    private void CheckLife()
    {
        if (_life <= 0)
        {
            _score.AddScore(_scoreValue);
            gameObject.SetActive(false);
        }
    }
    
    #endregion
    
    
    #region Private And Protected
    
    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private Vector3 _move;
    [SerializeField] private Vector3 _look;
    [SerializeField] private int _life = 3;
    [SerializeField] private Transform _player;
    [SerializeField] private float _activationDistance = 5f;
    [SerializeField] private GameObject _enemyToActivate;
    [SerializeField] private int _scoreValue;
    
    private float _currentLife;
    private bool _activated = false;
    private EnemyScore _score;
    
    
    #endregion
}
