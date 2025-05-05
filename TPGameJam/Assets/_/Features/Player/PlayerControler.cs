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
    }

    #endregion



    #region Main Methods

    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _look = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()); // recherche le pointeur de la souris
        _look.z = 0;
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

    #endregion


    #region Private And Protected

    [SerializeField] private int _life = 4;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private PoolManager _poolManager;

    private Vector2 _move;
    private Vector3 _look;
    private InputPlayer.InputPlayer _playerInput;
    private float _speedBase = 0.001f;

    #endregion
}
