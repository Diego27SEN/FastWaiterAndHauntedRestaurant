using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemy : MainEnemy, IEnemyEffect
{
    public GameObject ControllerMesero;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            colission = true;
            ApplyDebuff();
        }

    }
    

    public void MotionEnemy()
    {
        Vector3 dir = (ControllerMesero.transform.position - transform.position).normalized; //: Calcular la direccion desde el enemigo hacia el jugador
        transform.position += dir * speed * Time.deltaTime; //: Mover el enemigo hacia el jugador
    }
    public void Update()
    {
        MotionEnemy();
    }

    public void ApplyDebuff()
    {
        if(colission == true)
        {
            ControllerMesero.GetComponent<ControllerMesero>().MoveSpeed -= 1;
            Destroy(gameObject);
        }
    }
}
