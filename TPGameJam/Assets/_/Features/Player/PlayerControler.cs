using UnityEngine;
using UnityEngine.InputSystem;

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
    }
    private void OnDisable()
    {
        _playerInput.Disable();
    }

    
    void Update()
    {        
        Move();
        TargetMouse();
        UpdateMouseLook();
    }

    #endregion



    #region Main Methods

    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();        
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            var shootOne = _poolManager.GetFirstAvailableProjectile();
            shootOne.transform.position = transform.position;
            shootOne.transform.rotation = transform.rotation;
            shootOne.SetActive(true);
        }

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        
    }

    #endregion


    #region Utils

    private void Move()    
    {
       float speed = _speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + _speedBase +_move.x * speed, transform.position.y + _move.y * speed, 0);
    }

    private void TargetMouse()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _look - transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            gameObject.SetActive(false);
        }
    }

    private void CalculeLife()
    {
        if (_life <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void UpdateMouseLook()
    {
        
          Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          Vector2 direction = mousePos - transform.position;
          float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
          transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        
    }

    #endregion


    #region Private And Protected

    [SerializeField] private int _life = 4;
    [SerializeField] private float _speed = 1f;
    [SerializeField]private PoolManager _poolManager;
    
    private Vector2 _move;
    private Vector3 _look;
    private InputPlayer.InputPlayer _playerInput;
    private float _speedBase = 0.001f;
    

    #endregion
}
