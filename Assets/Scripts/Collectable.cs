using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour 
{
    public const int CASH_INCREASE = 100;
    public const int BULLETS_INCREASE = 30;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (name == "Cash")
            {
                collision.gameObject.SendMessage("AddCash", CASH_INCREASE);
                DestroyCollectable();
            }
            else if (name == "Bullets")
            {
                collision.gameObject.SendMessage("AddBullets", BULLETS_INCREASE);
                DestroyCollectable();
            }
        }
    }

    private void DestroyCollectable()
    {
        audio.Play();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        Destroy(this.gameObject, 1.0f);
    }
}
