using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] mace = new GameObject[5];
    Vector2[] macePos = new Vector2[5];
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject player;
    [SerializeField] Transform[] respawnPoint = new Transform[5];
    public static GameManager instance;
    [SerializeField] Animator playerAnim;
    [SerializeField] private float respawnTime;
    private Camera2DFollow cameraFollow;

    private float respawnTimeStart;

    private bool respawn;

    private void Start()
    {
        for(int i = 0; i <= mace.Length - 1; i++)
        {
            macePos[i] = mace[i].transform.position;
        }

        cameraFollow = GameObject.Find("Main Camera").GetComponent<Camera2DFollow>();

        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        CheckRespawn();
        ShowGameOverUI();
    }

    public void ResetStageWhole()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DialogeEditor.howManySentences = 0;
        DialogeEditor.inConversation = false;
        BasicEnemyController.enemyAttack = false;
        Coin.coinCounter = 0;
        player.SetActive(true);
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }

    public void CheckRespawn()
    {
        if (Time.time >= respawnTimeStart + respawnTime && respawn == true)
        {
            if (cameraFollow.enabled == false)
            {
                cameraFollow.enabled = true;
            }
            playerAnim.SetBool("Dead", false);
            DialogeEditor.inConversation = false;
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            var playerTemp = Instantiate(player, respawnPoint[Checkpoint.checkpointNumber].position, respawnPoint[Checkpoint.checkpointNumber].rotation);
            cameraFollow.target = playerTemp.transform.Find("CameraPlacement").transform;
            respawn = false;

            for(int i = 0; i <= mace.Length - 1; i++)
            {
               mace[i].GetComponent<Rigidbody2D>().gravityScale = 0f;
                mace[i].transform.position = macePos[i];
            }
        }   
    }

    public void UpdateCoinValue()
    {
        Coin.coinCounter++;
    }

    public void ShowGameOverUI()
    {
        if(Input.GetKeyDown("escape") && gameOverUI.activeSelf == false)
        {
            gameOverUI.SetActive(true);
            DialogeEditor.inConversation = true;
            player.GetComponent<PlayerCombatController>().enabled = false;
            player.GetComponent<Rigidbody2D>().isKinematic = true;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
            player.GetComponent<PlayerStats>().enabled = false;
        }

        if (Input.GetKeyDown("escape") && gameOverUI.activeSelf == true)
        {
            gameOverUI.SetActive(false);
            DialogeEditor.inConversation = false;
            player.GetComponent<PlayerCombatController>().enabled = true;
            player.GetComponent<Rigidbody2D>().isKinematic = false;
            player.GetComponent<PlayerStats>().enabled = true;
        }
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
