using System;
using UnityEngine;

public class EnemyScore : MonoBehaviour
{
    #region Publics
    
    public EnemyScore m_instance;
    
   
    #endregion
    
    
    #region Api

    private void Awake()
    {
        m_instance = this;
    }

    #endregion
    
    
    #region Main Method

    public void AddScore(int scoreValue)
    {
        _score += scoreValue;
        Debug.Log($"Score Added: {scoreValue}");
    }
    
    #endregion
    
    
    #region Private And Protected
    
    public int _score = 0;

    #endregion

}
