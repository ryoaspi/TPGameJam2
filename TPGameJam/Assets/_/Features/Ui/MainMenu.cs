using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Main Methods

    public void StarButton()
    {
        SceneManager.LoadScene(_startScene);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    #endregion


    #region Private And Protected

    [Header("Scene level 1")]
    [SerializeField] private string _startScene;

    #endregion
}
