using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public Enemy[] enemyScripts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            foreach (Enemy a in enemyScripts)
            {
                a.enabled = true;
            }
            Destroy(gameObject);
        }
    }
}
