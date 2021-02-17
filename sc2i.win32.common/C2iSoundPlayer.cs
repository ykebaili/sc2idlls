using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Media;
using sc2i.common;

namespace sc2i.win32.common
{
    public static class CSoundPlayer
    {
        private static bool m_bSonnerieEnCours = false;

        private static int m_nIdNextSonnerie = 0;
        private static int? m_nIdSonnerieEnCours = null;

        [DllImport("WinMM.dll")]
        public static extern bool PlaySound(string strFichier, int nMod, int nFlag);

        private static int SND_FILENAME = 0x00020000;
        private static int SND_PURGE = 0x0040;


        //Si retourne un entier, c'est que le système 
        //joue la sonnerie, sinon, c'est qu'il est occupé par une autre
        //sonnerie.
        //L'entier retourné doit être passé à StopSound pour
        //l'arrêt de l'alarme
        public static int? StartSound(string strFichierWav)
        {
            lock (typeof(CSoundPlayer))
            {
                if (m_bSonnerieEnCours)
                    return null;
                m_bSonnerieEnCours = true;
            }
            PlaySoundBackgroundDelegate delegue = new PlaySoundBackgroundDelegate(PlaySoundBackground);
            delegue.BeginInvoke(strFichierWav, null, null);
            m_nIdSonnerieEnCours = m_nIdNextSonnerie++;
            return m_nIdSonnerieEnCours;
        }

        private delegate void PlaySoundBackgroundDelegate(string strFichier);

        private static void PlaySoundBackground(string strFichier)
        {
            try
            {
                if (strFichier != "")
                {
                    using (CFichierLocalTemporaire temp = new CFichierLocalTemporaire("WAV"))
                    {
                        temp.CreateNewFichier();
                        File.Copy(strFichier, temp.NomFichier);
                        while (true)
                        {
                            PlaySound(temp.NomFichier, 0, SND_FILENAME);
                            if (m_nIdSonnerieEnCours == null)
                            {
                                PlaySound(null, 0, SND_PURGE);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    while (true)
                    {
                        Console.Beep(1000, 100);
                        System.Threading.Thread.Sleep(500);
                        if (m_nIdSonnerieEnCours == null)
                            break;
                    }
                }

            }
            catch
            {

            }
            finally
            {
                m_nIdSonnerieEnCours = null;
                m_bSonnerieEnCours = false;
            }
        }

        public static void PlaySound(byte[] btsSon)
        {
            try
            {
                if (btsSon != null)
                {
                    using (CFichierLocalTemporaire temp = new CFichierLocalTemporaire("WAV"))
                    {
                        temp.CreateNewFichier();
                        FileStream stream = new FileStream(temp.NomFichier, FileMode.CreateNew, FileAccess.Write);
                        stream.Write(btsSon, 0, btsSon.Length);
                        stream.Close();
                        PlaySound(temp.NomFichier, 0, SND_FILENAME);
                    }
                }
                else
                {
                    Console.Beep(1000, 100);
                }

            }
            catch
            {

            }
            finally
            {
                m_nIdSonnerieEnCours = null;
                m_bSonnerieEnCours = false;
            }
        }

        public static void PlaySound(Stream stream)
        {
            try
            {
                if (stream != null)
                {
                    using (CFichierLocalTemporaire temp = new CFichierLocalTemporaire("WAV"))
                    {
                        temp.CreateNewFichier();
                        FileStream strWrite = new FileStream(temp.NomFichier, FileMode.CreateNew, FileAccess.Write);
                        int nSize = 5 * 1024;
                        byte[] bts = new byte[nSize];
                        int nLu = 0;
                        while ((nLu = stream.Read(bts, 0, nSize)) != 0)
                            strWrite.Write(bts, 0, nLu);
                        strWrite.Close();
                        PlaySound(temp.NomFichier, 0, SND_FILENAME);
                    }
                }
                else
                {
                    Console.Beep(1000, 100);
                }

            }
            catch
            {

            }
            finally
            {
                m_nIdSonnerieEnCours = null;
                m_bSonnerieEnCours = false;
            }
        }

        public static void StopSound(int nIdSonnerie)
        {
            if (nIdSonnerie == m_nIdSonnerieEnCours)
            {
                m_nIdSonnerieEnCours = null;
            }
        }
    }
}