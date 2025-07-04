using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    private NavMeshAgent nav;
    public Vector3 _dir;
    private Vector3 _nextPos;
    private Rigidbody rb;
    public float gridSize = 0.90f; // Tamanho da grade de movimento
    public float moveSpeed = 5f;
    public float moveDelay = 0.5f; // Tempo entre movimentos
    private bool canMove = true;

    public GameObject _PauseScreen;
    private bool _isPaused;

    [Header("Segments Controller")]
    public playerPigSpawner snakeSegments;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        snakeSegments = GetComponent<playerPigSpawner>();
        rb = GetComponent<Rigidbody>();
        _isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && _dir != Vector3.back)
        {
            _dir = Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S) && _dir != Vector3.forward)
        {
            _dir = Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.A) && _dir != Vector3.right)
        {
            _dir = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && _dir != Vector3.left)
        {
            _dir = Vector3.right;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseManager();
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            StartCoroutine(Move());
        }
    }

    public void PauseManager()
    {
        if (!_isPaused)
        {
            _isPaused = true;
            _PauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _isPaused = false;
            _PauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void rbMove()
    {
        // Guarda a posição anterior da cabeça antes de mover
        Vector3 previousPosition = rb.position;

        // Calcula a nova posição
        _nextPos = previousPosition + _dir * gridSize;

        rb.MovePosition(_nextPos);

        snakeSegments.MoveSegments(previousPosition, gridSize);

    }

    private IEnumerator Move()
    {
        //canMove = false;

        // Guarda a posição anterior da cabeça antes de mover
        Vector3 previousPosition = transform.position + (transform.forward * -gridSize);

        // Calcula a nova posição
        _nextPos = transform.position + _dir * gridSize;

        // Rotaciona a cabeça para olhar na direção do movimento
        transform.rotation = Quaternion.LookRotation(_dir, Vector3.up);

        // Move a cabeça
        //nav.Warp(_nextPos);
        
        
        // Movimento Suave
        nav.SetDestination(_nextPos);

        // Move os segmentos chamando o script SnakeSegments
        snakeSegments.MoveSegments(previousPosition, gridSize);

        yield return new WaitForSeconds(0.9f);
        canMove = true;
    }
}
