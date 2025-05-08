using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerLifeUI : MonoBehaviour
{
    #region Api Unity
    void Start()
    {        
        _currentLife = _maxLife;        
        GenerateLifeUI();
    }

    private void Update()
    {
        UpdateLifeDisplay();
    }

    #endregion


    #region Main Methods

    public void SetInitialLife(int maxLife)
    {
        _maxLife = maxLife;
        _currentLife = maxLife;
        _lifeRects.Clear();

        foreach (Transform child in transform)
            Destroy(child.gameObject);

        GenerateLifeUI();
        UpdateLifeDisplay();
    }

    public void TakeDamage(int amount)
    {
        _currentLife -= amount;
        _currentLife = Mathf.Max(0, _currentLife);
        UpdateLifeDisplay();

    }

    public void Heal(int amount)
    {
        _currentLife += amount;
        _currentLife = Mathf.Min(_maxLife, _currentLife);
        UpdateLifeDisplay();

    }
    
    #endregion
    
    
    #region Utils

    private void GenerateLifeUI()
    {
        for (int i = 0; i < _maxLife; i++)
        {
            GameObject rect = Instantiate(_lifeRectPrefab,transform);
            Image img = rect.GetComponent<Image>();
            if (img != null) _lifeRects.Add(img);
        }
    }

    private void UpdateLifeDisplay()
    {
        for (int i = 0; i < _lifeRects.Count; i++)
        {            
            _lifeRects[i].enabled = i < _currentLife;
        }
    }
    
    #endregion
    
    
    #region Private And Protected

    [SerializeField] private GameObject _lifeRectPrefab;
    [SerializeField] private int _maxLife = 3;    
    private List<Image> _lifeRects = new List<Image>();
    private int _currentLife;


    #endregion
}
