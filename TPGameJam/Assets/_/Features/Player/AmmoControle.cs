using UnityEngine;

public class AmmoControle : MonoBehaviour
{
    #region Api

    private void OnEnable()
    {
        _lifeTime = _maxLiveTime;
    }




    void Update()
    {
        Move();
        ShootLiveTime();
    }

    #endregion


    #region Utils

    private void Move()
    {
        float speedAttack = _speedShoot * Time.deltaTime;
        transform.position += transform.up * speedAttack;
    }

    private void ShootLiveTime()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            gameObject.SetActive(false);

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);
    }

    #endregion


    #region Main Method

    public int GetDamage()
    {
        int damage = _damage;
        return damage;
    }

    #endregion


    #region Private And Protected

    [SerializeField] private float _speedShoot = 2f;
    [SerializeField] private int _damage = 1;
    private float _maxLiveTime = 2f;

    private float _lifeTime;
    #endregion
}