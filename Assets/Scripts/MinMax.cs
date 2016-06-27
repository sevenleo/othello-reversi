using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinMax : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    
    /*
    position minimax(position move,int[,] board, int depth, bool maximizingPlayer)    {
        //node nao eh char, coloquei soh pra compilar

        List<position> validmoves;
        int bestValue = 0;

        if (depth == 0)
            return move;
        else
            depth -= 1;

        if (board_to_matrix.Turn)   {
            bestValue = -(System.Int32.MaxValue);
            validmoves = calc_validmoves(board);

            validmoves.ForEach(child => { 
                int newvalue = minimax(child, board, depth, false);
                if (bestValue < newvalue);
                bestValue =newvalue;
            });
            return bestValue;
        }

        else  {    // minimizingplayer
        bestValue= System.Int32.MaxValue;
            validmoves = calculate_validmoves(move);
            foreach (char child in validmoves){
                int newvalue = minimax(child, depth, true);
                if (bestValue > newvalue);
                bestValue =newvalue;
            }
            return bestValue;
        }

    }


    */

    List<position> calc_validmoves( int[,]board){
        List<position> moves = new List<position>();
        return moves;
    }

    int CalculateBoardValue(int[,] board)
    {
        int value = 0;

        for (int line = 9; line >= 0; line--)
        {
            for (int row = 0; row < 10; row++)
            {
                if (board[line, row] == 1) value ++;
                if (board[line, row] == -1) value--;
            }
        }
        return value;
    }

}


/*
Minimax Pseudocode

alpaBetaMinimax(node, alpha, beta)

    
   #Returns best score for the player associated with the given node.
   #Also sets the variable bestMove to the move associated with the
   #best score at the root node.  
   

   # check if at search bound
   if node is at depthLimit
      return staticEval(node)

   # check if leaf
children = successors(node)
   if len(children) == 0
      if node is root
         bestMove = []
      return staticEval(node)

   # initialize bestMove
   if node is root
      bestMove = operator of first child
      # check if there is only one option
      if len(children) == 1
         return None

   if it is MAX's turn to move
      for child in children
         result = alphaBetaMinimax(child, alpha, beta)
         if result > alpha
            alpha = result
            if node is root
               bestMove = operator of child
         if alpha >= beta
            return alpha
      return alpha

   if it is MIN's turn to move
      for child in children
         result = alphaBetaMinimax(child, alpha, beta)
         if result<beta
            beta = result
            if node is root
               bestMove = operator of child
         if beta <= alpha
            return beta
      return beta
*/