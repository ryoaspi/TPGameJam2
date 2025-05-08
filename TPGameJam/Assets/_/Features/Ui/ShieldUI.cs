using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShieldUI : MonoBehaviour
{
    #region Api Unity
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    #endregion


    #region Utils


    #endregion


    #region Private And Protected

    [SerializeField] private GameObject _shieldPrefab;
    private List<Image> _shieldRects = new List<Image>();

    #endregion
}
