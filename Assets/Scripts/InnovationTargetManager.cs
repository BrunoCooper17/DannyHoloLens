using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnovationTargetManager : MonoBehaviour {

    public float GazeDistance;
    public bool bEarthFound = false;
    public int TargetsCount = 0;
    public GameObject earth = null;
    public GameObject[] TargetsRef;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(!bEarthFound && (earth = GameObject.Find("Earth (1)")))
        {
            bEarthFound = true;
            TargetsCount = earth.transform.childCount;
            TargetsRef = new GameObject[TargetsCount];
            for(int index = 0; index < TargetsCount; index++)
            {
                TargetsRef[index] = earth.transform.GetChild(index).gameObject;
            }
        }

        Ray gazeRay;

        if (UnityEngine.VR.VRDevice.isPresent)
        {
            gazeRay = new Ray(Camera.main.transform.position + (Camera.main.nearClipPlane * Camera.main.transform.forward),
                Camera.main.transform.forward);
        }
        else
        {
            //gazeRay = Camera.main.ScreenPointToRay(InputRouter.Instance.XamlMousePosition);
            //gazeRay.origin += (Camera.main.nearClipPlane * gazeRay.direction);
            gazeRay = new Ray(Camera.main.transform.position + (Camera.main.nearClipPlane * Camera.main.transform.forward),
                Camera.main.transform.forward);
        }

        RaycastHit info;
        if (Physics.Raycast(gazeRay, out info))
        {
            InnovationTarget tmpTarget = info.collider.gameObject.GetComponent<InnovationTarget>();
            if(tmpTarget)
            {
                tmpTarget.OnGazeSelect();
            }

            for(int index = 0; index < TargetsCount; index++)
            {
                if(info.collider.gameObject != TargetsRef[index])
                {
                    TargetsRef[index].GetComponent<InnovationTarget>().OnGazeDeselect();
                }
            }
        }
    }
}
