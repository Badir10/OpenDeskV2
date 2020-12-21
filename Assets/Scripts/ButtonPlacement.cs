using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonPlacement : MonoBehaviour
{
    public PositionController _positionController;
    public List<GameObject> _buttonAnchors;

    public GameObject musicButtons;
    public GameObject skyboxButtons;
    public GameObject surroundingButtons;

    public Vector3 musicPos;
    public Vector3 anchorPos;
    
    void Awake()
    {
        _positionController = GameObject.Find("OVRCameraRig").GetComponent<PositionController>();
        _buttonAnchors = _positionController.buttonAnchors;
        
        musicPos = musicButtons.transform.position;
        anchorPos = _buttonAnchors[0].transform.position;
    }

    private void Start()
    {
        
    }

    void Update()
    {
        musicButtons.transform.localPosition = _buttonAnchors[0].transform.position;
        skyboxButtons.transform.localPosition = _buttonAnchors[1].transform.position;
    }
}
