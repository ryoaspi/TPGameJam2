using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    #region Api Unity
    void Start()
    {
        
    }

    
    void Update()
    {
        _timer += Time.deltaTime;

    }

    #endregion


    #region Private And Protected

    private float _timer;
    [SerializeField] private TMP_Text _currentTimer;

    #endregion
}
