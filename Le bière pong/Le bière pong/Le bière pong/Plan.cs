using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AtelierXNA
{
   public abstract class Plan : PrimitiveDeBaseAnimée
   {
      const int NB_TRIANGLES = 2;
      protected Vector3 Origine { get; private set; }  //Le coin inférieur gauche du plan en tenant compte que la primitive est centrée au point (0,0,0)
      Vector2 Delta { get; set; } // un vecteur contenant l'espacement entre deux colonnes (en X) et entre deux rangées (en Y)
      protected Vector3[,] PtsSommets { get; private set; } //un tableau contenant les positions des différents sommets du plan
      protected int NbColonnes { get; private set; } // Devinez...
      protected int NbRangées { get; private set; } // idem 
      protected int NbTrianglesParStrip { get; private set; } //...
      protected BasicEffect EffetDeBase { get; private set; } // 

      public Plan(Game jeu, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, Vector2 étendue, Vector2 charpente, float intervalleMAJ)
         : base(jeu, homothétieInitiale, rotationInitiale, positionInitiale, intervalleMAJ)
      {
          NbColonnes = (int)charpente.X;
          NbRangées = (int)charpente.Y;
          Delta = étendue / charpente;
          Origine = new Vector3(-étendue.X / 2, -étendue.Y / 2, 0);
      }

      public override void Initialize()
      {
         NbTrianglesParStrip = NbColonnes * NB_TRIANGLES;
         NbSommets = (NbTrianglesParStrip + 2) * NbRangées;
         PtsSommets = new Vector3[NbColonnes + 1, NbRangées + 1];
         CréerTableauSommets();
         CréerTableauPoints();
         base.Initialize();
      }

      protected abstract void CréerTableauSommets();

      protected override void LoadContent()
      {
         EffetDeBase = new BasicEffect(GraphicsDevice);
         InitialiserParamètresEffetDeBase();
         base.LoadContent();
      }

      protected abstract void InitialiserParamètresEffetDeBase();

      private void CréerTableauPoints()
      {
         for (int i = 0; i <= NbRangées; ++i)
         {
            for (int j = 0; j <= NbColonnes; ++j)
            {
               PtsSommets[j, i] = new Vector3(Origine.X + j * Delta.X, Origine.Y + i * Delta.Y, Origine.Z);
            }
         }
      }

      public override void Draw(GameTime gameTime)
      {
         EffetDeBase.World = GetMonde();
         EffetDeBase.View = CaméraJeu.Vue;
         EffetDeBase.Projection = CaméraJeu.Projection;
         foreach (EffectPass passeEffet in EffetDeBase.CurrentTechnique.Passes)
         {
            passeEffet.Apply();
            for (int i = 0; i < NbRangées; ++i)
            {
               DessinerTriangleStrip(i);
            }
            // Ici, il devrait y avoir une boucle qui provoque le dessin de chaque TriangleStrip du plan
            // Le dessin d'un TriangleStrip en particulier devrait se faire par le biais d'un appel à la méthode DessinerTriangleStrip()
         }
         base.Draw(gameTime);
      }

      protected abstract void DessinerTriangleStrip(int noStrip);
   }
}
