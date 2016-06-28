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
        int[,] board = (int[,])actualboard.Clone();

        int value = board_to_matrix.CalculateBoardValue(board);
        //Debug.Log("value = " + value);


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
                newvalue = minimax(board, move, depth,false);
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

    

    static int minimax(int[,] actualboard, position move, int depth, bool player)
    {
        int[,] board = (int[,])actualboard.Clone();
        int newvalue, value = board_to_matrix.CalculateBoardValue(actualboard);
        List<position> changes = new List<position>();
        List<position> moves;

        if (board_to_matrix.add(board, board_to_matrix.matrix2board(move), changes, player))
        {
            moves = board_to_matrix.valid_moves(board);
            if (depth > 1 && moves.Count > 1) {

                
                position playthis = moves[0];

                //max
                if (player)
                {
                    moves.ForEach(newmove =>
                    {
                        newvalue = minimax(board, newmove, depth-1, player);
                        if (newvalue > value)
                        {
                            value = newvalue;
                            playthis = newmove;
                        }
                    });
                }

                //min
                else
                {
                    moves.ForEach(newmove =>
                    {
                        newvalue = minimax(board, newmove, depth-1, player);
                        if (newvalue < value)
                        {
                            value = newvalue;
                            playthis = newmove;
                        }
                    });
                }

            }

            //Debug.Log(" new = " + board_to_matrix.CalculateBoardValue(actualboard));
            return value;
        }

        else return 0;
        
    }
}