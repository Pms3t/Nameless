using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    class MainMenu : MonoBehaviour
    {
        [SerializeField] private Loader _loader;

        public void Play()
        {
            _loader.LoadNextLevel();
        }

        public void CurrentLevel()
        {
            _loader.LoadCurrentLevel();
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void BackToMenu()
        {
            _loader.LoadMenu();
        }
    }
}
