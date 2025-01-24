using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        print("collision entered");
        if (collision.gameObject.CompareTag("Platform"))
        {
            Destroy(collision.gameObject);
            print($"Destroyed: {collision.gameObject.name}");
        }
    }
}
