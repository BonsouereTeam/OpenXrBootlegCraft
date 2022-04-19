using UnityEngine;

public class RaphPhysicalButtonRealtimeGI : MonoBehaviour
{
    /// <summary>
    /// The button component of the button assembly
    /// </summary>
    public Rigidbody button;
    /// <summary>
    /// The force at which the button will try to move back up
    /// </summary>
    public float springForce = 1.0F;
    /// <summary>
    /// The point at which the button will get activated
    /// </summary>
    public float actuationPoint = -0.09F;
    /// <summary>
    /// The point at which a [isToggleButton] = true will go back to when locked
    /// </summary>
    public float lockPoint = -0.06F;

    private ConfigurableJoint joint;
    /// <summary>
    /// is the button currently ON
    /// </summary>
    private bool isActivated = false;
    /// <summary>
    /// is the button in the locked state
    /// </summary>
    private bool isLocked = false;
    /// <summary>
    /// Is the button locked and is about to get pressed down again
    /// </summary>
    private bool isGonnaUnlock = false;
    /// <summary>
    /// Is the [toggledObject] active
    /// </summary>
    public bool isToggledObjectOn;
    /// <summary>
    /// The Object to toggle
    /// </summary>
    public GameObject toggledObject;
    /// <summary>
    /// True if the button does not require do be held down
    /// </summary>
    public bool isToggleButton;

    public GameObject light;

    public bool staysLocked = false;

    void Start()
    {
        joint = button.GetComponent<ConfigurableJoint>();
        isToggledObjectOn = toggledObject.activeSelf;
    }

    void Update()
    {
        //Only send an update if the button was not pressed or was locked in the last frame
        if ((!isActivated || isGonnaUnlock) && button.transform.localPosition.y <= actuationPoint)
        {
            isActivated = true;
            isGonnaUnlock = false;
            if (isToggleButton)
            {
                isLocked = !isLocked;
            }
            toggledObject.SetActive(!isToggledObjectOn);
            button.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            light.SetActive(true);
        }
        //If the button is back above the actuation point, let it be activated a second time
        else if (isLocked && button.transform.localPosition.y >= actuationPoint)
        {
            if (!staysLocked)
            {
                isGonnaUnlock = true;
            }
            
        }
        //Only send an update if the button was pressed in the last frame and wasn't locked
        else if (isActivated && button.transform.localPosition.y > actuationPoint && !isLocked)
        {
            isActivated = false;
            toggledObject.SetActive(isToggledObjectOn);
            button.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            light.SetActive(false);
        }

        //Spring the button back up to the lock point or the origin
        if (isLocked ? button.transform.localPosition.y < lockPoint : button.transform.localPosition.y < 0.0F)
        {
            button.AddRelativeForce(Vector3.up * springForce, ForceMode.Force);
        }

        //Clamp button position to lock point or the origin
        button.transform.localPosition = new Vector3(0.0F, Mathf.Clamp(button.transform.localPosition.y, -joint.linearLimit.limit, isLocked ? lockPoint : 0.0F), 0.0F);


    }

}
