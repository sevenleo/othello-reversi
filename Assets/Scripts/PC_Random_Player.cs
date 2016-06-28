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

        Collider collided = GameObject.Find(name).GetComponent<Collider>();
        
        return collided;
    }







    public static Collider minimax_playing(int[,] actualboard,bool player)
    {
        int[,] board = actualboard;

        int newvalue = 0;
        position playthis=new position(-1,-1);

        int value = board_to_matrix.CalculateBoardValue(board);
        Debug.Log("value = " + value);


        List<position> moves = board_to_matrix.valid_moves(board);

        //max
        if (player)
        {
            moves.ForEach(move =>
            {
                newvalue = minimax(board, move, true);
                if (newvalue > value)
                {
                    value = newvalue;
                    playthis = move;
                }
            });
        }

        //min
        else
        {
            moves.ForEach(move =>
            {
                newvalue = minimax(board, move, false);
                if (newvalue < value)
                {
                    value = newvalue;
                    playthis = move;
                }
            });
        }

        string name = board_to_matrix.matrix2board(playthis);
        return GameObject.Find(name).GetComponent<Collider>();
    }

    
    static int minimax(int[,] actualboard, position move,bool player)
    {
        List<position> changes = new List<position>()  ;
        if (board_to_matrix.add(actualboard, board_to_matrix.matrix2board(move), changes, player))
        {
            Debug.Log(move.x + " " + move.y + " new = " + board_to_matrix.CalculateBoardValue(actualboard));
            return board_to_matrix.CalculateBoardValue(actualboard);
        }
        else return 0;
       
    }
}