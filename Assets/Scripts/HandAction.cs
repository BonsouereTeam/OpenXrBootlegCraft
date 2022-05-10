using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAction
{
    public bool isGrabbing;
    public float grab;
    public bool isPressingBumper;
    public float pressBumper;
    /// <summary>
    /// A ou X
    /// </summary>
    public bool isPressingFirstButton;
    /// <summary>
    /// B ou Y
    /// </summary>
    public bool isPressingSecondButton;

    public XRRayInteractor interactor;

    public HandAction()
    {
        isGrabbing = false;
        grab = 0;
        isPressingBumper = false;
        pressBumper = 0;
        isPressingFirstButton = false;
        isPressingSecondButton = false;
    }
}
