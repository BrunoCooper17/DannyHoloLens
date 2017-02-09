using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnovationTarget : GazeSelectionTarget {

    public bool bIsSelected = false;

    public MeshRenderer materialBarra;
    public MeshRenderer materialPunta;

    public ConstantRotateAxis rotateComponent;

    // Use this for initialization
    void Start () {
        rotateComponent = GetComponentInParent<ConstantRotateAxis>();
	}

    // 4F 28 4B
    public override void OnGazeSelect()
    {
        if(bIsSelected == false)
        {
            Debug.Log("HAZE INNOVATION TARGET " + name);

            Color tmpColor = materialBarra.materials[0].color;
            tmpColor.r = 0x4F / 255.0f;
            tmpColor.g = 0x28 / 255.0f;
            tmpColor.b = 0x4B / 255.0f;
            materialBarra.materials[0].color = tmpColor;
            materialPunta.materials[0].color = tmpColor;

            rotateComponent.speed = 0;
        }
        bIsSelected = true;
    }

    // 4F 28 4B
    public override void OnGazeDeselect()
    {
        if (bIsSelected == true)
        {
            Debug.Log("EXIT HAZE INNOVATION TARGET " + name);

            Color tmpColor = materialBarra.materials[0].color;
            tmpColor.r = 228 / 255.0f;
            tmpColor.g = 20 / 255.0f;
            tmpColor.b = 49 / 255.0f;
            materialBarra.materials[0].color = tmpColor;
            materialPunta.materials[0].color = tmpColor;

            rotateComponent.speed = -10;
        }
        bIsSelected = false;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
