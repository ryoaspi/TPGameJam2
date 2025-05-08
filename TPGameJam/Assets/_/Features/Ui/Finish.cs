using UnityEngine;

public class Finish : MonoBehaviour
{
    #region Api Unity

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Time.timeScale = 0;
            _credit.gameObject.SetActive(true);
        }
    }
    #endregion


    #region Private And Protected

    [SerializeField] private Canvas _credit;
        
    #endregion
}
