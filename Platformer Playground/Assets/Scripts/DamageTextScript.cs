using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextScript : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float colorLerpSpeed = 1.0f;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject gameController;

    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
    }
    void Update()
    {
        if (!gameController.GetComponent<GameController>().GetPaused())
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), speed * Time.unscaledDeltaTime);
            text.color = Color.Lerp(text.color, Color.clear, colorLerpSpeed * Time.unscaledDeltaTime);
            if (text.color.a < 0.1)
            {
                Destroy(parent);
            }
        }
    }
    public void SetText(int damageNumber)
    {
        text.text = damageNumber.ToString();
    }
}
