using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    public GameObject John;

    // Update is called once per frame
    void Update()
    {
        if (John != null)
        {
            //Para saber la posicion de John y que lo siga
            Vector3 position = transform.position;
            position.x = John.transform.position.x;
            transform.position = position;
        }
        
    }
}
