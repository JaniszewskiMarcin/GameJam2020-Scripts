using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnBrainDestroy : MonoBehaviour
{
    [SerializeField] SpriteRenderer brainDamage;
    [SerializeField] GameObject boss;

    private void Update()
    {
        if(boss == null)
        {
            EndGame();
        }
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(brainDamage.GetComponent<SpriteRenderer>().enabled != true)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                brainDamage.GetComponent<SpriteRenderer>().enabled = true;
                EndGame();
            }
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
