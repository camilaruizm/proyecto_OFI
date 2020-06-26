using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioeSCENA : MonoBehaviour
{
    public void bChange(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
    
}
