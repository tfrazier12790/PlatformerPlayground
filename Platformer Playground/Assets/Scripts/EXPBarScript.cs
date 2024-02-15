using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EXPBarScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text expBarText;
    [SerializeField] private TMP_Text expNumberText;
    [SerializeField] private float playerXPpercent;
    [SerializeField] private GameObject gameController;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameController = GameObject.FindWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.GetComponent<GameController>().GetPaused())
        {
            playerXPpercent = (float)player.GetComponent<PlayerScript>().GetExperience() / (float)player.GetComponent<PlayerScript>().GetExpToLevel();
            if (playerXPpercent > .9f && playerXPpercent < 1.0f)
            {
                playerXPpercent = .9f;
            }
            expBarText.text = new string('|', (int)Mathf.Ceil(36 * playerXPpercent));
            expNumberText.text = string.Format("Lv.{0} {1}/{2}", player.GetComponent<PlayerScript>().GetLevel(), player.GetComponent<PlayerScript>().GetExperience(), player.GetComponent<PlayerScript>().GetExpToLevel());
        }
    }
}
