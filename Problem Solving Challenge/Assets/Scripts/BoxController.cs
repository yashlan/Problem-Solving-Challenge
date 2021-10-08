using UnityEngine;
using Yashlan.manage;

namespace Yashlan.controller
{
    #region untuk problem ke 7 dst
    public class BoxController : MonoBehaviour 
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            #region untuk problem ke 8
            if (collision.gameObject.name == "box(Clone)")
            {
                SpawnerManager.Instance.ReSpawnBox(collision.gameObject);
            }
            #endregion
        }
    }
    #endregion
}