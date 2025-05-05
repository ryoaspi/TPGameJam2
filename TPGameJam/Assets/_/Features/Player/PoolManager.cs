using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PoolManager : MonoBehaviour
{
    #region Api

    private void Awake()
    {
        GetFirstAvailableProjectile();
    }



    void Start()
    {
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
        foreach (var instance in _poolContener)
        {
            if (instance.activeSelf == false) return instance;
        }

        // No available instance
        var newInstance = Instantiate(_projectilePrefab, transform);
        newInstance.SetActive(false);
        _poolContener.Add(newInstance);
        return newInstance;

    }


    #endregion


    #region Utils



    #endregion


    #region Private And Protected

    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private List<GameObject> _poolContener = new List<GameObject>();
    [SerializeField] private int _poolsize;

    #endregion
}