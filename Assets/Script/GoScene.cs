using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoScene : MonoBehaviour
{
    public void SceneLoader(int index)
    {
        SceneManager.LoadScene(index);
    }
}
