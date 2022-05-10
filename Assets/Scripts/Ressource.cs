using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressource : MonoBehaviour
{
    public GameObject RessourcePrefab;
    private int blockNumber;
    private bool isTouching;

    // Start is called before the first frame update
    void Start()
    {
        blockNumber = Random.Range(1,3);
        isTouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Paxel")
        {
            if (isTouching) return;

            isTouching = true;
            blockNumber--;
            PlayerController.Instance.GetBlock(RessourcePrefab);
            if (blockNumber == 0)
            {
                Destroy(this.transform.parent.gameObject);
                Destroy(this);
                return;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Paxel")
        {
            isTouching = false;
        }
    }
}
