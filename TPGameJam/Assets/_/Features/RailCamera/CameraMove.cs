using UnityEngine;

public class CameraMove : MonoBehaviour
{
    #region Api Unity

    void Start()
    {
        
    }

    
    void Update()
    {
        Move();
    }

    #endregion


    #region Utils

    private void Move()
    {
     
    }

    #endregion


    #region Private And Protected

    [SerializeField] private Transform[] _waypoints;
    

    #endregion
}
