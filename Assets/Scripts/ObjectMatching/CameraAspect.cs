using UnityEngine;

public class CameraAspect : MonoBehaviour
{
	void Start ()
    {
        //GetComponent<Camera>().aspect = 16 / 10f;
        //Screen.sleepTimeout = SleepTimeout.NeverSleep;

        var sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale =new Vector3(1, 1, 1);

        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;

        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3((float)worldScreenWidth / width, (float)worldScreenHeight / height,0f);
    }
}