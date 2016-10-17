using System;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class MenuManager
    {
        #region Member variables
        GameOverMenu myGameOverMenu;
        MainMenu myMainMenu;
        HighscoreMenu myHighscoreMenu;
        WinningMenu MyWinningMenu;
        #endregion

        #region Properties
        public MenuState MenuState
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MenuManager(SpriteFont aFont)
        {
            InitializeMemberVariables(aFont);
            SetListeners();
        }
        #endregion

        #region Events
        public event EventHandler ExitSelected;

        public event EventHandler StartSelected;
        #endregion

        #region Public methods
        public void Update()
        {
            switch (MenuState)
            {
                case MenuState.GameOver:
                    myGameOverMenu.Update();
                    break;
                case MenuState.Main:
                    myMainMenu.Update();
                    break;
                case MenuState.Highscore:
                    myHighscoreMenu.Update();
                    break;
                case MenuState.Winning:
                    MyWinningMenu.Update();
                    break;
            }
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            switch (MenuState)
            {
                case MenuState.GameOver:
                    myGameOverMenu.Draw(aSpriteBatch);
                    break;
                case MenuState.Main:
                    myMainMenu.Draw(aSpriteBatch);
                    break;
                case MenuState.Highscore:
                    myHighscoreMenu.Draw(aSpriteBatch);
                    break;
                case MenuState.Winning:
                    MyWinningMenu.Draw(aSpriteBatch);
                    break;
            }
        }
        #endregion

        #region Private method

        private void InitializeMemberVariables(SpriteFont aFont)
        {
            InitializeNonParameterMemberVariables();
            InitializeParameterMemberVariables(aFont);
        }

        private void InitializeNonParameterMemberVariables()
        {
            MenuState = MenuState.Main;
        }

        private void InitializeParameterMemberVariables(SpriteFont aFont)
        {
            myGameOverMenu = new GameOverMenu(aFont);
            myMainMenu = new MainMenu(aFont);
            myHighscoreMenu = new HighscoreMenu(aFont);
            MyWinningMenu = new WinningMenu(aFont);
        }

        private void SetMainMenuListeners()
        {
            myMainMenu.ExitSelected += MenuExitGame;
            myMainMenu.StartSelected += MenuResetAndStartGame;
            myMainMenu.HighscoreSelected += MenuGoToHighscoreMenu;
        }

        private void SetGameOverMenuListeners()
        {
            myGameOverMenu.MenuSelected += MenuGoToMenu;
            myGameOverMenu.RestartSelected += MenuResetAndStartGame;
        }

        private void SetHighscoreMenuListeners()
        {
            myHighscoreMenu.MenuSelected += MenuGoToMenu;
        }

        private void SetWinningMenuListeners()
        {
            MyWinningMenu.MenuSelected += MenuGoToMenu;
            MyWinningMenu.RestartSelected += MenuResetAndStartGame;
        }

        private void SetListeners()
        {
            SetMainMenuListeners();
            SetGameOverMenuListeners();
            SetHighscoreMenuListeners();
            SetWinningMenuListeners();
        }

        private void MenuExitGame(object aSender, EventArgs aEventArgs)
        {
            ExitSelected(this, EventArgs.Empty);
        }

        private void MenuResetAndStartGame(object aSender, EventArgs aEventArgs)
        {
            StartSelected(this, EventArgs.Empty);
        }

        private void MenuGoToMenu(object aSender, EventArgs aEventArgs)
        {
            MenuState = MenuState.Main;
        }

        private void MenuGoToHighscoreMenu(object aSender, EventArgs aEventArgs)
        {
            MenuState = MenuState.Highscore;
        }

        #endregion
    }
}
