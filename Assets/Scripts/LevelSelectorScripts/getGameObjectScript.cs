using UnityEngine;

public class getGameObjectScript : MonoBehaviour
{
    public GameObject game_object;

    private void Awake() {
        game_object = this.gameObject;
    }
}
