using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PoolManager : MonoBehaviour
{
    #region Api

    private void Awake()
    {
        for (int i = 0; i < _poolsize; i++)
        {
            var instance = Instantiate(_projectilePrefab, transform);
            instance.SetActive(false);
            _poolContener.Add(instance);
        }
        
    }



    void Start()
    {
        GetFirstAvailableProjectile();
    }

    #endregion


    #region Main Method

    public GameObject GetFirstAvailableProjectile()
    {
        foreach (var instance in _poolContener)
        {
            if (instance.activeSelf == false)
            {
                return instance;
            } 
            

        }

        // No available instance
        var newInstance = Instantiate(_projectilePrefab, transform);
        newInstance.SetActive(false);
        _poolContener.Add(newInstance);
        return newInstance;

    }


    #endregion


    #region Private And Protected

    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private List<GameObject> _poolContener = new List<GameObject>();
    [SerializeField] private int _poolsize = 10;

    #endregion
}