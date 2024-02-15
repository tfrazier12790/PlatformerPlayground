using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 lerpPos;
    [SerializeField] private float lerpSpeed = 0.1f;
    [SerializeField] private float lerpDistance = 1.0f;
    [SerializeField] private GameObject gameController;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //xOffset = transform.position.x - player.transform.position.x;
        yOffset = transform.position.y - player.transform.position.y;
        gameController = GameObject.FindWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.GetComponent<GameController>().GetPaused())
        {
            lerpPos = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, -10);
            transform.position = Vector3.Lerp(transform.position, lerpPos, lerpSpeed);
        }
    }
}
