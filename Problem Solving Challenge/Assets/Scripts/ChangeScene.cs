using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yashlan.util
{
    #region untuk problem ke 10
    public class ChangeScene : MonoBehaviour
    {
        public void ChangeSceneOnClick(string name) => SceneManager.LoadScene(name);
    }
    #endregion
}
