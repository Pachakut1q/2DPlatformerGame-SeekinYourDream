using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    private int EndLayer;
    // Start is called before the first frame update
    void Start()
    {
        EndLayer = LayerMask.NameToLayer("End");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == EndLayer)
        {
            GameManager.SetPlayerInEndZone(true);
        }
    }

    void Update()
    {
        if (GameManager.IsPlayerInEndZone())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.SetPlayerInEndZone(false);
                GameManager.LevelComplete();
            }
                
        }
    }
}
