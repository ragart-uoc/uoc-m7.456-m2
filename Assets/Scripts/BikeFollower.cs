using UnityEngine;

public class BikeFollower : MonoBehaviour
{
    public Transform bike;

    private Vector3 offset;

    private void Start()
    {
        offset = bike.transform.position - transform.position;
    }

    private void Update()
    {
        transform.position = bike.position - offset;
    }
}