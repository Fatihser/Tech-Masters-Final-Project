using UnityEngine;
using TMPro;
using System.Collections;

public class castle : MonoBehaviour
{
    private int enemy = 20;
    [SerializeField] TextMeshProUGUI enemyText;
    public typeWriterEffect tp;

    public SpriteRenderer playerRb;
    public float x = 1;

    public GameObject endCanvas;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player"&&enemy<=0)
        {
            tp.write("Welcome back hero!!!");
            StartCoroutine("fadePlayer");
        }
        else
        {
            //kaleye giremezsin
            tp.write("Dont come back before kill all enemies");
        }
    }

    IEnumerator fadePlayer()
    {
        while(x>0)
        {
            playerRb.color = new Color(255, 255, 255, x);
            x -= 0.08f;
            yield return new WaitForSeconds(0.2f);
        }
        endCanvas.SetActive(true);
    }
        public void updateEnemyText()
    {
        enemy--;
        enemyText.text = "Remaning enemy: " + enemy;
    }
}
