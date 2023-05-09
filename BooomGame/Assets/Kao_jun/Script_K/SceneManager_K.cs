using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_K : MonoBehaviour
{
   public void SceneChange_K(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
;    }
}
