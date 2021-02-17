using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using System.Drawing;

namespace sc2i.formulaire.win32.datagrid
{
    public interface IControlIncluableDansDataGrid : IControleWndFor2iWnd
    {
        DataGridView DataGrid { get; set; }

        //Indique si le controle a besoin de cette touche ou pas
        bool WantsInputKey(Keys keyData, bool dataGridViewWantsInputKey);

        //object GetValue();

        /// <summary>
        /// Selectionne l'intégralité du texte ou du truc édité
        /// </summary>
        void SelectAll();
    }

    
}
