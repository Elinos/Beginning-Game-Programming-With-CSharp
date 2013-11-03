using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using XnaCards;

namespace ProgrammingAssignment6
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // keep track of game state and current winner
        static GameState gameState = GameState.Play;
        Player currentWinner = Player.None;


        // hands and battle piles for the players
        WarHand playerOneHand;
        WarBattlePile playerOneBP;

        WarHand playerTwoHand;
        WarBattlePile playerTwoBP;
        // winner messages for players
        WinnerMessage playerOneWM;
        WinnerMessage playerTwoWM;

        // menu buttons
        MenuButton flipButton;
        MenuButton quitButton;
        MenuButton collectWinnings;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // make mouse visible and set resolution
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // create the deck object and shuffle
            Deck deck = new Deck(Content, 100, 100);
            deck.Shuffle();

            // create the player hands and fully deal the deck into the hands
            playerOneHand = new WarHand(WINDOW_WIDTH / 2, 100);
            playerTwoHand = new WarHand(WINDOW_WIDTH / 2, WINDOW_HEIGHT - 100);
            
            for (int i = 0; i < 2; i++)
            {
                playerOneHand.AddCard(deck.TakeTopCard());
                playerTwoHand.AddCard(deck.TakeTopCard());
            }

            // create the player battle piles
            playerOneBP = new WarBattlePile(WINDOW_WIDTH / 2, 200);
            playerTwoBP = new WarBattlePile(WINDOW_WIDTH / 2, WINDOW_HEIGHT - 200);

            // create the player winner messages
            playerOneWM = new WinnerMessage(Content, WINDOW_WIDTH / 2 + 150, 100);
            playerTwoWM = new WinnerMessage(Content, WINDOW_WIDTH / 2 + 150, WINDOW_HEIGHT - 100);


            // create the menu buttons
            flipButton = new MenuButton(Content, "flipbutton", 150, 150, GameState.Flip);
            quitButton = new MenuButton(Content, "quitbutton", 150, 450, GameState.Quit);
            collectWinnings = new MenuButton(Content, "collectwinningsbutton", 150, 300, GameState.CollectWinnings);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            MouseState mouse = Mouse.GetState();
            // update the menu buttons
            flipButton.Update(mouse);
            quitButton.Update(mouse);
            collectWinnings.Update(mouse);


            // update based on game state
            if (gameState == GameState.Play)
            {
                playerOneWM.Visible = false;
                playerTwoWM.Visible = false;
                flipButton.Visible = true;
                collectWinnings.Visible = false;
                if (playerOneHand.Empty)
                {
                    flipButton.Visible = false;
                    playerTwoWM.Visible = true;
                }
                else if (playerTwoHand.Empty)
                {
                    flipButton.Visible = false;
                    playerOneWM.Visible = true;
                }
                
            }

            if (gameState == GameState.Flip && playerOneBP.Empty && playerTwoBP.Empty)
            {
                Card cardToAddToPlayerOne = playerOneHand.TakeTopCard();
                cardToAddToPlayerOne.FlipOver();
                playerOneBP.AddCard(cardToAddToPlayerOne);

                Card cardToAddToPlayerTwo = playerTwoHand.TakeTopCard();
                cardToAddToPlayerTwo.FlipOver();
                playerTwoBP.AddCard(cardToAddToPlayerTwo);

                if (playerOneBP.GetTopCard().WarValue > playerTwoBP.GetTopCard().WarValue)
                {
                    currentWinner = Player.Player1;
                    playerOneWM.Visible = true;
                    playerTwoWM.Visible = false;
                }
                else if (playerOneBP.GetTopCard().WarValue < playerTwoBP.GetTopCard().WarValue)
                {
                    currentWinner = Player.Player2;
                    playerTwoWM.Visible = true;
                    playerOneWM.Visible = false;

                }
                else
                {
                    currentWinner = Player.None;
                    playerOneWM.Visible = false;
                    playerTwoWM.Visible = false;
                }
                flipButton.Visible = false;
                collectWinnings.Visible = true;
            }
            if (gameState == GameState.CollectWinnings)
            {
                if (currentWinner == Player.Player1)
                {
                    playerOneHand.AddCards(playerOneBP);
                    playerOneHand.AddCards(playerTwoBP);
                }
                else if (currentWinner == Player.Player2)
                {
                    playerTwoHand.AddCards(playerOneBP);
                    playerTwoHand.AddCards(playerTwoBP);
                }
                else
                {
                    playerOneHand.AddCards(playerOneBP);
                    playerTwoHand.AddCards(playerTwoBP);
                }
                gameState = GameState.Play;
            }
            if (gameState == GameState.Quit)
            {
                this.Exit();
            }



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Goldenrod);
            spriteBatch.Begin();

            // draw the game objects
            playerOneHand.Draw(spriteBatch);
            playerTwoHand.Draw(spriteBatch);

            playerOneBP.Draw(spriteBatch);
            playerTwoBP.Draw(spriteBatch);

            // draw the winner messages
            playerOneWM.Draw(spriteBatch);
            playerTwoWM.Draw(spriteBatch);


            // draw the menu buttons
            flipButton.Draw(spriteBatch);
            quitButton.Draw(spriteBatch);
            collectWinnings.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Changes the state of the game
        /// </summary>
        /// <param name="newState">the new game state</param>
        public static void ChangeState(GameState newState)
        {
            gameState = newState;
        }
    }
}
