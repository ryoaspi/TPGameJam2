using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour, InputPlayer.InputPlayer.IPlayerActions
{
    #region Api Unity

    void Awake()
    {
        _playerInput = new InputPlayer.InputPlayer();
        _playerInput.Player.SetCallbacks(this);
        
        
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _lifeUI.SetInitialLife(_life);
        _currentLife = _life;
        
                
    }
    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Start()
    {
        _coolDownCount = 0;
        _timeShieldCount = 0;
        _active = false;
        _shield.SetActive(false);
    }


    void Update()
    {        
        Move();
        TargetMouse();
        UpdateMouseLook();
        
        if (_isShooting && Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + _fireRate;
        }

        if (_active== true)
        {
            if (_timeShieldCount > 0)
            {
                _timeShieldCount -= Time.deltaTime;
            }
            else
            {
                _shieldUI.color = Color.gray;
                DeactiveShield(); // Désactiver automatiquement
            }
        }
        else
        {
            if (_coolDownCount > 0)
            {
                _coolDownCount -= Time.deltaTime;
            }
            else
            {
                _shieldUI.color = Color.blue;
            }
        }

    }

    #endregion



    #region Main Methods

    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();        
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _isShooting = true;
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            _isShooting = false;
        }
    }



    public void OnShield(InputAction.CallbackContext context)
    {
       
        if (context.phase == InputActionPhase.Performed)
        {
            if (!_active && _coolDownCount <= 0)
            {
                ActifShield();
            }
        }

    }

    

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void TakeDamage(int amount)
    {
        _life -= amount;
        _life = Mathf.Max(0, _life);
        _lifeUI.TakeDamage(amount);
    }

    public void Heal(int amount)
    {
        
        _life += amount;
        _lifeUI.Heal(amount);
    }

    #endregion


    #region Utils

    private void Move()    
    {
       float speed = _speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x +_move.x * speed, transform.position.y - _gravity  + _move.y * speed, 0);
    }

    private void TargetMouse()
    {
        transform.rotation = Quaternion.LookRotation(_look - transform.position, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            gameObject.SetActive(false);
            _lifeUI.TakeDamage(_currentLife);
            _currentLife = 0;
            GameOver();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Health"))
        {
            int different = _life - _currentLife;
            _currentLife = _life;           
            _lifeUI.Heal(different);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ShootEnemy>())
        {
            CalculeLife(collision.GetComponent<ShootEnemy>());
        }

    }

    private void Shoot()
    {
        var shootOne = _poolManager.GetFirstAvailableProjectile();
        shootOne.transform.position = transform.position;
        shootOne.transform.rotation = transform.rotation;
        shootOne.SetActive(true);
    }

    private void CalculeLife(ShootEnemy shoot)
    {
        if (_active == false) 
        {
            int damage = shoot.GetDamage();
            _currentLife -= damage;
            // Met à jour l'UI manuellement
            if (_lifeUI != null)
            {
                for (int i = 0; i < damage; i++)
                {
                    _lifeUI.TakeDamage(1);
                }
            }
            if (_currentLife <= 0)
            {
                gameObject.SetActive(false);
                _currentLife = _life;
                GameOver();
            }
        }
        
    }

    private void GameOver()
    {
        if (_canvasGameOver != null)
        {
            _canvasGameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void UpdateMouseLook()
    {
        
          Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          Vector2 direction = mousePos - transform.position;
          float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
          transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        
    }
    private void DeactiveShield()
    {
              
        _active = false;
        _timeShieldCount = _timeShield;
        _shield.gameObject.SetActive(false);
    }

    private void ActifShield()
    {
        
        _active = true;
        _timeShieldCount = _timeShield;
        _coolDownCount = _coolDown;
        _shield.gameObject.SetActive(true);
    }

    #endregion


    #region Private And Protected

    [SerializeField] private int _life = 4;
    [SerializeField] private PlayerLifeUI _lifeUI;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private PoolManager _poolManager;
    [SerializeField] private float _gravity = 0.01f;
    [SerializeField] private float _fireRate = 0.2f;

    [Header("bouclier")]
    [SerializeField] private GameObject _shield;
    [SerializeField] private float _timeShield = 2f;
    [SerializeField] private float _coolDown = 5;
    [SerializeField] private Image _shieldUI;
    private bool _active = false;
    private float _coolDownCount;
    private float _timeShieldCount;

    [Header("Ecran GameOver")]
    [SerializeField]private GameObject _canvasGameOver;
        
    private Vector2 _move;
    private Vector3 _look;
    private InputPlayer.InputPlayer _playerInput;
    private int _currentLife;
    private bool _isShooting = false;
    private float _nextFireTime = 0f;
    

    #endregion
}
