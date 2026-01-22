using UnityEngine;
using UnityEngine.Events;

public class MovableNail : MonoBehaviour
{
    private bool _canDrag;
    private float max = -0.136f;
    private float min = -1.841574f;

    [SerializeField] private MenuManager menuManager;

    private void OnMouseDown()
    {
       Debug.Log("_canDrag = true");
        _canDrag = true;
    }
    private void OnMouseUp()
    {
        _canDrag = false;
    }
    private void Update()
    {
        if (_canDrag) 
        {
            if (transform.localPosition.x > max) 
            {
                transform.localPosition = new Vector3(max,0,0);
                return;
            }
            if (transform.localPosition.x < min)
            {
                transform.localPosition = new Vector3(min, 0, 0);
                return;
            }
            transform.position += new Vector3(-Input.GetAxis("Mouse X")/2, 0, 0);
            menuManager.SetVolume();
        }
    }
}
