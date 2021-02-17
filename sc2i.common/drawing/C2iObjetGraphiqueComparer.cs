using System.Collections.Generic;

namespace sc2i.drawing
{
    public static class C2iObjetGraphiqueComparer
    {
        public static void OrdonnerDeGaucheADroite(List<I2iObjetGraphique> objs)
        {
            C2iObjetGraphiqueComparerSurX comparer = new C2iObjetGraphiqueComparerSurX();
            objs.Sort(comparer);
        }
        public static void OrdonnerDeDroiteAGauche(List<I2iObjetGraphique> objs)
        {
            C2iObjetGraphiqueComparerSurX comparer = new C2iObjetGraphiqueComparerSurX();
            objs.Sort(comparer);
            objs.Reverse();
        }
        public static void OrdonnerDeHautEnBas(List<I2iObjetGraphique> objs)
        {
            C2iObjetGraphiqueComparerSurY comparer = new C2iObjetGraphiqueComparerSurY();
            objs.Sort(comparer);
        }
        public static void OrdonnerDeBasEnHaut(List<I2iObjetGraphique> objs)
        {
            C2iObjetGraphiqueComparerSurY comparer = new C2iObjetGraphiqueComparerSurY();
            objs.Sort(comparer);
            objs.Reverse();
        }

        private class C2iObjetGraphiqueComparerSurX : IComparer<I2iObjetGraphique>
        {
            #region IComparer<I2iObjetGraphique> Membres

            public int Compare(I2iObjetGraphique x, I2iObjetGraphique y)
            {
                if (x.PositionAbsolue.X < y.PositionAbsolue.X)
                    return -1;
                else if (x.PositionAbsolue.X > y.PositionAbsolue.X)
                    return 1;
                else
                    return 0;
            }

            #endregion
        }
        private class C2iObjetGraphiqueComparerSurY : IComparer<I2iObjetGraphique>
        {
            #region IComparer<I2iObjetGraphique> Membres

            public int Compare(I2iObjetGraphique x, I2iObjetGraphique y)
            {
                if (x.PositionAbsolue.Y < y.PositionAbsolue.Y)
                    return -1;
                else if (x.PositionAbsolue.Y > y.PositionAbsolue.Y)
                    return 1;
                else
                    return 0;
            }

            #endregion
        }
    }


}
