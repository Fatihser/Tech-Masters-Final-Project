using UnityEngine;
using UnityEngine.UI;

public class tpWriterPos : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Image tex;
    void FixedUpdate()
    {
        tex.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }   
}
