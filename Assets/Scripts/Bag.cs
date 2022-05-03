using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public GameObject player;
    public Vector3 position;
    public float angleOffset;
    public Quaternion rotation;
    public GameObject block;
    public int blockCount;
    GameObject vrCamera;

    // Start is called before the first frame update
    void Start()
    {
        var cube = transform.Find("Cube");
        cube.GetComponent<Renderer>().material = block.GetComponent<Renderer>().sharedMaterial;

        var cameraTransform = player.transform.Find("Camera Offset").Find("Main Camera");

        vrCamera = cameraTransform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = position + vrCamera.transform.position;
        transform.rotation = rotation;

        var cameraTransform = vrCamera.transform;

        var angle = cameraTransform.eulerAngles.y - angleOffset;

        transform.RotateAround(cameraTransform.position, player.transform.up, angle);
    }
    /// <summary>
    /// Prend un bloc dans le sac.
    /// </summary>
    /// <returns>Retourne le bloc si présent. Retourne null si non présent.</returns>
    public GameObject TakeBlock()
    {
        if (blockCount > 0)
        {
            blockCount--;
            return Instantiate(block);
        }
        return null;
    }
    /// <summary>
    /// Mets un bloc dans le sac.
    /// </summary>
    public void PutBlock()
    {
        blockCount++;
    }
}
