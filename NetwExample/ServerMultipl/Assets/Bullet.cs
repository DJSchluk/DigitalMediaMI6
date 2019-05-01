using System.Collections;
using UnityEngine;



public class Bullet : MonoBehaviour {
    /*void OnCollisionEnter()
    {
        Destroy(gameObject);
    }*/
        

    public void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var healt = hit.GetComponent<Health>();
        if(healt != null)
        {
            healt.TakeDamage(10);
        }
        Destroy(gameObject);
    }
}
