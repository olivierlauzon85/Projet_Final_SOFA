using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace AtelierXNA
{
   public class InputManager : Microsoft.Xna.Framework.GameComponent
   {
      Keys[] AnciennesTouches { get; set; }
      Keys[] NouvellesTouches { get; set; }
      KeyboardState ÉtatClavier { get; set; }
      MouseState AncienÉtatSouris { get; set; }
      MouseState NouveauÉtatSouris { get; set; }

      public InputManager(Game game)
         : base(game)
      { }

      public override void Initialize()
      {
         AnciennesTouches = new Keys[0];
         NouvellesTouches = new Keys[0];

         base.Initialize();
      }

      public override void Update(GameTime gameTime)
      {
         AnciennesTouches = NouvellesTouches;
         ÉtatClavier = Keyboard.GetState();
         AncienÉtatSouris = NouveauÉtatSouris;
         NouveauÉtatSouris = Mouse.GetState();
         NouvellesTouches = ÉtatClavier.GetPressedKeys();

         base.Update(gameTime);
      }
      public bool EstClavierActivé
      {
         get { return NouvellesTouches.Length > 0; }
      }
      public bool EstEnfoncée(Keys touche)
      {
         return ÉtatClavier.IsKeyDown(touche);
      }

      public bool EstNouvelleTouche(Keys touche)
      {
         int NbTouches = AnciennesTouches.Length;
         bool EstNouvelleTouche = ÉtatClavier.IsKeyDown(touche);
         int i = 0;
         while (i < NbTouches && EstNouvelleTouche)
         {
            EstNouvelleTouche = AnciennesTouches[i] != touche;
            ++i;
         }
         return EstNouvelleTouche;
      }
      public bool EstAncienClicDroit()
      {
          if (NouveauÉtatSouris.RightButton == ButtonState.Pressed)
          {
             return AncienÉtatSouris.RightButton == ButtonState.Pressed;
          } 
          else
          {
             return false;
          }  
      }
      public bool EstAncienClicGauche()
      {
          if (NouveauÉtatSouris.LeftButton == ButtonState.Pressed)
          {
             return AncienÉtatSouris.LeftButton == ButtonState.Pressed;
          }  
          else
          {
             return false;
          }  
      }
      public bool EstNouveauClicDroit()
      {
          if (NouveauÉtatSouris.RightButton == ButtonState.Pressed)
          {
             return AncienÉtatSouris.RightButton == ButtonState.Released;
          } 
          else
          {
             return false;
          }
      }
      public bool EstNouveauClicGauche()
      {
          if (NouveauÉtatSouris.LeftButton == ButtonState.Pressed)
          {
             return AncienÉtatSouris.LeftButton == ButtonState.Released;
          }
          else
          {
             return false;
          }
      }
      public Point GetPositionSouris()
      {
         return new Point(NouveauÉtatSouris.X, NouveauÉtatSouris.Y);
      }
   }
}