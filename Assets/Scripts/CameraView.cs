using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraView : MonoBehaviour {

    public Slider sl;

    void Start()
    {
        sl.value = .3f;
    }

    void LateUpdate ()
    {
        Camera.main.GetComponent<Camera>().orthographicSize = sl.value * 100;
	}
    public void ReLoadLevel()
    {
        SceneManager.LoadScene(0);
    }


}
