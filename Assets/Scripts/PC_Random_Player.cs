using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;

public class PC_Player : MonoBehaviour {

    public static Collider random_playing()
    {
        int max_random_number = board_to_matrix.valid_moves(board_to_matrix.board).Count;
        int random_number = Random.Range(0, max_random_number);
        position random_position = board_to_matrix.valid_moves(board_to_matrix.board)[random_number];
        string name = board_to_matrix.matrix2board(random_position);

        return GameObject.Find(name).GetComponent<Collider>();
    }

    public static Collider minimax_playing(int[,] actualboard,bool player)
    {
        int[,] board = actualboard;



        return GameObject.Find("").GetComponent<Collider>();
    }


    position minimax()
    {
        return new position(0, 0);
    }
}