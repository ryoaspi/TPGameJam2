using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region Api Unity
    void Start()
    {
        _timer = 0f;
    }

    
    void Update()
    {
        _timer += Time.deltaTime;
        
        DisplayTime(_timer);

    }

    #endregion
    
    
    #region Utils

    private void DisplayTime(float timeToDisplay)
    {
        //convertir le temps en minutes et secondes
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        
        //afficher le temps sous format "MM : SS""
        _currentTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    #endregion


    #region Private And Protected

    private float _timer;
    [SerializeField] private TMP_Text _currentTimer;

    #endregion
}
