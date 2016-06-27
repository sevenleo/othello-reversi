using UnityEngine;
using System.Collections;

public class ChangeCameras : MonoBehaviour
{
    
    public Camera camera3d45;
    public Camera camera2d;
    public Camera cameratop;
    public Camera cameraclose;


    void Start()
    {
        camera3d45.enabled = true;
        camera2d.enabled = false;
        cameratop.enabled = false;
        cameraclose.enabled = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            st_3rd();
        }

        if (!cameraclose.enabled && !camera2d.enabled && Input.GetKeyDown(KeyCode.V))
        {
            top_3rd();
        }


        if (!cameratop.enabled && !camera2d.enabled)
        {

            if (Input.GetKeyDown(KeyCode.R))
            {
                lookbackin();
            }

            else if (Input.GetKeyUp(KeyCode.R))
            {
                lookbackout();
            }
        }
    }

    public void top_3rd()
    {
        camera3d45.enabled = !camera3d45.enabled;
        cameratop.enabled = !cameratop.enabled;

    }


    public void st_3rd()
    {
        camera3d45.enabled = !camera3d45.enabled;
        camera2d.enabled = !camera2d.enabled;
    }


    public void lookbackin()
    {
        camera3d45.enabled = false;
        cameraclose.enabled = true;

    }


    public void lookbackout()
    {
        cameraclose.enabled = false;
        camera3d45.enabled = true;
       
    }
}
