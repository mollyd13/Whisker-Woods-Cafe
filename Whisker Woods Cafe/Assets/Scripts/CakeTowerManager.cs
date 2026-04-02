using UnityEngine;
using System.Collections;

public class CakeTowerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  [SerializeField] private Transform blockPrefab;
  [SerializeField] private Transform blockHolder;
  [SerializeField] private Sprite[] cakeSprites;

  [SerializeField] private TMPro.TextMeshProUGUI livesText;
  [SerializeField] private TMPro.TextMeshProUGUI winText; // Optional, if you want to show "You Win!"
  private int cakesPlaced = 0;
  private int cakesToWin = 6;


  private Transform currentBlock = null;
  private Rigidbody2D currentRigidbody;

  private Vector2 blockStartPosition = new Vector2(0f, 4f);

  private float blockSpeed = 5f;
  private float blockSpeedIncrement = 0.5f;
  private int blockDirection = 1;
  private float xLimit = 5;

  private float timeBetweenRounds = 1f;

  // Variables to handle the game state.
  private int startingLives = 3;
  private int livesRemaining;
  private bool playing = true;

  // Start is called before the first frame update
  void Start() {
    livesRemaining = startingLives;
    livesText.text = $"{livesRemaining}";
    SpawnNewBlock();
  }
  public void CakePlaced()
  {
      if (!playing) return;

      cakesPlaced++;

      if (cakesPlaced >= cakesToWin)
      {
          playing = false;
          winText.text = "You Win!";
          Debug.Log("You Win!");
      }
  }

  private IEnumerator DelayedSpawn() {
    yield return new WaitForSeconds(timeBetweenRounds);
    SpawnNewBlock();

  }

  // Update is called once per frame
  void Update() {
    // If we have a waiting block, move it about.
    if (currentBlock && playing) {
      float moveAmount = Time.deltaTime * blockSpeed * blockDirection;
      currentBlock.position += new Vector3(moveAmount, 0, 0);
      // If we've gone as far as we want, reverse direction.
      if (Mathf.Abs(currentBlock.position.x) > xLimit) {
        // Set it to the limit so it doesn't go further.
        currentBlock.position = new Vector3(blockDirection * xLimit, currentBlock.position.y, 0);
        blockDirection = -blockDirection;
      }

      // If we press space drop the block.
      if (Input.GetKeyDown(KeyCode.Space)) {
        // Stop it moving.
        currentBlock = null;
        // Activate the RigidBody to enable gravity to drop it.
        currentRigidbody.simulated = true;
        // Spawn the next block.
        StartCoroutine(DelayedSpawn());
      }
    }

    // Temporarily assign a key to restart the game.
    if (Input.GetKeyDown(KeyCode.Escape)) {
      UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
  }

  // Called from LoseLife whenever it detects a block has fallen off.
  public void RemoveLife()
  {
    // Update the lives remaining UI element.
    livesRemaining = Mathf.Max(livesRemaining - 1, 0);
    livesText.text = $"{livesRemaining}";
    // Check for end of game.
    if (livesRemaining == 0)
    {
      playing = false;
    }
  }
private void SpawnNewBlock() {
    if (!playing) return;

    currentBlock = Instantiate(blockPrefab, blockHolder);
    currentBlock.position = blockStartPosition;

    SpriteRenderer sr = currentBlock.GetComponent<SpriteRenderer>();
    BoxCollider2D col = currentBlock.GetComponent<BoxCollider2D>();
    currentRigidbody = currentBlock.GetComponent<Rigidbody2D>();

    if (cakeSprites.Length > 0)
    {
        // Pick random cake
        Sprite randomSprite = cakeSprites[Random.Range(0, cakeSprites.Length)];
        sr.sprite = randomSprite;

        // Reset scale so no weird stretching carries over
        currentBlock.localScale = Vector3.one * 2.5f;

        // Match collider EXACTLY to sprite
        col.size = sr.sprite.bounds.size;
        col.offset = sr.sprite.bounds.center;
    }

    blockSpeed += blockSpeedIncrement;
}

}