
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    #region Main Methods
    public void RestartButton()
    {
        
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        if (!string.IsNullOrEmpty(_nameScene))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(_nameScene);
        }
        else Debug.Log("le nom de la scène du menu n'est pas défini");
    }
    
    #endregion

    #region Private And Protected

    [SerializeField] private string _nameScene;

    #endregion
}
