using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;
using System.Collections.Generic;

public class PC_Player : MonoBehaviour {

    public static Collider random_playing()
    {
        int max_random_number = board_to_matrix.valid_moves(board_to_matrix.main_board).Count;
        int random_number = Random.Range(0, max_random_number);
        position random_position = board_to_matrix.valid_moves(board_to_matrix.main_board)[random_number];
        string name = board_to_matrix.matrix2board(random_position);

        return GameObject.Find(name).GetComponent<Collider>();
    }

    public static Collider minimax_playing(int[,] actualboard,bool player)
    {
        int[,] board = actualboard;

        int newvalue = 0;
        position playthis;

        int value = board_to_matrix.CalculateBoardValue(board);
        List<position> moves = board_to_matrix.valid_moves(board);

        //max
        moves.ForEach(move => {
            newvalue = minimax(board, move);
            if (newvalue > value)
            {
                value = newvalue;
                playthis = move;
            }
        });
        


        return GameObject.Find("").GetComponent<Collider>();
    }


    static int minimax(int[,] actualboard, position move)
    {
        List<position> changes = new List<position>()  ;
        board_to_matrix.add(actualboard,board_to_matrix.matrix2board(move),changes);

        return 0;
    }
}