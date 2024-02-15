using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUIScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text HPBarText;
    [SerializeField] private TMP_Text HPNumberText;
    [SerializeField] private float playerHPpercent;
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
            playerHPpercent = (float)player.GetComponent<PlayerScript>().GetCurrentHealth() / (float)player.GetComponent<PlayerScript>().GetMaxHealth();
            if (playerHPpercent > .9f && playerHPpercent < 1.0f)
            {
                playerHPpercent = .9f;
            }
            if (playerHPpercent > 0) { HPBarText.text = new string('|', (int)Mathf.Ceil(36 * playerHPpercent)); }
            HPNumberText.text = string.Format("{0}/{1}", player.GetComponent<PlayerScript>().GetCurrentHealth(), player.GetComponent<PlayerScript>().GetMaxHealth());
        }
    }
}
