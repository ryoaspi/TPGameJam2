using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    #region Main Methods
    public void RestartButton()
    {
        SceneManager.LoadScene(_nameScene);
    }
    #endregion

    #region Private And Protected

    [SerializeField] private string _nameScene;

    #endregion
}
