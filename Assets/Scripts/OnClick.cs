using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Threading;

public class OnClick : MonoBehaviour {

    
    
    public Material[,] Materialboard = new Material[10, 10];
    List<position> changed= new List<position>();

    int[] scores = new int[3];

    int delay=2000;

    public GameObject Piece;
    public Material P1Color;
    public Material P2Color;
    public Material TipColor;
    public Canvas canvas;

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


           
            //p1-humano
            if (board_to_matrix.p1 && board_to_matrix.Turn)
            {
                //click no tabuleiro
                if (Input.GetMouseButtonDown(0))
                    CastRay();

            }
            //p2-humano
            else if (board_to_matrix.p2 && !board_to_matrix.Turn)
            {
                //click no tabuleiro
                if (Input.GetMouseButtonDown(0))
                    CastRay();

            }
            // pc vs pc
            else 
            {
                Play(PC_Random_Player.playing());
            }


        }

        //remove a tipcolor do tabuleiro colorindo com as cores originais do tabuleiro (preto/branco) salvas na matriz 
        board_to_matrix.not_valid_moves().ForEach(item =>
            GameObject.Find(board_to_matrix.matrix2board(item)).GetComponent<Renderer>().material = Materialboard[item.x, item.y]
        );

    }


    public Camera camera3d45;
    public Camera camera2d;
    public Camera cameratop;
    public Camera cameraclose;

    public Camera CameraAtiva()
    {
        if (camera2d.enabled) return camera2d;
        else if (cameratop.enabled) return cameratop;
        else if (cameraclose.enabled) return cameraclose;
        else return camera3d45;

    }


    void CastRay()    {

        Ray ray = CameraAtiva().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //raio que vai da camera ate a posicao do mouse
        if (Physics.Raycast(ray, out hit, 100))
        {
            //se o objeto de colisao for o tabuleiro
            if (hit.collider.tag == "board")
            {
                Play(hit.collider);
                GameObject collided = GameObject.Find("Piece_" + hit.collider.name + "(Clone)");
                if (collided != null)
                {
                    collided.GetComponent<Animation>().Play();
                    collided.GetComponent<Animator>().enabled = true;
                }
                

            }
        }
    }


    public void Play(Collider collider)
    {

            //nomeia e colore a nova peca que sera criada
            Piece.name = "Piece_" + collider.name;
            if (board_to_matrix.Turn) Piece.GetComponent<Renderer>().material = P1Color;
            else Piece.GetComponent<Renderer>().material = P2Color;

            //limpa pecas que devem ser limpas
            //changed = new List<position>();

            //tenta adicionar a peca ao tabuleiro add() é booleana
            if (board_to_matrix.add(collider.name, changed))
            {
                //coloca a peca na casa selecionada com uma distancia para visualizacao
                Vector3 distance = new Vector3(0, 0, (float)-0.5);
                Instantiate(Piece, collider.transform.position + distance, collider.transform.rotation);

                //desabilita o colisor/desabilita a jogada naquela casa
                //novos cliques nao farao efeito
                collider.enabled = false;

                //mostra o placar
                board_to_matrix.playersscore(scores);

                //RECOLORIR PEÇAS
                changed.ForEach(item => {
                    //Piece_D6(Clone)
                    Debug.Log("FLIP >> Piece_" + board_to_matrix.matrix2board(item) + "(Clone)");
                    GameObject.Find("Piece_" + board_to_matrix.matrix2board(item) + "(Clone)").GetComponent<Renderer>().material = (board_to_matrix.Turn ? P1Color : P2Color);
                    GameObject.Find("Piece_" + board_to_matrix.matrix2board(item) + "(Clone)").GetComponent<Animation>().Play();
                    changed.Remove(item);
                });

                //diminue o numero de jogadas 
                //if (board_to_matrix.all_empty().Count == 0) essa funcao é custosa demais
                if (scores[0] == 0)
                {
                    GameObject.FindGameObjectWithTag("messages").GetComponent<Text>().text = "Game Over";
                    //SceneManager.LoadScene("gameover");

                    if (scores[1] > scores[2]) GameObject.FindGameObjectWithTag("player1score").GetComponent<Text>().text = "VENCEDOR: " + scores[1];
                    else if (scores[1] < scores[2]) GameObject.FindGameObjectWithTag("player2score").GetComponent<Text>().text = "VENCEDOR: " + scores[2];
                    else
                    {
                        GameObject.FindGameObjectWithTag("player1score").GetComponent<Text>().text = "EMPATE: " + scores[1];
                        GameObject.FindGameObjectWithTag("player2score").GetComponent<Text>().text = "EMPATE: " + scores[2];
                    }
                }
            }
            else Debug.Log("(P" + (board_to_matrix.Turn ? "1" : "2") + ") Jogada inválida em " + collider.name);
        
    }





}
