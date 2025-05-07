using UnityEngine;

public class Health : MonoBehaviour
{
    #region Api Untiy

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    #endregion
}
