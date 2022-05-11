using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public XRInteractionManager xrInteractionManager;
    public Bag[] bags;
    private HandAction leftHandAction = new();
    private HandAction rightHandAction = new();
    /*[SerializeField] private InputActionReference jumpActionReference;
    private CharacterController controller;
    private Vector3 velocite;
    private float hauteurSaut = 1.0f;
    private float gravite = -9.81f;*/


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       // controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }
    public HandAction getAction(ControllerSide side)
    {
        switch (side)
        {
            case ControllerSide.Left:
                return leftHandAction;
            case ControllerSide.Right:
                return rightHandAction;
        }
        return null;
    }

    public void GetBlock(GameObject block)
    {
        foreach(Bag bag in bags)
        {
            if (bag.block.tag == block.tag)
            {
                bag.PutBlock();
                break;
            }
        }
    }

    private void Jump()
    {
       /* if (velocite.y < 0) velocite.y = 0f;

        if (jumpActionReference.action.triggered)
        {
            velocite.y += Mathf.Sqrt(hauteurSaut * -3.0f * gravite);
        }

        velocite.y += gravite * Time.deltaTime;
        controller.Move(velocite * Time.deltaTime);*/
    }
}
