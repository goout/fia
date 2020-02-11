using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBehavior : MonoBehaviour
{

     [SerializeField] private GameObject player;
     [SerializeField] private float yBottomBorder = -9f; 
     public float xLeftBorder = -9.35f;
     public float xRightBorder = 9f; 
     private float yOffset;
     private float xOffset;

     private float deathBorder = -30f;

    void Start()
    {
        yOffset = transform.position.y - player.transform.position.y;
        xOffset = transform.position.x - player.transform.position.x;
    }

    void LateUpdate()
    {
        if (player != null){
            if (player.transform.position.y > yBottomBorder) {
                transform.position = new Vector3(transform.position.x, player.transform.position.y + yOffset, transform.position.z);
            } 
            if (player.transform.position.y < deathBorder) {
                Destroy(player);
                SceneManager.LoadScene(0);
            }
            if (player.transform.position.x > xLeftBorder && player.transform.position.x < xRightBorder) {
                transform.position = new Vector3(player.transform.position.x + xOffset, transform.position.y, transform.position.z);
            }
        } 

    }
}
