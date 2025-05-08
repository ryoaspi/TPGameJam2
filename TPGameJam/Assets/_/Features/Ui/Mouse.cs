using UnityEngine;

public class Mouse : MonoBehaviour
{
    #region Api Unity

    private void Start()
    {
        Cursor.SetCursor(_cursor, _hotSpot, _cursorMode);
    }

    #endregion


    #region Private And Protected

    [SerializeField] private Texture2D _cursor;
    private CursorMode _cursorMode = CursorMode.Auto;
    private Vector2 _hotSpot =Vector2.zero;

    #endregion
}
