using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AtelierXNA
{
   class PlanColoré : Plan
   {
      VertexPositionColor[] Sommets { get; set; }
      Color Couleur { get; set; }

      public PlanColoré(Game jeu, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, Vector2 étendue, Vector2 charpente,
                        Color couleur, float intervalleMAJ) : base(jeu, homothétieInitiale, rotationInitiale, positionInitiale, étendue, charpente, intervalleMAJ)
      {
         Couleur = couleur;
      }

      protected override void CréerTableauSommets()
      {
         Sommets = new VertexPositionColor[NbSommets];
      }

      protected override void InitialiserParamètresEffetDeBase()
      {
         EffetDeBase.VertexColorEnabled = true;
      }

      protected override void InitialiserSommets() // Est appelée par base.Initialize()
      {
         int NoSommet = -1;
         for (int j = 0; j < NbRangées; ++j)
         {
            for (int i = 0; i <= NbColonnes; ++i)
            {
               Sommets[++NoSommet] = new VertexPositionColor(PtsSommets[i, j], Couleur);
               Sommets[++NoSommet] = new VertexPositionColor(PtsSommets[i, j + 1], Couleur);
            }
         }
      }

      protected override void DessinerTriangleStrip(int noStrip)
      {
         GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, Sommets, noStrip * Sommets.Length / NbRangées, NbTrianglesParStrip);
      }
   }
}
