using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// an object that tracks whether it's being pressed down upon
public class PressedDownObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsPressedDown {
        get; private set;
    }

    // Start is called before the first frame update
    void Start()
    {
        IsPressedDown = false;
    }

    public void OnPointerDown(PointerEventData data)
    {
        IsPressedDown = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        IsPressedDown = false;
    }
}
