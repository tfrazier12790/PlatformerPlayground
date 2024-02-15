using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    [SerializeField] private float startingPos;
    [SerializeField] private float length;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxSpeed;
    [SerializeField] private GameObject gameController;

    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
        startingPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void Update()
    {
        if(!gameController.GetComponent<GameController>().GetPaused())
        {
            float distance = (cam.transform.position.x * parallaxSpeed);

            transform.position = new Vector3(startingPos + distance, transform.position.y, transform.position.z);
        }
    }
}
