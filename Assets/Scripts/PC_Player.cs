using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;
using System.Collections.Generic;
using System.IO;

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







    public static Collider minimax_playing(int[,] actualboard,bool player, int depth)
    {
        int[,] board = actualboard;

        

        int value = board_to_matrix.CalculateBoardValue(board);
        Debug.Log("value = " + value);


        List<position> moves = board_to_matrix.valid_moves(board);

        //if (moves.Count<=0) Debug.LogError("Sem movimentos");

        //File.Delete("validmoves.txt");
        //File.AppendAllText("validmoves.txt", "-----------------------\n");
        //File.AppendAllText("validmoves.txt", "+ move: x y \n");


        int newvalue = 0;
        position playthis = moves[0];

        //max
        if (player){
            moves.ForEach(move =>
            {
                File.AppendAllText("validmoves.txt", "+ move:" + move.toString() + "\n");
                newvalue = minimax(board, move, depth,true);
                if (newvalue > value)
                {
                    value = newvalue;
                    playthis = move;
                }
            });
        }

        //min
        else{
            moves.ForEach(move =>
            {
                File.AppendAllText("validmoves.txt", "- move:" + move.toString() + "\n");
                newvalue = minimax(board, move, depth,false);
                if (newvalue < value)
                {
                    value = newvalue;
                    playthis = move;
                }
            });
        }

        string name = board_to_matrix.matrix2board(playthis);
        Debug.Log("try " + name);
        return GameObject.Find(name).GetComponent<Collider>();
    }

    

    static int minimax(int[,] actualboard, position move, int depth, bool player)
    {
        /*
        List<position> changes = new List<position>();

        if (board_to_matrix.add(actualboard, board_to_matrix.matrix2board(move), changes, player))
        {
            Debug.Log(move.x + " " + move.y + " new = " + board_to_matrix.CalculateBoardValue(actualboard));
            return board_to_matrix.CalculateBoardValue(actualboard);
        }

        else return 0;
        */

        int value = (int)(Random.Range(-100, 100));
        Debug.Log(move.x + " " + move.y + " new = " + value);
        return value;

       
    }
}