using System.Collections;
using System.IO;
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

    [SerializeField]
    private Transform pongDoorPrefab = null;

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
    public GameController gameController;
    private string playercollidertag;

    // Start is called before the first frame update
    void Awake()
    {
        var maze = MazeGenerator.Generate(width, height);
        Draw(maze, width, height);
        gameController.LoadPlayerPos();
        gameController.LoadEnemyPos();
        gameController.LoadScore();

        bool xCheckP = GameController.playerPos.x < 2.3f && GameController.playerPos.x > -2.3f;
        bool yCheckP = GameController.playerPos.y < 2.3f && GameController.playerPos.y > -2.3f;
        bool zCheckP = GameController.playerPos.z < 2.3f && GameController.playerPos.z > -2.3f;

        bool xCheckE = GameController.enemyPos.x < 2.3f && GameController.playerPos.x > -2.3f;
        bool yCheckE = GameController.enemyPos.y < 2.3f && GameController.playerPos.y > -2.3f;
        bool zCheckE = GameController.enemyPos.z < 2.3f && GameController.playerPos.z > -2.3f;

        if (GameController.playerPos != null && xCheckP && yCheckP && zCheckP)
        {
            Instantiate(player, GameController.playerPos, Quaternion.identity);
        } else
        {
            Instantiate(player, startCoords, Quaternion.identity);
        }

        Instantiate(goalpost, endCoords, Quaternion.identity);

        if (GameController.enemyPos != null && xCheckE && yCheckE && zCheckE)
        {
            StartCoroutine(SpawnEnemy(GameController.enemyPos));
        }
        else
        {
            StartCoroutine(SpawnEnemy(endCoords));
        }

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

                if (cell.HasFlag(WallState.PONGDOOR))
                {
                    var pongDoor = Instantiate(pongDoorPrefab, transform) as Transform;
                    pongDoor.position = position + new Vector3(0, 0.4f, size / 2);
                    pongDoor.localScale = new Vector3(size, pongDoor.localScale.y, pongDoor.localScale.z);
                } else
                {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null) {
            playercollidertag = GameObject.FindWithTag("Player").GetComponent<OnTriggerStayEvent>().getColTag();
        }
        
        if (playercollidertag == "Goal")
        {
            GameEnd();
        }
    }

    IEnumerator SpawnEnemy(Vector3 coords)
    {

        yield return new WaitForSeconds(1f);
        Instantiate(enemy, coords, Quaternion.identity);

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
        GameObject.FindWithTag("Player").GetComponent<OnTriggerStayEvent>().setColTag("");
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
