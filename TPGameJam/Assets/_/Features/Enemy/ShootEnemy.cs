using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    #region Api Unity

    private void OnEnable()
    {
        _currentTime = _maxTime;
    }
    
    void Update()
    {
        Move();
        ShootLivetime();
        GetDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    #endregion


    #region Main Method

    public int GetDamage()
    {
        int damage = _damage;
        return damage;
    }

    #endregion


    #region Utils

    private void Move()
    {        
        transform.position += transform.up * _speed * Time.deltaTime;        
    }

    private void ShootLivetime()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime < 0)
        {
            Destroy(gameObject);
        }
    }

    #endregion


    #region Private And Protected

    [SerializeField] private GameObject _ammo;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxTime;

    private float _currentTime;

    #endregion
}
