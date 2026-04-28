using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    public Transform target;


    void Update()
    {

        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z);
    }
}
