using UnityEngine;

namespace Yashlan.controller
{
    #region untuk problem ke 7
    public class BoxController : MonoBehaviour 
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.name == "circle")
            {
                CircleController.Instance.Score++;
                gameObject.SetActive(false);
            }
        }
    }
    #endregion
}