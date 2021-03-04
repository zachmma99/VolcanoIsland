using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float shootSpeed;   
     private Player player;
    private Enemy enemy;
    public GameObject hitEffect;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        direction = player.transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 shootDir = direction + new Vector3(0f, 0f, 0f);
        this.transform.Translate(shootDir * shootSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //Instantiate(hitEffect, this.transform.position, Quaternion.identity);

            GameObject.Destroy(this.gameObject);
        }
        if (other.tag == "Ground")
        {
            Instantiate(hitEffect, this.transform.position, Quaternion.identity);

            GameObject.Destroy(this.gameObject);
        }
    }
}
