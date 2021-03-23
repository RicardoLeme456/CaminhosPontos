using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    //Declarando uma variável para o objeto a ser percorrido pelos pontos usando um array
    public GameObject[] waypoints;
    //Colocou uma inicial vazio para dar um start
    int currentWP = 0;

    //Velocidade a qual o objeto vai se movimentar
    float speed = 5.0f;
    //A aproximação a qual o objeto vai ter em relação aos pontos
    float accuracy = 1.0f;
    //A movimentação a qual ela vai virar em relação ao meu objeto
    float rotSpeed = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        //O objeto se identifique no posicionamento que ele tá para que ele possa traçar o caminho
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }

    //É chamado depois que todos os métodos Updates são processados. É o último método a ser chamado
    void LateUpdate()
    {
        //Esquema de como ele faria o trajeto a partir do ponto inicial até o ponto final
        if (waypoints.Length == 0) return;
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x, this.transform.position.y, waypoints[currentWP].transform.position.z);
        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

        //O calculo do trajeto a ser percorrido pelos waypoints do ponto inicial até o ponto final
        if (direction.magnitude < accuracy)
        {
            currentWP++;
            if (currentWP >= waypoints.Length)
            {
               currentWP = 0;
            }
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
