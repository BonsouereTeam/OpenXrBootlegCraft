using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public XRInteractionManager xrInteractionManager;
    public Bag[] bags;
    private HandAction leftHandAction = new();
    private HandAction rightHandAction = new();
    
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
    }

    // Update is called once per frame
    void Update()
    {
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
}
