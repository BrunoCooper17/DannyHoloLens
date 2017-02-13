using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnovationTargetManager : MonoBehaviour {

    public float GazeDistance;
    public bool bEarthFound = false, bEarthRealFound = false;
    public int TargetsCount = 0;
    public int TargetsRealCount = 0;
    public GameObject earth = null;
    public GameObject earthReal = null;
    public GameObject[] TargetsRef;
    public GameObject[] TargetsRealRef;
    public GazeSelection GazeTmp;

    // Use this for initialization
    void Start () {
        GazeTmp = GetComponent<GazeSelection>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!((TransitionManager.Instance == null || (!TransitionManager.Instance.InTransition && !TransitionManager.Instance.IsIntro)) &&     // in the middle of a scene transition or if it is the intro, prevent gaze selection
            (GazeTmp.placementControl == null || !GazeTmp.placementControl.IsHolding)))                                                                       // the cube is being placed, prevent gaze selection
        {
            return;
        }

        if (!bEarthFound && (earth = GameObject.Find("Earth (1)")))
        {
            bEarthFound = true;
            TargetsCount = earth.transform.childCount;
            TargetsRef = new GameObject[TargetsCount];
            for(int index = 0; index < TargetsCount; index++)
            {
                TargetsRef[index] = earth.transform.GetChild(index).gameObject;
            }
        }

        if (!bEarthRealFound && (earthReal = GameObject.Find("EarthUpClose")))
        {
            bEarthRealFound = true;
            TargetsRealCount = earthReal.transform.childCount - 3;
            TargetsRealRef = new GameObject[TargetsRealCount];
            for (int index = 0; index < TargetsRealCount; index++)
            {
                TargetsRealRef[index] = earthReal.transform.GetChild(index+3).gameObject;
            }
        }

        if (!bEarthFound || !bEarthRealFound)
        {
            return;
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
                tmpTarget.StopPlanet();
            }

            int contStart = 0;

            {
                int contador = 0;
                for (int index = 0; index < TargetsCount; index++)
                {
                    if (info.collider.gameObject != TargetsRef[index])
                    {
                        TargetsRef[index].GetComponent<InnovationTarget>().OnGazeDeselect();
                        contador++;
                    }
                }

                if(contador == TargetsCount)
                {
                    contStart++;
                }
            }

            {
                int contador = 0;
                for (int index = 0; index < TargetsRealCount; index++)
                {
                    if (info.collider.gameObject != TargetsRealRef[index])
                    {
                        TargetsRealRef[index].GetComponent<InnovationTarget>().OnGazeDeselect();
                        contador++;
                    }
                }

                if (contador == TargetsRealCount)
                {
                    contStart++;
                }
            }

            if(contStart == 2)
            {
                TargetsRef[0].GetComponent<InnovationTarget>().StartPlanet();
                TargetsRealRef[0].GetComponent<InnovationTarget>().StartPlanet();
            }
        }
    }
}
