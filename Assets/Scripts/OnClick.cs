using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour {

    
    
    public Material[,] Materialboard = new Material[10, 10];
    public GameObject Piece;
    public Material P1Color;
    public Material P2Color;
    public Material TipColor;
    public Canvas canvas;
    int plays = 60;
    int[] scores = new int[3];

    // Use this for initialization
    void Start () {

        //insere as pecas iniciais
        board_to_matrix.startwith("D4", true);
        board_to_matrix.startwith("E5", true);
        board_to_matrix.startwith("D5", false);
        board_to_matrix.startwith("E4", false);
        
        //guarda as cores/materiais de cada bloco do tabuleiro
        for (int i = 1; i < 9; i++)
            for (int j = 1; j < 9; j++)
                Materialboard[i, j] = GameObject.Find (board_to_matrix.matrix2board(new position(i,j))).GetComponent<Renderer>().material;

        //score
        board_to_matrix.playersscore(scores);
        GameObject.FindGameObjectWithTag("player1score").GetComponent<Text>().text = "P1: " + scores[1];
        GameObject.FindGameObjectWithTag("player2score").GetComponent<Text>().text = "P2: " + scores[2];
        GameObject.FindGameObjectWithTag("messages").GetComponent<Text>().text = "Jogadas restantes:" + scores[0];

    }



    void Update()
    {
        if (scores[0] > 0)
        {
            //indica qual o jogador da vez
            if (board_to_matrix.Turn)
            {
                GameObject.FindGameObjectWithTag("player1score").GetComponent<Text>().fontStyle = FontStyle.Bold;
                GameObject.FindGameObjectWithTag("player2score").GetComponent<Text>().fontStyle = FontStyle.Normal;
            }
            else
            {
                GameObject.FindGameObjectWithTag("player1score").GetComponent<Text>().fontStyle = FontStyle.Normal;
                GameObject.FindGameObjectWithTag("player2score").GetComponent<Text>().fontStyle = FontStyle.Bold;
            }

            //score
            GameObject.FindGameObjectWithTag("player1score").GetComponent<Text>().text = "P1: " + scores[1];
            GameObject.FindGameObjectWithTag("player2score").GetComponent<Text>().text = "P2: " + scores[2];
            GameObject.FindGameObjectWithTag("messages").GetComponent<Text>().text = "Jogadas restantes:" + scores[0];

            //colore as jogadas validas com a tipcolor
            board_to_matrix.valid_moves().ForEach(item =>
                GameObject.Find(board_to_matrix.matrix2board(item)).GetComponent<Renderer>().material = TipColor
            );
        }

        //remove a tipcolor do tabuleiro colorindo com as cores originais do tabuleiro (preto/branco) salvas na matriz 
        board_to_matrix.not_valid_moves().ForEach(item =>
            GameObject.Find(board_to_matrix.matrix2board(item)).GetComponent<Renderer>().material = Materialboard[item.x,item.y]
        );

        //click no tabuleiro
        if (Input.GetMouseButtonDown(0))
            CastRay();
        

    }

    void CastRay()    {
        
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //raio que vai da camera ate a posicao do mouse
        if (Physics.Raycast(ray, out hit, 100))
        {
            //se o objeto de colisao for o tabuleiro
            if (hit.collider.tag == "board")  {

         
                //nomeia e colore a nova peca que sera criada
                Piece.name = "Piece_" + hit.collider.name;
                if (board_to_matrix.Turn) Piece.GetComponent<Renderer>().material = P1Color;
                else Piece.GetComponent<Renderer>().material = P2Color;

                //tenta adicionar a peca ao tabuleiro add() é booleana
                if (board_to_matrix.add(hit.collider.name))
                {
                    //coloca a peca na casa selecionada com uma distancia para visualizacao
                    Vector3 distance = new Vector3(0, 0, (float)-0.5);
                    Instantiate(Piece, hit.collider.transform.position + distance, hit.collider.transform.rotation);

                    //desabilita o colisor/desabilita a jogada naquela casa
                    //novos cliques nao farao efeito
                    hit.collider.enabled = false;

                    board_to_matrix.playersscore(scores);

                    //diminue o numero de jogadas 
                    //if (board_to_matrix.all_empty().Count == 0) essa funcao é custosa demais
                    if (scores[0] == 0)
                    {
                        GameObject.FindGameObjectWithTag("messages").GetComponent<Text>().text = "Game Over";
                        //SceneManager.LoadScene("gameover");

                        if(scores[1]>scores[2]) GameObject.FindGameObjectWithTag("player1score").GetComponent<Text>().text = "VENCEDOR: " + scores[1];
                        else if (scores[1] < scores[2]) GameObject.FindGameObjectWithTag("player2score").GetComponent<Text>().text = "VENCEDOR: " + scores[2];
                        else
                        {
                            GameObject.FindGameObjectWithTag("player1score").GetComponent<Text>().text = "EMPATE: " + scores[1];
                            GameObject.FindGameObjectWithTag("player2score").GetComponent<Text>().text = "EMPATE: " + scores[2];
                        }
                    }
                }
                else Debug.Log("(P"+(board_to_matrix.Turn? "1":"2") +") Jogada inválida em "+ hit.collider.name);
            }
        }
    }


}
