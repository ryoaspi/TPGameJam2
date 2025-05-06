using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PoolManager : MonoBehaviour
{
    #region Api

    private void Awake()
    {
        if (_projectilePrefab == null)
        {
            Debug.LogError("Projectile  prefab is not assigned", this);
            return; 
        }
        for (int i = 0; i < _poolsize; i++)
        {
            var instance = Instantiate(_projectilePrefab, transform);
            instance.SetActive(false);
            _poolContener.Add(instance);
        }
        
    }
    
    #endregion


    #region Main Method

    public GameObject GetFirstAvailableProjectile()
    {
        foreach (GameObject instance in _poolContener)
        {
            if (!instance.activeSelf)
            {
                return instance;
            } 
            

        }

        // No available instance
        GameObject newInstance = Instantiate(_projectilePrefab, transform);
        newInstance.SetActive(false);
        _poolContener.Add(newInstance);
        return newInstance;

    }


    #endregion


    #region Private And Protected

    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private int _poolsize = 10;
    
    private List<GameObject> _poolContener = new List<GameObject>();

    #endregion
}