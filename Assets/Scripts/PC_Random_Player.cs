using UnityEngine;
using System.Collections;

public class PC_Random_Player : MonoBehaviour {

    public static Collider playing()
    {
        int max_random_number = board_to_matrix.valid_moves().Count;
        int random_number = Random.Range(0, max_random_number);
        position random_position = board_to_matrix.valid_moves()[random_number];
        string name = board_to_matrix.matrix2board(random_position);
        return GameObject.Find(name).GetComponent<Collider>();
    }

}
