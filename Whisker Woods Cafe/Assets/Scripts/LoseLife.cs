using UnityEngine;

public class LoseLife : MonoBehaviour
{
  [SerializeField] private CakeTowerManager cakeTowerManager;

  private void OnTriggerEnter2D(Collider2D collision) {
    cakeTowerManager.RemoveLife();
  }
}
