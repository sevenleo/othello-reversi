using UnityEngine;
using System.Collections;

public class MinMax : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    
    int minimax(char node, int depth, bool maximizingPlayer)    {
        //node nao eh char, coloquei soh pra compilar

        int bestValue = 0;
        char[] validmoves;
        validmoves = new char[64];

        if (depth == 0)
            return node;
        else
            depth -= 1;

        if (maximizingPlayer)   {
            bestValue = -(System.Int32.MaxValue);
            validmoves = calculate_validmoves(node);

            foreach (char child in validmoves)          {
                  int newvalue = minimax(child, depth, false);
                if (bestValue < newvalue);
                bestValue =newvalue;
            }
            return bestValue;
        }
        else  {    // minimizingplayer
        bestValue= System.Int32.MaxValue;
            validmoves = calculate_validmoves(node);
            foreach (char child in validmoves){
                int newvalue = minimax(child, depth, true);
                if (bestValue > newvalue);
                bestValue =newvalue;
            }
            return bestValue;
        }
    }


    char[] calculate_validmoves(int node){
        return new char[64];
    }




    /*
     
      def generate_moves(self):
        """Return the list of legal moves where a move is a tuple.

        It returns a list of moves, if the game is not over. Otherwise, it
        returns an empty list. Note that a list with the singleton None is
        possible if the current player has no move but the game is not over.
        None is actually a valid move which rotates the turn to the other
        player and is only allowed if the current player has no legitimate
        move."""
        
        opp = -1 * self.player # opponent player num
        
        # A legal move is an empty square, s.t.
        # there is a contiguous straight line from this square consisting of
        # opponent squares followed by player's square.
        moves = []
        for i in range_size:
            for j in range_size:
                # find an empty square
                if self.board[i][j] != 0:
                    continue
                # look in every direction
                for dir in directions:
                    t = tuple_in_dir((i,j), dir)
                    # till you find an opponent piece
                    if (not tuple_valid(t)) or (self.board[t[0]][t[1]] != opp):
                        continue
                    # now, skip all the opponent pieces
                    while self.board[t[0]][t[1]] == opp:
                        t = tuple_in_dir(t, dir)
                        if not tuple_valid(t):
                            break
                    else:
                        # finally if we get one of our own pieces then
                        # make the move
                        if self.get_color(t) == self.player:
                            moves.append((i,j))
                            # no point looking in any other direction
                            break

        # if we don't have a move and the game is not over then
        # return the None move or "no move."
        if not moves and not self.terminal_test():
            moves = [None]
            
        return moves 
     
     */





}
