using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bag : MonoBehaviour
{
    public GameObject player;
    public Vector3 position;
    public float angleOffset;
    public Quaternion rotation;
    public GameObject block;
    public int blockCount;
    public TMP_Text text;
    GameObject vrCamera;

    // Start is called before the first frame update
    void Start()
    {
        block.gameObject.transform.localScale.Set(0.2f, 0.2f, 0.2f);

        var cube = transform.Find("Cube");
        cube.GetComponent<Renderer>().material = block.GetComponent<Renderer>().sharedMaterial;

        var cameraTransform = player.transform.Find("Camera Offset").Find("Main Camera");

        vrCamera = cameraTransform.gameObject;
        text.text = blockCount.ToString();
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
            text.text = blockCount.ToString();
            GameObject obj = Instantiate(block);
            obj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            return obj;
        }
        return null;
    }
    /// <summary>
    /// Mets un bloc dans le sac.
    /// </summary>
    public void PutBlock()
    {
        blockCount++;
        text.text = blockCount.ToString();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Hand")
        {
            HandAction handAction;
            if (other.name.Contains("Left"))
            {
                handAction = PlayerController.Instance.getAction(ControllerSide.Left);
            }
            else if (other.name.Contains("Right"))
            {
                handAction = PlayerController.Instance.getAction(ControllerSide.Right);
            }
            else
            {
                return;
            }

            if (handAction.isGrabbing)
            {
                IXRSelectInteractor interactor = handAction.interactor;
                if (interactor.hasSelection) return;

                var block = TakeBlock();
                if (block == null) return;
                
                IXRSelectInteractable interactable = block.GetComponent<XRGrabInteractable>();

                PlayerController.Instance.xrInteractionManager.SelectEnter(interactor, interactable);
            }
        }
    }
}
