using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için gerekli

public class MainMenuManager : MonoBehaviour
{
    // Unity Editor'da butona atanacak fonksiyon
    public void StartNewGame()
    {
        // GameScene'in indeksini veya adını buraya yazın
        SceneManager.LoadScene("GameScene"); 
    }

    // Diğer menü fonksiyonları buraya eklenecek (Slider'lar vb.)
    public void QuitGame()
    {
         Application.Quit();
    }
}