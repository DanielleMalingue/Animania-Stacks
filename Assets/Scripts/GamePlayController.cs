using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayController : MonoBehaviour
{

public static GamePlayController instance;

[HideInInspector]
public BoxScript currentBox;

public int scoreIncreaseAmount = 1; // Adjust as needed
private bool hasScored = false;

 public BoxSpawner boxSpawner;
public CameraFollow cameraScript;
private Vector3 initialBoxSpawnerPosition;
 public TextMeshProUGUI scoreText;
private int moveCount = 0;
private int moveCamera = 0;
private int score = 0;
  


 void Awake() {
    if(instance == null);
        instance = this;
    }   
 
    void Start(){
        boxSpawner.SpawnBox(); 
        initialBoxSpawnerPosition = boxSpawner.transform.position;
    }
   

    public void MoveCamera()
    {
        moveCount++;
        if (moveCount == 3)
        {

            moveCamera++;       
            cameraScript.targetPos.y += 3f;
           StartCoroutine(MoveBoxSpawnerSmoothly());
            StartCoroutine(MoveCameraContinuously());
            
        }
    
   }
   public void SpawnNewBox(){
       Invoke("NewBox", .2f);
    }

    void NewBox(){
        boxSpawner.SpawnBox();
    }

    public void RestartGame(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentBox.DropBox();
        }
         
 
    }
        IEnumerator MoveBoxSpawnerSmoothly() {
        float elapsedTime = 0f;
        float moveDuration = 1f; // Adjust this value as needed
        Vector3 targetPosition = initialBoxSpawnerPosition + Vector3.up * 3f; // Move up by 3 units
        Vector3 startPosition = initialBoxSpawnerPosition;

        while (elapsedTime < moveDuration) {
            elapsedTime += Time.deltaTime;
            boxSpawner.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            yield return null;
        }
        }
        IEnumerator MoveCameraContinuously()
{
    while (true) // Continue indefinitely
    {
        yield return new WaitForSeconds(7f); // Adjust interval as needed
        cameraScript.targetPos.y += 2f;
    }
        }

    // Function to update the score text
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // Update the score text with the current score
    }
public void IncreaseScore(int amount)
{
    score += amount;
    UpdateScoreText(); // Update the score text whenever the score changes
    Debug.Log("Score increased by: " + amount + ". Current score: " + score);
}

private void OnCollisionEnter2D(Collision2D collision)
{
    // Check if the object colliding with this platform has a "Box" tag
    if (collision.gameObject.CompareTag("Box") && !hasScored)
    {
        // Increase the score
        IncreaseScore(scoreIncreaseAmount);

        // Prevent multiple scoring from the same collision
        hasScored = true;
    }
    Debug.Log("Collision detected with: " + collision.gameObject.name);
}

    
}
