using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    //Chama quando clica no botão reset
    public void reset(){
        //Altera o texto para garantir que está certo mesmo quando inicia
        Game.txt.text = "ENCAIXE AS ENGRENAGENS EM QUALQUER ORDEM!";
        //Zera o contador
        Game.cont = 0;
        //Limpa o ganhou
        Game.ganhou = false;
        //Recarrega a cena
        SceneManager.LoadScene("Game");
        
    }
}
