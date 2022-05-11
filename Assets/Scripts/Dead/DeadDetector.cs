using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadDetector : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
        Respawn.Instance.ShowTextRespawn();
    }

}
