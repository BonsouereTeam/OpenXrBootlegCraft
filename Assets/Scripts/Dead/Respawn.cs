using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public static Respawn Instance { get; private set; }
    public float coolDownShowTextRespawn = 30;
    public GameObject respawnText;

    public void Awake()
    {
        DontDestroyOnLoad(this);
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void ShowTextRespawn()
    {
        respawnText.SetActive(true);
        StartCoroutine(CoolDown());
    }

    public IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDownShowTextRespawn);
        respawnText.SetActive(false);
    }
}
