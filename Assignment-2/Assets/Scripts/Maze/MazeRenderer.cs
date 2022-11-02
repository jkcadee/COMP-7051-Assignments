using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    private int width = 5;
    [SerializeField]
    private int height = 5;
    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform wallPrefab = null;

    private Vector3 startCoords;
    private Vector3 endCoords;

    private bool startFlag = false;
    private bool endFlag = false;

    [SerializeField]
    private GameObject player = null;
    public GameObject enemy;

    [SerializeField]
    private GameObject goalpost = null;

    public GameObject popUp;
    private InputActions inputActions;

    // Start is called before the first frame update
    void Awake()
    {
        var maze = MazeGenerator.Generate(width, height);
        Draw(maze, width, height);
        Instantiate(player, startCoords, Quaternion.identity);
        Instantiate(goalpost, endCoords, Quaternion.identity);
        StartCoroutine(SpawnEnemy());
        deActivatePopup();
        inputActions = new InputActions();
    }

    private void Draw(WallState[,] maze, int width, int height)
    {
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                var cell = maze[i, j];
                var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

                if (cell.HasFlag(WallState.ENTRANCE) && !startFlag)
                {
                    startCoords = new Vector3(-width / 2 + i, 0.1f, -height / 2 + j);
                    startFlag = true;
                }
                else if (cell.HasFlag(WallState.EXIT) && !endFlag)
                {
                    endCoords = new Vector3(-width / 2 + i, 0.1f, -height / 2 + j);
                    endFlag = true;
                }

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0.4f, size / 2);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0.4f, 0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if (i == width - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(size / 2, 0.4f, 0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if (j == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, 0.4f, -size / 2);
                        bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);

                    }
                }


            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        string playercollidertag = GameObject.FindWithTag("Player").GetComponent<OnTriggerStayEvent>().getColTag();
        if (playercollidertag == "Goal")
        {
            GameEnd();
        }
    }

    IEnumerator SpawnEnemy()
    {

        yield return new WaitForSeconds(1f);
        Instantiate(enemy, endCoords, Quaternion.identity);

    }

    private void OnEnable()
    {
        inputActions.Player.Reset.performed += ResetPosition;
        inputActions.Player.Reset.Enable();
    }

    private void activatePopup()
    {

        popUp.SetActive(true);

    }

    private void deActivatePopup()
    {

        popUp.SetActive(false);

    }

    void ResetPosition(InputAction.CallbackContext obj)
    {
        ActivateReset();
    }

    public void ActivateReset()
    {
        Debug.Log("Position Reset");
        deActivatePopup();
        GameObject playercharacter = GameObject.FindWithTag("Player");
        playercharacter.GetComponent<Rigidbody>().transform.position = startCoords;
        playercharacter.GetComponent<Rigidbody>().transform.rotation = Quaternion.identity;
        playercharacter.GetComponent<OnTriggerStayEvent>().setColTag("");
        playercharacter.GetComponent<AT_CharMovement>().enabled = true;

        AT_CameraRotate mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<AT_CameraRotate>();
        mainCamera.ResetRotation();

        EnemyLocomotion enemy = GameObject.FindWithTag("Enemy").GetComponent<EnemyLocomotion>();
        enemy.ResetPosition();
    }

    public void DoReset()
    {
        Debug.Log("Reset");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void GameEnd() {
        Debug.Log("Game End");
        activatePopup();
        GameObject.FindWithTag("Player").GetComponent<AT_CharMovement>().enabled = false;
    }
}
