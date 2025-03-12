using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_Character : MonoBehaviour
{
    // Update is called once per frame
    float positionX;
    public int lives = 10;
    public int points = 0;
    [SerializeField] float speed;
    [SerializeField] private TextMeshProUGUI info;
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        positionX += direction * Time.deltaTime * speed;
        transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
        if (Mathf.Abs(positionX) > Constants.WORLD_BOUNDARY_X)
        {
            positionX *= -0.98f;
            transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
        if (other.gameObject.CompareTag("Evil"))
        {
            lives--;
            if (lives <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else
        {
            ++points;
            info.SetText("Poäng nu: " + points);
        }
    }
}
