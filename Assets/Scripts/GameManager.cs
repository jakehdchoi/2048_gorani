using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] n;

    bool wait;
    int x, y;
    Vector3 firstPos, gap;
    GameObject[,] Square = new GameObject[4, 4];

    void Start()
    {
        Spawn();
        Spawn();
    }

    void Update()
    {
        // 뒤로가기
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            wait = true;
            firstPos = Input.GetMouseButtonDown(0) ? Input.mousePosition : (Vector3)Input.GetTouch(0).position;
        }

        if (Input.GetMouseButton(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            gap = (Input.GetMouseButton(0) ? Input.mousePosition : (Vector3)Input.GetTouch(0).position) - firstPos;
            if (gap.magnitude < 100) return;
            gap.Normalize();

            if (wait)
            {
                wait = false;

                // up
                if (gap.y > 0 && gap.x > -0.5f && gap.x < 0.5f) { }
                // down
                else if (gap.y < 0 && gap.x > -0.5f && gap.x < 0.5f) { }
                // right
                else if (gap.x > 0 && gap.y > -0.5f && gap.y < 0.5f) { }
                // left
                else if (gap.x < 0 && gap.y > -0.5f && gap.y < 0.5f) { }
                else return;

            }
        }
    }

    void Spawn()
    {
        while (true)
        {
            x = Random.Range(0, 4);
            y = Random.Range(0, 4);
            if (Square[x, y] == null) break;
        }
        Square[x, y] = Instantiate(Random.Range(0, 8) > 0 ? n[0] : n[1], new Vector3(1.2f * x - 1.8f, 1.2f * y - 1.8f, 0), Quaternion.identity);
        Square[x, y].GetComponent<Animator>().SetTrigger("Spawn");
    }

    public void Restart()
    {
        // 재시작 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
